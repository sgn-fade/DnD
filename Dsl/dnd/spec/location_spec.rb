# frozen_string_literal: true

RSpec.describe Location do
  let(:valid_location) do
    Location.new("Test Location") do
      type :castle_ruins
      tier 3
      desc "A mysterious set of ruins filled with danger and treasure."

      event :treasure_hunt do
        desc "A chance to find hidden treasure."
        action do
          desc "Search the ruins."
          in_case_of_success { transfer_to_event(:hidden_chamber) }
          in_case_of_failure { die }
        end
      end

      event :hidden_chamber do
        desc "A chance to find hidden treasure."
        action do
          desc "Search the chamber."
          in_case_of_success { transfer_to_event(:golem_encounter) }
          in_case_of_failure { die }
        end
      end

      enemy_encounter :golem_encounter do
        desc "Ancient guardians defending the ruins."
        encounter_one_of :stone_golem
        on_enemy_killed { die }
        on_battle_end { die }
      end
    end
  end

  describe "#validate" do
    it "does not raise an error for a valid location" do
      expect { valid_location.validate }.not_to raise_error
    end

    it "raises an error if name is nil" do
      valid_location.instance_variable_set("@name", nil)
      expect { valid_location.validate }.to raise_error(Exceptions::NotNullableAttributeError, "name cannot be nil")
    end

    it "raises an error if type is nil" do
      valid_location.instance_variable_set("@type", nil)
      expect { valid_location.validate }.to raise_error(Exceptions::NotNullableAttributeError, "type cannot be nil")
    end

    it "raises an error if tier is nil" do
      valid_location.instance_variable_set("@tier", nil)
      expect { valid_location.validate }.to raise_error(Exceptions::NotNullableAttributeError, "tier cannot be nil")
    end

    it "raises an error if description is nil" do
      valid_location.instance_variable_set("@description", nil)
      expect { valid_location.validate }.to raise_error(Exceptions::NotNullableAttributeError, "description cannot be nil")
    end

    it "raises an error if type is invalid" do
      valid_location.instance_variable_set("@type", :invalid_type)
      expect { valid_location.validate }.to raise_error(Exceptions::ArbitraryTypeError, "invalid_type is not a valid location type. Valid types: #{Location::TYPES}")
    end

    it "raises an error if there are no events or enemy encounters" do
      valid_location.instance_variable_get("@events").clear
      valid_location.instance_variable_get("@enemy_encounters").clear
      expect { valid_location.validate }.to raise_error(Exceptions::LimitViolationError, "Location must have at least one event or enemy encounter")
    end

    it "raises an error if an event references an invalid next_event" do
      first_event = valid_location.instance_variable_get("@events").first
      first_event.instance_variable_get("@actions").first.instance_variable_set("@positive_outcome",
                                                                                Outcome.new { transfer_to_event(:invalid_event) })
      expect { valid_location.validate }.to raise_error(Exceptions::InvalidReferenceError, "invalid_event is not a valid name")
    end

    it "raises an error if an enemy encounter references an invalid event" do
      valid_location.instance_variable_get("@enemy_encounters").first.instance_variable_set("@win_outcome",
                                                                                            Outcome.new { transfer_to_event(:invalid_event) })
      expect { valid_location.validate }.to raise_error(Exceptions::InvalidReferenceError, "invalid_event is not a valid name")
    end

    it "validates nested events and enemy encounters" do
      event = valid_location.instance_variable_get("@events").first
      expect(event).to receive(:validate)
      enemy_encounter = valid_location.instance_variable_get("@enemy_encounters").first
      expect(enemy_encounter).to receive(:validate)
      valid_location.validate
    end
  end
end