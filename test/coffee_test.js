const assert = require('chai').assert
const Coffee = require('../app')

describe('Coffe machine', function(){
  it('should pass', function(){
    assert.isTrue(true)
  })

  it ('should make a tea',function(){
    const tea = Coffee.make('T')
    assert.isNotNull(tea)
    assert.equal('T', tea.type)
  })

  it ('should make a coffee',function(){
    const c = Coffee.make('C')
    assert.isNotNull(c)
    assert.equal('C', c.type)
  })

  it ('should make a chocolate',function(){
    const c = Coffee.make('H')
    assert.isNotNull(c)
    assert.equal('H', c.type)
  })
})