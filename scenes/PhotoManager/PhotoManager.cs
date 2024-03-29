using Godot;
using Godot.Collections;
using System;
using System.Collections;
using System.Data;

public partial class PhotoManager : Node3D
{
    Dictionary<int, int> dict = new Dictionary<int, int>();
    int activePicture;
    public void dodajSliku(int i)
    {
        dict[i]++;
    }

    public override void _Ready()
    {
        dict.Add(0, 0);
        dict.Add(1, 0);
        dict.Add(2, 0);
    }

    public override void _PhysicsProcess(double delta)
    {
        if(Input.IsActionPressed("1"))
        {
            activePicture = dict[0];
            GD.Print(activePicture);
        }
        if(Input.IsActionPressed("2"))
        {
            activePicture = dict[1];
            GD.Print(activePicture);
        }
        if(Input.IsActionPressed("3"))
        {
            activePicture = dict[2];
            GD.Print(activePicture);
        }
    }
}
