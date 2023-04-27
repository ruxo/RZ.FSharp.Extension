module RZ.FSharp.Extension.OptionAsync

open Prelude

let inline private asyncMap ([<InlineIfLambda>] f) x = async.Bind(x, f >> async.Return)

let none() = async.Return None

let inline bind                f x = async.Bind(x, Option.bindAsync f)
let inline contains            v x = asyncMap (Option.contains v) x
let inline count                 x = asyncMap Option.count x
let inline flatten               x = asyncMap Option.flatten x
let inline filter      predicate x = asyncMap (Option.filter predicate) x
let inline filterAsync predicate x = async.Bind(x, Option.filterAsync predicate)
let inline defaultValue        v x = asyncMap (Option.defaultValue v) x
let        defaultValueAsync   v x = async.Bind(x, Option.map async.Return >> Option.defaultValue v)
let inline defaultWith         f x = asyncMap (Option.defaultWith f) x
let        defaultWithAsync    f x = async.Bind(x, Option.map async.Return >> Option.defaultWith f)
let inline map                 f x = asyncMap (Option.map f) x
let inline mapAsync            f x = async.Bind(x, Option.mapAsync f)

let get (x: OptionAsync<'A>) :Async<'A> =
    async {
        let! result = x
        return result |> Option.get
    }

let getOrFail (messenger: unit -> string) (x: OptionAsync<'A>) =
    async {
        match! x with
        | Some v -> return v
        | None -> return failwith <| messenger()
    }

let getOrFailAsync messenger x =
    async {
        match! x with
        | Some v -> return v
        | None -> let! s = messenger() in return failwith s
    }

let getOrRaise (raiser: unit -> exn) (x: OptionAsync<'A>) =
    async {
        match! x with
        | Some v -> return v
        | None -> return raise <| raiser()
    }

let getOrRaiseAsync raiser x =
    async {
        match! x with
        | Some v -> return v
        | None -> let! (e:exn) = raiser() in return raise e
    }

let iter (f: 'A -> unit) (x: OptionAsync<'A>) :Async<unit> =
    async {
        let! result = x
        return result |> Option.iter f
    }

let iterAsync (f: 'A -> Async<unit>) (x: OptionAsync<'A>) :Async<unit> =
    async {
        match! x with
        | Some v -> do! f v
        | None -> ()
    }

let orElse (ifNone: 'A option) (x: OptionAsync<'A>) :OptionAsync<'A> =
    async {
        let! result = x
        return result |> Option.orElse ifNone
    }

let orElseAsync (ifNone: OptionAsync<'A>) (x: OptionAsync<'A>) :OptionAsync<'A> =
    async {
        match! x with
        | Some v -> return Some v
        | None -> return! ifNone
    }

let orElseWith (f: unit -> 'A option) (x: OptionAsync<'A>) :OptionAsync<'A> =
    async {
        let! result = x
        return result |> Option.orElseWith f
    }

let orElseWithAsync (f: unit -> OptionAsync<'A>) (x: OptionAsync<'A>) :OptionAsync<'A> =
    async {
        match! x with
        | Some v -> return Some v
        | None -> return! f()
    }

let safeCall fun' a = async {
    try
        let! v = fun' a in return Some v
    with
    | _ -> return None
}

let safeCall2 fun' a b = async {
    try
        let! v = (fun' a b) in return Some v
    with
    | _ -> return None
}

let safeCall3 fun' a b c = async {
    try
        let! v = (fun' a b c) in return Some v
    with
    | _ -> return None
}

let safeCall4 fun' a b c d = async {
    try
        let! v = (fun' a b c d) in return Some v
    with
    | _ -> return None
}

let safeCall5 fun' a b c d e = async {
    try
        let! v = (fun' a b c d e) in return Some v
    with
    | _ -> return None
}

let safeCall6 fun' a b c d e f = async {
    try
        let! v = (fun' a b c d e f) in return Some v
    with
    | _ -> return None
}

let try' async' = async {
    try
        let! result = async'
        return Some(result)
    with
    | _ -> return None
}

let then' fsome fnone x = async {
    match! x with
    | Some v -> fsome v
    | None -> fnone()
}

let thenAsync fsome fnone x = async {
    match! x with
    | Some v -> do! fsome v
    | None -> do! fnone()
}

let inline toArray       x = asyncMap Option.toArray x
let inline toList        x = asyncMap Option.toList x
let inline toNullable    x = asyncMap Option.toNullable x
let inline toObj         x = asyncMap Option.toObj x
let inline toValueOption x = asyncMap Option.toValueOption x
let inline unwrap        x = asyncMap Option.unwrap x