module RZ.FSharp.Extension.VOptionAsync

open Prelude

let none() = async.Return ValueNone

let map (f: 'A -> 'B) (x: VOptionAsync<'A>) :VOptionAsync<'B> = async {
    let! result = x
    return result |> ValueOption.map f
}

let mapAsync f x = async {
    match! x with
    | ValueSome v -> let! r = f v in return ValueSome r
    | ValueNone -> return ValueNone
}

let bind (f: 'A -> VOptionAsync<'B>) (x: VOptionAsync<'A>) :VOptionAsync<'B> = async {
    let! result = x
    return! result |> ValueOption.bindAsync f
}

let bindAsync f x = async {
    match! x with
    | ValueSome v -> let! r = f v in return r
    | ValueNone -> return ValueNone
}

let filter (predicate: 'A -> bool) (x: VOptionAsync<'A>) :VOptionAsync<'A> =
    async {
        let! result = x
        return if result |> ValueOption.map predicate |> ValueOption.defaultValue false
               then result
               else ValueNone
    }

let filterAsync (predicate: 'A -> Async<bool>) (x: VOptionAsync<'A>) :VOptionAsync<'A> =
    async {
        let! result = x
        let! pass = result |> ValueOption.mapAsync predicate
        return if pass |> ValueOption.defaultValue false
               then result
               else ValueNone
    }

let defaultValue (v: 'A) (x: VOptionAsync<'A>) :Async<'A> =
    async {
        let! result = x
        return result |> ValueOption.defaultValue v
    }

let defaultValueAsync (def: Async<'A>) (x: VOptionAsync<'A>) :Async<'A> =
    async {
        match! x with
        | ValueSome v -> return v
        | ValueNone -> return! def
    }

let defaultWith (f: unit -> 'A) (x: VOptionAsync<'A>) :Async<'A> =
    async {
        let! result = x
        return result |> ValueOption.defaultWith f
    }

let defaultWithAsync (f: unit -> Async<'A>) (x: VOptionAsync<'A>) :Async<'A> =
    async {
        match! x with
        | ValueSome v -> return v
        | ValueNone -> return! f()
    }

let get (x: VOptionAsync<'A>) :Async<'A> =
    async {
        let! result = x
        return result |> ValueOption.get
    }

let getOrFail (messenger: unit -> string) (x: VOptionAsync<'A>) =
    async {
        match! x with
        | ValueSome v -> return v
        | ValueNone -> return failwith <| messenger()
    }

let getOrFailAsync messenger x =
    async {
        match! x with
        | ValueSome v -> return v
        | ValueNone -> let! s = messenger() in return failwith s
    }

let getOrRaise (raiser: unit -> exn) (x: VOptionAsync<'A>) =
    async {
        match! x with
        | ValueSome v -> return v
        | ValueNone -> return raise <| raiser()
    }

let getOrRaiseAsync raiser x =
    async {
        match! x with
        | ValueSome v -> return v
        | ValueNone -> let! (e:exn) = raiser() in return raise e
    }

let iter (f: 'A -> unit) (x: VOptionAsync<'A>) :Async<unit> =
    async {
        let! result = x
        return result |> ValueOption.iter f
    }

let iterAsync (f: 'A -> Async<unit>) (x: VOptionAsync<'A>) :Async<unit> =
    async {
        match! x with
        | ValueSome v -> do! f v
        | ValueNone -> ()
    }

let orElse (ifNone: 'A voption) (x: VOptionAsync<'A>) :VOptionAsync<'A> =
    async {
        let! result = x
        return result |> ValueOption.orElse ifNone
    }

let orElseAsync (ifNone: VOptionAsync<'A>) (x: VOptionAsync<'A>) :VOptionAsync<'A> =
    async {
        match! x with
        | ValueSome v -> return ValueSome v
        | ValueNone -> return! ifNone
    }

let orElseWith (f: unit -> 'A voption) (x: VOptionAsync<'A>) :VOptionAsync<'A> =
    async {
        let! result = x
        return result |> ValueOption.orElseWith f
    }

let orElseWithAsync (f: unit -> VOptionAsync<'A>) (x: VOptionAsync<'A>) :VOptionAsync<'A> =
    async {
        match! x with
        | ValueSome v -> return ValueSome v
        | ValueNone -> return! f()
    }

let safeCall fun' a = async {
    try
        let! v = fun' a in return ValueSome v
    with
    | _ -> return ValueNone
}

let safeCall2 fun' a b = async {
    try
        let! v = (fun' a b) in return ValueSome v
    with
    | _ -> return ValueNone
}

let safeCall3 fun' a b c = async {
    try
        let! v = (fun' a b c) in return ValueSome v
    with
    | _ -> return ValueNone
}

let safeCall4 fun' a b c d = async {
    try
        let! v = (fun' a b c d) in return ValueSome v
    with
    | _ -> return ValueNone
}

let safeCall5 fun' a b c d e = async {
    try
        let! v = (fun' a b c d e) in return ValueSome v
    with
    | _ -> return ValueNone
}

let safeCall6 fun' a b c d e f = async {
    try
        let! v = (fun' a b c d e f) in return ValueSome v
    with
    | _ -> return ValueNone
}

let then' fsome fnone x = async {
    match! x with
    | ValueSome v -> fsome v
    | ValueNone -> fnone()
}

let thenAsync fsome fnone x = async {
    match! x with
    | ValueSome v -> do! fsome v
    | ValueNone -> do! fnone()
}