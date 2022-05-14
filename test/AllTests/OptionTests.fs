module Tests

open Xunit
open RZ.FSharp.Extension.Option

[<Fact>]
let ``Binding multiple options should return correct final value`` () =
    let f x y = x + y

    let r = option {
        let! x = Some 111
        let! y = Some 222
        return f x y
    }
    Assert.Equal(Some 333, r)
    
[<Fact>]
let ``Binding some None value should return None`` () =
    let f x y z = x + y + z

    let r = option {
        let! x = Some 111
        let! y = Some 222
        let! z = None
        return f x y z
    }
    Assert.Equal(None, r)