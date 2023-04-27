using Godot;
using GodotProject.Content.Scripts.Player.PlayerComponent;

namespace GodotProject.Content.Scripts.Controllers
{
    public abstract partial class AiController : Controller
    {
        public bool isAttack { get; set; }
        new public float Speed { get; set; }
        public bool isAggresive { get; set; }
        public Timer WalkDuration { get; set; }
        public SlimePawn AiBody2D { get; set; }
        public AttackTransform Attack { get; set; }
        public AttackTransform UpAttack { get; set; }
        public abstract void ChangeState();
    }
}
