# frozen_string_literal: true

require_relative '../lib/dnd'

DND::scenario :the_fallen_hero do
  enemy :theodor do
    type :goblin
    desc "A small goblin with a big heart."
    hp 10
    damage 3
  end

  enemy :mimic do
    type :mimic
    desc "A mimic chest is a predator disguised as a treasure chest, with sharp teeth and a sticky tongue."
    hp 30
    damage 5
  end

  enemy :egorik do
    type :skeleton
    desc "A skeleton is a bone-clad undead creature, driven by dark magic."
    hp 7
    damage 3
  end

  enemy :chaos_fiend do
    type :fiend
    desc "A Chaos Fiend is a malevolent entity born of raw chaos,
          its form ever-shifting and twisted. Wielding destructive powers,
          it spreads madness and destruction wherever it treads."
    hp 50
    damage 9
  end

  enemy :the_inferno_wrath do
    type :dragon
    desc "It is a massive red dragon with scales like molten lava and eyes that burn with fiery rage.
          His breath turns cities to ash, and his roar echoes like a storm of flame."
    hp 500
    damage 50
  end

  location :deep_forest do
    tier 1
    type :dead_forest
    desc "A deep forest, full of evil creatures."
    event :start do
      desc "You decided to start your own long path to secret dungeon."

      action do
        desc "Go straight to the deep grove."
        in_case_of_success do
          transfer_to_event :deep_grove
        end
      end
      action do
        desc "Go right to the blue lake."
        in_case_of_success do
          transfer_to_event :goblin_encounter
        end
      end
      action do
        desc "Change your mind and back to home."
        in_case_of_success do
          die
        end
      end
    end
    event :deep_grove do
      desc "You see a dense grove of thorny bushes."
      action do
        desc "Make your way through the grove."
        require_to_have :dexterity, 9
        in_case_of_success do
          give_item do
            weapon :wanderer_daggers do
              desc "The wanderer's dropped daggers with patterns."
              type :daggers
              stat :dexterity, 15
            end
          end
          transfer_to_location :endless_bridge
        end
        in_case_of_failure do
          die
        end
      end
      action do
        desc "Go back to where you hear the splashing of water."
        in_case_of_success do
          transfer_to_event :goblin_encounter
        end
      end
    end
    enemy_encounter :goblin_encounter do
      desc "A goblin on the road. You must fight with him."
      encounter_one_of [:theodor]
      on_battle_end do
        transfer_to_location :endless_bridge
      end
      on_enemy_killed do
        give_item do
          gold 10
        end
        transfer_to_location :endless_bridge
      end
    end
  end
  location :endless_bridge do
    tier 2
    type :endless_bridge
    desc "A vast, crumbling stone bridge that stretches beyond sight, shrouded in eternal mist."
    event :road_across_bridge do
      desc "You can't see the end of the bridge."
      action do
        desc "Go straight across the bridge."
        in_case_of_success do
          transfer_to_event :meeting
        end
      end
      action do
        desc "Jump off the bridge."
        in_case_of_success do
          die
        end
      end
    end
    event :meeting do
      desc "You see a skeleton waving at you."
      action do
        desc "Wave back."
        require_to_have :constitution, 8
        in_case_of_success do
          transfer_to_location :castle_ruins
        end
        in_case_of_failure do
          transfer_to_event :skeleton_encounter
        end
      end
    end
    enemy_encounter :skeleton_encounter do
      desc "A furious skeleton glares at you with hollow eyes burning with malevolent energy.
            Every step it takes radiates hostility."
      encounter_one_of [:egorik]
      on_battle_end do
        transfer_to_location :castle_ruins
      end
      on_enemy_killed do
        give_item do
          gold 15
        end
      end
    end
  end
  location :castle_ruins do
    tier 3
    type :castle_ruins
    desc "The ruins of an ancient castle, crumbled by time and battle.
          Vines creep along the shattered stone walls, and shadows linger in the cold corridors."
    event :entrance do
      desc "You stand at the entrance to the castle ruins. The air is heavy with an eerie silence."
      action do
        desc "Step inside the ruins."
        in_case_of_success do
          transfer_to_event :main_hall
        end
      end
      action do
        desc "Turn back and leave."
        in_case_of_success do
          die
        end
      end
    end
    event :main_hall do
      desc "You enter the main hall, a vast chamber filled with broken pillars and scattered debris.
            The faint sound of whispering echoes through the air."
      action do
        desc "Follow the whispering."
        require_to_have :intelligence, 11
        in_case_of_success do
          give_item do
            gold 100000
          end
        end
        in_case_of_failure do
          transfer_to_event :fiend_encounter
        end
      end
    end
    enemy_encounter :fiend_encounter do
      desc "A malevolent Chaos Fiend looms before you, its warped, ever-shifting form crackling with unstable energy.
            The air around it twists and warps, a manifestation of its seething rage and destructive intent."
      encounter_one_of [:chaos_fiend]
      on_battle_end do
        transfer_to_event :stairs
      end
      on_enemy_killed do
        give_item do
          gold 40
        end
      end
    end
    event :stairs do
      desc "A long staircase leading both up and down, worn down by its own weight and the weight of its former occupants."
      action do
        desc "Climb the steps."
        in_case_of_success do
          transfer_to_event :library
        end
      end
      action do
        desc "Go down the steps."
        in_case_of_success do
          transfer_to_location :dungeon
        end
      end
    end
    event :library do
      desc "The library is a shadow of its former glory. Dust coats the air,
            illuminated by faint beams of light filtering through shattered windows.
            Books lie scattered across the floor, some torn, their pages crumpled or missing entirely."
      action do
        desc "Look through the books for something worthwhile."
        require_to_have :intelligence, 15
        in_case_of_success do
          give_item do
            weapon :old_staff do
              desc "A weathered wooden staff, its surface etched with faint runes and scars of age.
                    Despite its worn appearance, it radiates a faint aura, as if it still holds a trace of ancient magic."
              type :staff
              stat :intelligence, 20
            end
          end
          transfer_to_location :dungeon
        end
        in_case_of_failure do
          give_item do
            gold 50
          end
          transfer_to_location :dungeon
        end
      end
    end
  end
  location :dungeon do
    tier 4
    type :dungeon
    desc "A dark, ancient dungeon, its walls covered in dust and blood."
    event :enter_dungeon do
      desc "You find yourself in the dungeon, a labyrinthine path lined with traps and puzzles."
      action do
        desc "Enter the dungeon."
        in_case_of_success do
          die
        end
      end
    end
  end
end