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

open FSharp.Reflection

open CoffeeMachine.Main
open DrinkMaker.Data


let toString (x:'a) =
    match FSharpValue.GetUnionFields(x, typeof<'a>) with
    | case, _ -> case.Name

type DrinkDto = {
  Beverage: string
  ExtraHot: bool
  Sugar: int
  Stick: bool
  MoneyInserted: float
  ListPrice: float
}

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

//let report () =
//  printfn "Printing receipt..."
//  printReceipt ()
//


let makeDrink order =
  order
  |> make

let beverageToDto (b:Beverage) =
  {
    Beverage = toString b.Beverage
    ExtraHot = b.ExtraHot
    Sugar = b.Sugar
    MoneyInserted = b.MoneyInserted
    ListPrice = if Option.isSome b.ListPrice then b.ListPrice.Value else failwith "Invalid drink!"
    Stick = if Option.isSome b.Stick then b.Stick.Value else failwith "Invalid Drink!"
  }

let toDto drink =
  match drink with
  | Beverage b -> b |> beverageToDto |> JSON
  | Message m -> m |> JSON



let restMachine  =
  choose
    [
      allowCors
      GET >=> choose
        [
          path "/report" >=> request (fun r -> printReceipt () |> JSON)
          NOT_FOUND "Invalid route"
        ]
      POST >=> pathScan "/order/%s" (makeDrink >> toDto)
    ]
