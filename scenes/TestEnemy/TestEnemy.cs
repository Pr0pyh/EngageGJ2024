using Godot;
using System;

public partial class TestEnemy : CharacterBody3D
{	
	enum STATE {
		IDLE,
		FOLLOW,
		HURT,
		HEALED
	}
	
	[Export]
	public int number;

	STATE state;
	Player player;
	GpuParticles3D particle;
	MeshInstance3D dream;
	AnimationPlayer animPlayer;
	AnimationPlayer animMovePlayer;
	AudioStreamPlayer audioPlayer;
	AudioStreamPlayer audioPlayer2;
	AudioStreamPlayer3D audioPlayer3D;
	Timer resetTimer;
	Timer hurtTimer;
	Area3D followArea;
	Area3D attackArea;
	Node3D darkMan;
	Node3D lightMan;
	Level level;
	
	public override void _Ready()
	{
		state = STATE.IDLE;
		resetTimer = GetNode<Timer>("Timer");
		hurtTimer = GetNode<Timer>("HurtTimer");
		player = GetParent().GetNode<Player>("Player");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animMovePlayer = GetNode<Node3D>("anim").GetNode<AnimationPlayer>("AnimationPlayer");
		particle = GetNode<GpuParticles3D>("GPUParticles3D");
		followArea = GetNode<Area3D>("Area3D");
		attackArea = GetNode<Area3D>("AttackArea");
		darkMan = GetNode<Node3D>("anim");
		lightMan = GetNode<Node3D>("ImageToStl_com_npc");
		audioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		audioPlayer2 = GetNode<AudioStreamPlayer>("AudioStreamPlayer2");
		audioPlayer3D = GetNode<AudioStreamPlayer3D>("AudioStreamPlayer3D");
		level = GetParent<Level>();
		// if(number == 0)
		// 	dream = GetNode<MeshInstance3D>("Photo1");
		// if(number == 1)
		// 	dream = GetNode<MeshInstance3D>("Photo2");
		// if(number == 2)
		// 	dream = GetNode<MeshInstance3D>("Photo3");
		dream = GetNode<MeshInstance3D>($"Photo{number+1}");
		audioPlayer3D.Play();
		resetTimer.Start();
		darkMan.Visible = true;
		lightMan.Visible = false;
		GD.Print(player == null);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		switch (this.state) 
		{
			case STATE.IDLE :
			{
				animMovePlayer.Play("ArmatureAction");
				checkDistance();
				MoveAndSlide();
				break;	
			} 
			case STATE.FOLLOW :
			{
				checkDistance();
				followPlayer((float)delta);
				break;	
			}
			case STATE.HURT :
			{
				animMovePlayer.Play("flashed");
				checkIfHurt();
				break;
			}
			case STATE.HEALED :
			{
				break;
			}
		}
	}
	
		
	public int damage(int sentNumber, int count)
	{
		if(state != STATE.HEALED)
		{
			audioPlayer.Play();
			this.state = STATE.HURT;
			this.hurtTimer.WaitTime = 1.5f;
			this.hurtTimer.OneShot = true;
			this.hurtTimer.Start();
			followArea.Monitoring = false;
			GD.Print("pozovi");
			if(sentNumber == number)
			{
				audioPlayer3D.Stop();
				audioPlayer2.Play();
				level.lowerNumber();
				darkMan.Visible = false;
				lightMan.Visible = true;
				particle.Emitting = true;
				this.state = STATE.HEALED;
				this.hurtTimer.Stop();
				followArea.Monitoring = false;
				//animPlayer.Play("death");
				return number;
			}
			return -1;
		}
		
		return -1;
	}

	public void checkDistance()
	{
		if(player.GlobalTransform.Origin.DistanceTo(this.GlobalTransform.Origin) > 5.0f)
			state = STATE.IDLE;
		else
			state = STATE.FOLLOW;
	}
	public void show()
	{
		dream.Visible = true;
		GD.Print("pokazi");
	}

	public void unshow()
	{
		dream.Visible = false;
		GD.Print("unpokazi");
	}
	
	private void followPlayer(float delta)
	{
		animMovePlayer.Play("ArmatureAction");
		Vector3 playerPos = player.GlobalPosition;
		this.LookAt(
			new Vector3(playerPos.X, this.GlobalPosition.Y, playerPos.Z),
			new Vector3(0.0f, 1.0f, 0.0f),
			false
		);
		Vector3 newPos = this
						.GlobalPosition
						.MoveToward(player.GlobalPosition, (float)delta);
		this.GlobalPosition = newPos;
	}

	// ANDREJ LEPO PULLUJ
	public void _on_attack_area_body_entered(Node3D body)
	{
		if (body is Player)
		{
			Player player = (Player)body;
			if(state == STATE.HEALED)
				player.heal(30);
			else
				player.damage(30);
		}
	}
	
	public void _on_timer_timeout()
	{
		resetTimer.Start();
		if (this.state == STATE.IDLE)
		{
			Random rnd = new Random();
			float randDeg = (float)rnd.NextDouble()*360.0f;
			float randRad = randDeg * (float)Math.PI / 180.0f;
			this.RotateY(randRad);		
			this.Velocity = this.Transform.Basis.Z;
			// this.Velocity = this.Velocity.Rotated(new Vector3(0.0f, 1.0f, 0.0f), randRad);
		}
	}
	
	public void checkIfHurt()
	{
		if (this.hurtTimer.TimeLeft <= 0.1f) 
		{
			if(state != STATE.HEALED) this.state = STATE.IDLE;
			followArea.Monitoring = true;
		}
	}
	
	public void _on_area_3d_body_entered(Node3D body)
	{
		// this.state = STATE.FOLLOW;
	}
	
	public void _on_area_3d_body_exited(Node3D body)
	{
		// this.state = STATE.IDLE;
	}

	//public void _on_animation_player_animation_finished(String animName)
	//{
		//if(animName == "death")
			//QueueFree();
	//}
}
