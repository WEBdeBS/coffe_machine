module CoffeeMachine.DrinkRepository

open DrinkMaker.Data

open System
open MongoDB.Bson
open MongoDB.Driver


type BeverageReportDb = {
    Id: BsonObjectId
    Beverage: BeverageType
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
let client = MongoClient connectionString
let db = client.GetDatabase("Test")

NamelessInteractive.FSharp.MongoDB.SerializationProviderModule.Register()
NamelessInteractive.FSharp.MongoDB.Conventions.ConventionsModule.Register()


let save' (db: IMongoDatabase) (drink: Beverage) =
  let id = BsonObjectId(ObjectId.GenerateNewId())
  let collection = db.GetCollection<BeverageReportDb>("drinks")
  let record = {Id = id; Beverage = drink.Beverage; Price = drink.MoneyInserted}
  collection.InsertOne(record)
  drink

let loadAll' (db: IMongoDatabase) =
    let collection = db.GetCollection<BeverageReportDb>("drinks")
    collection.Find(FilterDefinition.Empty).ToList()
    |> Seq.map (fun b -> {Price = b.Price; Beverage = b.Beverage})
    |> Seq.toArray

let mapDrik beverageReportDb =
    List.map

let drinkRepository = save' db, loadAll' db
