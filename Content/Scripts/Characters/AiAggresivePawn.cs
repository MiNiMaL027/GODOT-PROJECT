using Godot;
using GodotProject.Content.Scripts.enums;

namespace GodotProject.Content.Scripts.Characters
{
    public abstract partial class AiAggresivePawn : AiPawn
    {
        public abstract void ChooseAttack();
        public abstract void FinishAttack();
        public string _animAttack { get; set; }

        new public void FlipCharacter(float velocity)
        {
            if (velocity > 0)
            {
                MoveDirection = MoveDirection.Right;
                Sprite.FlipH = true;
                DamageArea.CollisionShape.Position = new Vector2(-DamageArea.StartAreaPosition.X, DamageArea.StartAreaPosition.Y);
                DamageArea.CollisionShape.Rotation = -DamageArea.StartAreaRotation;

                (ObservationComponent.LeftCollisionShape.Shape as RectangleShape2D).Size = new Vector2(
                    ObservationComponent.CollisionShapeScale.X / 2,
                    ObservationComponent.CollisionShapeScale.Y);
                ObservationComponent.LeftCollisionShape.Position = new Vector2(
                    -ObservationComponent.CollisionShapePosition.X / 2,
                    ObservationComponent.CollisionShapePosition.Y);
                (ObservationComponent.RightCollisionShape.Shape as RectangleShape2D).Size = ObservationComponent.CollisionShapeScale;
                ObservationComponent.RightCollisionShape.Position = ObservationComponent.CollisionShapePosition;
            }
            else if (velocity < 0)
            {
                MoveDirection = MoveDirection.Left;
                Sprite.FlipH = false;
                DamageArea.CollisionShape.Position = new Vector2(DamageArea.StartAreaPosition.X, DamageArea.StartAreaPosition.Y);
                DamageArea.CollisionShape.Rotation = DamageArea.StartAreaRotation;

                (ObservationComponent.RightCollisionShape.Shape as RectangleShape2D).Size = new Vector2(
                    ObservationComponent.CollisionShapeScale.X / 2,
                    ObservationComponent.CollisionShapeScale.Y);
                ObservationComponent.RightCollisionShape.Position = new Vector2(
                    ObservationComponent.CollisionShapePosition.X / 2,
                    ObservationComponent.CollisionShapePosition.Y);
                (ObservationComponent.LeftCollisionShape.Shape as RectangleShape2D).Size = ObservationComponent.CollisionShapeScale;
                ObservationComponent.LeftCollisionShape.Position = new Vector2(
                    -ObservationComponent.CollisionShapePosition.X,
                    ObservationComponent.CollisionShapePosition.Y);
            }
        }
    }
}
