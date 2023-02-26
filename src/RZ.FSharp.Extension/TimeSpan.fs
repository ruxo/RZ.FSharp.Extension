module RZ.FSharp.Extension.TimeSpan

open System.Runtime.CompilerServices

type private TS = System.TimeSpan

[<IsReadOnly; Struct; NoComparison; NoEquality>]
type IntoFloat = IntoFloat with
    static member inline ($) (IntoFloat, x: float) = x
    static member inline ($) (IntoFloat, x: int) = float x
    
[<IsReadOnly; Struct; NoComparison>]
type TimeSpanUnit =
| Days
| Hours
| Minutes
| Seconds
| Milliseconds
| Microseconds
with
    member my.``of`` x =
        match my with
        | Days         -> TS.FromDays x
        | Hours        -> TS.FromHours x
        | Minutes      -> TS.FromMinutes x
        | Seconds      -> TS.FromSeconds x
        | Milliseconds -> TS.FromMilliseconds x
        | Microseconds -> TS.FromMicroseconds x
    static member inline (*) (n,t: TimeSpanUnit) = t.``of`` <| (IntoFloat $ n)