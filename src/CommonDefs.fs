namespace RZ.FSharp.Extension

type OptionAsync<'T> = Async<'T option>
type ResultAsync<'T,'E> = Async<Result<'T,'E>>

module Iterator =
  open System.Collections.Generic

  let fold reducer (init:'b) (itor:IEnumerator<'a>) =
    use _ = itor
    if itor.MoveNext() then
      let mutable v = init
      while itor.MoveNext() do
        v <- reducer v itor.Current
      v
    else
      init

module Async =
    let return' v = async { return v }