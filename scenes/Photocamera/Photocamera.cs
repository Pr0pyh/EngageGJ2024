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
	ColorRect colorRect;
	PhotoManager photoManager;
	//privatne funkcije
	private void nodeInitialize()
	{
		nAnimPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		photoShot = GetNode<Area3D>("Photospace");
		photoView = GetNode<Area3D>("Photoview");
		colorRect = GetNode<CanvasLayer>("CanvasLayer").GetNode<ColorRect>("ColorRect");
		photoManager = GetNode<PhotoManager>("PhotoManager");
		photoShot.Monitoring = false;
		photoShot.Visible = false;
		photoView.Monitoring = false;
		photoView.Visible = false;
		colorRect.Visible = false;
	}
	public void shoot(Player player)
	{
		player.damage(10);
		nAnimPlayer.Play("shoot");
	}

	public void shoot2()
	{
		photoView.Visible = !photoView.Visible;
		photoView.Monitoring = !photoView.Monitoring;
	}
	// private void shootState()
	// {
	// 	if(Input.IsActionJustPressed("shoot"))
	// 	{
	// 		shoot();
	// 	}
	// 	if(Input.IsActionJustPressed("shoot2"))
	// 	{
	// 		shoot2();
	// 	}
	// 	if(Input.IsActionJustReleased("shoot2"))
	// 	{
	// 		shoot2();
	// 	}
	// }
	public override void _Ready()
	{
		nodeInitialize();
	}

	public override void _PhysicsProcess(double delta)
	{
		switch(state)
		{
			case STATE.SHOOT:
				// shootState();
				break;
		}
	}

	public void _on_photospace_body_entered(Node body)
	{
		if(body.GetType() == typeof(TestEnemy))
		{
			TestEnemy enemy = (TestEnemy)body;
			int number = enemy.damage(photoManager.activePicture, photoManager.activePictureCount);
			photoManager.obrisiSliku(number);
		}
		if(body.GetType() == typeof(Item))
		{
			Item item = (Item)body;
			photoManager.dodajSliku(item.number);
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

	public void _on_photoview_body_exited(Node body)
	{
		if(body.GetType() == typeof(TestEnemy))
		{
			TestEnemy enemy = (TestEnemy)body;
			enemy.unshow();
		}
	}
}
