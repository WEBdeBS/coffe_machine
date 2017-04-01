module Main

open CoffeeMachine.Main

[<EntryPoint>]
let main argv =
    if Array.length argv <> 1
    then printfn "Usage is CoffeeMachine report|<order>"
         -1
    else argv.[0]
         |> make
         |> function
         | Some beverage -> printfn "Here's your drink:\n\n %A\n" beverage
         | None -> printfn "\nCouldn't make any drink. Look at the display"
         0
