using GodotProject.Content.Scripts.Ai.AiComponents.Stans.Options;
using GodotProject.Content.Scripts.Characters.Wolf;
using GodotProject.Content.Scripts.Controllers;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.Common
{
    public class Rest<T> : State<T>
    {
        public override void Enter(T Owner)
        {
            
        }

        public override void Execute(T Owner)
        {
            if(Owner is RestController<T> _Owner)
            {
                StateOptions.MovePawn(_Owner);
            }
               
        }

        public override void Exit(T Owner)
        {
            
        }
    }
}
