using Godot;
using System;

public class MainScene : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    AnimationPlayer _animationPlayer;
    PackedScene _mainUIScene;
    AudioStreamPlayer _introSound;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _mainUIScene = GD.Load<PackedScene>("res://UI/Main.tscn");
        
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _introSound = GetNode<AudioStreamPlayer>("IntroSound");

        _animationPlayer.Connect("animation_finished", this, nameof(OnAnimationFinished));
        //_animationPlayer.Connect("animation_started", this, nameof(OnAnimationStarted));
        _animationPlayer.Play("WelcomeAnimation");

        //GetNode<Label>("CanvasLayer/WelcomeLabel").QueueFree();

        //MainUI mainUI = _mainUIScene.Instance<MainUI>();

        //GetNode<CanvasLayer>("CanvasLayer").AddChild(mainUI);
    }

    void OnAnimationFinished(string animationName)
    {
        GetNode<Label>("CanvasLayer/WelcomeLabel").QueueFree();

        MainUI mainUI = _mainUIScene.Instance<MainUI>();

        GetNode<CanvasLayer>("CanvasLayer").AddChild(mainUI);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
