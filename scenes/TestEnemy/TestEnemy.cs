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
	
	public override void _PhysicsProcess(double delta)
	{
		switch (this.state) 
		{
			case STATE.IDLE :
			{
				GD.Print("IDLE STATE");
				MoveAndSlide();
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
	
	private void _on_timer_timeout()
	{
		if (this.state == STATE.IDLE)
		{
			Random rnd = new Random();
			float randDeg = (float)rnd.NextDouble()*360.0f;
			float randRad = randDeg * (float)Math.PI / 180.0f;
			this.RotateY(randRad);		
			this.Velocity = new Vector3(1.0f, 0.0f, 0.0f);
			//this.Velocity = this.Velocity.Rotated(new Vector3(0.0f, 1.0f, 0.0f), randRad);
		}
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
