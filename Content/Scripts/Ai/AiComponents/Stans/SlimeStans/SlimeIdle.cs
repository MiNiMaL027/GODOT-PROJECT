using Godot;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.SlimeStans
{
    public class SlimeIdle : State<SlimeController>
    {
        public override void Enter(SlimeController Owner)
        {
            Owner.Animation.Play("Idle");
            Owner.AiBody2D.Velocity = new Vector2(0f, Owner.AiBody2D.Velocity.Y);
        }

        public override void Execute(SlimeController Owner)
        {

        }

        public override void Exit(SlimeController Owner)
        {

        }
    }
}
