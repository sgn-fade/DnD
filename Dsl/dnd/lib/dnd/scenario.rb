# frozen_string_literal: true
require_relative 'location'
require_relative 'exceptions'
require_relative 'weapon'
require_relative 'enemy'

class Scenario
  def initialize(name, &block)
    @name = name
    @locations = []
    @enemies = []
    instance_eval(&block)
  end

  def location(name, &block)
    @locations << Location.new(name, &block)
  end

  def enemy(name, &block)
    @enemies << Enemy.new(name, &block)
  end

  def to_json(*args)
    {
      enemies: @enemies.map(&:to_hash),
      name: @name,
      locations: @locations.map(&:to_hash),
    }.to_json(*args)
  end

  def validate
    not_nullables = [:name]
    not_nullables.each do |attr|
      raise Exceptions::NotNullableAttributeError.new("#{attr} cannot be nil") if instance_variable_get("@#{attr}").nil?
    end
    if @locations.empty?
      raise Exceptions::LimitViolationError.new("Scenario must have at least one location")
    end
    @locations.each(&:validate)
    @enemies.each(&:validate)
    validate_location_references
    validate_enemy_references
  end

  private

  def validate_references(field, reference_type, valid_references)
    @locations.each do |location|
      location.instance_variable_get("@#{field}").each do |el|
        el.send(:get_references, reference_type).each do |ref|
          unless valid_references.include?(ref)
            raise Exceptions::InvalidReferenceError.new("#{ref} is not a valid reference. Valid references: #{valid_references}")
          end
        end
      end
    end
  end

  def validate_location_references
    location_names = @locations.map { |location| location.instance_variable_get("@name") }.to_a
    validate_references(:events, :next_location, location_names)
    validate_references(:enemy_encounters, :next_location, location_names)
  end

  def validate_enemy_references
    enemy_names = @enemies.map { |enemy| enemy.instance_variable_get("@name") }.to_a

    validate_references(:enemy_encounters, :enemy, enemy_names)
    validate_references(:enemy_encounters, :fight, enemy_names)
    validate_references(:events, :fight, enemy_names)
  end
end