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
    Trigger _loadMenuTrigger;
    Timer _timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _mainUIScene = GD.Load<PackedScene>("res://UI/Main.tscn");
        
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _introSound = GetNode<AudioStreamPlayer>("IntroSound");
        _loadMenuTrigger = GetNode<Trigger>("LoadMenuTrigger");

        _loadMenuTrigger.Connect("triggered", this, nameof(LoadMenuTriggered));

        _animationPlayer.Connect("animation_finished", this, nameof(OnAnimationFinished));
        //_animationPlayer.Connect("animation_started", this, nameof(OnAnimationStarted));
        _animationPlayer.Play("WelcomeAnimation");

        //GetNode<Label>("CanvasLayer/WelcomeLabel").QueueFree();

        //MainUI mainUI = _mainUIScene.Instance<MainUI>();

        //GetNode<CanvasLayer>("CanvasLayer").AddChild(mainUI);

        _timer = new Timer();
        AddChild(_timer);
        _timer.WaitTime = 0.5f;
        _timer.OneShot = true;
        _timer.Connect("timeout", this, nameof(TestTimerTimeout));
    }

    void TestTimerTimeout()
    {
        _animationPlayer.Play(_animationPlayer.AssignedAnimation);
        GD.Print("\"Loading complete\"");

        LoadUIComponents();
    }

    void LoadUIComponents()
    {
        MainUI mainUI = _mainUIScene.Instance<MainUI>();

        for (int i = 0; i < 20; i++)
        {
            mainUI.MenuItems.Add(new Ceira.Classes.MenuItem() { Title = Guid.NewGuid().ToString() });
        }

        GetNode<CanvasLayer>("MenuLayer").AddChild(mainUI);
    }

    void LoadMenuTriggered(bool triggerValue)
    {
        if (triggerValue)
        {
            _animationPlayer.Stop(false);
            _timer.Start();

            GD.Print("Load stuff - To do");

        }
    }

    void OnAnimationFinished(string animationName)
    {
        GetNode<Label>("MenuLayer/WelcomeLabel").QueueFree();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
