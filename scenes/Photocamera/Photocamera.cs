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
    //privatne funkcije
    private void nodeInitialize()
    {
        nAnimPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }
    private void shoot()
    {
        nAnimPlayer.Play("shoot");
    }
    private void shootState()
    {
        if(Input.IsActionJustPressed("shoot"))
            shoot();
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
}
