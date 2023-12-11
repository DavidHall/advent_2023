open System.IO
open System.Text.RegularExpressions


let replace (day: string, num: string) = fun (x) -> Regex.Replace(x, day, num) 

let replaceOne = replace("one", "one1one")
let replaceTwo = replace("two", "two2two")
let replaceThree = replace("three", "three3three")
let replaceFour = replace("four", "four4four")
let replaceFive = replace("five", "five5five")
let replaceSix = replace("six", "six6six")
let replaceSeven = replace("seven", "seven7seven")
let replaceEight = replace("eight", "eight8eight")
let replaceNine = replace("nine", "nine9nine")

let replaceNumbers target = replaceOne(target) |> replaceTwo |> replaceThree |> replaceFour |> replaceFive |> replaceSix |> replaceSeven |> replaceEight |> replaceNine

let filterWord wordToFilter = 
    Regex.Replace(wordToFilter, "[^0-9]", "");

let path = "./input.txt"
// For more information see https://aka.ms/fsharp-console-apps
let lines = File.ReadAllLines(path)                

let inline charToString c: string = c.ToString() 

let filteredLines = lines |> Seq.map(fun x -> replaceNumbers(x)) |>  Seq.map(fun x -> filterWord (x))

filteredLines |> Seq.iter(fun l -> printfn "%s" l)

let firstChars = filteredLines |> Seq.map(fun x -> x |> Seq.head) |> Seq.map(charToString)
let lastChars = filteredLines |> Seq.map(fun x -> x |> Seq.last) |> Seq.map(charToString)

//let firstAndLast = filteredLines |> Seq.map(fun x -> x.Chars(0) + x.Chars(1))

let vecadd = Seq.map2 (+)

let sums = vecadd firstChars lastChars

let tmp = sums |> Seq.map(int)

let result = tmp |> Seq.sum




printfn "%d" result

    
