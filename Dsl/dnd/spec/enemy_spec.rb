require 'rspec'
require_relative '../lib/dnd/enemy'

RSpec.describe Enemy do
  describe '#validate' do
    let(:enemy) do
      Enemy.new('Goblin') do
        desc('Fierce enemy')
        type(:goblin)
        hp(100)
        damage(10)
      end
    end

    it 'raises an error for missing name' do
      enemy.instance_variable_set(:@name, nil)

      expect { enemy.validate }.to raise_error(Exceptions::NotNullableAttributeError, 'name cannot be nil')
    end

    it 'raises an error for missing description' do
      enemy.instance_variable_set(:@description, nil)

      expect { enemy.validate }.to raise_error(Exceptions::NotNullableAttributeError, 'description cannot be nil')
    end

    it 'raises an error for missing type' do
      enemy.instance_variable_set(:@type, nil)

      expect { enemy.validate }.to raise_error(Exceptions::NotNullableAttributeError, 'type cannot be nil')
    end

    it 'raises an error for missing hp' do
      enemy.instance_variable_set(:@hp, nil)

      expect { enemy.validate }.to raise_error(Exceptions::NotNullableAttributeError, 'hp cannot be nil')
    end

    it 'raises an error for missing damage' do
      enemy.instance_variable_set(:@damage, nil)

      expect { enemy.validate }.to raise_error(Exceptions::NotNullableAttributeError, 'damage cannot be nil')
    end

    it 'raises an error if type is not valid' do
      enemy.instance_variable_set(:@type, :unknown_type)

      expect { enemy.validate }.to raise_error(Exceptions::ArbitraryTypeError, 'unknown_type is not a valid enemy type. Valid types: [:goblin, :skeleton, :mimic, :fiend, :dragon]')
    end

    it 'does not raise any error when all attributes are valid' do
      expect { enemy.validate }.not_to raise_error
    end

  end
end
