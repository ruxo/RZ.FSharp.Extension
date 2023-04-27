namespace RZ.FSharp.Extension

open System.Runtime.CompilerServices

[<Extension>]
type OptionExtension =
    [<Extension>] static member inline count         x = Option.count x
    [<Extension>] static member inline flatten       x = Option.flatten x
    [<Extension>] static member inline get           x = Option.get x
    [<Extension>] static member inline toArray       x = Option.toArray x
    [<Extension>] static member inline toList        x = Option.toList x
    [<Extension>] static member inline toNullable    x = Option.toNullable x
    [<Extension>] static member inline toObj         x = Option.toObj x
    [<Extension>] static member inline toValueOption x = Option.toValueOption x
    [<Extension>] static member inline unwrap        x = Option.unwrap x
    
    [<Extension>] static member inline ap           (f,                   x) = Option.ap x f
    [<Extension>] static member inline bind         (x,[<InlineIfLambda>] f) = Option.bind f x
    [<Extension>] static member inline contains     (x,                   v) = Option.contains v x
    [<Extension>] static member inline defaultValue (x,                   v) = x |> Option.defaultValue v
    [<Extension>] static member inline defaultWith  (x,[<InlineIfLambda>] f) = x |> Option.defaultWith f
    [<Extension>] static member inline exists       (x,[<InlineIfLambda>] f) = Option.exists f x
    [<Extension>] static member inline filter       (x,[<InlineIfLambda>] p) = Option.filter p x
    [<Extension>] static member inline forall       (x,[<InlineIfLambda>] p) = Option.forall p x
    [<Extension>] static member inline get          (x,[<InlineIfLambda>] f) = Option.get (Option.map f x)
    [<Extension>] static member inline iter         (x,[<InlineIfLambda>] f) = x |> Option.iter f
    [<Extension>] static member inline map          (x,[<InlineIfLambda>] f) = Option.map f x
    [<Extension>] static member inline orElse       (x,                   v) = x |> Option.orElse v
    [<Extension>] static member inline orElseWith   (x,[<InlineIfLambda>] f) = x |> Option.orElseWith f
    [<Extension>] static member inline unwrapOrFail (x,                   v) = x |> Option.unwrapOrFail v
    [<Extension>] static member inline unwrapOrRaise(x,                   v) = x |> Option.unwrapOrRaise v
    
    [<Extension>] static member inline fold         (x, i, [<InlineIfLambda>] f) = Option.fold f i x
    [<Extension>] static member inline foldBack     (x, i, [<InlineIfLambda>] f) = Option.foldBack f x i
    [<Extension>] static member inline map          (x, y, [<InlineIfLambda>] f) = Option.map2 f y x
  
    [<Extension>] static member inline then'(x,[<InlineIfLambda>] f,[<InlineIfLambda>] g) = x |> Option.then' f g
    
    [<Extension>] static member inline map          (x, y, z, [<InlineIfLambda>] f) = Option.map3 f z y x
        
    [<Extension>] static member inline call(f,x) = f |> Option.call x
    [<Extension>] static member inline call(f,x,y) = f |> Option.call2 x y
    [<Extension>] static member inline call(f,x,y,z) = f |> Option.call3 x y z
    [<Extension>] static member inline call(f,x,y,z,u) = f |> Option.call4 x y z u
    [<Extension>] static member inline call(f,x,y,z,u,v) = f |> Option.call5 x y z u v
    [<Extension>] static member inline call(f,x,y,z,u,v,w) = f |> Option.call6 x y z u v w
    
    [<Extension>] static member inline mapTask  (x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.mapTask f
    [<Extension>] static member inline bindTask (x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.bindTask f
    [<Extension>] static member inline mapAsync (x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.mapAsync f
    [<Extension>] static member inline bindAsync(x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.bindAsync f