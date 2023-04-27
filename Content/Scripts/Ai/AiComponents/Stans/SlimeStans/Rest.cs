using GodotProject.Content.Scripts.Ai.AiComponents.Stans.Options;
using GodotProject.Content.Scripts.Controllers;
using static Godot.TextServer;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.SlimeStans
{
    public class Rest : State<SlimeController>
    {
        public override void Enter(SlimeController Owner)
        {

        }

        public override void Execute(SlimeController Owner)
        {
            StateOptions.MovePawn(Owner);
        }

        public override void Exit(SlimeController Owner)
        {

        }


    }
}
