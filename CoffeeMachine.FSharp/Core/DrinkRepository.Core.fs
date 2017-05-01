module internal CoffeeMachine.DrinkRepository.Core

open DrinkMaker.Data
open CoffeeMachine.DrinkRepository.Data

open System
open MongoDB.Bson
open MongoDB.Driver

let db'' (connectionString: string) (dbName: string) =
  let client = MongoClient connectionString
  client.GetDatabase(dbName)

let save' (db: IMongoDatabase) (drink: Beverage) =
  let id = BsonObjectId(ObjectId.GenerateNewId())
  let collection = db.GetCollection<BeverageReportDb>("drinks")
  let record = {
    Id = id;
    Beverage = drink.Beverage;
    Price = match drink.ListPrice with
            | Some price -> price
            | _  -> failwith "Trying to save an incomplete record!"
  }
  collection.InsertOne(record)
  drink

let loadAll' (db: IMongoDatabase) () =
    let collection = db.GetCollection<BeverageReportDb>("drinks")
    collection.Find(FilterDefinition.Empty).ToList()
    |> Seq.map (fun b -> {Price = b.Price; Beverage = b.Beverage})
    |> Seq.toArray

let drinkRepository'' connectionString dbName =
  save' (db'' connectionString dbName), loadAll' (db'' connectionString dbName) 
