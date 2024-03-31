using Godot;
using System;

public partial class Ending : Control
{
    AnimationPlayer animPlayer;
    public override void _Ready()
    {
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animPlayer.Play("ending");
    }

    public void _on_animation_player_animation_finished(String animName)
    {
        if(animName == "ending")
            GetTree().ChangeSceneToFile("res://scenes/Level/MainMenu.tscn");
    }
}
