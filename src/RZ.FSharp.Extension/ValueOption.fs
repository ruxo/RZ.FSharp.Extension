module RZ.FSharp.Extension.ValueOption

// Functor Application
let inline ap other = function
| ValueNone -> ValueNone
| ValueSome f -> other |> ValueOption.map f

let inline call x = function
| ValueNone -> ValueNone
| ValueSome f -> ValueSome (f x)

let inline call2 a b = function
| ValueNone -> ValueNone
| ValueSome f -> ValueSome (f a b)

let inline call3 a b c = function
| ValueNone -> ValueNone
| ValueSome f -> ValueSome (f a b c)

let inline call4 a b c d = function
| ValueNone -> ValueNone
| ValueSome f -> ValueSome (f a b c d)

let inline call5 a b c d e = function
| ValueNone -> ValueNone
| ValueSome f -> ValueSome (f a b c d e)

let inline call6 a b c d e f = function
| ValueNone -> ValueNone
| ValueSome fun' -> ValueSome (fun' a b c d e f)

// Option extensions

let unwrap (x: 'a voption) :'a =
    match x with
    | ValueSome v -> v
    | ValueNone -> raise <| UnwrapError.from($"Unwrap ValueNone value of Option<{typeof<'a>.Name}>")

let inline unwrapOrFail ([<InlineIfLambda>] messenger: unit -> string) = function
| ValueSome v -> v
| ValueNone -> failwith <| messenger()

let inline unwrapOrRaise ([<InlineIfLambda>] raiser: unit -> exn) = function
| ValueSome v -> v
| ValueNone -> raise <| raiser()

let inline then' ([<InlineIfLambda>] fsome) ([<InlineIfLambda>] fnone) = function
| ValueSome x -> fsome x
| ValueNone -> fnone()

let inline safeCall fun' a =
    try
        ValueSome (fun' a)
    with
    | _ -> ValueNone

let inline safeCall2 fun' a b =
    try
        ValueSome (fun' a b)
    with
    | _ -> ValueNone

let inline safeCall3 fun' a b c =
    try
        ValueSome (fun' a b c)
    with
    | _ -> ValueNone

let inline safeCall4 fun' a b c d =
    try
        ValueSome (fun' a b c d)
    with
    | _ -> ValueNone

let inline safeCall5 fun' a b c d e =
    try
        ValueSome (fun' a b c d e)
    with
    | _ -> ValueNone

let inline safeCall6 fun' a b c d e f =
    try
        ValueSome (fun' a b c d e f)
    with
    | _ -> ValueNone

let mapAsync (f: 'A -> Async<'B>) = function
| ValueSome x -> async { let! r = f x in return ValueSome r }
| ValueNone -> async.Return ValueNone

let inline bindAsync ([<InlineIfLambda>] f: 'A -> VOptionAsync<'B>) = function
| ValueSome x -> f x
| ValueNone -> async.Return ValueNone

let filterAsync (f :'A -> Async<bool>) = function
| ValueSome x -> async { let! r = f x in return if r then ValueSome x else ValueNone }
| ValueNone -> async.Return ValueNone

let inline defaultValueAsync (v :Async<'A>) = function
| ValueSome x -> async.Return x
| ValueNone -> v

let inline defaultWithAsync ([<InlineIfLambda>] f :unit -> Async<'A>) = function
| ValueSome x -> async.Return x
| ValueNone -> f()

let unwrapOrFailAsync (messenger :unit -> Async<string>) = function
| ValueSome x -> async.Return x
| ValueNone -> async { let! r = messenger() in return failwith r }

let unwrapOrRaiseAsync (raiser :unit -> Async<exn>) = function
| ValueSome x -> async.Return x
| ValueNone -> async { let! r = raiser() in return raise r }

let inline iterAsync ([<InlineIfLambda>] f :'T -> Async<unit>) = function
| ValueSome x -> f x
| ValueNone -> async.Return ()

let inline orElseAsync (if_none :Async<'T voption>) x =
    match x with
    | ValueSome _ -> async.Return x
    | ValueNone -> if_none

let inline orElseWithAsync ([<InlineIfLambda>] f :unit -> Async<'T voption>) x =
    match x with
    | ValueSome _ -> async.Return x
    | ValueNone -> f()

open System.Threading.Tasks

let inline mapTask ([<InlineIfLambda>] f: 'a -> Task<'b>) = function
| ValueSome x -> task { let! result = f x in return ValueSome result }
| ValueNone -> Task.FromResult ValueNone

let inline bindTask ([<InlineIfLambda>] f: 'a -> Task<'b voption>) = function
| ValueSome x -> f x
| ValueNone -> Task.FromResult ValueNone

let filterT (f :'A -> Task<bool>) = function
| ValueSome x -> task { let! r = f x in return if r then ValueSome x else ValueNone }
| ValueNone -> Task.FromResult ValueNone

let inline defaultValueT (v :Task<'A>) = function
| ValueSome x -> Task.FromResult x
| ValueNone -> v

let inline defaultWithT ([<InlineIfLambda>] f :unit -> Task<'A>) = function
| ValueSome x -> Task.FromResult x
| ValueNone -> f()

let unwrapOrFailT (messenger :unit -> Task<string>) = function
| ValueSome x -> Task.FromResult x
| ValueNone -> task { let! r = messenger() in return failwith r }

let unwrapOrRaiseT (raiser :unit -> Task<exn>) = function
| ValueSome x -> Task.FromResult x
| ValueNone -> task { let! r = raiser() in return raise r }

let inline iterT ([<InlineIfLambda>] f :'T -> Task<unit>) = function
| ValueSome x -> f x
| ValueNone -> Task.FromResult ()

let inline orElseT (if_none :Task<'T voption>) x =
    match x with
    | ValueSome _ -> Task.FromResult x
    | ValueNone -> if_none

let inline orElseWithT ([<InlineIfLambda>] f :unit -> Task<'T voption>) x =
    match x with
    | ValueSome _ -> Task.FromResult x
    | ValueNone -> f()
    
type OptionBuilder() =
    member _.Bind (x, f) =
        match x with
        | ValueSome v -> f v
        | ValueNone -> ValueNone
        
    member _.Return x = ValueSome x
    member _.ReturnFrom x = x
    
    member _.Zero() = ValueNone
    
let voption = OptionBuilder()