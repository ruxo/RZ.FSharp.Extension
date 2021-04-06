module RZ.FSharp.Extension.Seq

open System.Collections.Generic
open System.Linq

let inline createComparer<'T> comparer hash_func =
    { new IEqualityComparer<'T> with 
        member _.Equals(x,y) = comparer x y
        member _.GetHashCode x = hash_func x }

let inline except3 (comparer: IEqualityComparer<'T>) (another: 'T seq) (source: 'T seq) =
    source.Except(another, comparer)

type IEnumerable<'T> with
    member inline this.except(another, comparer) = this |> except3 comparer another
