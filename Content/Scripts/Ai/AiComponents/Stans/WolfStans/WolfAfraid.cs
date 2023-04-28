using GodotProject.Content.Scripts.Ai.AiComponents.Stans.Options;
using GodotProject.Content.Scripts.Characters.Wolf;
using System;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.WolfStans
{
    public class WolfAfraid : State<WolfController>
    {
        public override void Enter(WolfController Owner)
        {
            Owner.Speed *= 2;
        }

        public override void Execute(WolfController Owner)
        {
            StateOptions.ChoseDirectionRandom(Owner);
            StateOptions.MovePawn(Owner);
        }

        public override void Exit(WolfController Owner)
        {
            Owner.Speed /= 2;
        }
    }
}
