module Main

open CoffeeMachine.Main
open DrinkMaker.Data

[<EntryPoint>]
let main argv =
    if usage argv
    then argv.[0]
         |> make
         |> function
         | Some beverage -> printfn "Here's your drink:\n\n %A\n\n" beverage
         | None -> ()
    0
