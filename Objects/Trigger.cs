using Godot;
using System;

public class Trigger : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    bool _triggered;
    [Export]
    public bool Triggered
    {
        get => _triggered;
        set
        {
            EmitSignal("triggered", value);
            _triggered = value;
        }
    }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddUserSignal("triggered", new Godot.Collections.Array() { "Bool" });
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
