namespace RZ.FSharp.Extension

open System.Runtime.CompilerServices

[<Extension>]
type ResultExtension =
    [<Extension>] static member inline isOk(x: Result<'a,'err>) :bool = Result.isOk x
    [<Extension>] static member inline isError(x: Result<'a,'err>) :bool = Result.isError x
    
    [<Extension>] static member inline map(x: Result<'a,'err>, f: 'a -> 'b) :Result<'b,'err> = Result.map f x
    [<Extension>] static member inline bind(x: Result<'a,'err>, f: 'a -> Result<'b,'err>) :Result<'b,'err> = Result.bind f x
    
    [<Extension>] static member inline unwrap(x: Result<'a,'err>) :'a =
                    match x with
                    | Ok v -> v
                    | Error e -> raise <| UnwrapError.from($"Unwrap Error value of Result<{typeof<'a>.Name},{typeof<'err>.Name}>", e)
                    
    [<Extension>] static member inline get_err(x: Result<'a,'err>) :'err =
                    match x with
                    | Ok v -> raise <| UnwrapError.from($"Unwrap Ok value of Result<{typeof<'a>.Name},{typeof<'err>.Name}>", v)
                    | Error e -> e