# frozen_string_literal: true
require_relative 'exceptions'
require_relative 'enemy_encounter'
require_relative 'event'

class Location
  TYPES = [:dungeon, :dead_forest, :endless_bridge, :castle_ruins, :firebound_plato]

  def initialize(name, &block)
    @name = name
    @tier = nil
    @description = nil
    @events = []
    @enemy_encounters = []
    @type = nil
    instance_eval(&block)
  end

  def type(type)
    @type = type
  end

  def tier(tier)
    @tier = tier
  end

  def desc(description)
    @description = description
  end

  def event(name, &block)
    @events << Event.new(name, &block)
  end

  def enemy_encounter(name, &block)
    @enemy_encounters << EnemyEncounter.new(name, &block)
  end

  def to_hash
    {
      name: @name,
      type: @type,
      tier: @tier,
      description: @description,
      events: @events.map(&:to_hash),
      enemy_encounters: @enemy_encounters.map(&:to_hash)
    }
  end

  def validate
    not_nullables = [:name, :type, :tier, :description]
    not_nullables.each do |attr|
      raise Exceptions::NotNullableAttributeError.new("#{attr} cannot be nil") if instance_variable_get("@#{attr}").nil?
    end
    unless TYPES.include?(@type)
      raise Exceptions::ArbitraryTypeError.new("#{@type} is not a valid location type. Valid types: #{TYPES}")
    end
    if @events.empty? and @enemy_encounters.empty?
      raise Exceptions::LimitViolationError.new("Location must have at least one event or enemy encounter")
    end
    @events.each(&:validate)
    @enemy_encounters.each(&:validate)

    validate_event_transitions
  end

  private

  def validate_event_transitions
    names = [@events.map { |e| e.instance_variable_get("@name") },
             @enemy_encounters.map { |e| e.instance_variable_get("@name") }].flatten

    transitions = [@events.map { |e| e.send(:get_references, :next_event) },
                   @enemy_encounters.map { |e| e.send(:get_references, :next_event) }].flatten.compact
    validate_transitions(transitions.flatten, names)
  end

  def validate_transitions(transitions, names)
    transitions.each do |transition|
      unless names.include?(transition)
        raise Exceptions::InvalidReferenceError.new("#{transition} is not a valid name")
      end
    end
  end
end

