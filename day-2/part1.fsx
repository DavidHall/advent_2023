#r "nuget: Unquote"
open Swensen.Unquote

let run () =
    printf "Testing..."
    test <@ 1 + 1 = 2 @>
    printfn "...done"

run ()