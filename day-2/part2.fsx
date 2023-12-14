#r "nuget: Unquote"
open Swensen.Unquote
open System.IO
open System.Text.RegularExpressions

let sample = "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red"
let sampleDraws = ": 8 green, 6 blue; 5 red, 7 blue"

type CubeColor = Green | Blue | Red | Unknown
type GameDraw = { color: CubeColor; count: int; }
type Game = { id: int; draws: GameDraw array array; }

let rx = Regex(@"Game ([0-9]*)(.*)", RegexOptions.Compiled)
let rxDraw = Regex(@"([0-9]*) (green|blue|red)*", RegexOptions.Compiled)

let matchColor (color: string) =
    match color with
    | "green" -> Green
    | "red" -> Red
    | "blue" -> Blue
    | _ -> Unknown

let parseDraws (draws: string) =
    let split = draws.Split [|';'|]

    let result = split |> Seq.map(fun (draw) -> 
        let m = rxDraw.Matches(draw)
        let filtered = m |> Seq.filter(fun (x) -> x.Value.Length <> 1)
        let d = filtered |> Seq.map(fun (x) ->
            { count= int x.Groups[1].Value; color= matchColor x.Groups[2].Value }
        )
        Seq.toArray d    
    ) 
    (result)



let parseGame gameText = 
    let m = rx.Match(gameText)
    let draws = parseDraws m.Groups[2].Value
    let result = { id = int m.Groups[1].Value; draws = Seq.toArray draws }
    result



// let isGameValid (game: Game, red: int, blue: int, green: int ) = 
//     let listOfDraws = game.draws |> Array.toList
    
//     let filtered = listOfDraws |> List.collect(fun (x) -> x |> Array.toList |> List.filter(
//         fun (a) -> a.color = CubeColor.Red && a.count > red || a.color = CubeColor.Blue && a.count > blue || a.color = CubeColor.Green && a.count > green
//         ))

//     filtered.IsEmpty

let path = "./input.txt"
// For more information see https://aka.ms/fsharp-console-apps
let lines = File.ReadAllLines(path)   

let games = lines |> Seq.map(fun (x) -> parseGame x)

let findMinCubes (game: Game) = 
    let red = game.draws |> Array.collect (fun (a) -> a |> Array.filter (fun (x) -> x.color = CubeColor.Red))
    let blue = game.draws |> Array.collect (fun (a) -> a |> Array.filter (fun (x) -> x.color = CubeColor.Blue))
    let green = game.draws |> Array.collect (fun (a) -> a |> Array.filter (fun (x) -> x.color = CubeColor.Green))
    
    let result = [ ( CubeColor.Red, red |> Array.maxBy(fun (x) -> x.count) ); (CubeColor.Blue, blue |> Array.maxBy(fun (x) -> x.count) ); ( CubeColor.Green, green |> Array.maxBy(fun (x) -> x.count) )]
    (result)

let countFromT (t: CubeColor* GameDraw) = 
    match t with 
    | (_, b) -> b.count

let result = games |> Seq.map findMinCubes |> Seq.map (fun (x) -> countFromT(x.[0]) * countFromT(x.[1]) * countFromT(x.[2])) |> Seq.sum

printfn "%A" result

let run () =
    printf "Testing..."
    test <@ 1 + 1 = 2 @>
    printfn "...done"

run ()
