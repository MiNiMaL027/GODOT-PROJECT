using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents;
using GodotProject.Content.Scripts.Characters.Interfaces;
using GodotProject.Content.Scripts.Controllers;
using GodotProject.Content.Scripts.enums;

namespace GodotProject.Content.Scripts.Characters
{
    public abstract partial class AiPawn : Pawn,IHaveSound
    {      
        new public AiController Controller { get; set; }
        public ObservationComponent ObservationComponent { get; set; }
        public AttackComponent AttackRangeComponent { get; set; }
        public AudioStreamPlayer2D Audio { get; set; }
        public AudioStreamMP3 WalkSound { get; set; }
        public Sprite2D Sprite { get; set; }
        public int MemoryTime { get; set; } = 5;
        public float BackToNormalTime { get; set; } = 0.1f;

        public virtual void FlipCharacter(float velocity)
        {
            if (velocity > 0)
            {
                MoveDirection = MoveDirection.Right;
                Sprite.FlipH = true;
            }
            else if (velocity < 0)
            {
                MoveDirection = MoveDirection.Left;
                Sprite.FlipH = false;
            }
        }
    }
}
