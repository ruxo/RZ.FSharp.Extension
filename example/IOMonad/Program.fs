open System
open RZ.FSharp.Extension
open RZ.FSharp.Extension.IO

// ------------------------------------ MOST SIMPLE IO ------------------------------------------------
let unit_env_sample =
    let read_text() = io { return Console.ReadLine() }
    let write_text s = io { Console.Write(s: string) }

    let program =
        io {
            do! write_text("What's your name? ")
            let! name = read_text()
            do! write_text($"Hello, {name}!\n")
        }
    program
    
unit_env_sample.run().get()
    
// ---------------------------------------- WITH AN ENVIRONMENT ---------------------------------------
type MyConsole =
    abstract member ReadLine: unit -> string
    abstract member Write: string -> unit
    
let read_text<'env when 'env :> MyConsole>() :IO<'env,string> = fun env -> Ok(env.ReadLine())
let write_text<'env when 'env :> MyConsole> s :IO<'env,unit> = fun env -> Ok(env.Write s)
    
let env_sample :IO<MyConsole,unit> =
    let program =
        io {
            do! write_text "Running with Environment"
            do! write_text("What's your name? ")
            let! name = read_text()
            do! write_text($"Hello, {name}!\n")
        }
    program
    
[<Struct; NoComparison; NoEquality>]
type TestEnv =
    interface MyConsole with
        member _.ReadLine() = "Rux"
        member _.Write s = printf $"Write: %s{s}"
        
    static member Default = Unchecked.defaultof<TestEnv>

env_sample.run(TestEnv.Default).get()