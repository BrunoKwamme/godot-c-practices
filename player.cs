using Godot;
using System;

public partial class player : Area2D
{
	[Signal]
	public delegate void HitEventHandler();
	
	[Export]
	public int Speed {get; set;} = 120;
	public Vector2 ScreenSize;

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		//Hide();
	}
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero; // The player's movement vector.

		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
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
	//    else
	//    {
	// 	   animatedSprite2D.Stop();
	//    }
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
		// else
		// {
		// 	animatedSprite2D.FlipH = false;
		// }
		}
		private void _on_body_entered(Node2D body)
		{
			Hide(); // Player disappears after being hit.
			EmitSignal(SignalName.Hit);
			// Must be deferred as we can't change physics properties on a physics callback.
			GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
		}
		public void Start(Vector2 position)
		{
			Position = position;
			Show();
			GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
		}
}

