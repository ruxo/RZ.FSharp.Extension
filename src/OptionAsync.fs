module RZ.FSharp.Extension.OptionAsync

open Prelude

let none() = Async.return' None

let map (f: 'A -> 'B) (x: OptionAsync<'A>) :OptionAsync<'B> = async {
    let! result = x
    return result |> Option.map f
}

let mapAsync f x = async {
    match! x with
    | Some v -> let! r = f v in return Some r
    | None -> return None
}

let bind (f: 'A -> OptionAsync<'B>) (x: OptionAsync<'A>) :OptionAsync<'B> = async {
    let! result = x
    return! result |> Option.bindAsync f
}

let bindAsync f x = async {
    match! x with
    | Some v -> let! r = f v in return r
    | None -> return None
}

let filter (predicate: 'A -> bool) (x: OptionAsync<'A>) :OptionAsync<'A> =
    async {
        let! result = x
        return if result |> Option.map predicate |> Option.defaultValue false
               then result
               else None
    }

let filterAsync (predicate: 'A -> Async<bool>) (x: OptionAsync<'A>) :OptionAsync<'A> =
    async {
        let! result = x
        let! pass = result |> Option.mapAsync predicate
        return if pass |> Option.defaultValue false
               then result
               else None
    }

let defaultValue (v: 'A) (x: OptionAsync<'A>) :Async<'A> =
    async {
        let! result = x
        return result |> Option.defaultValue v
    }

let defaultValueAsync (def: Async<'A>) (x: OptionAsync<'A>) :Async<'A> =
    async {
        match! x with
        | Some v -> return v
        | None -> return! def
    }

let defaultWith (f: unit -> 'A) (x: OptionAsync<'A>) :Async<'A> =
    async {
        let! result = x
        return result |> Option.defaultWith f
    }

let defaultWithAsync (f: unit -> Async<'A>) (x: OptionAsync<'A>) :Async<'A> =
    async {
        match! x with
        | Some v -> return v
        | None -> return! f()
    }

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