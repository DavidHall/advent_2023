open System.IO
open System.Text.RegularExpressions

let filterWord wordToFilter = 
    Regex.Replace(wordToFilter, "[^0-9]", "");

let path = "./input.txt"
// For more information see https://aka.ms/fsharp-console-apps
let lines = File.ReadAllLines(path)                

let inline charToInt c = int32 c - int32 '0'

let inline charToString c: string = c.ToString() 

let filteredLines = lines |> Seq.map(fun x -> filterWord (x))


let firstChars = filteredLines |> Seq.map(fun x -> x |> Seq.head) |> Seq.map(charToString)
let lastChars = filteredLines |> Seq.map(fun x -> x |> Seq.last) |> Seq.map(charToString)

//let firstAndLast = filteredLines |> Seq.map(fun x -> x.Chars(0) + x.Chars(1))

let vecadd = Seq.map2 (+)

let sums = vecadd firstChars lastChars

let tmp = sums |> Seq.map(int)

let result = tmp |> Seq.sum

//let result = Seq.sum(tmp)

    // To check
//firstChars |> Seq.iter(fun x -> printfn  "%s" x)
//lastChars |> Seq.iter(fun x -> printfn  "%s" x)
//tmp |> Seq.iter(fun x -> printfn "%d" x)
printfn "%d" result

    
