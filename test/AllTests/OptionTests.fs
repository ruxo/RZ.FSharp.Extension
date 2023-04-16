module Tests

open Xunit
open RZ.FSharp.Extension
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
    
[<Fact>]
let ``Return some value directly`` () =
    let v = Some 123
    
    let r = option {
        return! v
    }
    Assert.Equal(v, r)
    
[<Fact>]
let ``ValueOption is assigned as Option`` () =
    let v = ValueSome 123
    let z: int option = v.toOption()
    Assert.Equal(z.Value, v.Value)