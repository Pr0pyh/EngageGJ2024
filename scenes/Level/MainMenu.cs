using Godot;
using System;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Visible;
    }
    public void _on_start_pressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/Level/Level1.tscn");
    }

    public void _on_exit_pressed()
    {
        GetTree().Quit();
    }
}
