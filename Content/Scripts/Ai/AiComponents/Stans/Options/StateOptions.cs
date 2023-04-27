using Godot;
using GodotProject.Content.Scripts.Controllers;
using GodotProject.Content.Scripts.enums;

namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans.Options
{
    public static class StateOptions
    { 
        public static void MovePawn(AiController Owner)
        {
            if (Owner.AiBody2D.MoveDirection == MoveDirection.Right)
            {
                Owner.AiBody2D.FlipCharacter(1);
                MoveToDirection(Owner, 1f);
            }
            if (Owner.AiBody2D.MoveDirection == MoveDirection.Left)
            {
                Owner.AiBody2D.FlipCharacter(-1);
                MoveToDirection(Owner, -1f);
            }
        }

        private static void MoveToDirection(AiController Owner, float velocity)
        {
            if (Owner.AiBody2D.isHurt)
            {
                Owner.AiBody2D.Velocity = new Vector2(0f, Owner.AiBody2D.Velocity.Y);
            }
            else
            {
                Owner.AiBody2D.Velocity = new Vector2(velocity * Owner.Speed, Owner.AiBody2D.Velocity.Y);
                Owner.Animation.Play("Walk");

            }
        }

        public static void ChoseDirectionRetailivelyFromPlayer(AiController Owner)
        {
            if (Owner.AiBody2D.ObservationComponent.PawnEnemy.GlobalPosition.X >= Owner.AiBody2D.GlobalPosition.X)
            {
                Owner.AiBody2D.MoveDirection = MoveDirection.Right;
            }
            else
            {
                Owner.AiBody2D.MoveDirection = MoveDirection.Left;
            }
        }
    }
}
