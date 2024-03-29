using Godot;
using System;

public partial class Photocamera : Node3D
{
    //node deklaracije
    AnimationPlayer nAnimPlayer;
    //privatne funkcije
    private void nodeInitialize()
    {
        nAnimPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }
    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(double delta)
    {
        
    }
}
