module RZ.FSharp.Extension.Option

// Functor Application
let inline ap other = function
| None -> None
| Some f -> other |> Option.map f

let inline call x = function
| None -> None
| Some f -> Some (f x)

let inline call2 a b = function
| None -> None
| Some f -> Some (f a b)

let inline call3 a b c = function
| None -> None
| Some f -> Some (f a b c)

let inline call4 a b c d = function
| None -> None
| Some f -> Some (f a b c d)

let inline call5 a b c d e = function
| None -> None
| Some f -> Some (f a b c d e)

let inline call6 a b c d e f = function
| None -> None
| Some fun' -> Some (fun' a b c d e f)

// Option extensions

let inline getOrFail ([<InlineIfLambda>] messenger: unit -> string) = function
| Some v -> v
| None -> failwith <| messenger()

let inline getOrRaise ([<InlineIfLambda>] raiser: unit -> exn) = function
| Some v -> v
| None -> raise <| raiser()

let inline then' ([<InlineIfLambda>] fsome) ([<InlineIfLambda>] fnone) = function
| Some x -> fsome x
| None -> fnone()

let inline safeCall fun' a =
    try
        Some (fun' a)
    with
    | _ -> None

let inline safeCall2 fun' a b =
    try
        Some (fun' a b)
    with
    | _ -> None

let inline safeCall3 fun' a b c =
    try
        Some (fun' a b c)
    with
    | _ -> None

let inline safeCall4 fun' a b c d =
    try
        Some (fun' a b c d)
    with
    | _ -> None

let inline safeCall5 fun' a b c d e =
    try
        Some (fun' a b c d e)
    with
    | _ -> None

let inline safeCall6 fun' a b c d e f =
    try
        Some (fun' a b c d e f)
    with
    | _ -> None

let mapAsync (f: 'A -> Async<'B>) = function
| Some x -> async { let! r = f x in return Some r }
| None -> Async.return' None

let inline bindAsync ([<InlineIfLambda>] f: 'A -> OptionAsync<'B>) = function
| Some x -> f x
| None -> Async.return' None

let filterAsync (f :'A -> Async<bool>) = function
| Some x -> async { let! r = f x in return if r then Some x else None }
| None -> Async.return' None

let inline defaultValueAsync (v :Async<'A>) = function
| Some x -> Async.return' x
| None -> v

let inline defaultWithAsync ([<InlineIfLambda>] f :unit -> Async<'A>) = function
| Some x -> Async.return' x
| None -> f()

let getOrFailAsync (messenger :unit -> Async<string>) = function
| Some x -> Async.return' x
| None -> async { let! r = messenger() in return failwith r }

let getOrRaiseAsync (raiser :unit -> Async<exn>) = function
| Some x -> Async.return' x
| None -> async { let! r = raiser() in return raise r }

let inline iterAsync ([<InlineIfLambda>] f :'T -> Async<unit>) = function
| Some x -> f x
| None -> Async.return' ()

let inline orElseAsync (if_none :Async<'T option>) x =
    match x with
    | Some _ -> Async.return' x
    | None -> if_none

let inline orElseWithAsync ([<InlineIfLambda>] f :unit -> Async<'T option>) x =
    match x with
    | Some _ -> Async.return' x
    | None -> f()

open System.Threading.Tasks

let inline mapTask ([<InlineIfLambda>] f: 'a -> Task<'b>) = function
| Some x -> task { let! result = f x in return Some result }
| None -> Task.FromResult None

let inline bindTask ([<InlineIfLambda>] f: 'a -> Task<'b option>) = function
| Some x -> f x
| None -> Task.FromResult None

let filterT (f :'A -> Task<bool>) = function
| Some x -> task { let! r = f x in return if r then Some x else None }
| None -> Task.FromResult None

let inline defaultValueT (v :Task<'A>) = function
| Some x -> Task.FromResult x
| None -> v

let inline defaultWithT ([<InlineIfLambda>] f :unit -> Task<'A>) = function
| Some x -> Task.FromResult x
| None -> f()

let getOrFailT (messenger :unit -> Task<string>) = function
| Some x -> Task.FromResult x
| None -> task { let! r = messenger() in return failwith r }

let getOrRaiseT (raiser :unit -> Task<exn>) = function
| Some x -> Task.FromResult x
| None -> task { let! r = raiser() in return raise r }

let inline iterT ([<InlineIfLambda>] f :'T -> Task<unit>) = function
| Some x -> f x
| None -> Task.FromResult ()

let inline orElseT (if_none :Task<'T option>) x =
    match x with
    | Some _ -> Task.FromResult x
    | None -> if_none

let inline orElseWithT ([<InlineIfLambda>] f :unit -> Task<'T option>) x =
    match x with
    | Some _ -> Task.FromResult x
    | None -> f()
    
type OptionBuilder() =
    member _.Bind (x, f) =
        match x with
        | Some v -> f v
        | None -> None
        
    member _.Return x = Some x
    member _.ReturnFrom x = x
    
    member _.Zero() = None
    
let option = OptionBuilder()