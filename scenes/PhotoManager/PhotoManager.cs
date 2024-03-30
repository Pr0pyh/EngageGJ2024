using Godot;
using Godot.Collections;
using System;
using System.Collections;
using System.Data;

public partial class PhotoManager : Node3D
{
    Dictionary<int, int> dict = new Dictionary<int, int>();
    Array<MeshInstance3D> meshes = new Array<MeshInstance3D>();
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
        if(i == -1)
            return;
        dict[i]--;
        if(dict[i]<=0)
        {
            meshes[i].Visible = false;
        }
    }

    public override void _Ready()
    {
        dict.Add(0, 0);
        dict.Add(1, 0);
        dict.Add(2, 0);
        photo1 = GetNode<MeshInstance3D>("Photo1");
        photo2 = GetNode<MeshInstance3D>("Photo2");
        photo3 = GetNode<MeshInstance3D>("Photo3");
        meshes.Add(photo1);
        meshes.Add(photo2);
        meshes.Add(photo3);
        photo1.Visible = false;
        photo2.Visible = false;
        photo3.Visible = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        activePictureCount = dict[activePicture];
        if(Input.IsActionJustPressed("1") && dict[0]>0)
        {
            activePicture = 0;
            photo1.Visible = true;
            photo2.Visible = false;
            photo3.Visible = false;
            GD.Print(activePicture);
            GD.Print(activePictureCount);
        }
        if(Input.IsActionJustPressed("2") && dict[1]>0)
        {
            activePicture = 1;
            photo1.Visible = false;
            photo2.Visible = true;
            photo3.Visible = false;
            GD.Print(activePicture);
            GD.Print(activePictureCount);
        }
        if(Input.IsActionJustPressed("3") && dict[2]>0)
        {
            activePicture = 2;
            photo1.Visible = false;
            photo2.Visible = false;
            photo3.Visible = true;
            GD.Print(activePicture);
            GD.Print(activePictureCount);
        }
    }
}
