module RZ.FSharp.Extension.Map

let byKey (key: 'item -> 'key) = Seq.map (fun i -> key(i), i) >> Map.ofSeq