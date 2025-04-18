# frozen_string_literal: true

class Item
  TYPES = [:weapon, :gold]

  def initialize(&block)
    instance_eval(&block)
  end

  def weapon(name, &block)
    @type = :weapon
    @body = Weapon.new(name, &block)
  end

  def gold(value)
    @type = :gold
    @body = value
  end

  def to_hash
    {
      :type => @type,
      :body => @type == :gold ? @body.to_s : @body.to_hash,
    }
  end

  def validate
    not_nullables = [:type, :body]
    not_nullables.each do |attr|
      raise Exceptions::NotNullableAttributeError.new("#{attr} cannot be nil") if instance_variable_get("@#{attr}").nil?
    end
    unless TYPES.include?(@type)
      raise Exceptions::ArbitraryTypeError.new("#{@type} is not a valid item type. Valid types: #{TYPES}")
    end
    @body.validate if @type == :weapon
  end
end
