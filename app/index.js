const THC = ['T', 'H', 'C']


module.exports = {
  make: function(order){
    [beverageType, sugar] = order.split(':')
    const beverage = {}
    if (THC.indexOf(beverageType) == -1) {
      return null
    }
    beverage.type = beverageType
    
    if (sugar){
      beverage.sugar = parseInt(sugar)
    }
    return beverage
  }
}