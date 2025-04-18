# frozen_string_literal: true

require 'rspec'

RSpec.describe Scenario do
  describe '#validate' do
    let(:scenario) do
      Scenario.new("Epic Adventure") do
        location("Haunted Forest") do
          type :dead_forest
          tier 1
          desc "A spooky forest with lingering fog."

          event("Start Fire") do
            desc "Start a fire in the forest."
            action do
              desc "Light a fire to ward off the cold."
              require_to_have(:dexterity, 5)
              in_case_of_success { die }
              in_case_of_failure { die }
            end
          end

          enemy_encounter("Ambush") do
            desc "A goblin ambush!"
            encounter_one_of(["Goblin"])
            on_enemy_killed { die }
            on_battle_end { die }
          end
        end

        enemy("Goblin") do
          type :goblin
          desc "A small but vicious creature."
          hp 10
          damage 3
        end
      end
    end

    it 'does not raise any errors when all attributes are valid' do
      expect { scenario.validate }.not_to raise_error
    end

    it 'raises LimitViolationError when a location is missing' do
      scenario.instance_variable_set(:@locations, [])
      expect { scenario.validate }.to raise_error(Exceptions::LimitViolationError, /Scenario must have at least one location/)
    end

    it 'raises NotNullableAttributeError if a required attribute is nil' do
      scenario.instance_variable_set(:@name, nil)
      expect { scenario.validate }.to raise_error(Exceptions::NotNullableAttributeError, /name cannot be nil/)
    end

    it 'raises InvalidReferenceError when an enemy is missing from references' do
      scenario.instance_variable_get(:@enemies).clear
      expect { scenario.validate }.to raise_error(Exceptions::InvalidReferenceError, /Goblin is not a valid reference/)
    end
  end
end
