using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans.Options;
using GodotProject.Content.Scripts.Characters.Wolf.NeutralWolf;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.WolfStans.NeutralWolfStans
{
    public class WolfWary : State<NeutralWolfController>
    {
        public override void Enter(NeutralWolfController Owner)
        {
            Owner.Animation.Play("Idle");
            Owner.AiBody2D.Velocity = new Vector2(0f, Owner.AiBody2D.Velocity.Y);
        }

        public override void Execute(NeutralWolfController Owner)
        {
            if(Owner.AiBody2D.ObservationComponent.PawnEnemy.GlobalPosition.X - 10 > Owner.AiBody2D.GlobalPosition.X)
                Owner.AiBody2D.FlipCharacter(1);

            else if(Owner.AiBody2D.ObservationComponent.PawnEnemy.GlobalPosition.X + 10 < Owner.AiBody2D.GlobalPosition.X)
                Owner.AiBody2D.FlipCharacter(-1);
        }

        public override void Exit(NeutralWolfController Owner)
        {
            
        }
    }
}
