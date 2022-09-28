using Ceira.Classes;
using Godot;
using System;
using System.Runtime.Serialization.Json;

public class ClickableIcon : ToolButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    AudioStreamPlayer _selectSound;

    public MenuItem MenuItem;
    public event Action<MenuItem> Focused;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _selectSound = GetNode<AudioStreamPlayer>("SelectSound");

        //Connect("focus_exited", this, nameof(FocusExited));
        Connect("focus_entered", this, nameof(FocusEntered));        
    }

    void FocusEntered()
    {
        _selectSound.Play();

        Focused?.Invoke(MenuItem);
    }

}
