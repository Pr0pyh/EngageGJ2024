using Godot;
using System;

public partial class TestEnemy : CharacterBody3D
{
    public void damage()
    {
        GD.Print("pozovi");
    }

    public void show()
    {
        GD.Print("pokazi");
    }
}
