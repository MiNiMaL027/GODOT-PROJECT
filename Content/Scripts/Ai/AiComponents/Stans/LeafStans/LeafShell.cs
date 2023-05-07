using Godot;
using System;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.LeafStans
{
    public class LeafShell : State<LeafController>
    {
        public override void Enter(LeafController Owner)
        {
            Owner.Animation.Play("Shell");
            Owner.AiBody2D.Velocity = new Vector2(0f, Owner.AiBody2D.Velocity.Y);
            Owner.AiBody2D.BodyCollision.ChangeCollisionShape(7.33f, 19.33f);
        }

        public override void Execute(LeafController Owner)
        {
            
        }

        public override void Exit(LeafController Owner)
        {
            Owner.AiBody2D.HealthComponent.DefenseComponent.Spikes = 0;
            Owner.AiBody2D.BodyCollision.Block = false;
            Owner.AiBody2D.BodyCollision.RevertToPreviousShape();
            (Owner.AiBody2D as LeafPawn).ChooseAttack(false);
        }
    }
}
