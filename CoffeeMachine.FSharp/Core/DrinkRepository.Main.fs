module CoffeeMachine.DrinkRepository.Main

open CoffeeMachine.DrinkRepository.Core

open MongoDB.Bson
open MongoDB.Driver


let connectionString = "mongodb://localhost"
let client = MongoClient connectionString
let db = client.GetDatabase("Test")

NamelessInteractive.FSharp.MongoDB.SerializationProviderModule.Register()
NamelessInteractive.FSharp.MongoDB.Conventions.ConventionsModule.Register()



let drinkRepository = save' db, loadAll' db