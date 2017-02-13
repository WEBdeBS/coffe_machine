const priceList = require('./priceList')



module.exports = {
  make: function (order) {
    [beverageType, sugar, money] = order.split(':')
    const beverage = priceList(beverageType)
    if (beverage === undefined) {
      return null
    }

    const value = beverage.price - parseFloat(money)
    if (isNaN(value) || value > 0) {
      return {
        message: `${value.toFixed(2)}$ missing`
      }
    }

    if (sugar) {
      beverage.sugar = parseInt(sugar)
      beverage.stick = beverage.sugar > 0
    }
    return beverage
  }
}