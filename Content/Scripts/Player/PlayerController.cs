using Godot;
using GodotProject.Content.Scripts.Controllers;
using GodotProject.Content.Scripts.Player.PlayerComponent;

public partial class PlayerController : Controller
{ 
	public RayCast2D RayCast;
	public player CharacterBody;
	public Timer AccelerationSpeed;
	string _animRun = "Run";
	string _animIdle = "Idle";
	string _animJump = "Jump(Fall)";
	string _animAttack = "Attack(Idle)";
	public AttackTransform FirstAttack;
	public AttackTransform SecondAttack;
	public AttackTransform RunAttack;
	public bool IsCrouch;
	public bool IsJump;
	public bool IsAttack;
	public bool IsSecondAttack;
	public bool IsSlide;

	public override void _Ready()
	{
		Animation = GetNode<AnimationPlayer>("Player/AnimationPlayer");
		RayCast = GetNode<RayCast2D>("Player/RayCast2D");      
		CharacterBody = GetNode<player>("Player");
		AccelerationSpeed = GetNode<Timer>("Player/AccelerationSpeed");     

		AccelerationSpeed.Timeout += IncreaseAccelerationSpeed;

		FirstAttack = new AttackTransform() { Transform = new Vector2(15.30f, 39.53f), Rotation = 80f, Position = new Vector2(12f, -8f) };
		SecondAttack = new AttackTransform() { Transform = new Vector2(9.19f, 54.41f), Rotation = 90f, Position = new Vector2(1f, -11f) };
		RunAttack = new AttackTransform() { Transform = new Vector2(16.45f, 40.81f), Rotation = 80f, Position = new Vector2(18f, -2f) };       
	}

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
{
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("Left", "Right", "Down", "Jump");

		Vector2 velocity = CharacterBody.Velocity;

		Animation.SpeedScale = 0.5f;

		// Add the gravity.
		if (!CharacterBody.IsOnFloor())
			velocity.Y += gravity * (float)delta;

		if (!CharacterBody.isHurt)
		{        
			if (!IsSlide)
				Attack(direction, velocity);

			if (!IsAttack)
				MoveAndAnim(direction, velocity);

			if (IsAttack && !RayCast.IsColliding() && _animAttack == "Attack(Run)")
			{
				Animation.SpeedScale = 0.3f;
				CharacterBody.Velocity = velocity;

				CharacterBody.MoveAndSlide();
			}
		}      
	}

	public void MoveAndAnim(Vector2 direction, Vector2 velocity)
	{       
		// Crouch
		if (Input.IsActionJustPressed("Down") && velocity.X != 0 && !IsCrouch && _currentSpeed >= _startSpeed * 1.3f)
		{
			IsSlide = true;

			Animation.Play("Slide");
			CharacterBody.StartCrouch();
		}
		else if (Input.IsActionJustPressed("Down"))
		{
			Animation.Play("Crouch(Start)");
			_currentSpeed = _startSpeed / 3;
			_animIdle = "Crouch(Start)";
			_animRun = "Crouch(Start)";
		}
		else if (Input.IsActionJustReleased("Down") && !IsSlide)
		{
			Animation.Play("Crouch(Finish)");

			_animIdle = "Crouch(Finish)";
			_animRun = "Crouch(Finish)";
		}
	
		// Handle Jump.
		if (Input.IsActionJustPressed("Jump") && CharacterBody.IsOnFloor() && !IsSlide)
		{
			IsJump = true;
			int toPosition;
			_animJump = "Jump(Start)";

			if (_animRun == "Crouch(Start)")
			{
				velocity.Y = JumpVelocity + JumpVelocity/6;
				toPosition = 30;
			}
			else if(_animRun == "Crouch")
			{
				velocity.Y = JumpVelocity + JumpVelocity / 7;
				toPosition = 25;
			}
			else
			{
				velocity.Y = JumpVelocity;
				toPosition = 20;
			}           

			if (velocity.X == 0 && CharacterBody.Sprite.FlipH && IsJump)
			{
				Tween tween = GetTree().CreateTween();
				tween.TweenProperty(this, "position", new Vector2(Position.X - toPosition, Position.Y), 0.5f);
			}
			else if (velocity.X < 1 && !CharacterBody.Sprite.FlipH && IsJump)
			{
				Tween tween = GetTree().CreateTween();
				tween.TweenProperty(this, "position", new Vector2(Position.X + toPosition, Position.Y), 0.5f);
			}
		}

		if (!IsSlide)
		{ 
			if (direction.X != 0 && RayCast.IsColliding())
			{
				velocity.X = direction.X * _currentSpeed;

				CharacterBody.FlipCharacter(velocity);

				if(_currentSpeed == _startSpeed)
					Animation.SpeedScale = (_currentSpeed / Speed) / 2 + 0.1f;

				Animation.Play(_animRun);
			}
			else if (!RayCast.IsColliding())
			{
				Animation.Play(_animJump);

				_currentSpeed = _startSpeed;
			}
			else if(direction.X == 0 && RayCast.IsColliding())
			{
				velocity.X = Mathf.MoveToward(CharacterBody.Velocity.X, 0, _currentSpeed);

				if(!IsCrouch)
					_currentSpeed = _startSpeed;
				Animation.Play(_animIdle);
			}        
		}

		CharacterBody.Velocity = velocity;
		CharacterBody.MoveAndSlide();
	}

	public void Attack(Vector2 direction, Vector2 velocity)
	{      
		if (Input.IsActionJustPressed("Attack"))
		{
			CharacterBody.DamageArea.CollisionShape.Disabled = true;
			if (direction.X != 0 && !IsAttack && _currentSpeed >= _startSpeed * 1.3f || !RayCast.IsColliding())
			{
				_animAttack = "Attack(Run)";              
			}
			else if (direction.X == 0)
			{
				_animAttack = "Attack(Idle)";
			}

			EndCrouch();
		}

		if (Input.IsActionPressed("Attack"))
		{
			if (_animAttack == "Attack(Idle)")
			{
				if (!IsAttack)
					CharacterBody.FlipCharacter(velocity);             

				velocity.X = direction.X * _startSpeed/3;
			}
			else
				velocity.X = Mathf.MoveToward(CharacterBody.Velocity.X, 0, _currentSpeed);

			IsAttack = true;
			Animation.Play(_animAttack);

			if(!IsJump)
			{
				CharacterBody.Velocity = velocity;
				CharacterBody.MoveAndSlide();
			}
		}
	} 
	
	private void IncreaseAccelerationSpeed()
	{
		if (_currentSpeed <= Speed && !IsCrouch && !IsAttack && !CharacterBody.isHurt)
		{
			_currentSpeed += 2;
		}
	}
  
	# region CallMethod
	public void BeginCrouch()
	{
		IsCrouch = true;
		_animIdle = "Crouch";
		_animRun = "Crouch";
		_currentSpeed = _startSpeed/3;

		CharacterBody.StartCrouch();

		Animation.Play(_animRun);
	}

	public void EndCrouch()
	{
		IsCrouch = false;
		_animIdle = "Idle";
		_animRun = "Run";

		if(!IsAttack)
		   _currentSpeed = _startSpeed;

		CharacterBody.StopCrouch();
	}

	public void EndJump()
	{
		IsJump = false;
		_animJump = "Jump(Fall)";
	}

	public void StartIdleAttack()
	{
		_currentSpeed = _startSpeed/5;
		IsSecondAttack = false;

		CharacterBody.DamageArea.ChangeDamageArea(FirstAttack);
	}

	public void StartRunAttack()
	{
		if(!IsJump)
			_currentSpeed = 0;

		CharacterBody.DamageArea.ChangeDamageArea(RunAttack);
	}

	public void EndRunAttack()
	{
		_currentSpeed = _startSpeed;
		_animAttack = "Attack(Idle)";
		IsAttack = false;
	}

	public void EndIdleAttack()
	{
		_currentSpeed = _startSpeed;
		IsAttack = false;
	}

	public void StartHurt()
	{
		CharacterBody.isHurt = true;
		IsAttack = false;

		EndCrouch();

		CharacterBody.Velocity = new Vector2(0,CharacterBody.Velocity.Y);
	}

	public void EndHurt()
	{
		CharacterBody.isHurt = false;
	}

	public void EndSlide()
	{
		_currentSpeed = _startSpeed;
		IsSlide = false;

		CharacterBody.StopCrouch();
	}

	public void StartSecondAttack()
	{
		IsSecondAttack = true;

		CharacterBody.DamageArea.ChangeDamageArea(SecondAttack);
	}

	public void EndSecondAttack()
	{
		IsSecondAttack = false;
	}

	public void Dead()
	{
		CharacterBody.CollisionShape.Disabled = true;
	}

	#endregion
}
