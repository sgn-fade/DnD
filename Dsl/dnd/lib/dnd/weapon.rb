# frozen_string_literal: true
require_relative 'exceptions'

class Weapon
  TYPES = [:sword, :daggers, :staff]

  def initialize(name, &block)
    @name = name
    @description = nil
    @type = nil
    @stat = nil
    instance_eval(&block)
  end

  def desc(description)
    @description = description
  end

  def type(type)
    @type = type
  end

  def stat(stat_type, value)
    @stat = Stat.new(stat_type, value)
  end

  def to_hash
    {
      :name => @name,
      :description => @description,
      :type => @type,
      :stat => @stat.nil? ? {} : @stat.to_hash
    }
  end

  def validate
    not_nullables = [:name, :description, :type, :stat]
    not_nullables.each do |attr|
      raise Exceptions::NotNullableAttributeError.new("#{attr} cannot be nil") if instance_variable_get("@#{attr}").nil?
    end
    unless TYPES.include?(@type)
      raise Exceptions::ArbitraryTypeError.new("#{@type} is not a valid weapon type. Valid types: #{TYPES}")
    end
    @stat.validate if @stat
  end
end
