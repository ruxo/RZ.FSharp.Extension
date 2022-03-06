module RZ.FSharp.Extension.Prelude

let inline sideEffect ([<InlineIfLambda>] f) x = (f x); x

let inline flip f a b = f b a
let inline constant x = fun _ -> x