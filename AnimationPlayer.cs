using Godot;

public partial class Character : player
{
	private AnimationPlayer _animationPlayer;

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _Process(float _delta)
	{
		if (Input.IsActionPressed("ui_right"))
		{
			_animationPlayer.Play("walking");
		}
		else
		{
			_animationPlayer.Stop();
		}
	}
}
