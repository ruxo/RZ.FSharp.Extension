[<Microsoft.FSharp.Core.AutoOpen>]
module RZ.FSharp.IO.Prelude

open System
open System.Runtime.CompilerServices
open RZ.FSharp.Extension

type IOResult<'a> = Result<'a,exn>
type IOError<'a> = Async<IOResult<'a>>
type IO<'env,'T> = 'env -> IOError<'T>

let inline ioOk (x: 'a) :IOError<'a> = async.Return (Ok x)
let inline ioErr e :IOError<'a> = async.Return (Error e)
let        ioUnit :IOError<unit> = ioOk ()
  
type HasLocalContext<'env when 'env :> HasLocalContext<'env>> =
    abstract member createLocal: unit -> 'env

[<Extension>]
type IOErrorExtension =
  [<Extension>]
  static member map(my: IOError<'a>, f: 'a -> 'b) :IOError<'b> = async { let! v = my in return v.map(f) }
    
  [<Extension>]
  static member bind(my: IOError<'a>, f: 'a -> IOError<'b>) :IOError<'b> =
    async {
      match! my with
      | Error e -> return Error e
      | Ok v -> return! f v
    }
    
  [<Extension>]
  static member inline await(my: IOError<'a>) :'a = (my |> Async.RunSynchronously).unwrap()
    
[<RequireQualifiedAccess>]
module IO =
    let inline wrap (x: 'a) :IO<'env,'a> = fun _ -> ioOk x
    let inline map ([<InlineIfLambda>] f: 'a -> 'b) ([<InlineIfLambda>] x: IO<'env,'a>) = fun env -> x(env).map(f)
    let run (env: 'env, x: IO<'env,'a>) :IOError<'a> =
        try
          x(env)
        with
        | e -> ioErr e
   
    let inline bind ([<InlineIfLambda>] f: 'a -> IO<'env,'b>) ([<InlineIfLambda>] m: IO<'env,'a>) :IO<'env,'b> =
        fun env -> m(env).bind(fun x -> (f x) env)
      
    let retry(ma: IO<'env,'a>) :IO<'env,'a> =
        fun env -> async {
          let mutable result = None
          while result.IsNone do
            let! r = ma env
            match r with
            | Ok _ -> result <- Some r
            | Error _ -> ()
          return result.unwrap()
        }
    
    let fork(inner: IO<'env,unit>) :IO<'env,IO<'env,unit>>
        when 'env :> HasLocalContext<'env> and 'env :> SupportCancel
        = fun env -> async {
            let innerEnv = env.createLocal()
            let innerCancel = innerEnv.cancel
            Async.Start(Async.Ignore(inner innerEnv), innerCancel.token)
            return Ok <| fun _ -> ioOk(innerCancel.cancel())
        }
        
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
    [<Extension>] static member inline map ([<InlineIfLambda>] my,[<InlineIfLambda>] f) = my |> IO.map f
    [<Extension>] static member inline bind([<InlineIfLambda>] my,[<InlineIfLambda>] f) = my |> IO.bind f
    [<Extension>] static member inline run ([<InlineIfLambda>] my, env) = IO.run (env, my)
    
    [<Extension>] static member fold(my: IO<'env,'a>, Schedule schedule, init: 'b, folder) :IO<'env,'b> =
                    fun (env: 'env) -> async {
                        let mutable state = init
                        let! result = my env
                        let mutable value = result
                        use waiter = schedule.GetEnumerator()
                        while waiter.MoveNext() && value.isOk() do
                            if waiter.Current <> TimeSpan.Zero then
                                do! Async.Sleep waiter.Current
                            state <- folder state (Result.unwrap value)
                            let! result = my env
                            value <- result
                        return Ok state
                    }