using Godot;

namespace GodotProject.Content.Scripts.Controllers
{
    public partial  class Controller : Node2D
    {
        public bool isAttack { get; set; }

        public const float Speed = 100.0f;
        public const float JumpVelocity = -200.0f;
        public AnimationPlayer Animation;
        public float _currentSpeed = Speed / 2;
        public const float _startSpeed = Speed / 2;
    }
}
