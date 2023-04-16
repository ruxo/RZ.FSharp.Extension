namespace RZ.FSharp.Extension

open System.Runtime.CompilerServices

[<Extension>]
type MapExtension =
    [<Extension>] static member inline count       x = Map.count x
    [<Extension>] static member inline keys        x = Map.keys x
    [<Extension>] static member inline maxKeyValue x = Map.maxKeyValue x
    [<Extension>] static member inline minKeyValue x = Map.minKeyValue x
    [<Extension>] static member inline toArray     x = Map.toArray x
    [<Extension>] static member inline toList      x = Map.toList x
    [<Extension>] static member inline toSeq       x = Map.toSeq x
    [<Extension>] static member inline values      x = Map.values x
    
    [<Extension>] static member inline containsKey(x,                    k) = Map.containsKey k x
    [<Extension>] static member inline exists     (x, [<InlineIfLambda>] f) = Map.exists f x
    [<Extension>] static member inline filter     (x, [<InlineIfLambda>] f) = Map.filter f x
    [<Extension>] static member inline find       (x,                    k) = Map.find k x
    [<Extension>] static member inline findKey    (x, [<InlineIfLambda>] f) = Map.findKey f x
    [<Extension>] static member inline forall     (x, [<InlineIfLambda>] f) = Map.forall f x
    [<Extension>] static member inline iter       (x, [<InlineIfLambda>] f) = Map.iter f x
    [<Extension>] static member inline map        (x, [<InlineIfLambda>] f) = Map.map f x
    [<Extension>] static member inline partition  (x, [<InlineIfLambda>] f) = Map.partition f x
    [<Extension>] static member inline pick       (x, [<InlineIfLambda>] f) = Map.pick f x
    [<Extension>] static member inline remove     (x,                    k) = Map.remove k x
    [<Extension>] static member inline tryFind    (x,                    k) = Map.tryFind k x
    [<Extension>] static member inline tryFindKey (x, [<InlineIfLambda>] f) = Map.tryFindKey f x
    [<Extension>] static member inline tryPick    (x, [<InlineIfLambda>] f) = Map.tryPick f x
    
    [<Extension>] static member inline add        (x, k,                    v) = Map.add k v x
    [<Extension>] static member inline change     (x, k, [<InlineIfLambda>] f) = Map.change k f x
    [<Extension>] static member inline fold       (x, i, [<InlineIfLambda>] f) = Map.fold f i x
    [<Extension>] static member inline foldBack   (x, i, [<InlineIfLambda>] f) = Map.foldBack f x i
    
    [<Extension>] static member inline toMap(x, [<InlineIfLambda>] f) = Map.byKey f x