module IOWithEnv.Random

open System

type RandomEff =
    abstract member Next: int -> int
    
type HasRandom =
    abstract member RandomEff: RandomEff
    
[<NoComparison; NoEquality>]
type RealRandom() =
    let r = Random()
    
    static member Default = RealRandom()
    
    interface RandomEff with
        member _.Next max = r.Next max
        
let inline random_next max = fun (rt: #HasRandom) -> Ok(rt.RandomEff.Next max)