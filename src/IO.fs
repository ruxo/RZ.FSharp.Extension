﻿module RZ.FSharp.Extension.IO

open System.Runtime.CompilerServices

type IOError<'T> = Result<'T, exn>
type IO<'env,'T> = 'env -> IOError<'T>
  
[<RequireQualifiedAccess>]
module IO =
  let inline wrap (x: 'a) :IO<'env,'a> = fun _ -> Ok x
  let inline map ([<InlineIfLambda>] f: 'a -> 'b) ([<InlineIfLambda>] x: IO<'env,'a>) = fun env -> x(env).map(f)
  let run (env: 'env, x: IO<'env,'a>) :IOError<'a> =
    try
      x(env)
    with
    | e -> Error e
 
  let inline bind ([<InlineIfLambda>] f: 'a -> IO<'env,'b>) ([<InlineIfLambda>] m: IO<'env,'a>) :IO<'env,'b> =
    fun env -> m(env).bind(fun x -> (f x) env)
    
  let inline retry([<InlineIfLambda>] ma: IO<'env,'a>) :IO<'env,'a> =
    fun env -> let mutable result = None
               while result.isNone() do
                 let r = ma(env)
                 match r with
                 | Ok _ -> result <- Some r
                 | Error _ -> ()
               result.get()
 
  type IOBuilder() =
    member inline _.Return(x: 'a) :IO<'err,'a> = wrap x
    member inline _.ReturnFrom([<InlineIfLambda>] x: IO<'err,'a>) :IO<'err,'a> = x
    member inline _.Bind([<InlineIfLambda>] m: IO<'err,'a>, [<InlineIfLambda>] f: 'a -> IO<'err,'b>) :IO<'err,'b> = bind f m
    member inline _.Yield(r: IOError<'a>) :IO<'err,'a> = fun _ -> r
    member inline _.Zero() :IO<'err,unit> = wrap ()
    
    member inline _.Delay([<InlineIfLambda>] w: unit -> IO<'err,'a>) :IO<'err,'a> = fun env -> w() env
 
let io = IO.IOBuilder()

[<Extension>]
type IOExtension() =
  [<Extension>] static member inline map([<InlineIfLambda>] my,[<InlineIfLambda>] f) = my |> IO.map f
  [<Extension>] static member inline run([<InlineIfLambda>] my, env) = IO.run (env, my)