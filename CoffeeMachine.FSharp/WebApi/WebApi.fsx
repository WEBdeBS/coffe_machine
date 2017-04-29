#r "../packages/Suave/lib/net40/Suave.dll"
#r "../build/Core.dll"

open Suave                 // always open suave
open Suave.Successful      // for OK-result
open Suave.Web             // for config
open CoffeeMachine.Main

startWebServer defaultConfig (OK "Hello World!")
