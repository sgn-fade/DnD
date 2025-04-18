# frozen_string_literal: true
require_relative 'action'
require_relative 'exceptions'

class Event
  def initialize(name, &block)
    @name = name
    @description = nil
    @actions = []
    instance_eval(&block)
  end

  def desc(description)
    @description = description
  end

  def action(&block)
    @actions << Action.new(&block)
  end

  def to_hash
    {
      name: @name,
      description: @description,
      actions: @actions.map(&:to_hash)
    }
  end

  def validate
    not_nullables = [:name, :description]
    not_nullables.each do |attr|
      raise Exceptions::NotNullableAttributeError.new("#{attr} cannot be nil") if instance_variable_get("@#{attr}").nil?
    end
    unless (1..3).include? @actions.length
      raise Exceptions::LimitViolationError.new("Number of actions must be between 1 and 3 inclusive")
    end
    @actions.each do |action|
      action.validate
    end
  end

  private

  def get_possible_outcomes
    outcomes = @actions.flat_map do |action|
      [
        action.instance_variable_get("@positive_outcome"),
        action.instance_variable_get("@negative_outcome")
      ]
    end
    outcomes.compact
  end

  def get_references(reference_type)
    @actions.flat_map { |a| a.instance_variable_get("@items_to_give") }.to_a if reference_type == :item
    outcomes = get_possible_outcomes
    outcomes.filter_map do |outcome|
      outcome.instance_variable_get("@body") if outcome.instance_variable_get("@type") == reference_type
    end
  end
end
