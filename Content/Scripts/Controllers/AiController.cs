using Godot;
using GodotProject.Content.Scripts.Characters;
using GodotProject.Content.Scripts.Player.PlayerComponent;

namespace GodotProject.Content.Scripts.Controllers
{
    public abstract partial class AiController : Controller
    {
        public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
        new public float Speed { get; set; }
        public bool isAggresive { get; set; }
        public bool isAttaker { get; set; }
        public Timer WalkDuration { get; set; }
        public AiPawn AiBody2D { get; set; }
        public AttackTransform Attack { get; set; }
        public AttackTransform UpAttack { get; set; }
        public abstract void ChangeState(bool Aggresive = false);
    }
}
