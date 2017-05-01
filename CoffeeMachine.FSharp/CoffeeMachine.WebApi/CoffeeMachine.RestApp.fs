module CoffeeMachine.RestApp

open Suave
open Suave.Operators
open Suave.Web
open Suave.Http
open Suave.Filters
open Suave.Writers
open Suave.RequestErrors
open Suave.Successful

open Newtonsoft.Json
open Newtonsoft.Json.Serialization

open CoffeeMachine.Main
open DrinkMaker.Data


let badRequest order =
  order
  |> sprintf "order %s not valid"
  |> BAD_REQUEST


let toJson v =
  let jsonSerializerSettings = new JsonSerializerSettings()
  jsonSerializerSettings.ContractResolver <- new CamelCasePropertyNamesContractResolver()
  JsonConvert.SerializeObject(v, jsonSerializerSettings)

let setCORSHeaders =
    setHeader  "Access-Control-Allow-Origin" "*"
    >=> setHeader "Access-Control-Allow-Headers" "content-type"

let allowCors : WebPart =
    choose [
        OPTIONS >=>
            fun context ->
                context |> (
                    setCORSHeaders
                    >=> OK "CORS approved" )
    ]

let JSON v =
  v
  |> toJson
  |> OK
  >=> Writers.setMimeType "application/json; charset=utf-8"
  >=> setCORSHeaders



let makeDrink order =
  order
  |> make
  |> function
  | Beverage beverage -> beverage |> JSON
  | Message m -> m |> JSON

let restMachine  =
  choose
    [
      allowCors
      GET >=> choose
        [
          pathScan "/order/%s" makeDrink
          NOT_FOUND "Invalid route"
        ]
      POST >=> pathScan "/order/%s" (makeDrink)
    ]
