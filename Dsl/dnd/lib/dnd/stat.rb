# frozen_string_literal: true
require_relative 'exceptions'

class Stat
  TYPES = [:strength, :dexterity, :constitution, :intelligence].freeze

  def initialize(type, value)
    @type = type
    @value = value
  end

  def stat(stat_type, value)
    Stat.new(stat_type, value)
  end

  def to_hash
    {
      type: @type,
      value: @value
    }
  end

  def validate
    not_nullables = [:type, :value]
    not_nullables.each do |attr|
      raise Exceptions::NotNullableAttributeError.new("#{attr} cannot be nil") if instance_variable_get("@#{attr}").nil?
    end

    unless TYPES.include?(@type)
      raise Exceptions::ArbitraryTypeError.new("#{@type} is not a valid stat type. Valid types: #{TYPES}")
    end
    if @value <= 0
      raise Exceptions::LimitViolationError.new("#{@value} is not a valid stat value. Stat values must be positive")
    end
  end
end
