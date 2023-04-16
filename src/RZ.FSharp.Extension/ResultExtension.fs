namespace RZ.FSharp.Extension

open System.Runtime.CompilerServices

[<Extension>]
type ResultExtension =
    [<Extension>] static member inline count         x = Result.count x
    [<Extension>] static member inline flatten       x = Result.flatten x
    [<Extension>] static member inline get           x = Result.get x
    [<Extension>] static member inline isOk          x = Result.isOk x
    [<Extension>] static member inline isError       x = Result.isError x
    [<Extension>] static member inline toArray       x = Result.toArray x
    [<Extension>] static member inline toList        x = Result.toList x
    [<Extension>] static member inline toNullable    x = x |> Result.toOption |> Option.toNullable
    [<Extension>] static member inline toObj         x = x |> Result.toOption |> Option.toObj
    [<Extension>] static member inline toValueOption x = Result.toValueOption x
    [<Extension>] static member inline unwrap        x = Result.unwrap x
    [<Extension>] static member inline unwrapErr     x = Result.unwrapErr x
    
    [<Extension>] static member inline ap           (x,                   f) = Result.ap x f
    [<Extension>] static member inline bind         (x,[<InlineIfLambda>] f) = Result.bind f x
    [<Extension>] static member inline contains     (x,                   v) = Result.contains v x
    [<Extension>] static member inline defaultValue (x,                   v) = x |> Result.defaultValue v
    [<Extension>] static member inline defaultWith  (x,[<InlineIfLambda>] f) = x |> Result.defaultWith f
    [<Extension>] static member inline exists       (x,[<InlineIfLambda>] f) = Result.exists f x
    [<Extension>] static member inline filter       (x,[<InlineIfLambda>] p) = Result.filter p x
    [<Extension>] static member inline forall       (x,[<InlineIfLambda>] p) = Result.forall p x
    [<Extension>] static member inline iter         (x,[<InlineIfLambda>] f) = x |> Result.iter f
    [<Extension>] static member inline map          (x,[<InlineIfLambda>] f) = Result.map f x
    [<Extension>] static member inline mapError     (x,[<InlineIfLambda>] f) = Result.mapError f x
    [<Extension>] static member inline orElse       (x,                   v) = x |> Result.orElse v
    [<Extension>] static member inline orElseWith   (x,[<InlineIfLambda>] f) = x |> Result.orElseWith f
    [<Extension>] static member inline unwrapOrFail (x,[<InlineIfLambda>] m) = x |> Result.unwrapOrFail m
    [<Extension>] static member inline unwrapOrRaise(x,[<InlineIfLambda>] e) = x |> Result.unwrapOrRaise e
    
    [<Extension>] static member inline fold    (x, i, [<InlineIfLambda>] f) = Result.fold f i x
    [<Extension>] static member inline foldBack(x, i, [<InlineIfLambda>] f) = Result.foldBack f x i
    
    [<Extension>] static member inline get     (x,[<InlineIfLambda>] ok,[<InlineIfLambda>] err) = x |> Result.get ok err
    [<Extension>] static member inline mapBoth (x,[<InlineIfLambda>] ok,[<InlineIfLambda>] err) = x |> Result.mapBoth ok err
    [<Extension>] static member inline bindBoth(x,[<InlineIfLambda>] ok,[<InlineIfLambda>] err) = x |> Result.bindBoth ok err
    [<Extension>] static member inline then'   (x,[<InlineIfLambda>] ok,[<InlineIfLambda>] err) = x |> Result.then' ok err
    [<Extension>] static member inline filter  (x,[<InlineIfLambda>] p ,[<InlineIfLambda>] err) = x |> Result.filter p err
    
    [<Extension>] static member inline call(f,x) = f |> Result.call x
    [<Extension>] static member inline call2(f,x,y) = f |> Result.call2 x y
    [<Extension>] static member inline call3(f,x,y,z) = f |> Result.call3 x y z
    [<Extension>] static member inline call4(f,x,y,z,u) = f |> Result.call4 x y z u
    [<Extension>] static member inline call5(f,x,y,z,u,v) = f |> Result.call5 x y z u v
    [<Extension>] static member inline call6(f,x,y,z,u,v,w) = f |> Result.call6 x y z u v w