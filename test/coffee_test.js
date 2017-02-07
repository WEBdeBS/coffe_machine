const assert = require('chai').assert
const Coffee = require('../app')

describe('Coffe machine', function(){
  it('should pass', function(){
    assert.isTrue(true)
  })

  it ('should make a tea',function(){
    const tea = Coffee.make('T:0:0.4')
    assert.isNotNull(tea)
    assert.equal('T', tea.type)
  })

  it ('should make a coffee',function(){
    const c = Coffee.make('C:0:0.6')
    assert.isNotNull(c)
    assert.equal('C', c.type)
  })

  it ('should make a chocolate',function(){
    const c = Coffee.make('H:0:0.5')
    assert.isNotNull(c)
    assert.equal('H', c.type)
  })

  it ('beverage does not exists, should return null`',function(){
    const c = Coffee.make('K:0:0.4')
    assert.isNull(c)
  })

  it ('should make a coffee with 1 spoon of sugar',function(){
    const c = Coffee.make('C:1:0.6')
    assert.isNotNull(c)
    assert.equal('C', c.type)
    assert.equal(1, c.sugar)
  })

  it ('should make a tea with 2 spoons of sugar',function(){
    const tea = Coffee.make('T:2:0.4')
    assert.isNotNull(tea)
    assert.equal('T', tea.type)
    assert.equal(2, tea.sugar)
  })

  it ('should make a tea with 2 spoons of sugar add stick',function(){
    const tea = Coffee.make('T:2:0.4')
    assert.isNotNull(tea)
    assert.equal('T', tea.type)
    assert.equal(2, tea.sugar)
    assert.equal(true, tea.stick)
  })

  it ('should make a tea with 0 spoons of sugar without stick',function(){
    const tea = Coffee.make('T:0:0.4')
    assert.isNotNull(tea)
    assert.equal('T', tea.type)
    assert.equal(0, tea.sugar)
    assert.equal(false, tea.stick)
  })

  it ('should not make tea without enough money',function(){
    const message = Coffee.make('T:0:0.3')
    assert.isNotNull(message)
    assert.equal('0.10$ missing', message.message)
  })

  it ('should not make coffee without enough money',function(){
    const message = Coffee.make('C:0:0.5')
    assert.isNotNull(message)
    assert.equal('0.10$ missing', message.message)
  })

  it ('should make orange with O parameter',function(){
    const message = Coffee.make('O:0:0.8')
    assert.isNotNull(message)
    assert.equal('O', message.type)
  })
})