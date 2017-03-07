const assert = require('chai').assert
const Coffee = require('../app')

describe('Coffe report', function () {
  //how many of each drink was sold and the total amount of money earned so far.\

  beforeEach(function () {
    Coffee.reset();
  })

  it('should have total amount of money', function () {
    assert.equal(Coffee.report().total, 0)
  })

  it('total should equal x', function () {
    Coffee.make('T:0:0.5')
    assert.equal(Coffee.report().total, 0.4)
  })

  it('total should equal x', function () {
    Coffee.make('C:0:0.7')
    assert.equal(Coffee.report().total, 0.6)
  })

  it('total should equal x', function () {
    Coffee.make('C:0:0.7')
    Coffee.make('T:0:0.7')
    assert.equal(Coffee.report().total, 1)
  })

  it('beverages.C should equal three', function () {
    Coffee.make('C:0:0.7')
    Coffee.make('C:0:0.7')
    Coffee.make('C:0:0.7')
    assert.equal(Coffee.report().beverages.C, 3)
  })

  it('beverages ....', function () {
    Coffee.make('C:0:0.7')
    Coffee.make('T:0:0.7')
    Coffee.make('C:0:0.7')
    Coffee.make('T:0:0.7')
    Coffee.make('H:0:0.7')
    Coffee.make('H:0:0.7')
    Coffee.make('C:0:0.7')
    assert.equal(Coffee.report().beverages.C, 3)
    assert.equal(Coffee.report().beverages.T, 2)
    assert.equal(Coffee.report().beverages.H, 2)
  })

  it('beverages.T should equal one', function () {
    Coffee.make('T:0:0.7')
    assert.equal(Coffee.report().beverages.T, 1)
  })




})