# frozen_string_literal: true

module Exceptions
  class DNDError < StandardError; end

  class NotNullableAttributeError < DNDError; end

  class ArbitraryTypeError < DNDError; end

  class LimitViolationError < DNDError; end

  class InvalidReferenceError < DNDError; end

end
