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
    Label _appletTitleLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _appletTitleLabel = GetNode<Label>("UIApplet/AppletTitle");

        _appletTitleLabel.Text = "";

        // Removes the placeholder object
        GetNode<ClickableIcon>("MainArea/ScrollContainer/HBoxContainer/Placeholder").QueueFree();

        _clickableItemScene = GD.Load<PackedScene>("res://UI/ClickableIcon/ClickableIcon.tscn");

        _animationPlayer.Play("In");


        Modulate = new Color(Modulate.r, Modulate.g, Modulate.b, 0);
        
        ParseItems();

        GetNode<ScrollContainer>("MainArea/ScrollContainer").GetNode<HBoxContainer>("HBoxContainer").GetChild<ClickableIcon>(1).GrabFocus();
    }

    void ParseItems()
    {
        HBoxContainer itemsContainer = GetNode<HBoxContainer>("MainArea/ScrollContainer/HBoxContainer");

        foreach (MenuItem item in MenuItems)
        {
            ClickableIcon icon = _clickableItemScene.Instance<ClickableIcon>();

            icon.Text = item.Title;


            icon.MenuItem = item;
            icon.Focused += ItemChanged;

            itemsContainer.AddChild(icon);
        }
    }

    void ItemChanged(MenuItem item)
    {
        _appletTitleLabel.Text = item.Title;
    }

}
