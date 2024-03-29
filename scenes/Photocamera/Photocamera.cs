using Godot;
using System;

public partial class Photocamera : Node3D
{
    //enum deklaracije
    enum STATE {
        SHOOT
    }
    STATE state;
    //node deklaracije
    AnimationPlayer nAnimPlayer;
    Area3D photoShot;
    Area3D photoView;
    //privatne funkcije
    private void nodeInitialize()
    {
        nAnimPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        photoShot = GetNode<Area3D>("Photospace");
        photoView = GetNode<Area3D>("Photoview");
        photoShot.Monitoring = false;
        photoShot.Visible = false;
        photoView.Monitoring = false;
        photoView.Visible = false;
    }
    private void shoot()
    {
        nAnimPlayer.Play("shoot");
    }

    private void shoot2()
    {
        photoView.Visible = !photoView.Visible;
        photoView.Monitoring = !photoView.Monitoring;
    }
    private void shootState()
    {
        if(Input.IsActionJustPressed("shoot"))
        {
            shoot();
        }
        if(Input.IsActionJustPressed("shoot2"))
        {
            shoot2();
        }
        if(Input.IsActionJustReleased("shoot2"))
        {
            shoot2();
        }
    }
    public override void _Ready()
    {
        nodeInitialize();
    }

    public override void _PhysicsProcess(double delta)
    {
        switch(state)
        {
            case STATE.SHOOT:
                shootState();
                break;
        }
    }

    public void _on_photospace_body_entered(Node body)
    {
        if(body.GetType() == typeof(TestEnemy))
        {
            TestEnemy enemy = (TestEnemy)body;
            enemy.damage();
        }
    }

    public void _on_photoview_body_entered(Node body)
    {
        if(body.GetType() == typeof(TestEnemy))
        {
            TestEnemy enemy = (TestEnemy)body;
            enemy.show();
        }
    }
}
