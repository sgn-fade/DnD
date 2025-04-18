# frozen_string_literal: true
require 'rspec'
require_relative '../lib/dnd/action'
require_relative '../lib/dnd/exceptions'
require_relative '../lib/dnd/stat'

RSpec.describe Action do
  let(:valid_action) do
    Action.new do
      desc "Test action"
      require_to_have :strength, 10
      in_case_of_success { transfer_to_event(:victory) }
      in_case_of_failure { die }
    end
  end

  let(:invalid_action) do
    Action.new do
    end
  end

  describe "#validate" do
    it "does not raise an error for a valid action" do
      expect { valid_action.validate }.not_to raise_error
    end

    it "raises an error if description is missing" do
      invalid_action.instance_variable_set("@positive_outcome", Outcome.new { die })
      expect { invalid_action.validate }.to raise_error(Exceptions::NotNullableAttributeError, "description cannot be nil")
    end

    it "raises an error if positive outcome is missing" do
      invalid_action.instance_variable_set("@description", "Test")
      expect { invalid_action.validate }.to raise_error(Exceptions::NotNullableAttributeError, "positive_outcome cannot be nil")
    end
  end
end
