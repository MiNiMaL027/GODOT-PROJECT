using GodotProject.Content.Scripts.Ai.AiComponents.Stans.Options;
using GodotProject.Content.Scripts.Characters.Wolf;
using System;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.WolfStans.FriendlyWolfStans
{
    public class WolfAfraid : State<FriendlyWolfController>
    {
        public override void Enter(FriendlyWolfController Owner)
        {
            Owner.Speed *= 2;
        }

        public override void Execute(FriendlyWolfController Owner)
        {
            StateOptions.ChoseDirectionRandom(Owner);
            StateOptions.MovePawn(Owner);
        }

        public override void Exit(FriendlyWolfController Owner)
        {
            Owner.Speed /= 2;
        }
    }
}
