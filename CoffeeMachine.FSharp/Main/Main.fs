module Main

open System
open System.Configuration
open CoffeeMachine.Main
open DrinkMaker.Data

Convert.ToBoolean(ConfigurationManager.AppSettings.Item("Empty"))
|> CoffeeMachine.Maker.setEmptyConfiguration 
|> ignore



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
