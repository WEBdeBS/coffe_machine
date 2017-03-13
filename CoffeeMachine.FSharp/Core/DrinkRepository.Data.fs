module CoffeeMachine.DrinkRepository.Data

open DrinkMaker.Data
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
