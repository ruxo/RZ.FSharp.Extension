namespace RZ.FSharp.Extension

open System.Runtime.CompilerServices

[<Extension>]
type ValueResultExtension =
    [<Extension>] static member inline count         x = ValueResult.count x
    [<Extension>] static member inline flatten       x = ValueResult.flatten x
    [<Extension>] static member inline get           x = ValueResult.get x
    [<Extension>] static member inline isOk          x = ValueResult.isOk x
    [<Extension>] static member inline isError       x = ValueResult.isError x
    [<Extension>] static member inline toArray       x = ValueResult.toArray x
    [<Extension>] static member inline toList        x = ValueResult.toList x
    [<Extension>] static member inline toNullable    x = x |> ValueResult.toValueOption |> ValueOption.toNullable
    [<Extension>] static member inline toObj         x = x |> ValueResult.toValueOption |> ValueOption.toObj
    [<Extension>] static member inline toValueOption x = ValueResult.toValueOption x
    [<Extension>] static member inline unwrap        x = ValueResult.unwrap x
    [<Extension>] static member inline unwrapErr     x = ValueResult.unwrapErr x
    
    [<Extension>] static member inline ap           (x,                   f) = ValueResult.ap x f
    [<Extension>] static member inline bind         (x,[<InlineIfLambda>] f) = ValueResult.bind f x
    [<Extension>] static member inline contains     (x,                   v) = ValueResult.contains v x
    [<Extension>] static member inline defaultValue (x,                   v) = x |> ValueResult.defaultValue v
    [<Extension>] static member inline defaultWith  (x,[<InlineIfLambda>] f) = x |> ValueResult.defaultWith f
    [<Extension>] static member inline exists       (x,[<InlineIfLambda>] f) = ValueResult.exists f x
    [<Extension>] static member inline filter       (x,[<InlineIfLambda>] p) = ValueResult.filter p x
    [<Extension>] static member inline forall       (x,[<InlineIfLambda>] p) = ValueResult.forall p x
    [<Extension>] static member inline iter         (x,[<InlineIfLambda>] f) = x |> ValueResult.iter f
    [<Extension>] static member inline map          (x,[<InlineIfLambda>] f) = ValueResult.map f x
    [<Extension>] static member inline mapError     (x,[<InlineIfLambda>] f) = ValueResult.mapError f x
    [<Extension>] static member inline orElse       (x,                   v) = x |> ValueResult.orElse v
    [<Extension>] static member inline orElseWith   (x,[<InlineIfLambda>] f) = x |> ValueResult.orElseWith f
    [<Extension>] static member inline unwrapOrFail (x,[<InlineIfLambda>] m) = x |> ValueResult.unwrapOrFail m
    [<Extension>] static member inline unwrapOrRaise(x,[<InlineIfLambda>] e) = x |> ValueResult.unwrapOrRaise e
    
    [<Extension>] static member inline fold    (x, i, [<InlineIfLambda>] f) = ValueResult.fold f i x
    [<Extension>] static member inline foldBack(x, i, [<InlineIfLambda>] f) = ValueResult.foldBack f x i
    
    [<Extension>] static member inline get     (x,[<InlineIfLambda>] ok,[<InlineIfLambda>] err) = x |> ValueResult.get ok err
    [<Extension>] static member inline mapBoth (x,[<InlineIfLambda>] ok,[<InlineIfLambda>] err) = x |> ValueResult.mapBoth ok err
    [<Extension>] static member inline bindBoth(x,[<InlineIfLambda>] ok,[<InlineIfLambda>] err) = x |> ValueResult.bindBoth ok err
    [<Extension>] static member inline then'   (x,[<InlineIfLambda>] ok,[<InlineIfLambda>] err) = x |> ValueResult.then' ok err
    [<Extension>] static member inline filter  (x,[<InlineIfLambda>] p ,[<InlineIfLambda>] err) = x |> ValueResult.filter p err
    
    [<Extension>] static member inline call(f,x) = f |> ValueResult.call x
    [<Extension>] static member inline call(f,x,y) = f |> ValueResult.call2 x y
    [<Extension>] static member inline call(f,x,y,z) = f |> ValueResult.call3 x y z
    [<Extension>] static member inline call(f,x,y,z,u) = f |> ValueResult.call4 x y z u
    [<Extension>] static member inline call(f,x,y,z,u,v) = f |> ValueResult.call5 x y z u v
    [<Extension>] static member inline call(f,x,y,z,u,v,w) = f |> ValueResult.call6 x y z u v w