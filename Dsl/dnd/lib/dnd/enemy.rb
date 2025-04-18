# frozen_string_literal: true
require_relative 'exceptions'

class Enemy
  TYPES = [:goblin, :skeleton, :mimic, :fiend, :dragon]

  def initialize(name, &block)
    @name = name
    @description = nil
    @type = nil
    @hp = nil
    @damage = nil
    instance_eval(&block)
  end

  def desc(description)
    @description = description
  end

  def type(type)
    @type = type
  end

  def hp(hp)
    @hp = hp
  end

  def damage(damage)
    @damage = damage
  end

  def to_hash
    {
      name: @name,
      description: @description,
      type: @type,
      hp: @hp,
      damage: @damage,
    }
  end

  def validate
    [:name, :description, :type, :hp, :damage].each do |attr|
      raise Exceptions::NotNullableAttributeError.new("#{attr} cannot be nil") if instance_variable_get("@#{attr}").nil?
    end

    unless TYPES.include?(@type)
      raise Exceptions::ArbitraryTypeError.new("#{@type} is not a valid enemy type. Valid types: #{TYPES}")
    end
    raise Exceptions::LimitViolationError.new("HP must be a positive integer. Received: #{@hp}") if @hp <= 0
    raise Exceptions::LimitViolationError.new("Damage must be a positive integer. Received: #{@damage}") if @damage <= 0
  end
end
