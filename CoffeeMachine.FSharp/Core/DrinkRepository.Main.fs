module CoffeeMachine.DrinkRepository.Main

open CoffeeMachine.DrinkRepository.Core

open System.Configuration
open MongoDB.Bson
open MongoDB.Driver


let connectionString =  ConfigurationManager.AppSettings.Item("ConnectionString")

let dbName = ConfigurationManager.AppSettings.Item("DbName")

NamelessInteractive.FSharp.MongoDB.SerializationProviderModule.Register()
NamelessInteractive.FSharp.MongoDB.Conventions.ConventionsModule.Register()

let db = db'' connectionString dbName

let drinkRepository = save' db, loadAll' db