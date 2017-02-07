module.exports = {
  make: function(order){
    if (order === 'T') {
      return {type: 'T'}
    }
    if (order === 'C') {
      return {type: 'C'}
    }
    if (order === 'H') {
      return {type: 'H'}
    }
  }
}