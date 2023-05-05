using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans.SlimeStans;
using GodotProject.Content.Scripts.Characters;
using GodotProject.Content.Scripts.Characters.Wolf;
using GodotProject.Content.Scripts.Player.PlayerComponent;
using System;
using static Godot.TextServer;

public partial class SlimeController : RestController<SlimeController>
{
    public State<SlimeController> Aggression { get; set; } = new SlimeAggression();

    public string _animAttack { get; set; }

    public override void _Ready()
    {
        AiBody2D = GetNode<SlimePawn>("AiBody");
        Animation = GetNode<AnimationPlayer>("AiBody/Animation");
        WalkDuration = GetNode<Timer>("AiBody/WalkDuration");
        Speed = 10f;
        StateController = new StateController<SlimeController>(this);
        StateController.SetCurrentState(Idle);

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
            StateController.Update();
        }
    }

    public override void ChangeState(bool Aggresive = false)
    {
        if (isAggresive || Aggresive)
        {
            StateController.ChangeState(Aggression);
        }
        else
            StateController.ChangeState(Idle);
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
