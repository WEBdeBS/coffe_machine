const priceList = require('./priceList')
const BeverageQuantityChecker = require('./BeverageQuantityChecker')
const EmailNotifier = require('./EmailNotifier')

let amount = 0;
let beverages = {}

function beverageDecorator(beverage, beverageType) {

  if (!beverageType[1]) {
    return beverage
  }

  if (beverageType[1] !== 'h') {
    throw new Error('variant is not defined')
  }

  if (beverageType[0] === 'O') {
    throw new Error('orange can not be hot')
  }

  beverage.isExtraHot = true

  return beverage
}

module.exports = {

  report: function () {
    return {
      total: amount,
      beverages
    };
  },

  reset: function () {
    amount = 0;
    beverages = {}
  },

  make: function (order) {
    [beverageType, sugar, money] = order.split(':')

    const beverage = beverageDecorator(priceList(beverageType[0]), beverageType)

    const value = beverage.price - parseFloat(money)

    if (isNaN(value) || value > 0) {
      return {
        message: `${value.toFixed(2)}$ missing`
      }
    }
    amount += beverage.price
    beverages[beverageType[0]] = (beverages[beverageType[0]] || 0) +1;
    if (sugar) {
      beverage.sugar = parseInt(sugar)
      beverage.stick = beverage.sugar > 0
    }

    if (BeverageQuantityChecker.isEmpty(beverageType[0])){
      EmailNotifier.notifyMissingDrink(beverageType[0])
      throw Error('')
    }

    return beverage
  }
}