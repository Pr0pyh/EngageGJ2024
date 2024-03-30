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

	//Node access varijable
	Camera3D nCamera;
	Node3D hand;
	AnimationPlayer animPlayer;
	//private promenjive
	float mouseMove;
	float sway = 2;
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
			animPlayer.Play("walk");
		else
			animPlayer.Stop();
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

	public void quitInput()
	{
		if(Input.IsActionPressed("exit"))
			GetTree().Quit();
	}

	private void nodeInitialize()
	{
		nCamera = GetNode<Node3D>("Head").GetNode<Camera3D>("Camera3D");
		hand = GetNode<Node3D>("Head").GetNode<Camera3D>("Camera3D").GetNode<Node3D>("Hand");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
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
	}

	public override void _PhysicsProcess(double delta)
	{
		switch(state)
		{
			case STATE.MOVE:
				move();
				swayMove(delta);
				quitInput();
				break;
		}
	}
};
