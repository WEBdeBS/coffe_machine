module CoffeeMachine.DrinkRepository.Main

open CoffeeMachine.DrinkRepository.Core

open MongoDB.Bson
open MongoDB.Driver


let connectionString = "mongodb://localhost"
let dbName = "Test"

NamelessInteractive.FSharp.MongoDB.SerializationProviderModule.Register()
NamelessInteractive.FSharp.MongoDB.Conventions.ConventionsModule.Register()

let db = db'' connectionString dbName

let drinkRepository = save' db, loadAll' db