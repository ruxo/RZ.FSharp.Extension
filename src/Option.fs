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

let inline mapAsync ([<InlineIfLambda>] f: 'A -> Async<'B>) = function
| Some x -> async { let! r = f x in return Some r }
| None -> Async.return' None

let inline bindAsync ([<InlineIfLambda>] f: 'A -> OptionAsync<'B>) = function
| Some x -> f x
| None -> Async.return' None

open System.Threading.Tasks

let inline mapTask ([<InlineIfLambda>] f: 'a -> Task<'b>) = function
| Some x -> task { let! result = f x in return Some result }
| None -> Task.FromResult None

let inline bindTask ([<InlineIfLambda>] f: 'a -> Task<'b option>) = function
| Some x -> f x
| None -> Task.FromResult None