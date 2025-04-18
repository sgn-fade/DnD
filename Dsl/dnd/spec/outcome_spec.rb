# frozen_string_literal: true

require 'rspec'

RSpec.describe Outcome do
  describe '#validate' do
    it 'raises an error if @body is nil and @type is not :death' do
      outcome = Outcome.new { transfer_to_event(nil) }

      expect { outcome.validate }.to raise_error(Exceptions::NotNullableAttributeError, 'body cannot be nil')
    end

    it 'does not raise any error is @body is not nil' do
      outcome = Outcome.new { transfer_to_event(:some_event) }

      expect { outcome.validate }.not_to raise_error
    end

    it 'does not raise an error even if @body is nil' do
      outcome = Outcome.new { die }

      expect { outcome.validate }.not_to raise_error
    end
  end
end