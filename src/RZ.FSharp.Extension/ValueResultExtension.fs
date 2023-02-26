namespace RZ.FSharp.Extension

open System.Runtime.CompilerServices

[<Extension>]
type ValueResultExtension =
    [<Extension>] static member inline isOk       x = ValueResult.isOk x
    [<Extension>] static member inline isError    x = ValueResult.isError x
    [<Extension>] static member inline unwrap     x = ValueResult.unwrap x
    [<Extension>] static member inline unwrapErr  x = ValueResult.unwrapErr x
    [<Extension>] static member inline flatten    x = ValueResult.flatten x
    
    [<Extension>] static member inline unwrapOrFail (x,[<InlineIfLambda>] m) = x |> ValueResult.unwrapOrFail m
    [<Extension>] static member inline unwrapOrRaise(x,[<InlineIfLambda>] e) = x |> ValueResult.unwrapOrRaise e
    [<Extension>] static member inline defaultValue (x,                   v) = x |> ValueResult.defaultValue v
    [<Extension>] static member inline defaultWith  (x,[<InlineIfLambda>] f) = x |> ValueResult.defaultWith f
    [<Extension>] static member inline iter         (x,[<InlineIfLambda>] f) = x |> ValueResult.iter f
    [<Extension>] static member inline orElse       (x,                   v) = x |> ValueResult.orElse v
    [<Extension>] static member inline orElseWith   (x,[<InlineIfLambda>] f) = x |> ValueResult.orElseWith f
    [<Extension>] static member inline map          (x,[<InlineIfLambda>] f) = ValueResult.map f x
    [<Extension>] static member inline bind         (x,[<InlineIfLambda>] f) = ValueResult.bind f x
    [<Extension>] static member inline ap           (x,                   f) = ValueResult.ap x f
    
    [<Extension>] static member inline get     (x,[<InlineIfLambda>] ok,[<InlineIfLambda>] err) = x |> ValueResult.get ok err
    [<Extension>] static member inline mapBoth (x,[<InlineIfLambda>] ok,[<InlineIfLambda>] err) = x |> ValueResult.mapBoth ok err
    [<Extension>] static member inline bindBoth(x,[<InlineIfLambda>] ok,[<InlineIfLambda>] err) = x |> ValueResult.bindBoth ok err
    [<Extension>] static member inline then'   (x,[<InlineIfLambda>] ok,[<InlineIfLambda>] err) = x |> ValueResult.then' ok err
    [<Extension>] static member inline filter  (x,[<InlineIfLambda>] p ,[<InlineIfLambda>] err) = x |> ValueResult.filter p err
    
    [<Extension>] static member inline call(f,x) = f |> ValueResult.call x
    [<Extension>] static member inline call2(f,x,y) = f |> ValueResult.call2 x y
    [<Extension>] static member inline call3(f,x,y,z) = f |> ValueResult.call3 x y z
    [<Extension>] static member inline call4(f,x,y,z,u) = f |> ValueResult.call4 x y z u
    [<Extension>] static member inline call5(f,x,y,z,u,v) = f |> ValueResult.call5 x y z u v
    [<Extension>] static member inline call6(f,x,y,z,u,v,w) = f |> ValueResult.call6 x y z u v w