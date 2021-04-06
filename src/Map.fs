module RZ.FSharp.Extension.Map

let inline values m = m |> Map.toSeq |> Seq.map snd
let byKey (key: 'item -> 'key) = Seq.map (fun i -> key(i), i) >> Map.ofSeq
