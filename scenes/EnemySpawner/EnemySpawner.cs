using Godot;
using System;

public partial class EnemySpawner : Node3D
{
    [Export]
    PackedScene enemyScene;
    [Export]
    float minRange;
    [Export]
    float maxRange;
    int countNumber = 0;
    Timer timer;
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
    }
    public void _on_timer_timeout()
    {
        RandomNumberGenerator random = new RandomNumberGenerator();
        random.Randomize();
        TestEnemy enemy = (TestEnemy)enemyScene.Instantiate();
        enemy.number = random.RandiRange(0,2);
        GetParent().AddChild(enemy);
        enemy.GlobalPosition = new Vector3(random.RandfRange(minRange, maxRange), this.GlobalPosition.Y, this.GlobalPosition.Z);
        if(countNumber < 10)
            timer.Start();
    }
}
