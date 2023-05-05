using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans.Options;
using GodotProject.Content.Scripts.Characters.Wolf.NeutralWolf;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.WolfStans.NeutralWolfStans
{
    public class WolfAggresive : State<NeutralWolfController>
    {
        public override void Enter(NeutralWolfController Owner)
        {
            Owner.Speed *= 2;
            Owner.AiBody2D.CollisionLayer = 3;
            Owner.AiBody2D.AttackRangeComponent.GetChild<CollisionShape2D>(0).Disabled = false;
            Owner.AiBody2D.ObservationComponent.СhangeMemoryTime(20);
        }

        public override void Execute(NeutralWolfController Owner)
        {
            if (Owner.AiBody2D.ObservationComponent.PawnEnemy.HealthComponent.IsDead)
            {
                Owner.StateController.ChangeState(Owner.Idle);
            }

            StateOptions.ChoseDirectionRetailivelyFromPlayer(Owner);
            StateOptions.MovePawn(Owner);
        }

        public override void Exit(NeutralWolfController Owner)
        {
            Owner.Speed /= 2;
            Owner.AiBody2D.CollisionLayer = 10;
            Owner.AiBody2D.AttackRangeComponent.GetChild<CollisionShape2D>(0).Disabled = true;
            Owner.AiBody2D.ObservationComponent.СhangeMemoryTime(Owner.AiBody2D.MemoryTime);
            Owner.isAttaker = false;
        }
    }
}
