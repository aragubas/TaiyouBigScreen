using Ceira.Classes;
using Godot;
using Newtonsoft.Json;
using System;
using System.IO;

public class MainSingleton : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public Settings AppSettings = new Settings();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //string ConfigurationFilePath = System.File
        string ConfigDirectory = System.IO.Path.Combine(AppContext.BaseDirectory, ".conf");
        string ConfigFilePath = System.IO.Path.Combine(ConfigDirectory, "Settings.json");

        //
        // Create .conf directory and file
        //
        if (!System.IO.Directory.Exists(ConfigDirectory))
        {
            System.IO.Directory.CreateDirectory(ConfigDirectory);
            CreateNewConfigFile(ConfigFilePath);
        }
        else
        {
            // If the user deleted config file
            if (!System.IO.File.Exists(ConfigFilePath))
            {
                CreateNewConfigFile(ConfigFilePath);
            }
        }

        //
        // Read config file
        //
        ReadConfigFile(ConfigFilePath);
    }

    void ReadConfigFile(string configFilePath)
    {
        string ConfigFileString = System.IO.File.ReadAllText(configFilePath);

        try
        {
            Settings parsedSettings = JsonConvert.DeserializeObject<Settings>(ConfigFileString);

            AppSettings = parsedSettings;

        }
        catch (Newtonsoft.Json.JsonException)
        {
            GD.Print("Could not read configuration file.");

            AppSettings = new Settings();
            CreateNewConfigFile(configFilePath);
        }

        ApplyConfigFile();
    }

    void ApplyConfigFile()
    {
        OS.WindowFullscreen = AppSettings.Fullscreen;
    }

    void CreateNewConfigFile(string configFilePath)
    {
        AppSettings.LoadDefaultSettings();

        System.IO.File.WriteAllText(configFilePath, JsonConvert.SerializeObject(AppSettings));

        AppSettings.FirstTime = true;
    }
}
