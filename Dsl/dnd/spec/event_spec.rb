require 'rspec'
require_relative '../lib/dnd/event'
require_relative '../lib/dnd/action'
require_relative '../lib/dnd/exceptions'

RSpec.describe Event do
  let(:valid_event) do
    Event.new("Test Event") do
      desc "A description of the event."
      action do
        desc "Action 1"
        require_to_have :strength, 5
        in_case_of_success { transfer_to_event(:some_event) }
        in_case_of_failure { die }
      end
      action do
        desc "Action 2"
        in_case_of_success { transfer_to_location(:some_location) }
        in_case_of_failure { transfer_to_event(:some_encounter) }
      end
    end
  end

  let(:invalid_event) do
    Event.new("Invalid Event") do
    end
  end

  describe "#validate" do
    it "does not raise an error for a valid event" do
      expect { valid_event.validate }.not_to raise_error
    end

    it "raises an error if name is nil" do
      invalid_event.instance_variable_set("@name", nil)
      expect { invalid_event.validate }.to raise_error(Exceptions::NotNullableAttributeError, "name cannot be nil")
    end

    it "raises an error if description is nil" do
      invalid_event.instance_variable_set("@description", nil)
      expect { invalid_event.validate }.to raise_error(Exceptions::NotNullableAttributeError, "description cannot be nil")
    end

    it "raises an error if there are fewer than 2 actions" do
      invalid_event.instance_variable_set("@description", "Test description")
      expect { invalid_event.validate }.to raise_error(Exceptions::LimitViolationError, "Number of actions must be between 1 and 3 inclusive")
    end
  end

  describe "#get_references" do
    it "returns all references of the specified type" do
      references_to_event = valid_event.send(:get_references, :next_event)
      expect(references_to_event).to contain_exactly(:some_event, :some_encounter)

      references_to_location = valid_event.send(:get_references, :next_location)
      expect(references_to_location).to contain_exactly(:some_location)
    end

    it "returns an empty array if no references of the specified type exist" do
      references_to_fight = valid_event.send(:get_references, :fight)
      expect(references_to_fight).to eq([])
    end
  end
end