module RZ.FSharp.Extension.KeyValuePair

open System.Collections.Generic

let inline key (kv: KeyValuePair<_,_>) = kv.Key
let inline value (kv: KeyValuePair<_,_>) = kv.Value
let inline ofTuple (x, y) = KeyValuePair.Create(x, y)
let inline toTuple (kv: KeyValuePair<_,_>) = kv.Key, kv.Value
