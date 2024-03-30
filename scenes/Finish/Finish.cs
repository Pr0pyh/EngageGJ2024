using Godot;
using System;

public partial class Finish : Area3D
{
    [Export]
    PackedScene nextLevel;
    MeshInstance3D black;
    MeshInstance3D gold;
    public bool pristup = false;
    public override void _Ready()
    {
        black = GetNode<MeshInstance3D>("Black");
        gold = GetNode<MeshInstance3D>("Gold");
        black.Visible = true;
        gold.Visible = false;
    }
    public void _on_body_entered(Node3D body)
    {
        if(body.GetType() == typeof(Player) && pristup)
        {
            GetTree().ChangeSceneToPacked(nextLevel);
        }
    }

    public void pristupAllow()
    {
        pristup = true;
        black.Visible = false;
        gold.Visible = true;
    }
}
