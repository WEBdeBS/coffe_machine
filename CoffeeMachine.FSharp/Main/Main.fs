module Main

open CoffeeMachine.Main
open DrinkMaker.Data

[<EntryPoint>]
let main argv =
    if usage argv
    then argv.[0]
        |> make
        |> function
        | Beverage beverage -> printfn "Here's your drink:\n\n %A\n\n" beverage
        | Message m -> printfn "%s" m        
    else ()
    0
