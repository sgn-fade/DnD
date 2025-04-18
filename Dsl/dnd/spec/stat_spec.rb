require 'rspec'

require_relative '../lib/dnd/stat'
require_relative '../lib/dnd/exceptions'

RSpec.describe Stat do
  describe '#validate' do
    it 'does not raise an error if all attributes are valid' do
      stat = Stat.new(:strength, 10)
      expect { stat.validate }.not_to raise_error
    end

    it 'raises an error if type is nil' do
      stat = Stat.new(nil, 10)
      expect { stat.validate }.to raise_error(Exceptions::NotNullableAttributeError, 'type cannot be nil')
    end

    it 'raises an error if value is nil' do
      stat = Stat.new(:strength, nil)
      expect { stat.validate }.to raise_error(Exceptions::NotNullableAttributeError, 'value cannot be nil')
    end

    it 'raises an error if stat_type is not a valid type' do
      stat = Stat.new(:speed, 10)
      expect { stat.validate }.to raise_error(Exceptions::ArbitraryTypeError, 'speed is not a valid stat type. Valid types: [:strength, :dexterity, :constitution, :intelligence]')
    end
  end
end