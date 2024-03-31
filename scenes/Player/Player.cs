using Godot;
using System;
using System.Data;

public partial class Player : CharacterBody3D
{
	//enum deklaracija
	enum STATE {
		MOVE
	}
	STATE state;

	//export varijable
	[Export]
	public float eSpeed;
	[Export]
	public float eSensitivity;
	[Export]
	public bool final;

	//Node access varijable
	Camera3D nCamera;
	Camera3D viewmodelCamera;
	Node3D hand;
	AnimationPlayer animPlayer;
	AnimationPlayer animPlayer2;
	Photocamera photocamera;
	ProgressBar healthBar;
	AudioStreamPlayer audioPlayer;
	AudioStreamPlayer audioPlayer2;
	AudioStreamPlayer audioPlayer3;
	//private promenjive
	float mouseMove;
	float sway = 2;
	[Export]
	public int health;
	double amount;
	double maxHOffset;
	double maxVOffset;
	double trauma;
	//private funkcije definicije
	private void move()
	{
		Godot.Vector3 moveVector = new Godot.Vector3(0.0f, 0.0f, 0.0f);
		if(Input.IsActionPressed("up"))
			moveVector -= nCamera.GlobalTransform.Basis.Z;
		if(Input.IsActionPressed("down"))
			moveVector += nCamera.GlobalTransform.Basis.Z;
		if(Input.IsActionPressed("right"))
			moveVector += nCamera.GlobalTransform.Basis.X;
		if(Input.IsActionPressed("left"))
			moveVector -= nCamera.GlobalTransform.Basis.X;
		moveVector = new Vector3(moveVector.X, 0.0f, moveVector.Z);
		if(moveVector != new Vector3(0.0f, 0.0f, 0.0f))
		{
			animPlayer.Play("walk");
			if(!audioPlayer.Playing)
				audioPlayer.Play();
		}
		else
		{
			animPlayer.Stop();
			audioPlayer.Stop();
		}
		viewmodelCamera.GlobalTransform = nCamera.GlobalTransform;
		Velocity = eSpeed*moveVector;
		MoveAndSlide();
	}
	private void swayMove(double delta)
	{
		if(mouseMove != null)
		{
			if(mouseMove>sway)
				hand.Position = hand.Position.Lerp(new Godot.Vector3(1.25f, 0.1f, -1.0f), (float)(delta*5));
			else if(mouseMove<-sway)
				hand.Position = hand.Position.Lerp(new Godot.Vector3(0.85f, -0.1f, -1.0f), (float)(delta*5));
			else
				hand.Position = hand.Position.Lerp(new Godot.Vector3(1.0f, 0.0f, -1.0f), (float)(delta*5));
		}
	}

	private void shootState()
	{
		if(Input.IsActionJustPressed("shoot"))
		{
			photocamera.shoot(this);
		}
		if(Input.IsActionJustPressed("shoot2"))
		{
			photocamera.shoot2();
		}
		if(Input.IsActionJustReleased("shoot2"))
		{
			photocamera.shoot2();
		}
	}
	public void quitInput()
	{
		if(Input.IsActionPressed("exit"))
			GetTree().Quit();
	}

	private void nodeInitialize()
	{
		nCamera = GetNode<Node3D>("Head").GetNode<Camera3D>("Camera3D");
		viewmodelCamera = nCamera.GetNode<SubViewportContainer>("SubViewportContainer").GetNode<SubViewport>("SubViewport").GetNode<Camera3D>("viewModel");
		hand = GetNode<Node3D>("Head").GetNode<Camera3D>("Camera3D").GetNode<Node3D>("Hand");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animPlayer2 = GetNode<AnimationPlayer>("AnimationPlayer2");
		photocamera = hand.GetNode<Photocamera>("Photocamera");
		healthBar = GetNode<CanvasLayer>("CanvasLayer").GetNode<ProgressBar>("Health");
		audioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		audioPlayer2 = GetNode<AudioStreamPlayer>("AudioStreamPlayer2");
		audioPlayer3 = GetNode<AudioStreamPlayer>("AudioStreamPlayer3");
	}

	private void shake()
	{
		amount = trauma;
		nCamera.HOffset = (float)(maxHOffset * amount * GD.RandRange(-1, 1));
		nCamera.VOffset = (float)(maxVOffset * amount * GD.RandRange(-1, 1));
	}

	private void shakeState(double delta)
	{
		if(!(trauma < 0.0))
		{
			shake();
			trauma = Mathf.Max((float)(trauma - 0.8*delta), 0.0f);
		}
	}

	//godot functions
	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseMotion mouseMotion)
		{
			mouseMove = -mouseMotion.Relative.X;
			nCamera.RotateX(Mathf.DegToRad(mouseMotion.Relative.Y*eSensitivity*-1));
			nCamera.RotationDegrees = new Vector3(Mathf.Clamp(nCamera.RotationDegrees.X, -75.0f, 75.0f), 0.0f, 0.0f);
			this.RotateY(Mathf.DegToRad(mouseMotion.Relative.X*eSensitivity*-1));
		}
	}
	public override void _Ready()
	{
		nodeInitialize();
		Input.MouseMode = Input.MouseModeEnum.Captured;
		maxHOffset = 1;
		maxVOffset = 1;
		healthBar.Value = health;
	}

	public override void _PhysicsProcess(double delta)
	{
		switch(state)
		{
			case STATE.MOVE:
				move();
				shootState();
				shakeState(delta);
				swayMove(delta);
				quitInput();
				break;
		}
	}

	public void damage(int number)
	{
		animPlayer2.Play("damage");
		if(!audioPlayer2.Playing)
			audioPlayer2.Play();
		trauma = number/100.0f;
		health -= number;
		healthBar.Value = health;
		if(health <= 0)
		{
			animPlayer2.Stop();
			animPlayer2.Play("death");
			//komentar
		}
	}

	public void heal(int number)
	{
		health = Mathf.Min(health+number, 100);
		healthBar.Value = health;
		animPlayer2.Play("heal");
		if(!audioPlayer3.Playing)
			audioPlayer3.Play();
		GD.Print(health);
	}

	public void _on_animation_player_2_animation_finished(String animName)
	{
		if(animName == "death")
		{
			if(!final)
			{
				GetTree().ReloadCurrentScene();
			}
			else
			{
				GetTree().ChangeSceneToFile("res://scenes/Level/Ending.tscn");
			}
		}
	}
};
