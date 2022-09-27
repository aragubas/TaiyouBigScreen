using Ceira.Classes;
using Godot;
using System;
using System.Collections.Generic;

public class MainUI : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    AnimationPlayer _animationPlayer;
    public List<MenuItem> MenuItems = new List<MenuItem>();
    PackedScene _clickableItemScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _clickableItemScene = GD.Load<PackedScene>("res://UI/ClickableIcon/ClickableIcon.tscn");

        _animationPlayer.Play("In");

        GetNode<ScrollContainer>("MainArea/ScrollContainer").GetNode<HBoxContainer>("HBoxContainer").GetChild<ClickableIcon>(0).GrabFocus();

        Modulate = new Color(Modulate.r, Modulate.g, Modulate.b, 0);
        HBoxContainer itemsContainer = GetNode<HBoxContainer>("MainArea/ScrollContainer/HBoxContainer");

        foreach (MenuItem item in MenuItems)
        {
            ClickableIcon icon = _clickableItemScene.Instance<ClickableIcon>();
            icon.Text = item.Title;

            itemsContainer.AddChild(icon);
        }
    }
}
