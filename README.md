# RZ F# Extension

Provide utility functions, especially common functions for `Option` and `Result` types.

## Prelude

```f#
open RZ.FSharp.Extensions.Prelude
```

| Function            | Signature                                             | Description                       |
|---------------------|-------------------------------------------------------|-----------------------------------|
| `sideEffect`        | `(a → unit) → a → a`                                  | Execute a side effect on a value. |
| `flip`              | `(a → b) → (b → a)`                                   | Flip the arguments of a function. |
| `constant`          | `a → (_ → a)`                                         | Return a constant function.       |
| `createComparer<T>` | `(T → T → bool) → (T -> int) -> IEqualityComparer<T>` | Create a comparer                 |
| `Async.return'`     | `a -> Async<a>`                                       | Return an async constant value.   |

### Types Aliases

| Alias              | Underlying Type      |
|--------------------|----------------------|
| `OptionAsync<T>`   | `Async<T option>`    |
| `ResultAsync<T,E>` | `Async<Result<T,E>>` |

## KeyValuePair

```f#
open RZ.FSharp.Extensions.KeyValuePair
```

| Function  | Signature                    | Description                           |
|-----------|------------------------------|---------------------------------------|
| `key`     | `KeyValuePair<a, b> → a`     | Get the key of a `KeyValuePair`.      |
| `value`   | `KeyValuePair<a, b> → b`     | Get the value of a `KeyValuePair`.    |
| `ofTuple` | `a * b → KeyValuePair<a, b>` | Create a `KeyValuePair` from a tuple. |
| `toTuple` | `KeyValuePair<a, b> → a * b` | Create a tuple from a `KeyValuePair`. |

## Seq

```f#
open RZ.FSharp.Extensions.Seq
```
| Function        | Signature                              | Description                      |
|-----------------|----------------------------------------|----------------------------------|
| `fromIterator`  | `IEnumerator<a> → a seq`               | Create a `Seq` from an iterator. |
| `single`        | `a seq → a option`                     | Create a single.                 |
| `tryFold`       | `(a → a → a) → a seq → a option`       | Try to fold a sequence itself.   |
| `tryMin`        | `a seq → a option`                     | Try to find the minimum value.   |
| `tryMn`         | `a seq → a option`                     | Try to find the maximum value.   |
| `Iterator.fold` | `(a → b → a) → a → IEnumerator<a> → a` | Fold an iterator.                |

## Map

```f#
open RZ.FSharp.Extensions.Map
```

| Function | Signature                    | Description                       |
|----------|------------------------------|-----------------------------------|
| `values` | `Map<a, b> → b seq`          | Get the values of a `Map`.        |
| `byKey`  | `(a → k) → a seq → Map<k,a>` | Get the values of a `Map` by key. |

## Option

```f#
open RZ.FSharp.Extensions.Option
```

| Function        | Signature                              | Description                                    |
|-----------------|----------------------------------------|------------------------------------------------|
| `ap`            | `a option → (a → b) option → b option` | Apply an option value to a function.           |
| `call`(2-6)     | `a → (a → b) option → b option`        | Call a function option with a parameter.       |
| `getOrFail`     | `(unit → string) → a option → a`       | Get an option value or fail with a message.    |
| `getOrRaise`    | `(unit → exn) → a option → a`          | Get an option value or raise an exception.     |
| `safeCall`(2-6) | `(a → b) → a → b option`               | Call a function and trap exceptions as `None`. |
| `then'`         | `(a → b) → (unit → b) → a option → b`  | Perform action for all possible branches.      |

### Simple option workflow

Consider

```f#
let myDiv x y = if y == 0 then None else Some (x / y)
let getInput() = Some 5
let input = getInput()
let result = input |> Option.bind (myDiv 100) |> Option.map ((+) 1) // (100 / 5) + 1
printfn $"%d{result}"
```

When it is written in `option` workflow:

```f#
let result = option {
    let! input = getInput()
    let! div = myDiv 100 input
    return (div + 1)
}
printfn $"%d{result}"
```

### Lifting to Async

### Lifting to Task

## Result

```f#
open RZ.FSharp.Extensions.Result
```


### Lifting to Async

### Lifting to Task