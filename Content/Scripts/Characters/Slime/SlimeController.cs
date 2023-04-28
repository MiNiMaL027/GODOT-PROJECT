using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans.SlimeStans;
using GodotProject.Content.Scripts.Controllers;
using GodotProject.Content.Scripts.Player.PlayerComponent;
using System;
using static Godot.TextServer;

public partial class SlimeController : AiController
{
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    public State<SlimeController> Rest { get; set; } = new Rest();
    public State<SlimeController> Aggression { get; set; } = new Aggression();
    public State<SlimeController> Idle { get; set; } = new Idle();
    public StateController<SlimeController> StanController { get; set; }

    public override void _Ready()
    {
        AiBody2D = GetNode<SlimePawn>("AiBody");
        Animation = GetNode<AnimationPlayer>("AiBody/Animation");
        WalkDuration = GetNode<Timer>("AiBody/WalkDuration");
        Speed = 10f;
        StanController = new StateController<SlimeController>(this);
        StanController.SetCurrentState(Rest);
        WalkDuration.Timeout += ChooseDirection;

        Attack = new AttackTransform() { Transform = new Vector2(11f, 22f), Rotation = 0f, Position = new Vector2(8f, -6f) };
        UpAttack = new AttackTransform() { Transform = new Vector2(7f, 28f), Rotation = 99f, Position = new Vector2(0f, -6f) };
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = AiBody2D.Velocity;
        if (!AiBody2D.IsOnFloor())
            velocity.Y += gravity * (float)delta;

        if (!isAttack)
        {
            AiBody2D.Velocity = velocity;
            AiBody2D.MoveAndSlide();
            StanController.Update();
        }
    }

    public void ChooseDirection()
    {
        var rnd = new Random();
        var direction = rnd.Next(0,3);

        if(direction > 1)
        {
            AiBody2D.FlipCharacter(1);
            StanController.ChangeState(Rest);
        }
        else if(direction < 1)
        {
            AiBody2D.FlipCharacter(-1);
            StanController.ChangeState(Rest);
        }
        else if(direction == 1)
            StanController.ChangeState(Idle);
    }

    public override void ChangeState()
    {
        if (isAggresive)
        {
            StanController.ChangeState(Aggression);
        }
        else
            StanController.ChangeState(Idle);
    }

    #region CallMethod

    public virtual void Dead()
    {
        this.QueueFree();
    }

    public void Endhurt()
    {
        AiBody2D.isHurt = false;
    }

    #endregion
}
