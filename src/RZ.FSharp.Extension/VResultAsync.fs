module RZ.FSharp.Extension.VResultAsync

open Prelude
open ValueResult

let ok v :VResultAsync<'a,'b> = async.Return (ValueOk v)
let error v :VResultAsync<'a,'b> = async.Return (ValueError v)

let get right wrong x = async {
    match! x with
    | ValueOk v -> return right v
    | ValueError e -> return wrong e
}

let getAsyncPattern right aRight wrong aWrong x = async {
    match! x with
    | ValueOk v -> let! r = right v in return aRight r
    | ValueError e -> let! r = wrong e in return aWrong r
}

let inline getAsync right wrong = getAsyncPattern right id wrong id

let inline mapBoth right wrong = get (right >> ValueOk) (wrong >> ValueError)

let mapBothAsync right wrong = getAsyncPattern right ValueOk wrong ValueError

let inline map f x = x |> mapBoth f id
let inline mapAsync f x = x |> mapBothAsync f async.Return

let filter predicate error x = async {
    match! x with
    | ValueOk v -> return if predicate v then ValueOk v else ValueError (error v)
    | ValueError e -> return ValueError e
}

let filterAsync predicate error x = async {
    match! x with
    | ValueOk v -> let! r = predicate v
                   if r
                   then return ValueOk v
                   else let! er = error v in return ValueError er
    | ValueError e -> return ValueError e
}

let inline defaultValue dv = get id (constant dv)
let inline defaultValueAsync dv = getAsync async.Return dv

let inline defaultWith f = get id f
let inline defaultWithAsync f = getAsync async.Return f

let getOk x = async {
    match! x with
    | ValueOk v -> return v
    | ValueError _ -> return failwith "Result is error but tried to get ValueOk!"
}
let getError x = async {
    match! x with
    | ValueOk _ -> return failwith "Result is ok but tried to get error!"
    | ValueError e -> return e
}

let inline bindBoth right wrong x :VResultAsync<'c,'d> = get right wrong x
let inline bindBothAsync right wrong x :VResultAsync<'c,'d> = getAsync right wrong x

let inline bind f x = x |> bindBoth f ValueError
let inline bindAsync f x = x |> bindBothAsync f error

let inline getOrFail messenger = get id (failwith << messenger)
let inline getOrFailAsync messenger = getAsyncPattern async.Return id messenger failwith

let inline getOrRaise (raiser: 'b -> exn) = get id (raise << raiser)
let inline getOrRaiseAsync (raiser: 'b -> Async<exn>) = getAsyncPattern async.Return id raiser raise

let inline iter ([<InlineIfLambda>] right: 'a -> unit) = get right (constant ())
let inline iterAsync ([<InlineIfLambda>] right: 'a -> Async<unit>) = getAsync right (constant <| async.Return ())

let inline orElse elseValue = get ValueOk (constant elseValue)
let inline orElseAsync elseFunc = getAsync ok elseFunc

let inline safeCall ([<InlineIfLambda>] eHandler) fun' a = async {
    try
        let! v = fun' a
        return ValueOk v
    with
    | e -> return ValueError (eHandler e)
}

let inline safeCall2 ([<InlineIfLambda>] eHandler) fun' a b = async {
    try
        let! v = fun' a b
        return ValueOk v
    with
    | e -> return ValueError (eHandler e)
}

let inline safeCall3 ([<InlineIfLambda>] eHandler) fun' a b c = async {
    try
        let! v = fun' a b c
        return ValueOk v
    with
    | e -> return ValueError (eHandler e)
}

let inline safeCall4 ([<InlineIfLambda>] eHandler) fun' a b c d = async {
    try
        let! v = fun' a b c d
        return ValueOk v
    with
    | e -> return ValueError (eHandler e)
}

let inline safeCall5 ([<InlineIfLambda>] eHandler) fun' a b c d e = async {
    try
        let! v = fun' a b c d e
        return ValueOk v
    with
    | e -> return ValueError (eHandler e)
}

let inline safeCall6 ([<InlineIfLambda>] eHandler) fun' a b c d e f = async {
    try
        let! v = fun' a b c d e f
        return ValueOk v
    with
    | e -> return ValueError (eHandler e)
}

let inline then' ([<InlineIfLambda>] right: 'a -> unit) ([<InlineIfLambda>] wrong: 'b -> unit) = get right wrong
let inline thenAsync ([<InlineIfLambda>] right: 'a -> Async<unit>) ([<InlineIfLambda>] wrong: 'b -> Async<unit>) = getAsync right wrong