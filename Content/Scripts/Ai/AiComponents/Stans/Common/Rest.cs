using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans.Options;
using GodotProject.Content.Scripts.Characters.Wolf;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.Common
{
    public class Rest<T> : State<T>
    {
        public override void Enter(T Owner)
        {
            if (Owner is RestController<T> _Owner)
            {
                _Owner.AiBody2D.Audio.Stream = _Owner.AiBody2D.WalkSound;
                _Owner.AiBody2D.Audio.Play();
            }
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
            if (Owner is RestController<T> _Owner)
            {              
                _Owner.AiBody2D.Audio.Stop();
            }
        }
    }
}
