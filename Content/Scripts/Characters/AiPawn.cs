using Godot;
using GodotProject.Content.Scripts.Controllers;

namespace GodotProject.Content.Scripts.Characters
{
    public abstract partial class AiPawn : Pawn
    {      
        new public AiController Controller { get; set; }
        public ObservationComponent ObservationComponent { get; set; }
        public AttackComponent AttackRangeComponent { get; set; }
        public int MemoryTime { get; set; } = 5;
        public float BackToNormalTime { get; set; } = 0.1f;

        public abstract void FlipCharacter(float velocity);
    }
}
