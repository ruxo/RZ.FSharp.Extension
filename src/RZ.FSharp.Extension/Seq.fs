module RZ.FSharp.Extension.Seq

open System.Collections.Generic
open System.Linq

module Iterator =
  let fold reducer (init:'b) (itor:IEnumerator<'a>) :'b =
    use _ = itor
    if itor.MoveNext() then
      let mutable v = init
      while itor.MoveNext() do
        v <- reducer v itor.Current
      v
    else
      init

let inline except3 (comparer: IEqualityComparer<'T>) (another: 'T seq) (source: 'T seq) =
    source.Except(another, comparer)

let single (xs: 'T seq) :'T voption =
    use iter = xs.GetEnumerator()
    if iter.MoveNext() then
        let r = iter.Current
        if iter.MoveNext() then ValueNone else ValueSome r
    else
        ValueNone

let fromIterator (itor:IEnumerator<'T>) :'T seq = seq {
  use _ = itor
  while itor.MoveNext() do yield itor.Current
}

let tryFold f (ss: 'T seq) :'T voption =
    use itor = ss.GetEnumerator()
    if itor.MoveNext()
    then ValueSome (itor |> Iterator.fold f itor.Current)
    else ValueNone

let inline tryMin (ss :'T seq) :'T voption = ss |> tryFold min
let inline tryMax (ss :'T seq) :'T voption = ss |> tryFold max