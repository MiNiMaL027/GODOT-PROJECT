using GodotProject.Content.Scripts.Ai.AiComponents.Stans.Options;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.SlimeStans
{
    public class SlimeAggression : State<SlimeController>
    {
        public override void Enter(SlimeController Owner)
        {
            Owner.Speed *= 2;
        }

        public override void Execute(SlimeController Owner)
        {
            if (Owner.AiBody2D.ObservationComponent.PawnEnemy.HealthComponent.IsDead)
                Owner.StateController.ChangeState(Owner.Idle);
            StateOptions.ChoseDirectionRetailivelyFromPlayer(Owner);
            StateOptions.MovePawn(Owner);
        }

        public override void Exit(SlimeController Owner)
        {
            Owner.Speed /= 2;
        }     
    }
}
