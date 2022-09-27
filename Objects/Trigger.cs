using Godot;
using System;

public class Trigger : Node
{
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


    public override void _Ready()
    {
        AddUserSignal("triggered", new Godot.Collections.Array() { "Bool" });
    }
}
