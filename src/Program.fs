// Learn more about F# at http://fsharp.org

open System
open Hello.World

[<EntryPoint>]
let main argv =
    Say.hello "World from F#!"
    0 // return an integer exit code
