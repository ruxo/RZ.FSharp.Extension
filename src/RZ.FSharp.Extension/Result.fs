module RZ.FSharp.Extension.Result

open Prelude

// Functor Application
let inline ap (other: Result<'a, 'err>) (f: Result<'a -> 'b, 'err>) :Result<'b, 'err> =
    match f with
    | Ok f -> other |> Result.map f
    | Error e -> Error e

let inline call (x: 'a) (f: Result<'a -> 'b, 'err>) :Result<'b, 'err> =
    match f with
    | Error e -> Error e
    | Ok f -> Ok (f x)

let inline call2 a b f =
    match f with
    | Error e -> Error e
    | Ok f -> Ok (f a b)

let inline call3 a b c f =
    match f with
    | Error e -> Error e
    | Ok f -> Ok (f a b c)

let inline call4 a b c d f =
    match f with
    | Error e -> Error e
    | Ok f -> Ok (f a b c d)

let inline call5 a b c d e f =
    match f with
    | Error e -> Error e
    | Ok f -> Ok (f a b c d e)

let inline call6 a b c d e f func =
    match func with
    | Error e -> Error e
    | Ok fun' -> Ok (fun' a b c d e f)

// Result extensions

let inline get ([<InlineIfLambda>] right: 'a -> 'b) ([<InlineIfLambda>] wrong: 'err -> 'b) (x: Result<'a,'err>) =
    match x with
    | Ok y -> right y
    | Error e -> wrong e

let unwrap (x: Result<'a,'err>) =
    match x with
    | Ok v -> v
    | Error e -> raise <| UnwrapError.from($"Unwrap Error value of Result<{typeof<'a>.Name},{typeof<'err>.Name}>", e)
    
let unwrapErr (x: Result<'a,'err>) =
    match x with
    | Ok v -> raise <| UnwrapError.from($"Unwrap Ok value of Result<{typeof<'a>.Name},{typeof<'err>.Name}>", v)
    | Error e -> e

let inline unwrapOrFail (messenger: 'err -> string) (x: Result<'a, 'err>) :'a =
    match x with
    | Ok v -> v
    | Error e -> failwith <| messenger e

let inline unwrapOrRaise (thrower: 'err -> exn) (x: Result<'a, 'err>) :'a =
    match x with
    | Ok v -> v
    | Error e -> raise <| thrower e

let inline mapBoth ([<InlineIfLambda>] fright) ([<InlineIfLambda>] fwrong) = get (Ok << fright) (Error << fwrong)
let inline isError x = x |> get (constant false) (constant true)
let inline isOk x = x |> get (constant true) (constant false)

let inline filter (predicate: 'a -> bool) (error: 'a -> 'err) (x: Result<'a,'err>) :Result<'a,'err> =
    match x with
    | Ok v -> if predicate v then Ok v else Error (error v)
    | Error e -> Error e

let inline flatten (r: Result<Result<'a,'err>,'err>) :Result<'a,'err> = r |> get id Error
let inline bindBoth ([<InlineIfLambda>] f: 'a -> Result<'c,'err>) ([<InlineIfLambda>] fwrong: 'b -> Result<'c,'err>) =
    get f fwrong
    
let inline defaultValue def = get id (constant def)
let inline defaultWith ([<InlineIfLambda>] def) = get id def

let inline iter ([<InlineIfLambda>] right) = get right (constant ())

let inline orElse (elseValue: Result<'a,'err>) (x: Result<'a,'err>) :Result<'a,'err> =
    match x with
    | Ok v -> Ok v
    | Error _ -> elseValue

let inline orElseWith ([<InlineIfLambda>] elseFunc: 'err -> Result<'a,'err>) (x: Result<'a,'err>) :Result<'a,'err> =
    match x with
    | Ok v -> Ok v
    | Error e -> elseFunc e

let inline safeCall ([<InlineIfLambda>] eHandler) fun' a =
    try
        Ok (fun' a)
    with
    | e -> Error (eHandler e)

let inline safeCall2 ([<InlineIfLambda>] eHandler) fun' a b =
    try
        Ok (fun' a b)
    with
    | e -> Error (eHandler e)

let inline safeCall3 ([<InlineIfLambda>] eHandler) fun' a b c =
    try
        Ok (fun' a b c)
    with
    | e -> Error (eHandler e)

let inline safeCall4 ([<InlineIfLambda>] eHandler) fun' a b c d =
    try
        Ok (fun' a b c d)
    with
    | e -> Error (eHandler e)

let inline safeCall5 ([<InlineIfLambda>] eHandler) fun' a b c d e =
    try
        Ok (fun' a b c d e)
    with
    | e -> Error (eHandler e)

let inline safeCall6 ([<InlineIfLambda>] eHandler) fun' a b c d e f =
    try
        Ok (fun' a b c d e f)
    with
    | e -> Error (eHandler e)

let inline then' ([<InlineIfLambda>] right: 'a -> unit) ([<InlineIfLambda>] wrong: 'err -> unit)
                 (x: Result<'a,'err>) =
    match x with
    | Ok v -> right v
    | Error e -> wrong e

// Lifting

let inline mapBothAsync ([<InlineIfLambda>] fright: 'a -> Async<'c>) ([<InlineIfLambda>] fwrong: 'b -> Async<'d>) = function
| Ok x -> async { let! result = fright x in return Ok result }
| Error y -> async { let! result = fwrong y in return Error result }

let inline mapAsync ([<InlineIfLambda>] fright: 'a -> Async<'c>) = function
| Ok x -> async { let! result = fright x in return Ok result }
| Error (y:'b) -> async { return Error y }

let inline bindBothAsync ([<InlineIfLambda>] f: 'a -> ResultAsync<'c,'d>) ([<InlineIfLambda>] fwrong: 'b -> ResultAsync<'c,'d>) = function
| Ok x -> f x
| Error y -> fwrong y

let inline bindAsync ([<InlineIfLambda>] f: 'a -> ResultAsync<'c,'b>)  = function
| Ok x -> f x
| Error y -> async { return Error y }

let filterAsync predicate error x =
    match x with
    | Ok v -> async { let! r = predicate v
                      in if r then return Ok v
                               else let! ev = error v in return Error ev }
    | Error _ -> async.Return x

let inline defaultValueAsync def = function
    | Ok v -> async.Return v
    | Error _ -> def

let inline defaultWithAsync ([<InlineIfLambda>] def) = function
    | Ok v -> async.Return v
    | Error e -> def e

let getOrFailAsync (messenger :'b -> Async<string>) = function
    | Ok v -> async.Return v
    | Error e -> async { let! r = messenger e in return failwith r }

let getOrRaiseAsync (raiser :'b -> Async<exn>) = function
    | Ok v -> async.Return v
    | Error e -> async { let! r = raiser e in return raise r }

let inline iterAsync ([<InlineIfLambda>] right) x =
    match x with
    | Ok v -> right v
    | Error _ -> async.Return ()

let inline orElseAsync if_error x =
    match x with
    | Ok _ -> async.Return x
    | Error _ -> if_error

let inline orElseWithAsync ([<InlineIfLambda>] if_error) x =
    match x with
    | Ok _ -> async.Return x
    | Error e -> if_error e

open System.Threading.Tasks

let inline mapTask ([<InlineIfLambda>] f: 'a -> Task<'c>) ([<InlineIfLambda>] fwrong: 'b -> 'd) = function
| Ok x -> task { let! result = f x in return Ok result }
| Error y -> Task.FromResult <| Error (fwrong y)

let inline bindTask ([<InlineIfLambda>] f: 'a -> Task<Result<'c,'d>>) ([<InlineIfLambda>] fwrong: 'b -> 'd) = function
| Ok x -> f x
| Error y -> Task.FromResult <| Error (fwrong y)

let filterT predicate error x =
    match x with
    | Ok v -> task { let! r = predicate v
                      in if r then return Ok v
                               else let! ev = error v in return Error ev }
    | Error _ -> Task.FromResult x

let inline defaultValueT def = function
    | Ok v -> Task.FromResult v
    | Error _ -> def

let inline defaultWithT ([<InlineIfLambda>] def) = function
    | Ok v -> Task.FromResult v
    | Error e -> def e

let getOrFailT (messenger :'b -> Async<string>) = function
    | Ok v -> Task.FromResult v
    | Error e -> task { let! r = messenger e in return failwith r }

let getOrRaiseT (raiser :'b -> Async<exn>) = function
    | Ok v -> Task.FromResult v
    | Error e -> task { let! r = raiser e in return raise r }

let inline iterT ([<InlineIfLambda>] right) x =
    match x with
    | Ok v -> right v
    | Error _ -> Task.FromResult ()

let inline orElseT if_error x =
    match x with
    | Ok _ -> Task.FromResult x
    | Error _ -> if_error

let inline orElseWithT ([<InlineIfLambda>] if_error) x =
    match x with
    | Ok _ -> Task.FromResult x
    | Error e -> if_error e

// build result builder!
type ResultBuilder() =
    member inline _.Bind (current: Result<'a, 'error>, [<InlineIfLambda>] statement: 'a -> Result<'b, 'error>) =
        match current with
        | Result.Ok value -> statement value
        | Result.Error error -> Error error

    member inline _.Return v = Ok v

module Builder =
    let result = ResultBuilder()