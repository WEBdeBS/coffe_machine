module Main
open CoffeeMachine.Main

[<EntryPoint>]
let main argv =
    if Array.length argv <> 1
    then printfn "Invalid Parameters: %A" argv
         -1
    else  argv.[0]
          |> make
          |> function
          | Some b -> printfn "Here's your drink:\n\n %A\n" b
          | None -> printfn "\nCouldn't make any drink. Look at the display"
          0 // return an integer exit code
