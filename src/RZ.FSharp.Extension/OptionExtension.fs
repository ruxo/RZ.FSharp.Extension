namespace RZ.FSharp.Extension

open System.Runtime.CompilerServices

[<Extension>]
type OptionExtension =
  [<Extension>] static member inline unwrap  x = Option.unwrap x
  [<Extension>] static member inline flatten x = Option.flatten x
  
  [<Extension>] static member inline unwrapOrFail (x,                   v) = x |> Option.unwrapOrFail v
  [<Extension>] static member inline unwrapOrRaise(x,                   v) = x |> Option.unwrapOrRaise v
  [<Extension>] static member inline defaultValue (x,                   v) = x |> Option.defaultValue v
  [<Extension>] static member inline defaultWith  (x,[<InlineIfLambda>] f) = x |> Option.defaultWith f
  [<Extension>] static member inline iter         (x,[<InlineIfLambda>] f) = x |> Option.iter f
  [<Extension>] static member inline orElse       (x,                   v) = x |> Option.orElse v
  [<Extension>] static member inline orElseWith   (x,[<InlineIfLambda>] f) = x |> Option.orElseWith f
  [<Extension>] static member inline map          (x,[<InlineIfLambda>] f) = Option.map f x
  [<Extension>] static member inline bind         (x,[<InlineIfLambda>] f) = Option.bind f x
  [<Extension>] static member inline ap           (x,                   f) = Option.ap f x
  [<Extension>] static member inline get          (x,[<InlineIfLambda>] f) = Option.get (Option.map f x)
  [<Extension>] static member inline filter       (x,[<InlineIfLambda>] p) = Option.filter p x

  [<Extension>] static member inline then'(x,[<InlineIfLambda>] some,[<InlineIfLambda>] none) = x |> Option.then' some none
      
  [<Extension>] static member inline call(f,x) = f |> Option.call x
  [<Extension>] static member inline call2(f,x,y) = f |> Option.call2 x y
  [<Extension>] static member inline call3(f,x,y,z) = f |> Option.call3 x y z
  [<Extension>] static member inline call4(f,x,y,z,u) = f |> Option.call4 x y z u
  [<Extension>] static member inline call5(f,x,y,z,u,v) = f |> Option.call5 x y z u v
  [<Extension>] static member inline call6(f,x,y,z,u,v,w) = f |> Option.call6 x y z u v w
  
  [<Extension>] static member inline mapTask(x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.mapTask f
  [<Extension>] static member inline bindTask(x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.bindTask f
  [<Extension>] static member inline mapAsync(x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.mapAsync f
  [<Extension>] static member inline bindAsync(x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.bindAsync f