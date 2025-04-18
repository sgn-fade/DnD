# frozen_string_literal: true
require_relative '../lib/dnd'

DND::scenario :the_long_way do
   enemy :theodor do
      type :skeleton
      desc "A small goblin with a big heart"
      hp 10
      damage 3
    end
  location :deep_forest do
    tier 1
    type :dead_forest
    desc "A deep forest, full of evil creatures"
    event :start do
      desc "You decided to start your own long path to dragons shelter"

      action do
        desc "Start your path"
        in_case_of_success do
          transfer_to_event :big_tree
        end
        end
      action do
        desc "Get tag"

        in_case_of_success do
          give_tag :tag1
          transfer_to_event :big_tree
        end
      end
      action do
        desc "Stay home under the covers"
        require_to_have :strength, 99
        in_case_of_success do
          die
        end
      end
    end
    event :big_tree do
      desc "An old tree which saw the birth of the world"
      action do
        desc "Climb a tree to look around"
        require_to_have :dexterity, 9
        in_case_of_success do
          transfer_to_event "Tree's crown"
        end
        in_case_of_failure do
          die
        end
      end
      action do
        desc "To take your road"
        in_case_of_success do
          transfer_to_event :goblin_encounter
        end
        end
      action do
        desc "Check tag"
        required_tag "tag1"
        in_case_of_success do
          transfer_to_event :goblin_encounter
        end
      end
    end
    event "Tree's crown" do
      desc "an amazing view unfolds before you"
      action do
        desc "go downstairs"
        in_case_of_success do
          transfer_to_event :big_tree
        end
      end
    end
    enemy_encounter :goblin_encounter do
      desc "A goblin on the road"
      encounter_one_of [:theodor]
      on_battle_end do
        transfer_to_event :goblin_encounter
      end
      on_enemy_killed do
        give_item do
          gold 5
        end
      end
    end
  end
end