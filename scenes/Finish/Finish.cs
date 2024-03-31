using Godot;
using System;

public partial class Finish : Area3D
{
    [Export]
    PackedScene nextLevel;
    MeshInstance3D black;
    MeshInstance3D gold;
    AnimationPlayer animPlayer;
    AudioStreamPlayer3D audioPlayer;
    public bool pristup = false;
    public override void _Ready()
    {
        black = GetNode<MeshInstance3D>("Black");
        gold = GetNode<MeshInstance3D>("Gold");
        audioPlayer = GetNode<AudioStreamPlayer3D>("AudioStreamPlayer3D");
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        gold.Visible = false;
    }
    public void _on_body_entered(Node3D body)
    {
        if(body.GetType() == typeof(Player) && pristup)
        {
            animPlayer.Play("fade");
        }
    }

    public void pristupAllow()
    {
        audioPlayer.Play();
        pristup = true;
        gold.Visible = true;
    }

    public void _on_animation_player_animation_finished(String animName)
    {
        if(animName == "fade")
            GetTree().ChangeSceneToPacked(nextLevel);
    }
}
