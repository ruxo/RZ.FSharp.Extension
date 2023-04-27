[<AutoOpen>]
module RZ.FSharp.Extension.Prelude

open System
open System.Collections.Generic

let inline sideEffect ([<InlineIfLambda>] f) x = (f x); x

let inline flip f a b = f b a
let inline constant x = fun _ -> x

let inline createComparer<'T> comparer hash_func =
    { new IEqualityComparer<'T> with
        member _.Equals(x,y) = comparer x y
        member _.GetHashCode x = hash_func x }

type OptionAsync<'T> = Async<'T option>
type VOptionAsync<'T> = Async<'T voption>
type ResultAsync<'T,'E> = Async<Result<'T,'E>>

/// Invalid state caused by incorrectly unlifting a monad
exception UnwrapError of exn

type UnwrapError with
    static member from(message, ?data: obj) =
        let e = exn message
        data |> Option.iter (fun d -> e.Data["value"] <- d)
        UnwrapError e

let inline tryParse ([<InlineIfLambda>] parser :string -> bool * 'a) (s :string) = let ok, v = parser s in if ok then ValueSome v else ValueNone

let parseBool   = tryParse Boolean.TryParse
let parseInt8   = tryParse SByte.TryParse
let parseInt16  = tryParse Int16.TryParse
let parseInt32  = tryParse Int32.TryParse
let parseInt64  = tryParse Int64.TryParse
let parseUInt8  = tryParse Byte  .TryParse
let parseUInt16 = tryParse UInt16.TryParse
let parseUInt32 = tryParse UInt32.TryParse
let parseUInt64 = tryParse UInt64.TryParse
let parseFloat  = tryParse Single.TryParse
let parseDouble = tryParse Double.TryParse
let parseDecimal  = tryParse Decimal.TryParse
let parseTimeSpan = tryParse TimeSpan.TryParse
let parseDateTime = tryParse DateTime.TryParse
let parseDateTimeOffset = tryParse DateTimeOffset.TryParse