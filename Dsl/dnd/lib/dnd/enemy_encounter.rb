# frozen_string_literal: true
require_relative 'outcome'
require_relative 'exceptions'
require_relative 'item'

class EnemyEncounter
  def initialize(name, &block)
    @name = name
    @description = nil
    @win_outcome = nil
    @battle_outcome = nil
    @enemies = []
    instance_eval(&block)
  end

  def desc(description)
    @description = description
  end

  def encounter_one_of(enemies)
    @enemies = enemies
  end

  def on_enemy_killed(&block)
    @win_outcome = Outcome.new(&block)
  end

  def on_battle_end(&block)
    @battle_outcome = Outcome.new(&block)
  end

  def to_hash
    {
      name: @name,
      description: @description,
      enemies: @enemies,
      battle_outcome: @battle_outcome.to_hash,
      win_outcome: @win_outcome.to_hash,
    }
  end

  def validate
    not_nullables = [:name, :description, :enemies, :battle_outcome, :win_outcome]
    not_nullables.each do |attr|
      raise Exceptions::NotNullableAttributeError.new("#{attr} cannot be nil") if instance_variable_get("@#{attr}").nil?
    end
  end

  private

  def get_references(reference_type)
    return @enemies if reference_type == :enemy
    outcomes = [@battle_outcome, @win_outcome]
    outcomes.compact.filter_map do |outcome|
      outcome.instance_variable_get("@body") if outcome.instance_variable_get("@type") == reference_type
    end
  end
end
