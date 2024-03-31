using Godot;
using Godot.Collections;
using System;
using System.Collections;
using System.Data;
using System.Linq;

public partial class PhotoManager : Node3D
{
    [Export]
    PackedScene photoInstance1;
    [Export]
    PackedScene photoInstance2;
    [Export]
    PackedScene photoInstance3;
    Dictionary<int, int> dict = new Dictionary<int, int>();
    Array<Node3D> meshes = new Array<Node3D>();
    Array<PackedScene> premadeMeshes = new Array<PackedScene>();
    Node3D photo1;
    Node3D photo2;
    Node3D photo3;
    AnimationPlayer animPlayer;
    AnimationPlayer animPlayer2;
    ProgressBar countBar;
    AudioStreamPlayer audioPlayer;
    public int activePicture = 0;
    public int activePictureCount = 0;
    public void dodajSliku(int i)
    {
        if(dict[i]<6)
        {
            dict[i]++;
            audioPlayer.Play();
            photo1.Visible = false;
            photo2.Visible = false;
            photo3.Visible = false;
            countBar.Value = dict[i]*20;
            meshes[i].Visible = true;
            activePicture = i;
            GD.Print(activePicture);
            GD.Print(activePictureCount);
            animPlayer.Play("entry");
            animPlayer2.Play("countUp");
            if(dict[i] > 1)
            {
                Node3D photo = premadeMeshes[i].Instantiate<Node3D>();
                meshes[i].AddChild(photo);
                photo.RotateX(Mathf.DegToRad(3.0f*dict[i]));
            }
        }
    }

    public void obrisiSliku(int i)
    {
        if(i == -1)
            return;
        if(dict[i]>0)
            dict[i]--;
        if(dict[i]<=0)
        {
            meshes[i].Visible = false;
        }
        countBar.Value = dict[i]*20;
        audioPlayer.Play();
        animPlayer2.Play("countDown");
        if(dict[i] >= 1)
        {
            Array<Node> nodes = meshes[i].GetChildren();
            meshes[i].RemoveChild(nodes[0]);
        }
    }

    public override void _Ready()
    {
        dict.Add(0, 0);
        dict.Add(1, 0);
        dict.Add(2, 0);
        photo1 = GetNode<Node3D>("Picture1");
        photo2 = GetNode<Node3D>("Picture2");
        photo3 = GetNode<Node3D>("Picture3");
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animPlayer2 = GetNode<AnimationPlayer>("AnimationPlayer2");
        audioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        countBar = GetNode<CanvasLayer>("CanvasLayer").GetNode<ProgressBar>("Health");
        meshes.Add(photo1);
        meshes.Add(photo2);
        meshes.Add(photo3);
        premadeMeshes.Add(photoInstance1);
        premadeMeshes.Add(photoInstance2);
        premadeMeshes.Add(photoInstance3);
        photo1.Visible = false;
        photo2.Visible = false;
        photo3.Visible = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        activePictureCount = dict[activePicture];
        if(Input.IsActionJustPressed("1") && dict[0]>0)
        {
            animPlayer.Play("entry");
            audioPlayer.Play();
            activePicture = 0;
            countBar.Value = dict[0]*20;
            photo1.Visible = true;
            photo2.Visible = false;
            photo3.Visible = false;
            GD.Print(activePicture);
            GD.Print(activePictureCount);
        }
        if(Input.IsActionJustPressed("2") && dict[1]>0)
        {
            animPlayer.Play("entry");
            audioPlayer.Play();
            activePicture = 1;
            countBar.Value = dict[1]*20;
            photo1.Visible = false;
            photo2.Visible = true;
            photo3.Visible = false;
            GD.Print(activePicture);
            GD.Print(activePictureCount);
        }
        if(Input.IsActionJustPressed("3") && dict[2]>0)
        {
            animPlayer.Play("entry");
            audioPlayer.Play();
            activePicture = 2;
            countBar.Value = dict[2]*20;
            photo1.Visible = false;
            photo2.Visible = false;
            photo3.Visible = true;
            GD.Print(activePicture);
            GD.Print(activePictureCount);
        }
    }
}
