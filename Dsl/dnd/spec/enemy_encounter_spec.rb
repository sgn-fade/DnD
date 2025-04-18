RSpec.describe EnemyEncounter do
  let(:valid_encounter) do
    EnemyEncounter.new("Test Encounter") do
      desc "A test encounter with an enemy"
      encounter_one_of [:goblin, :orc]
      on_enemy_killed { transfer_to_event(:victory_event) }
      on_battle_end { die }
    end
  end

  describe "#validate" do
    it "does not raise an error for a valid encounter" do
      expect { valid_encounter.validate }.not_to raise_error
    end

    it "raises an error if name is nil" do
      valid_encounter.instance_variable_set("@name", nil)
      expect { valid_encounter.validate }.to raise_error(Exceptions::NotNullableAttributeError, "name cannot be nil")
    end

    it "raises an error if description is nil" do
      valid_encounter.instance_variable_set("@description", nil)
      expect { valid_encounter.validate }.to raise_error(Exceptions::NotNullableAttributeError, "description cannot be nil")
    end

    it "raises an error if enemies are nil" do
      valid_encounter.instance_variable_set("@enemies", nil)
      expect { valid_encounter.validate }.to raise_error(Exceptions::NotNullableAttributeError, "enemies cannot be nil")
    end

    it "raises an error if battle_outcome is nil" do
      valid_encounter.instance_variable_set("@battle_outcome", nil)
      expect { valid_encounter.validate }.to raise_error(Exceptions::NotNullableAttributeError, "battle_outcome cannot be nil")
    end

    it "raises an error if win_outcome is nil" do
      valid_encounter.instance_variable_set("@win_outcome", nil)
      expect { valid_encounter.validate }.to raise_error(Exceptions::NotNullableAttributeError, "win_outcome cannot be nil")
    end
  end

  describe "#get_references" do
    it "returns all references of the specified type" do
      references_to_event = valid_encounter.send(:get_references, :next_event)
      expect(references_to_event).to contain_exactly(:victory_event)

      references_to_enemy = valid_encounter.send(:get_references, :enemy)
      expect(references_to_enemy).to contain_exactly(:goblin, :orc)
    end

    it "returns an empty array if no references of the specified type exist" do
      references_to_location = valid_encounter.send(:get_references, :next_location)
      expect(references_to_location).to eq([])
    end
  end
end
