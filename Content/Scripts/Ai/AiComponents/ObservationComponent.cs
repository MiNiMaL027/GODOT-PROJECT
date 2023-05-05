using Godot;
using GodotProject.Content.Scripts.Characters;
using static Godot.RenderingDevice;

namespace GodotProject.Content.Scripts.Ai.AiComponents
{
    public partial class ObservationComponent : Area2D
    {
        public AiPawn AiOwner { get; set; }
        public Pawn PawnEnemy { get; set; }
        public Timer MemoryTimer { get; set; }
        public CollisionShape2D RightCollisionShape { get; set; }
        public CollisionShape2D LeftCollisionShape { get; set; }
        public Vector2 CollisionShapeScale { get; set; }
        public Vector2 CollisionShapePosition { get; set; }

        public void Init(AiPawn pawn)
        {
            AiOwner = pawn;
            LeftCollisionShape = GetChild<CollisionShape2D>(0);
            RightCollisionShape = GetChild<CollisionShape2D>(1);
            CollisionShapeScale = (RightCollisionShape.Shape as RectangleShape2D).Size;
            CollisionShapePosition = RightCollisionShape.Position;

            MemoryTimer = new Timer();
            MemoryTimer.WaitTime = AiOwner.MemoryTime;
            MemoryTimer.OneShot = true;
            MemoryTimer.Autostart = false;
            AddChild(MemoryTimer);
            MemoryTimer.Timeout += ForgetEnemy;

            this.BodyEntered += FindEnemy;
            this.BodyExited += LostEnemy;
        }

        public void СhangeMemoryTime(float time)
        {
            MemoryTimer.WaitTime = time;
        }

        public void FindEnemy(Node2D area)
        {
            if (area is player player)
            {
                PawnEnemy = player;

                AiOwner.Controller.WalkDuration.Stop();

                AiOwner.Controller.isAggresive = true;

                if (MemoryTimer.TimeLeft != 0)
                {
                    MemoryTimer.Stop();
                    AiOwner.Controller.ChangeState(true);
                }
                else
                {
                    MemoryTimer.Stop();

                    AiOwner.Controller.ChangeState();
                }
            }
        }

        public void LostEnemy(Node2D area)
        {
            if (area is player && !AiOwner.HealthComponent.IsDead)
                MemoryTimer.Start();
        }

        public void ForgetEnemy()
        {
            PawnEnemy = null;
            AiOwner.Velocity = new Vector2(0, 0);

            AiOwner.Controller.WalkDuration.Start();

            AiOwner.Controller.isAggresive = false;
            AiOwner.Controller.isAttaker = false;

            AiOwner.Controller.ChangeState();
        }
    }
}
