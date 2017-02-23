module CoffeeMachine.DrinkRepository

open DrinkMaker.Data
open System
open MongoDB.Bson
open MongoDB.Driver
open MongoDB.FSharp

type BeverageReportDb = {
    Id: BsonObjectId;
    Beverage: string
    Price: float
}


type BeverageReport = {
    Beverage: BeverageType
    Price: float
}

type Pippo = {
    Pluto: string
}


let connectionString = "mongodb://localhost"
let client = new MongoClient(connectionString)
let db = client.GetDatabase("Test")

let save' (db: IMongoDatabase) (drink: Beverage) =
  let id = BsonObjectId(ObjectId.GenerateNewId())
  let collection = db.GetCollection<BeverageReportDb>("drinks")
  let beverage = match drink.Beverage with
                 | Coffee -> "Coffee"
                 | Tea -> "Tea"
                 | Orange -> "Orange"
                 | Chocolate -> "Chocolate"
                 | InvalidOrder -> failwith "Cannot save an invalid order"
  let record = {Id = id; Beverage = beverage; Price = drink.Price}
  collection.InsertOne(record)
  drink

//For some reason, the discrimated union is not saved :-\
let deserializeBeverage s =
    match s with
    | "Coffee" -> Coffee
    | "Tea" -> Tea
    | "Orange" -> Orange
    | "Chocolate" -> Chocolate
    | _ -> failwith "Invalid beverage type"

let loadAll' (db: IMongoDatabase) =
    let collection = db.GetCollection<BeverageReportDb>("drinks")
    collection.Find(FilterDefinition.Empty).ToList()
    |> Seq.map (fun b -> {Price = b.Price; Beverage = b.Beverage |> deserializeBeverage})
    |> Seq.toArray    

let mapDrik beverageReportDb =
    List.map

let drinkRepository = save' db, loadAll' db
