const assert = require('chai').assert
const proxyquire = require('proxyquire')
const checkerStub = {}
const notifierStub = {}

const Coffee = proxyquire('../app', {
  './BeverageQuantityChecker': checkerStub,
  './EmailNotifier': notifierStub
})


describe('Coffe machine', function () {
  it('should notify when is empty', function (done) {
    checkerStub.isEmpty = () => true
    let isCalled = false
    notifierStub.notifyMissingDrink = () => {
      isCalled = true
      done()
    }
    assert.throws(() => Coffee.make('T:0:0.5'))
    assert.isTrue(isCalled)
  })

  it('should not notify when it isn\'t empty', function () {
    checkerStub.isEmpty = () => false
    let isCalled = false
    notifierStub.notifyMissingDrink = () => {
      let isCalled = true
    }
    Coffee.make('T:0:0.5')
    assert.isFalse(isCalled)
  })

  it('should not make the drink when is empty', function () {
    checkerStub.isEmpty = () => true
    assert.throws(() => Coffee.make('T:0:0.5'))

  })


})



