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

let single (xs: 'T seq) =
    use iter = xs.GetEnumerator()
    if iter.MoveNext() then
        let r = iter.Current
        if iter.MoveNext() then None else Some r
    else
        None

let fromIterator (itor:IEnumerator<'T>) = seq {
  use _ = itor
  while itor.MoveNext() do yield itor.Current
}

let tryFold f (ss: 'T seq) =
    let itor = ss.GetEnumerator()
    if itor.MoveNext() then
      Some (itor |> Iterator.fold f itor.Current)
    else
      itor.Dispose()
      None

let inline tryMin (ss: 'T seq) = ss |> tryFold min
let inline tryMax (ss:'T seq) = ss |> tryFold max