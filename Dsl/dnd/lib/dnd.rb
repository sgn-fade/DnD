# frozen_string_literal: true

require_relative "version"
require_relative 'dnd/scenario'
require 'json'
module DND
  def self.scenario(name, &block)
    Dir.mkdir("scenarios") unless Dir.exist?("scenarios")
    scenario = Scenario.new(name, &block)
    scenario.validate
    File.open("C:/Main/media/projects/godot/dnd/scripts/#{name}.json", "w") do |file|
      file.write(JSON.pretty_generate(scenario))
    end
  end
end
