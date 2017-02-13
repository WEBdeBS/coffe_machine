module.exports = function (beverageType){
  const priceList = { T : 0.4, H : 0.5, C : 0.6, O : 0.8 }
  
  if (priceList[beverageType]){
    return {type: beverageType, price: priceList[beverageType]}
  }
  return undefined
}



// class Coffee extends BaseBeverage{
//   costructor(){
//     this.price = 0.6
//   }
// }

// function Tea(){
//   this.price = 0.4
// }