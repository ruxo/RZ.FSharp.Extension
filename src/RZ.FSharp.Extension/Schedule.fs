namespace RZ.FSharp.Extension

open System
open System.Runtime.CompilerServices

[<IsReadOnly; Struct; NoComparison; NoEquality>]
type Schedule = Schedule of TimeSpan seq with
    static member (||||) (Schedule a , Schedule b) = Schedule <| seq {
        use aIter = a.GetEnumerator()
        use bIter = b.GetEnumerator()
        
        let mutable hasA = aIter.MoveNext()
        let mutable hasB = bIter.MoveNext()
        
        while hasA || hasB do
            match hasA, hasB with
            | true, true -> min aIter.Current bIter.Current
            | true, false -> aIter.Current
            | false, true -> bIter.Current
            | false, false -> failwith "Just impossible"
            
            hasA <- aIter.MoveNext()
            hasB <- bIter.MoveNext()
    }
            
    static member (||||) (Schedule a, ScheduleTransformer b) = Schedule(b a)
and
    [<IsReadOnly; Struct; NoComparison; NoEquality>]
    ScheduleTransformer = ScheduleTransformer of (TimeSpan seq -> TimeSpan seq) with
        static member (||||) (ScheduleTransformer a, Schedule b) = Schedule(a b)

module Schedule =
    let forever = Schedule <| Seq.initInfinite (fun _ -> TimeSpan.Zero)
    let recurs n = ScheduleTransformer(Seq.take n)
    let spaced space = Schedule <| Seq.initInfinite (fun _ -> space: TimeSpan)