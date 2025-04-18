# frozen_string_literal: true

require 'rspec'

require_relative '../lib/dnd/weapon'
require_relative '../lib/dnd/exceptions'

RSpec.describe Weapon do
  describe '#validate' do
    let(:weapon) do
      Weapon.new('Excalibur') do
        desc 'Legendary sword of King Arthur'
        type :sword
        stat(:strength, 50)
      end
    end

    it 'does not raise an error if all attributes are valid' do
      expect { weapon.validate }.not_to raise_error
    end

    it 'raises an error if name is nil' do
      weapon.instance_variable_set(:@name, nil)
      expect { weapon.validate }.to raise_error(Exceptions::NotNullableAttributeError, 'name cannot be nil')
    end

    it 'raises an error if description is nil' do
      weapon.instance_variable_set(:@description, nil)
      expect { weapon.validate }.to raise_error(Exceptions::NotNullableAttributeError, 'description cannot be nil')
    end

    it 'raises an error if type is nil' do
      weapon.instance_variable_set(:@type, nil)
      expect { weapon.validate }.to raise_error(Exceptions::NotNullableAttributeError, 'type cannot be nil')
    end

    it 'raises an error if stat is nil' do
      weapon.instance_variable_set(:@stat, nil)
      expect { weapon.validate }.to raise_error(Exceptions::NotNullableAttributeError, 'stat cannot be nil')
    end

    it 'raises an error if type is invalid' do
      weapon.type(:bow)
      expect { weapon.validate }.to raise_error(Exceptions::ArbitraryTypeError, 'bow is not a valid weapon type. Valid types: [:sword, :daggers, :staff]')
    end

    it 'validates the stat object' do
      stat_mock = double('Stat')
      expect(stat_mock).to receive(:validate)
      weapon.instance_variable_set(:@stat, stat_mock)
      expect { weapon.validate }.not_to raise_error
    end
  end
end
