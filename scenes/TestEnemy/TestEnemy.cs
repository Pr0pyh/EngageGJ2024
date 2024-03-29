using Godot;
using System;

public partial class TestEnemy : CharacterBody3D
{	
	enum STATE {
		IDLE,
		FOLLOW
	}
	
	STATE state;
	Player player;
	
	public override void _Ready()
	{
		state = STATE.FOLLOW;
		player = GetParent().GetNode<Player>("Player");
		GD.Print(player == null);
	}
	
	public override void _Process(double delta)
	{
		switch (this.state) 
		{
			case STATE.IDLE :
			{
				GD.Print("IDLE STATE");
				break;	
			} 
			case STATE.FOLLOW :
			{
				followPlayer((float)delta);
				break;	
			}
		}
	}
	
		
	public void damage()
	{
		GD.Print("pozovi");
	}

	public void show()
	{
		GD.Print("pokazi");
	}
	
	private void followPlayer(float delta)
	{
		Vector3 newPos = this
						.GlobalPosition
						.MoveToward(player.GlobalPosition, (float)delta);
		this.GlobalPosition = newPos;
	}
	
	private void _on_area_3d_body_entered(Node3D body)
	{
		if (this.state != STATE.FOLLOW) this.state = STATE.FOLLOW;
	}
	
	private void _on_area_3d_body_exited(Node3D body)
	{
		if (this.state != STATE.IDLE) this.state = STATE.IDLE;
	}
}
