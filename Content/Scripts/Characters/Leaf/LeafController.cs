using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans.LeafStans;
using GodotProject.Content.Scripts.Characters.Wolf;
using GodotProject.Content.Scripts.Player.PlayerComponent;

public partial class LeafController : RestController<LeafController>
{
    public State<LeafController> Shell { get; set; } = new LeafShell();

	public override void _Ready()
	{
        AiBody2D = GetNode<LeafPawn>("LeafBody");
        Animation = GetNode<AnimationPlayer>("LeafBody/Animation");
        WalkDuration = GetNode<Timer>("LeafBody/WalkDuration");
        Speed = 5f;
        StateController = new StateController<LeafController>(this);
        StateController.SetCurrentState(Idle);

        WalkDuration.Timeout += ChooseDirection;

        Attack = new AttackTransform() { Transform = new Vector2(9.5f, 21f), Rotation = 99f, Position = new Vector2(0f, -3f) };
        UpAttack = new AttackTransform() { Transform = new Vector2(5f, 26f), Rotation = 99f, Position = new Vector2(0f, -6f) };
    }

	public override void _Process(double delta)
	{
        Vector2 velocity = AiBody2D.Velocity;
        if (!AiBody2D.IsOnFloor())
            velocity.Y += gravity * (float)delta;

        AiBody2D.Velocity = velocity;
        AiBody2D.MoveAndSlide();
        StateController.Update();
    }

    public override void ChangeState(bool Aggresive = false)
    {
        if (isAggresive || Aggresive)
        {
            StateController.ChangeState(Shell);
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
        Animation.Play("Shell");
    }
    public void ShellState()
    {
        AiBody2D.HealthComponent.DefenseComponent.Spikes = 1;
        AiBody2D.BodyCollision.Block = true;
        (AiBody2D as LeafPawn).ChooseAttack(true);
    }

    #endregion
}
