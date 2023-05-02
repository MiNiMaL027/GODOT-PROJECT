using Godot;
using GodotProject.Content.Scripts.Characters.Wolf;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.Common
{
    public class Idle<T> : State<T>
    {
        public override void Enter(T Owner)
        {
            if (Owner is RestController<T> _Owner)
            {
                _Owner.Animation.Play("Idle");
                _Owner.AiBody2D.Velocity = new Vector2(0f, _Owner.AiBody2D.Velocity.Y);
            }
        }

        public override void Execute(T Owner)
        {
            
        }

        public override void Exit(T Owner)
        {
            
        }
    }
}
