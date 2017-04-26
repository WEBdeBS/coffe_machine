module Main

open CoffeeMachine.Main
open DrinkMaker.Data
open Chessie
open Chessie.ErrorHandling

[<EntryPoint>]
let main argv =
    if usage argv
    then argv.[0]
         |> make
         |> function
         | Some beverage -> printfn "Here's your drink:\n\n %A\n\n" beverage
         | None -> ()
    
    // argv.[0]
    // |> makeComposed
    // |> function
    // | Ok (beverage,_) -> printfn "Here's your drink:\n\n %A\n\n" beverage
    // | _ -> ()
    0
