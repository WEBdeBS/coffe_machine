module DrinkMaker.Data

type BeverageType = InvalidOrder | Tea | Coffee | Chocolate |  Orange

type Beverage = {
    Beverage: BeverageType
    ExtraHot: bool
    Sugar: int
    Stick: bool
}

type Drink =
  | Message of string
  | Drink of Beverage option
