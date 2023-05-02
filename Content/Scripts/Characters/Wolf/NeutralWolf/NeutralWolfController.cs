using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans;
using GodotProject.Content.Scripts.Ai.AiComponents;
using GodotProject.Content.Scripts.Player.PlayerComponent;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans.WolfStans.NeutralWolfStans;

namespace GodotProject.Content.Scripts.Characters.Wolf.NeutralWolf
{
    public partial class NeutralWolfController : RestController<NeutralWolfController>
    {
        public State<NeutralWolfController> Aggression { get; set; } = new WolfAggresive();
        public State<NeutralWolfController> Wary { get; set; } = new WolfWary();

        public override void _Ready()
        {
            
            AiBody2D = GetNode<NeutralWolfPawn>("WolfBody");
            Animation = GetNode<AnimationPlayer>("WolfBody/Animation");
            WalkDuration = GetNode<Timer>("WolfBody/WalkDuration");
            Speed = 30f;
            StateController = new StateController<NeutralWolfController>(this);
            StateController.SetCurrentState(Rest);

            Attack = new AttackTransform() { Transform = new Vector2(7f, 26f), Rotation = 0f, Position = new Vector2(18f, -7f) };
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
            if (isAttaker || Aggresive)
                StateController.ChangeState(Aggression);

            else if (isAggresive)
                StateController.ChangeState(Wary);

            else
                StateController.ChangeState(Rest);
        }

        #region CallMethod

        public void Dead()
        {
            this.QueueFree();
        }

        public void Endhurt()
        {
            AiBody2D.isHurt = false;
            isAttaker = true;
            ChangeState(true);
        }

        #endregion
    }
}
