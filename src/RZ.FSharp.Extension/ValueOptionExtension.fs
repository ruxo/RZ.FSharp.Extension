namespace RZ.FSharp.Extension

open System.Runtime.CompilerServices

[<Extension>]
type ValueOptionExtension =
    [<Extension>] static member inline count         x = ValueOption.count x
    [<Extension>] static member inline flatten       x = ValueOption.flatten x
    [<Extension>] static member inline get           x = ValueOption.get x
    [<Extension>] static member inline toArray       x = ValueOption.toArray x
    [<Extension>] static member inline toList        x = ValueOption.toList x
    [<Extension>] static member inline toNullable    x = ValueOption.toNullable x
    [<Extension>] static member inline toObj         x = ValueOption.toObj x
    [<Extension>] static member inline toOption      x = ValueOption.toOption x
    [<Extension>] static member inline unwrap        x = ValueOption.unwrap x
    
    [<Extension>] static member inline ap           (x,                   f) = ValueOption.ap f x
    [<Extension>] static member inline bind         (x,[<InlineIfLambda>] f) = ValueOption.bind f x
    [<Extension>] static member inline contains     (x,                   v) = ValueOption.contains v x
    [<Extension>] static member inline defaultValue (x,                   v) = x |> ValueOption.defaultValue v
    [<Extension>] static member inline defaultWith  (x,[<InlineIfLambda>] f) = x |> ValueOption.defaultWith f
    [<Extension>] static member inline exists       (x,[<InlineIfLambda>] f) = ValueOption.exists f x
    [<Extension>] static member inline filter       (x,[<InlineIfLambda>] p) = ValueOption.filter p x
    [<Extension>] static member inline forall       (x,[<InlineIfLambda>] p) = ValueOption.forall p x
    [<Extension>] static member inline get          (x,[<InlineIfLambda>] f) = ValueOption.get (ValueOption.map f x)
    [<Extension>] static member inline iter         (x,[<InlineIfLambda>] f) = x |> ValueOption.iter f
    [<Extension>] static member inline map          (x,[<InlineIfLambda>] f) = ValueOption.map f x
    [<Extension>] static member inline orElse       (x,                   v) = x |> ValueOption.orElse v
    [<Extension>] static member inline orElseWith   (x,[<InlineIfLambda>] f) = x |> ValueOption.orElseWith f
    [<Extension>] static member inline unwrapOrFail (x,                   v) = x |> ValueOption.unwrapOrFail v
    [<Extension>] static member inline unwrapOrRaise(x,                   v) = x |> ValueOption.unwrapOrRaise v
    
    [<Extension>] static member inline fold         (x, i, [<InlineIfLambda>] f) = ValueOption.fold f i x
    [<Extension>] static member inline foldBack     (x, i, [<InlineIfLambda>] f) = ValueOption.foldBack f x i
    [<Extension>] static member inline map          (x, y, [<InlineIfLambda>] f) = ValueOption.map2 f y x
  
    [<Extension>] static member inline then'(x,[<InlineIfLambda>] f,[<InlineIfLambda>] g) = x |> ValueOption.then' f g
    
    [<Extension>] static member inline map          (x, y, z, [<InlineIfLambda>] f) = ValueOption.map3 f z y x
        
    [<Extension>] static member inline call(f,x) = f |> ValueOption.call x
    [<Extension>] static member inline call2(f,x,y) = f |> ValueOption.call2 x y
    [<Extension>] static member inline call3(f,x,y,z) = f |> ValueOption.call3 x y z
    [<Extension>] static member inline call4(f,x,y,z,u) = f |> ValueOption.call4 x y z u
    [<Extension>] static member inline call5(f,x,y,z,u,v) = f |> ValueOption.call5 x y z u v
    [<Extension>] static member inline call6(f,x,y,z,u,v,w) = f |> ValueOption.call6 x y z u v w
    
    [<Extension>] static member inline mapTask(x: ValueOption<'a>, [<InlineIfLambda>] f) = x |> ValueOption.mapTask f
    [<Extension>] static member inline bindTask(x: ValueOption<'a>, [<InlineIfLambda>] f) = x |> ValueOption.bindTask f
    [<Extension>] static member inline mapAsync(x: ValueOption<'a>, [<InlineIfLambda>] f) = x |> ValueOption.mapAsync f
    [<Extension>] static member inline bindAsync(x: ValueOption<'a>, [<InlineIfLambda>] f) = x |> ValueOption.bindAsync f