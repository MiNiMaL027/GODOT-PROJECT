using GodotProject.Content.Scripts.Ai.AiComponents.Stans.Options;
using GodotProject.Content.Scripts.Characters.Wolf;
using System;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.WolfStans
{
    public class WolfRest : State<WolfController>
    {
        public override void Enter(WolfController Owner)
        {
           
        }

        public override void Execute(WolfController Owner)
        {
            StateOptions.MovePawn(Owner);
        }

        public override void Exit(WolfController Owner)
        {
            
        }
    }
}
