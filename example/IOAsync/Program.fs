open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open RZ.FSharp.Extension.IO

type RandomAff =
    abstract member Next: int -> Async<int>
    
type HasRandom =
    abstract member Random: RandomAff
    
type LiveRandom private () =
    let r = Random()
    
    static member Default = LiveRandom()

    interface RandomAff with
        member _.Next delay = async {
            do! Task.Delay(delay * 500) |> Async.AwaitTask
            return r.Next 10
        }
        
let inline next_random delay :IO<'env,int> = fun (rt: #HasRandom) -> async {
    let! v = rt.Random.Next delay in return Ok v
}
        
type ConsoleIO =
    abstract member ReadLine: unit -> string
    abstract member Write: string -> unit
    abstract member WriteLine: string -> unit
    
type HasConsole =
    abstract member Console: ConsoleIO
        
[<IsReadOnly; Struct; NoComparison; NoEquality>]
type RealConsoleIO =
    static member Default = RealConsoleIO()
    
    interface ConsoleIO with
        member _.ReadLine() = Console.ReadLine()
        member _.Write s    = Console.Write s
        member _.WriteLine s= Console.WriteLine s
        
let inline read_line()  = fun (rt: #HasConsole) -> asok(rt.Console.ReadLine() )
let inline write s      = fun (rt: #HasConsole) -> asok(rt.Console.Write s    )
let inline write_line s = fun (rt: #HasConsole) -> asok(rt.Console.WriteLine s)

let play = io {
    do! write "Guess: "
    let! n = read_line().map(int)
    let! r = next_random 3
    do! write_line $"Your {n} vs {r}"
}

[<IsReadOnly; Struct; NoComparison; NoEquality>]
type Live =
    static member Default = Live()
    interface HasRandom with member _.Random = LiveRandom.Default
    interface HasConsole with member _.Console = RealConsoleIO.Default

play.run(Live.Default).await()