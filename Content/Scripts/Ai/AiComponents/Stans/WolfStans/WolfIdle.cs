using Godot;
using GodotProject.Content.Scripts.Characters.Wolf;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.WolfStans
{
    public class WolfIdle : State<WolfController>
    {
        public override void Enter(WolfController Owner)
        {
            Owner.Animation.Play("Idle");
            Owner.AiBody2D.Velocity = new Vector2(0f, Owner.AiBody2D.Velocity.Y);
        }

        public override void Execute(WolfController Owner)
        {
            
        }

        public override void Exit(WolfController Owner)
        {
            
        }
    }
}
