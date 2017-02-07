const THC = ['T', 'H', 'C']


module.exports = {
  make: function (order) {
    [beverageType, sugar, money] = order.split(':')
    const beverage = {}
    if (THC.indexOf(beverageType) == -1) {
      return null
    }

      const value = this.getPrice(beverageType) - parseFloat(money)
      if (isNaN(value) || value > 0) {
        return {
          message: `${value.toFixed(2)}$ missing`
        }
      }

    beverage.type = beverageType
    if (sugar) {
      beverage.sugar = parseInt(sugar)
      beverage.stick = beverage.sugar > 0
    }
    return beverage
  },
  getPrice: function (beverageType){
    const priceList = { T : 0.4, H : 0.5, C : 0.6 }
    return priceList[beverageType]
  }
}

