﻿module RZ.FSharp.Extension.ResultAsync

open Prelude

let ok v :ResultAsync<'a,'b> = Async.return' (Ok v)
let error v :ResultAsync<'a,'b> = Async.return' (Error v)

let get right wrong x = async {
    match! x with
    | Ok v -> return right v
    | Error e -> return wrong e
}

let getAsyncPattern right aRight wrong aWrong x = async {
    match! x with
    | Ok v -> let! r = right v in return aRight r
    | Error e -> let! r = wrong e in return aWrong r
}

let inline getAsync right wrong = getAsyncPattern right id wrong id

let inline mapBoth right wrong = get (right >> Ok) (wrong >> Error)

let mapBothAsync right wrong = getAsyncPattern right Ok wrong Error

let inline map f x = x |> mapBoth f id
let inline mapAsync f x = x |> mapBothAsync f Async.return'

let filter predicate error x = async {
    match! x with
    | Ok v -> return if predicate v then Ok v else Error (error v)
    | Error e -> return Error e
}

let filterAsync predicate error x = async {
    match! x with
    | Ok v -> let! r = predicate v
              if r
              then return Ok v
              else let! er = error v in return Error er
    | Error e -> return Error e
}

let inline defaultValue dv = get id (constant dv)
let inline defaultValueAsync dv = getAsync Async.return' dv

let inline defaultWith f = get id f
let inline defaultWithAsync f = getAsync Async.return' f

let inline getRight ([<InlineIfLambda>] wrong) = get id wrong
let inline getRightAsync ([<InlineIfLambda>] wrong) = getAsync Async.return' wrong

let inline bindBoth right wrong x :ResultAsync<'c,'d> = get right wrong x
let inline bindBothAsync right wrong x :ResultAsync<'c,'d> = getAsync right wrong x

let inline bind f x = x |> bindBoth f Error
let inline bindAsync f x = x |> bindBothAsync f error

let inline getOrFail messenger = get id (failwith << messenger)
let inline getOrFailAsync messenger = getAsyncPattern Async.return' id messenger failwith

let inline getOrRaise (raiser: 'b -> exn) = get id (raise << raiser)
let inline getOrRaiseAsync (raiser: 'b -> Async<exn>) = getAsyncPattern Async.return' id raiser raise

let inline iter ([<InlineIfLambda>] right: 'a -> unit) = get right (constant ())
let inline iterAsync ([<InlineIfLambda>] right: 'a -> Async<unit>) = getAsync right (constant <| Async.return' ())

let inline orElse elseValue = get Ok (constant elseValue)
let inline orElseAsync elseFunc = getAsync ok elseFunc

let inline safeCall ([<InlineIfLambda>] eHandler) fun' a = async {
    let! f = fun'
    try
        return Ok (f a)
    with
    | e -> return Error (eHandler e)
}

let inline safeCall2 ([<InlineIfLambda>] eHandler) fun' a b = async {
    let! f = fun'
    try
        return Ok (f a b)
    with
    | e -> return Error (eHandler e)
}

let inline safeCall3 ([<InlineIfLambda>] eHandler) fun' a b c = async {
    let! f = fun'
    try
        return Ok (f a b c)
    with
    | e -> return Error (eHandler e)
}

let inline safeCall4 ([<InlineIfLambda>] eHandler) fun' a b c d = async {
    let! f = fun'
    try
        return Ok (f a b c d)
    with
    | e -> return Error (eHandler e)
}

let inline safeCall5 ([<InlineIfLambda>] eHandler) fun' a b c d e = async {
    let! f = fun'
    try
        return Ok (f a b c d e)
    with
    | e -> return Error (eHandler e)
}

let inline safeCall6 ([<InlineIfLambda>] eHandler) fun' a b c d e f = async {
    let! f' = fun'
    try
        return Ok (f' a b c d e f)
    with
    | e -> return Error (eHandler e)
}

let inline then' ([<InlineIfLambda>] right: 'a -> unit) ([<InlineIfLambda>] wrong: 'b -> unit) = get right wrong
let inline thenAsync ([<InlineIfLambda>] right: 'a -> Async<unit>) ([<InlineIfLambda>] wrong: 'b -> Async<unit>) = getAsync right wrong