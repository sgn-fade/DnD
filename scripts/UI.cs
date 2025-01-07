using Godot;
using System;
using DND;

public partial class UI : CanvasLayer
{
	[Export] private Label _locationName;
	[Export] private Label _locationDescription;
	[Export] private TextureRect _locationImage;
	
	[Export] private Label _eventName;
	[Export] private Label _eventDescription;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ChangeLocation(Location location)
	{
		_locationName.Text = location.Name;
		_locationDescription.Text = location.Description;
		_locationImage.Texture = location.Background;
	}

	public void ChangeEvent(Event @event)
	{
		_eventName.Text = @event.Name;
		_eventDescription.Text = @event.Description;
		//TODO actions
	}
}
