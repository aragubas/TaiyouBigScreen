using Godot;
using System;

public class MainUI : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    AnimationPlayer _animationPlayer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        _animationPlayer.Play("In");

        GetNode<ScrollContainer>("ScrollContainer").GetNode<HBoxContainer>("HBoxContainer").GetChild<ClickableIcon>(0).GrabFocus();

        Modulate = new Color(Modulate.r, Modulate.g, Modulate.b, 0);
    }
}
