using Godot;
using System;

public partial class Level : Node3D
{
	[Export]
	public int number;
	[Export]
	Finish finish;

	public override void _Ready()
	{
		finish = GetNode<Finish>("Finish");
	}
	public void lowerNumber()
	{
		number--;
		if(number <= 0)
			finish.pristupAllow();
	}
	//komentar
	
}
