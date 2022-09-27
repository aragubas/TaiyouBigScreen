using Godot;
using System;

public class ClickableIcon : ToolButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    Tween _tween;
    float _opacityUnfocused = 0.3f;
    float _transitionTime = 0.25f;
    AudioStreamPlayer _selectSound;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _tween = GetNode<Tween>("Tween");
        _selectSound = GetNode<AudioStreamPlayer>("SelectSound");

        Connect("focus_exited", this, nameof(FocusExited));
        Connect("focus_entered", this, nameof(FocusEntered));


        _tween.InterpolateProperty(this, "modulate:a", Modulate.a, _opacityUnfocused, _transitionTime);
        _tween.Start();
    }

    void FocusExited()
    {
        _tween.StopAll();

        _tween.InterpolateProperty(this, "modulate:a", Modulate.a, _opacityUnfocused, _transitionTime);
        _tween.Start();
    }

    void FocusEntered()
    {
        _tween.StopAll();
        
        _tween.InterpolateProperty(this, "modulate:a", Modulate.a, 1, _transitionTime);
        _tween.Start();

        _selectSound.Play();
    }

}
