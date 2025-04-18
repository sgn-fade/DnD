# frozen_string_literal: true
require_relative 'exceptions'

class Outcome
  TYPES = [:death, :next_event, :next_location]

  def initialize(&block)
    @type = nil
    @body = nil
    @item_to_give = nil
    @tag_to_give = nil
    instance_eval(&block)
  end

  def transfer_to_event(event)
    @type = :next_event
    @body = event
  end

  def transfer_to_location(location)
    @type = :next_location
    @body = location
  end

  def die
    @type = :death
  end

  def give_item(&block)
    @item_to_give = Item.new(&block)
  end

  def to_hash
    {
      type: @type,
      body: @body,
      tag_to_give: @tag_to_give,
      item_to_give: @item_to_give.nil? ? {} : @item_to_give.to_hash,
    }
  end
  def give_tag(tag)
    @tag_to_give = tag
  end
  def validate
    if @body.nil? and (@type != :death or !@item_to_give.nil?)
      raise Exceptions::NotNullableAttributeError.new("body cannot be nil")
    end
    @item_to_give.validate if @item_to_give
  end
end
