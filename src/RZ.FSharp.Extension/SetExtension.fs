namespace RZ.FSharp.Extension

open System.Runtime.CompilerServices

[<Extension>]
type SetExtension =
    [<Extension>] static member inline count         x = Set.count x
    [<Extension>] static member inline intersectMany x = Set.intersectMany x
    [<Extension>] static member inline maxElement    x = Set.maxElement x
    [<Extension>] static member inline minElement    x = Set.minElement x
    [<Extension>] static member inline toArray       x = Set.toArray x
    [<Extension>] static member inline toList        x = Set.toList x
    [<Extension>] static member inline toSeq         x = Set.toSeq x
    [<Extension>] static member inline unionMany     x = Set.unionMany x
    
    [<Extension>] static member inline add             (x,                    v) = Set.add v x
    [<Extension>] static member inline contains        (x,                    v) = Set.contains v x
    [<Extension>] static member inline difference      (x,                    y) = Set.difference y x
    [<Extension>] static member inline exists          (x, [<InlineIfLambda>] f) = Set.exists f x
    [<Extension>] static member inline filter          (x, [<InlineIfLambda>] f) = Set.filter f x
    [<Extension>] static member inline forall          (x, [<InlineIfLambda>] f) = Set.forall f x
    [<Extension>] static member inline intersect       (x,                    y) = Set.intersect y x
    [<Extension>] static member inline isProperSubset  (x,                    y) = Set.isProperSubset y x
    [<Extension>] static member inline isProperSuperset(x,                    y) = Set.isProperSuperset y x
    [<Extension>] static member inline isSubset        (x,                    y) = Set.isSubset y x
    [<Extension>] static member inline isSuperset      (x,                    y) = Set.isSuperset y x
    [<Extension>] static member inline iter            (x, [<InlineIfLambda>] f) = Set.iter f x
    [<Extension>] static member inline map             (x, [<InlineIfLambda>] f) = Set.map f x
    [<Extension>] static member inline partition       (x, [<InlineIfLambda>] f) = Set.partition f x
    [<Extension>] static member inline remove          (x,                    v) = Set.remove v x
    [<Extension>] static member inline union           (x,                    y) = Set.union y x
    
    [<Extension>] static member inline fold    (x, i, [<InlineIfLambda>] f) = Set.fold f i x
    [<Extension>] static member inline foldBack(x, i, [<InlineIfLambda>] f) = Set.foldBack f x i