# frozen_string_literal: true
require_relative 'exceptions'
require_relative 'outcome'
require_relative 'stat'

class Action
  def initialize(&block)
    @description = nil
    @required_stat = nil
    @positive_outcome = nil
    @negative_outcome = nil
    @required_tag = nil
    instance_eval(&block)
  end

  def desc(description)
    @description = description
  end

  def require_to_have(stat_type, value)
    @required_stat = Stat.new(stat_type, value)
  end
  def required_tag(tag)
    @required_tag = tag
  end
  def in_case_of_success(&block)
    @positive_outcome = Outcome.new(&block)
  end

  def in_case_of_failure(&block)
    @negative_outcome = Outcome.new(&block)
  end

  def to_hash
    {
      description: @description,
      required_stat: @required_stat.nil? ? {} : @required_stat.to_hash,
      positive_outcome: @positive_outcome.nil? ? {} : @positive_outcome.to_hash,
      negative_outcome: @negative_outcome.nil? ? {} : @negative_outcome.to_hash,
    }
  end

  def validate
    not_nullables = [:description, :positive_outcome]
    not_nullables.each do |attr|
      raise Exceptions::NotNullableAttributeError.new("#{attr} cannot be nil") if instance_variable_get("@#{attr}").nil?
    end
    @required_stat.validate if @required_stat
    @positive_outcome.validate if @positive_outcome
    @negative_outcome.validate if @negative_outcome
  end
end
