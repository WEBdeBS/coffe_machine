module DrinkMaker.Data

type BeverageType = Tea | Coffee | Chocolate | InvalidOrder

type Beverage = {
    Beverage: BeverageType
    Sugar: int
    Stick: bool
}

type Drink =
  | Message of string
  | Drink of Beverage option
