using Godot;
using System;

public partial class Item : StaticBody3D
{
    [Export]
    public int number;
    MeshInstance3D dream;

    public override void _Ready()
    {
        if(number == 0)
			dream = GetNode<MeshInstance3D>("Photo1");
		if(number == 1)
			dream = GetNode<MeshInstance3D>("Photo2");
		if(number == 2)
			dream = GetNode<MeshInstance3D>("Photo3");
        dream.Visible = true;
    }
}
