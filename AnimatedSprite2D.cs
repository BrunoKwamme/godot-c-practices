using Godot;

public partial class Character : CharacterBody2D
{
	private AnimationPlayer _animationPlayer;

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer2>("AnimationPlayer2");
	}

	public override void _Process(float _delta)
	{
		if (Input.IsActionPressed("ui_right"))
		{
			_animationPlayer.Play("walk");
		}
		else
		{
			_animationPlayer.Stop();
		}
	}
}
