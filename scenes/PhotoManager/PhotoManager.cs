using Godot;
using Godot.Collections;
using System;
using System.Collections;
using System.Data;

public partial class PhotoManager : Node3D
{
    Dictionary<int, int> dict = new Dictionary<int, int>();
    MeshInstance3D photo1;
    MeshInstance3D photo2;
    MeshInstance3D photo3;
    public int activePicture = 0;
    public int activePictureCount = 0;
    public void dodajSliku(int i)
    {
        dict[i]++;
    }

    public void obrisiSliku(int i)
    {
        dict[i]--;
    }

    public override void _Ready()
    {
        dict.Add(0, 0);
        dict.Add(1, 0);
        dict.Add(2, 0);
        photo1 = GetNode<MeshInstance3D>("Photo1");
        photo2 = GetNode<MeshInstance3D>("Photo2");
        photo3 = GetNode<MeshInstance3D>("Photo3");
        photo1.Visible = false;
        photo2.Visible = false;
        photo3.Visible = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        activePictureCount = dict[activePicture];
        if(Input.IsActionJustPressed("1"))
        {
            activePicture = 0;
            photo1.Visible = true;
            photo2.Visible = false;
            photo3.Visible = false;
            GD.Print(activePicture);
        }
        if(Input.IsActionJustPressed("2"))
        {
            activePicture = 1;
            photo1.Visible = false;
            photo2.Visible = true;
            photo3.Visible = false;
            GD.Print(activePicture);
        }
        if(Input.IsActionJustPressed("3"))
        {
            activePicture = 2;
            photo1.Visible = false;
            photo2.Visible = false;
            photo3.Visible = true;
            GD.Print(activePicture);
        }
    }
}
