module RZ.FSharp.Extension.ValueResult

open System.Runtime.CompilerServices
open Prelude

[<Struct>]
type ValueResult<'A,'E> =
| ValueOk of ok:'A
| ValueError of error:'E

// ============================================ EXTENSION =============================================
let inline bind (f: 'A -> ValueResult<'B,'E>) (my: ValueResult<'A,'E>) :ValueResult<'B,'E> =
    match my with
    | ValueOk v -> f v
    | ValueError e -> ValueError e
    
let inline count result =
    match result with
    | ValueOk _ -> 1
    | ValueError _ -> 0

let inline contains value result =
    match result with
    | ValueOk v -> v = value
    | ValueError _ -> false

let exists predicate result =
    match result with
    | ValueOk v -> predicate v
    | ValueError _ -> false
    
let fold f state result =
    match result with
    | ValueOk v -> f state v
    | ValueError _ -> state
    
let foldBack f result state =
    match result with
    | ValueOk v -> f v state
    | ValueError _ -> state

let forall predicate result =
    match result with
    | ValueOk v -> predicate v
    | ValueError _ -> true

let inline get ([<InlineIfLambda>] right: 'a -> 'b) ([<InlineIfLambda>] wrong: 'err -> 'b) (x: ValueResult<'a,'err>) =
    match x with
    | ValueOk y -> right y
    | ValueError e -> wrong e

let inline map ([<InlineIfLambda>] f: 'A -> 'B) (my: ValueResult<'A,'E>) :ValueResult<'B,'E> =
    match my with
    | ValueOk v -> ValueOk (f v)
    | ValueError e -> ValueError e

let inline mapError ([<InlineIfLambda>] f: 'E -> 'B) (my: ValueResult<'A,'E>) :ValueResult<'A,'B> =
    match my with
    | ValueOk v -> ValueOk v
    | ValueError e -> ValueError (f e)

let unwrap (x: ValueResult<'a,'err>) =
    match x with
    | ValueOk v -> v
    | ValueError e -> raise <| UnwrapError.from($"Unwrap ValueError value of ValueResult<{typeof<'a>.Name},{typeof<'err>.Name}>", e)
    
let unwrapErr (x: ValueResult<'a,'err>) =
    match x with
    | ValueOk v -> raise <| UnwrapError.from($"Unwrap ValueOk value of ValueResult<{typeof<'a>.Name},{typeof<'err>.Name}>", v)
    | ValueError e -> e

let inline unwrapOrFail ([<InlineIfLambda>] messenger: 'err -> string) (x: ValueResult<'a, 'err>) :'a =
    match x with
    | ValueOk v -> v
    | ValueError e -> failwith <| messenger e

let inline unwrapOrRaise ([<InlineIfLambda>] thrower: 'err -> exn) (x: ValueResult<'a, 'err>) :'a =
    match x with
    | ValueOk v -> v
    | ValueError e -> raise <| thrower e

let inline mapBoth ([<InlineIfLambda>] fright) ([<InlineIfLambda>] fwrong) = get (ValueOk << fright) (ValueError << fwrong)
let inline isError x = x |> get (constant false) (constant true)
let inline isOk x = x |> get (constant true) (constant false)

let filter (predicate: 'a -> bool) (error: 'a -> 'err) (x: ValueResult<'a,'err>) :ValueResult<'a,'err> =
    match x with
    | ValueOk v -> if predicate v then ValueOk v else ValueError (error v)
    | ValueError e -> ValueError e

let inline flatten (r: ValueResult<ValueResult<'a,'err>,'err>) :ValueResult<'a,'err> = r |> get id ValueError
let inline bindBoth ([<InlineIfLambda>] f: 'a -> ValueResult<'c,'err>) ([<InlineIfLambda>] fwrong: 'b -> ValueResult<'c,'err>) =
    get f fwrong
    
let inline defaultValue def = get id (constant def)
let inline defaultWith ([<InlineIfLambda>] def) = get id def

let inline iter ([<InlineIfLambda>] right) = get right (constant ())

let inline orElse (elseValue: ValueResult<'a,'err>) (x: ValueResult<'a,'err>) :ValueResult<'a,'err> =
    match x with
    | ValueOk v -> ValueOk v
    | ValueError _ -> elseValue

let inline orElseWith ([<InlineIfLambda>] elseFunc: 'err -> ValueResult<'a,'err>) (x: ValueResult<'a,'err>) :ValueResult<'a,'err> =
    match x with
    | ValueOk v -> ValueOk v
    | ValueError e -> elseFunc e

let inline safeCall ([<InlineIfLambda>] eHandler) fun' a =
    try
        ValueOk (fun' a)
    with
    | e -> ValueError (eHandler e)

let inline safeCall2 ([<InlineIfLambda>] eHandler) fun' a b =
    try
        ValueOk (fun' a b)
    with
    | e -> ValueError (eHandler e)

let inline safeCall3 ([<InlineIfLambda>] eHandler) fun' a b c =
    try
        ValueOk (fun' a b c)
    with
    | e -> ValueError (eHandler e)

let inline safeCall4 ([<InlineIfLambda>] eHandler) fun' a b c d =
    try
        ValueOk (fun' a b c d)
    with
    | e -> ValueError (eHandler e)

let inline safeCall5 ([<InlineIfLambda>] eHandler) fun' a b c d e =
    try
        ValueOk (fun' a b c d e)
    with
    | e -> ValueError (eHandler e)

let inline safeCall6 ([<InlineIfLambda>] eHandler) fun' a b c d e f =
    try
        ValueOk (fun' a b c d e f)
    with
    | e -> ValueError (eHandler e)

let inline then' ([<InlineIfLambda>] right: 'a -> unit) ([<InlineIfLambda>] wrong: 'err -> unit)
                 (x: ValueResult<'a,'err>) =
    match x with
    | ValueOk v -> right v
    | ValueError e -> wrong e
    
let toArray result =
    match result with
    | ValueOk v -> Array.singleton v
    | ValueError _ -> Array.empty
    
let toList result =
    match result with
    | ValueOk v -> List.singleton v
    | ValueError _ -> List.empty
    
let inline toValueOption result =
    match result with
    | ValueOk v -> ValueSome v
    | ValueError _ -> ValueNone

// ====================================== FUNCTOR APPLICATION =========================================
let inline ap (other: ValueResult<'a, 'err>) (f: ValueResult<'a -> 'b, 'err>) :ValueResult<'b, 'err> =
    match f with
    | ValueOk f -> map f other
    | ValueError e -> ValueError e

let inline call (x: 'a) (f: ValueResult<'a -> 'b, 'err>) :ValueResult<'b, 'err> =
    match f with
    | ValueError e -> ValueError e
    | ValueOk f -> ValueOk (f x)

let inline call2 a b f =
    match f with
    | ValueError e -> ValueError e
    | ValueOk f -> ValueOk (f a b)

let inline call3 a b c f =
    match f with
    | ValueError e -> ValueError e
    | ValueOk f -> ValueOk (f a b c)

let inline call4 a b c d f =
    match f with
    | ValueError e -> ValueError e
    | ValueOk f -> ValueOk (f a b c d)

let inline call5 a b c d e f =
    match f with
    | ValueError e -> ValueError e
    | ValueOk f -> ValueOk (f a b c d e)

let inline call6 a b c d e f func =
    match func with
    | ValueError e -> ValueError e
    | ValueOk fun' -> ValueOk (fun' a b c d e f)

// ============================================ LIFTING ===============================================

let inline mapBothAsync ([<InlineIfLambda>] fright: 'a -> Async<'c>) ([<InlineIfLambda>] fwrong: 'b -> Async<'d>) = function
| ValueOk x -> async { let! result = fright x in return ValueOk result }
| ValueError y -> async { let! result = fwrong y in return ValueError result }

let inline mapAsync ([<InlineIfLambda>] fright: 'a -> Async<'c>) = function
| ValueOk x -> async { let! result = fright x in return ValueOk result }
| ValueError (y:'b) -> async { return ValueError y }

let filterAsync predicate error x =
    match x with
    | ValueOk v -> async { let! r = predicate v
                           in if r then return ValueOk v
                                   else let! ev = error v in return ValueError ev }
    | ValueError _ -> async.Return x

let inline defaultValueAsync def = function
    | ValueOk v -> async.Return v
    | ValueError _ -> def

let inline defaultWithAsync ([<InlineIfLambda>] def) = function
    | ValueOk v -> async.Return v
    | ValueError e -> def e

let getOrFailAsync (messenger :'b -> Async<string>) = function
    | ValueOk v -> async.Return v
    | ValueError e -> async { let! r = messenger e in return failwith r }

let getOrRaiseAsync (raiser :'b -> Async<exn>) = function
    | ValueOk v -> async.Return v
    | ValueError e -> async { let! r = raiser e in return raise r }

let inline iterAsync ([<InlineIfLambda>] right) x =
    match x with
    | ValueOk v -> right v
    | ValueError _ -> async.Return ()

let inline orElseAsync if_error x =
    match x with
    | ValueOk _ -> async.Return x
    | ValueError _ -> if_error

let inline orElseWithAsync ([<InlineIfLambda>] if_error) x =
    match x with
    | ValueOk _ -> async.Return x
    | ValueError e -> if_error e

// ======================================= TASK CONVERSIONS ===========================================
open System.Threading.Tasks

let inline mapTask ([<InlineIfLambda>] f: 'a -> Task<'c>) ([<InlineIfLambda>] fwrong: 'b -> 'd) = function
| ValueOk x -> task { let! result = f x in return ValueOk result }
| ValueError y -> Task.FromResult <| ValueError (fwrong y)

let inline bindTask ([<InlineIfLambda>] f: 'a -> Task<ValueResult<'c,'d>>) ([<InlineIfLambda>] fwrong: 'b -> 'd) = function
| ValueOk x -> f x
| ValueError y -> Task.FromResult <| ValueError (fwrong y)

let filterT predicate error x =
    match x with
    | ValueOk v -> task { let! r = predicate v
                          in if r then return ValueOk v
                                  else let! ev = error v in return ValueError ev }
    | ValueError _ -> Task.FromResult x

let inline defaultValueT def = function
    | ValueOk v -> Task.FromResult v
    | ValueError _ -> def

let inline defaultWithT ([<InlineIfLambda>] def) = function
    | ValueOk v -> Task.FromResult v
    | ValueError e -> def e

let getOrFailT (messenger :'b -> Async<string>) = function
    | ValueOk v -> Task.FromResult v
    | ValueError e -> task { let! r = messenger e in return failwith r }

let getOrRaiseT (raiser :'b -> Async<exn>) = function
    | ValueOk v -> Task.FromResult v
    | ValueError e -> task { let! r = raiser e in return raise r }

let inline iterT ([<InlineIfLambda>] right) x =
    match x with
    | ValueOk v -> right v
    | ValueError _ -> Task.FromResult ()

let inline orElseT if_error x =
    match x with
    | ValueOk _ -> Task.FromResult x
    | ValueError _ -> if_error

let inline orElseWithT ([<InlineIfLambda>] if_error) x =
    match x with
    | ValueOk _ -> Task.FromResult x
    | ValueError e -> if_error e

// build result builder!
[<IsReadOnly; Struct; NoEquality; NoComparison>]
type ResultBuilder =
    member inline _.Bind (current: ValueResult<'a, 'error>, [<InlineIfLambda>] statement: 'a -> ValueResult<'b, 'error>) =
        match current with
        | ValueResult.ValueOk value -> statement value
        | ValueResult.ValueError error -> ValueError error

    member inline _.Return v = ValueOk v

module Builder =
    let vresult = ResultBuilder()