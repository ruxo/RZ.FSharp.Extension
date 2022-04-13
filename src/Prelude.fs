module RZ.FSharp.Extension.Prelude

open System.Collections.Generic

let inline sideEffect ([<InlineIfLambda>] f) x = (f x); x

let inline flip f a b = f b a
let inline constant x = fun _ -> x

let inline createComparer<'T> comparer hash_func =
    { new IEqualityComparer<'T> with
        member _.Equals(x,y) = comparer x y
        member _.GetHashCode x = hash_func x }

type OptionAsync<'T> = Async<'T option>
type ResultAsync<'T,'E> = Async<Result<'T,'E>>

module Async =
    let return' v = async { return v }