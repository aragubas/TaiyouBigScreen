using Godot;
using System;
using UIAppletAPI;

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
    int _lastTargetFPS = 0;
    WorldEnvironment _worldEnvironment;
    Viewport _backwaveViewport;

    [Export]
    public bool SkipIntro = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _mainUIScene = GD.Load<PackedScene>("res://UI/Main.tscn");
        
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _introSound = GetNode<AudioStreamPlayer>("IntroSound");
        _loadMenuTrigger = GetNode<Trigger>("LoadMenuTrigger");
        _worldEnvironment = GetNode<WorldEnvironment>("WorldEnvironment");
        _backwaveViewport = GetNode<Viewport>("Viewport");

        _loadMenuTrigger.Connect("triggered", this, nameof(LoadMenuTriggered));
        _animationPlayer.Connect("animation_finished", this, nameof(OnAnimationFinished));
        //_animationPlayer.Connect("animation_started", this, nameof(OnAnimationStarted));

        if (SkipIntro)
        {
            GetNode<Label>("MenuLayer/WelcomeLabel").QueueFree();
            LoadUIComponents();
        }
        else
        {
            _animationPlayer.Play("WelcomeAnimation");
            
            // Loading test timer
            _timer = new Timer();
            AddChild(_timer);
            _timer.WaitTime = 3f;
            _timer.OneShot = true;
            _timer.Connect("timeout", this, nameof(TestTimerTimeout));

        }

        // Adds support for glow in GLES 2
        if (OS.GetCurrentVideoDriver() == OS.VideoDriver.Gles2)
        {
            _worldEnvironment.Environment.GlowHdrThreshold = 0.9f;
            _worldEnvironment.Environment.GlowIntensity = 1.5f;
            _worldEnvironment.Environment.GlowStrength = 1.3f;
            //_worldEnvironment.Environment.GlowBlendMode = Godot.Environment.GlowBlendModeEnum.Softlight;


            _backwaveViewport.World.Environment.GlowHdrThreshold = 0.9f;
            _backwaveViewport.World.Environment.GlowIntensity = 1.4f;
            _backwaveViewport.World.Environment.GlowStrength = 1.3f;
            _backwaveViewport.World.Environment.GlowLevels__4 = false;
            _backwaveViewport.Size = new Vector2(512, 512);
        }

    }

    public override void _Notification(int what)
    {
        if (what == NotificationWmFocusOut)
        {
            Visible = false;
            Engine.TargetFps = 10;
            GetTree().Paused = true;
        }

        if (what == NotificationWmFocusIn)
        {
            GetTree().Paused = false;
            Visible = true;
            Engine.TargetFps = 0;

        }
    }

    void TestTimerTimeout()
    {
        GD.Print("\"Loading complete\"");

        try
        {
            LoadUIComponents();

            _animationPlayer.Play(_animationPlayer.AssignedAnimation);
        }
        catch (Exception ex)
        {
            _animationPlayer.Play("ErrorAnimation");
            GetNode<Label>("MenuLayer/WelcomeLabel").Text = "Error";
            GD.Print("Error while loading UI");
        }
    }

    void LoadUIComponents()
    {
        //throw new Exception("some error");
        MainUI mainUI = _mainUIScene.Instance<MainUI>();

        // Add default items
        mainUI.MenuItems.Add(new MenuItem() { Title = "Home" });
        mainUI.MenuItems.Add(new MenuItem() { Title = "Settings" });
        for (int i = 0; i < 18; i++)
        {
            mainUI.MenuItems.Add(new MenuItem() { Title = Guid.NewGuid().ToString() });
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
        if (animationName == "WelcomeAnimation")
        {
            Label welcomeLabel = GetNode<Label>("MenuLayer/WelcomeLabel");
            if (welcomeLabel != null)
            {
                welcomeLabel.QueueFree();
            }

        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
