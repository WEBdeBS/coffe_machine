#r "../packages/Suave/lib/net40/Suave.dll"
#r "../build/Core.dll"

open Suave                 // always open suave
open Suave.Successful      // for OK-result
open Suave.Web             // for config
open CoffeeMachine.Main
open System
open System.Net

let ipZero = IPAddress.Parse("0.0.0.0")
let port = System.Environment.GetEnvironmentVariable("PORT")

let cfg =
        { defaultConfig with
            bindings =
                 [ HttpBinding.create HTTP ipZero (uint16 port) ]
               }

//printfn "%A" (make "Hh:1:0.9")

startWebServer cfg (OK "Hello World!")
