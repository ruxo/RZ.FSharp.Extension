namespace RZ.FSharp.Extension

open System.Runtime.CompilerServices

[<Extension>]
type OptionExtension =
  [<Extension>] static member inline isSome(x: Option<'a>) :bool = Option.isSome x
  [<Extension>] static member inline isNone(x: Option<'a>) :bool = Option.isNone x
  
  [<Extension>] static member inline ap(x: Option<'a -> 'b>, other) = x |> Option.ap other
  [<Extension>] static member inline call(x: Option<'a -> 'b>, p) = x |> Option.call p
  [<Extension>] static member inline join(x: 'a option option) = x |> Option.flatten

  [<Extension>] static member inline iter(x: Option<'a>, [<InlineIfLambda>] fsome) = x |> Option.iter fsome
  [<Extension>] static member inline then'(x: Option<'a>, [<InlineIfLambda>] fsome) ([<InlineIfLambda>] fnone) = x |> Option.then' fsome fnone
  [<Extension>] static member inline filter(x: Option<'a>, [<InlineIfLambda>] predicate) = x |> Option.filter predicate
  [<Extension>] static member        get(x: Option<'a>) =
                  match x with
                  | Some v -> v
                  | None -> raise <| UnwrapError.from($"Unwrap None value of Option<{typeof<'a>.Name}>")
      
  [<Extension>] static member inline defaultValue(x: Option<'a>, def) = x |> Option.defaultValue def
  [<Extension>] static member inline defaultWith(x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.defaultWith f
  [<Extension>] static member inline orElse(x: Option<'a>, v) = x |> Option.orElse v
  [<Extension>] static member inline orElseWith(x: Option<'a>, [<InlineIfLambda>] fnone) = x |> Option.orElseWith fnone
  [<Extension>] static member inline bind(x: Option<'a>, [<InlineIfLambda>] y) = x |> Option.bind y
  [<Extension>] static member inline map(x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.map f
  [<Extension>] static member inline mapTask(x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.mapTask f
  [<Extension>] static member inline bindTask(x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.bindTask f
  [<Extension>] static member inline mapAsync(x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.mapAsync f
  [<Extension>] static member inline bindAsync(x: Option<'a>, [<InlineIfLambda>] f) = x |> Option.bindAsync f