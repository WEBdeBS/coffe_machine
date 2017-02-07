const THC = ['T', 'H', 'C']


module.exports = {
  make: function(order){
    if (THC.indexOf(order) > -1) {
      return {type: order}
    }
    return null
  }
}