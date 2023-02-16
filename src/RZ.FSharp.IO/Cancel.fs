namespace RZ.FSharp.IO

#nowarn "760" // ignore using new

open System.Runtime.CompilerServices
open System.Threading

type Cancel =
    abstract member token: CancellationToken
    abstract member cancel: unit -> unit
    abstract member createLocal: unit -> Cancel

type SupportCancel =
    abstract member cancel: Cancel
    
module Cancel =
    [<Struct; NoComparison; NoEquality>]
    type private LocalCancel(ct: CancellationTokenSource) =
        static member createLocalTokenFrom(token: CancellationToken) = 
            let localSource = CancellationTokenSource()
            token.Register(fun() -> localSource.Cancel()) |> ignore
            LocalCancel(localSource)
            
        interface Cancel with
            member _.token = ct.Token
            member _.cancel() = ct.Cancel()
            member _.createLocal() = LocalCancel.createLocalTokenFrom ct.Token
            
    [<IsReadOnly; Struct; NoComparison; NoEquality>]
    type private DefaultCancel =
        interface Cancel with
            member _.token = Async.DefaultCancellationToken
            member _.cancel() = Async.CancelDefaultToken()
            member _.createLocal() = LocalCancel.createLocalTokenFrom Async.DefaultCancellationToken
            
    let ``default``() :Cancel = DefaultCancel()