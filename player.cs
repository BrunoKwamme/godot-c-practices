using Godot;
using System;

public partial class player : Area2D
{
	
	[Export]
	public int Speed {get; set;} = 200;
	public Vector2 ScreenSize;
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		//Hide();
	}

	public override void _Process(double delta)
	{
		
		var velocity = Vector2.Zero; // The player's movement vector.
		velocity.Y+=1;
		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
		}
		if (Input.IsActionPressed("Jump"))
		{
			velocity.Y -= 100;
		}
		if (Input.IsActionPressed("move_left"))
		{
			velocity.X -= 1;
		}
		

	   var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

	   if (velocity.Length() > 0)
	   {
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
	   }

		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
		if (velocity.X != 0)
		{
			animatedSprite2D.Animation = "Run";
			animatedSprite2D.FlipV = false;
			// See the note below about boolean assignment.
			animatedSprite2D.FlipH = velocity.X < 0;
		}
		else if (velocity.X == 0)
		{
			animatedSprite2D.Animation = "Idle";
			animatedSprite2D.Play();
		}
		if (velocity.X < 0)
		{
			animatedSprite2D.FlipH = true;
		}
	}
		
}

