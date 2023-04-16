module RZ.FSharp.Extension.Seq

open System.Collections.Generic
open System.Linq
open System.Runtime.CompilerServices

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

[<Extension>]
type SeqExtension =
    [<Extension>] static member inline fromIterator  x = fromIterator x
    [<Extension>] static member inline single        x = single x
    [<Extension>] static member inline average       x = Seq.average x
    [<Extension>] static member inline cache         x = Seq.cache x
    [<Extension>] static member inline cast<'T>      x = Seq.cast<'T> x
    [<Extension>] static member inline concat        x = Seq.concat x
    [<Extension>] static member inline distinct      x = Seq.distinct x
    [<Extension>] static member inline exactlyOne    x = Seq.exactlyOne x
    [<Extension>] static member inline head          x = Seq.head x
    [<Extension>] static member inline indexed       x = Seq.indexed x
    [<Extension>] static member inline isEmpty       x = Seq.isEmpty x
    [<Extension>] static member inline last          x = Seq.last x
    [<Extension>] static member inline length        x = Seq.length x
    [<Extension>] static member inline max           x = Seq.max x
    [<Extension>] static member inline min           x = Seq.min x
    [<Extension>] static member inline pairwise      x = Seq.pairwise x
    [<Extension>] static member inline readonly      x = Seq.readonly x
    [<Extension>] static member inline rev           x = Seq.rev x
    [<Extension>] static member inline sort          x = Seq.sort x
    [<Extension>] static member inline sum           x = Seq.sum x
    [<Extension>] static member inline tail          x = Seq.tail x
    [<Extension>] static member inline toArray       x = Seq.toArray x
    [<Extension>] static member inline toList        x = Seq.toList x
    [<Extension>] static member inline transpose     x = Seq.transpose x
    [<Extension>] static member inline tryExactlyOne x = Seq.tryExactlyOne x
    [<Extension>] static member inline tryHead       x = Seq.tryHead x
    [<Extension>] static member inline tryLast       x = Seq.tryLast x
    [<Extension>] static member inline tryMin        x = tryMin x
    [<Extension>] static member inline tryMax        x = tryMax x
    
    [<Extension>] static member inline tryFold         (x, [<InlineIfLambda>] f) = tryFold f x
    [<Extension>] static member inline allPairs        (x,                    y) = Seq.allPairs y x
    [<Extension>] static member inline append          (x,                    y) = Seq.append y x
    [<Extension>] static member inline averageBy       (x, [<InlineIfLambda>] f) = Seq.averageBy f x
    [<Extension>] static member inline choose          (x, [<InlineIfLambda>] f) = Seq.choose f x
    [<Extension>] static member inline chunkBySize     (x,                    n) = Seq.chunkBySize n x
    [<Extension>] static member inline collect         (x, [<InlineIfLambda>] f) = Seq.collect f x
    [<Extension>] static member inline contains        (x,                    v) = Seq.contains v x
    [<Extension>] static member inline countBy         (x, [<InlineIfLambda>] f) = Seq.countBy f x
    [<Extension>] static member inline distinctBy      (x, [<InlineIfLambda>] f) = Seq.distinctBy f x
    [<Extension>] static member inline except          (x,                    y) = Seq.except y x
    [<Extension>] static member inline exists          (x, [<InlineIfLambda>] f) = Seq.exists f x
    [<Extension>] static member inline filter          (x, [<InlineIfLambda>] f) = Seq.filter f x
    [<Extension>] static member inline find            (x, [<InlineIfLambda>] f) = Seq.find f x
    [<Extension>] static member inline findBack        (x, [<InlineIfLambda>] f) = Seq.findBack f x
    [<Extension>] static member inline findIndex       (x, [<InlineIfLambda>] f) = Seq.findIndex f x
    [<Extension>] static member inline findIndexBack   (x, [<InlineIfLambda>] f) = Seq.findIndexBack f x
    [<Extension>] static member inline forall          (x, [<InlineIfLambda>] f) = Seq.forall f x
    [<Extension>] static member inline groupBy         (x, [<InlineIfLambda>] f) = Seq.groupBy f x
    [<Extension>] static member inline item            (x,                    i) = Seq.item i x
    [<Extension>] static member inline iter            (x, [<InlineIfLambda>] f) = Seq.iter f x
    [<Extension>] static member inline iteri           (x, [<InlineIfLambda>] f) = Seq.iteri f x
    [<Extension>] static member inline map             (x, [<InlineIfLambda>] f) = Seq.map f x
    [<Extension>] static member inline mapi            (x, [<InlineIfLambda>] f) = Seq.mapi f x
    [<Extension>] static member inline maxBy           (x, [<InlineIfLambda>] f) = Seq.maxBy f x
    [<Extension>] static member inline minBy           (x, [<InlineIfLambda>] f) = Seq.minBy f x
    [<Extension>] static member inline permute         (x, [<InlineIfLambda>] f) = Seq.permute f x
    [<Extension>] static member inline pick            (x, [<InlineIfLambda>] f) = Seq.pick f x
    [<Extension>] static member inline reduce          (x, [<InlineIfLambda>] f) = Seq.reduce f x
    [<Extension>] static member inline reduceBack      (x, [<InlineIfLambda>] f) = Seq.reduceBack f x
    [<Extension>] static member inline removeAt        (x,                    i) = Seq.removeAt i x
    [<Extension>] static member inline skip            (x,                    i) = Seq.skip i x
    [<Extension>] static member inline skipWhile       (x, [<InlineIfLambda>] f) = Seq.skipWhile f x
    [<Extension>] static member inline sortBy          (x, [<InlineIfLambda>] f) = Seq.sortBy f x
    [<Extension>] static member inline sortByDescending(x, [<InlineIfLambda>] f) = Seq.sortByDescending f x
    [<Extension>] static member inline sortWith        (x, [<InlineIfLambda>] f) = Seq.sortWith f x
    [<Extension>] static member inline splitInto       (x,                    i) = Seq.splitInto i x
    [<Extension>] static member inline sumBy           (x, [<InlineIfLambda>] f) = Seq.sumBy f x
    [<Extension>] static member inline take            (x,                    n) = Seq.take n x
    [<Extension>] static member inline takeWhile       (x, [<InlineIfLambda>] f) = Seq.takeWhile f x
    [<Extension>] static member inline truncate        (x,                    n) = Seq.truncate n x
    [<Extension>] static member inline tryFind         (x, [<InlineIfLambda>] f) = Seq.tryFind f x
    [<Extension>] static member inline tryFindBack     (x, [<InlineIfLambda>] f) = Seq.tryFindBack f x
    [<Extension>] static member inline tryFindIndex    (x, [<InlineIfLambda>] f) = Seq.tryFindIndex f x
    [<Extension>] static member inline tryFindIndexBack(x, [<InlineIfLambda>] f) = Seq.tryFindIndexBack f x
    [<Extension>] static member inline tryItem         (x,                    i) = Seq.tryItem i x
    [<Extension>] static member inline tryPick         (x, [<InlineIfLambda>] f) = Seq.tryPick f x
    [<Extension>] static member inline where           (x, [<InlineIfLambda>] f) = Seq.where f x
    [<Extension>] static member inline windowed        (x,                    n) = Seq.windowed n x
    [<Extension>] static member inline zip             (x,                    y) = Seq.zip y x
    
    [<Extension>] static member inline compareWith (x, y, [<InlineIfLambda>] f) = Seq.compareWith f y x
    [<Extension>] static member inline except      (x, y,                    c) = except3 c y x
    [<Extension>] static member inline exists      (x, y, [<InlineIfLambda>] f) = Seq.exists2 f y x
    [<Extension>] static member inline fold        (x, i, [<InlineIfLambda>] f) = Seq.fold f i x
    [<Extension>] static member inline foldBack    (x, i, [<InlineIfLambda>] f) = Seq.foldBack f x i
    [<Extension>] static member inline forall      (x, y, [<InlineIfLambda>] f) = Seq.forall2 f y x
    [<Extension>] static member inline insertAt    (x, i,                    v) = Seq.insertAt i v x
    [<Extension>] static member inline insertManyAt(x, i,                    v) = Seq.insertManyAt i v x
    [<Extension>] static member inline iter        (x, y, [<InlineIfLambda>] f) = Seq.iter2 f y x
    [<Extension>] static member inline iteri       (x, y, [<InlineIfLambda>] f) = Seq.iteri2 f y x
    [<Extension>] static member inline map         (x, y, [<InlineIfLambda>] f) = Seq.map2 f y x
    [<Extension>] static member inline mapFold     (x, i, [<InlineIfLambda>] f) = Seq.mapFold f i x
    [<Extension>] static member inline mapFoldBack (x, i, [<InlineIfLambda>] f) = Seq.mapFoldBack f x i
    [<Extension>] static member inline mapi        (x, y, [<InlineIfLambda>] f) = Seq.mapi2 f y x
    [<Extension>] static member inline removeManyAt(x, i,                    v) = Seq.removeManyAt i v x
    [<Extension>] static member inline scan        (x, i, [<InlineIfLambda>] f) = Seq.scan f i x
    [<Extension>] static member inline scanBack    (x, i, [<InlineIfLambda>] f) = Seq.scanBack f x i
    [<Extension>] static member inline updateAt    (x, i,                    v) = Seq.updateAt i v x
    [<Extension>] static member inline zip         (x, y,                    z) = Seq.zip3 z y x
    
    [<Extension>] static member inline fold        (x, i, [<InlineIfLambda>] f) = Iterator.fold f i x
    
    [<Extension>] static member inline fold    (x, y, i, [<InlineIfLambda>] f) = Seq.fold2 f i y x
    [<Extension>] static member inline foldBack(x, y, i, [<InlineIfLambda>] f) = Seq.foldBack2 f y x i
    [<Extension>] static member inline map     (x, y, z, [<InlineIfLambda>] f) = Seq.map3 f z y x