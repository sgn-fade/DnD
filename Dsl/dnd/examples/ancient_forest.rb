# frozen_string_literal: true
require_relative '../lib/dnd'

DND::scenario(:ancient_forest) do
  weapon(:flaming_sword) do
    desc("A sword imbued with eternal flames, capable of scorching even the toughest foes.")
    type(:sword)
    stat(:strength, 10)
  end

  weapon(:shadow_daggers) do
    desc("A pair of daggers that grant unparalleled stealth and agility.")
    type(:daggers)
    stat(:dexterity, 8)
  end

  enemy(:forest_goblin) do
    desc("A mischievous goblin lurking in the shadows of the forest.")
    type(:goblin)
    hp(15)
    damage(5)
  end

  enemy(:shadow_mimic) do
    desc("A dark creature that disguises itself as a harmless chest.")
    type(:mimic)
    hp(30)
    damage(10)
  end

  enemy(:ancient_dragon) do
    desc("A fearsome dragon that guards the heart of the forest.")
    type(:dragon)
    hp(150)
    damage(30)
  end

  location(:forest_clearing) do
    type(:dead_forest)
    tier(1)
    desc("A quiet clearing in the heart of the forest, with an eerie silence.")

    event(:find_weapon) do
      desc("You discover a mysterious weapon hidden under some fallen leaves.")
      action do
        desc("You discover a mysterious weapon hidden under some fallen leaves.")
        require_to_have(:dexterity, 5)
        give_items([:flaming_sword])
        in_case_of_success do
          transfer_to_event(:approach_cave)
        end
        in_case_of_failure do
          transfer_to_random_encounter(:goblin_ambush)
        end
      end
    end

    enemy_encounter(:goblin_ambush) do
      desc("A group of forest goblins emerges from the undergrowth, weapons drawn!")
      encounter_one_of([:forest_goblin])
      reward(:shadow_daggers)
      in_case_of_success do
        transfer_to_event(:approach_cave)
      end
      in_case_of_failure do
        die
      end
    end
  end

  location(:hidden_cave) do
    type(:castle_ruins)
    tier(2)
    desc("A hidden cave deep in the forest, rumored to house ancient treasures.")

    event(:ancient_statue) do
      action do
        desc("You find a statue that seems to react to your presence. Will you touch it?")
        require_to_have(:intelligence, 8)
        in_case_of_success do
          transfer_to_event(:dragon_encounter)
        end
        in_case_of_failure do
          transfer_to_random_encounter(:mimic_challenge)
        end
      end
    end

    enemy_encounter(:mimic_challenge) do
      desc("You reach out to open a chest, only to realize it's a mimic!")
      encounter_one_of([:shadow_mimic])
      reward(:flaming_sword)
      in_case_of_success do
        transfer_to_event(:dragon_encounter)
      end
      in_case_of_failure do
        die
      end
    end
  end

  location(:dragon_lair) do
    type(:firebound_plato)
    tier(3)
    desc("The lair of the Ancient Dragon, filled with treasures and peril.")

    enemy_encounter(:dragon_encounter) do
      desc("The Ancient Dragon appears, roaring fiercely!")
      encounter_one_of([:ancient_dragon])
      reward(:ancient_treasure)
      in_case_of_success do
        transfer_to_event(:victory)
      end
      in_case_of_failure do
        die
      end
    end

    event(:victory) do
      desc("You defeat the Ancient Dragon and claim the treasures of the forest!")
      action do
        desc("End your adventure and claim the treasures of the forest!")
        in_case_of_success do
          transfer_to_location(:forest_clearing)
        end
      end
    end
  end
end