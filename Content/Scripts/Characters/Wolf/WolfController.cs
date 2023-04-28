using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans;
using GodotProject.Content.Scripts.Controllers;
using System;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans.WolfStans;
using GodotProject.Content.Scripts.Characters.CharacterComponents.UI;

namespace GodotProject.Content.Scripts.Characters.Wolf
{
    public partial class WolfController : AiController
    {
        public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
        public State<WolfController> Rest { get; set; } = new WolfRest();
        public State<WolfController> Afraid { get; set; } = new WolfAfraid();
        public State<WolfController> Idle { get; set; } = new WolfIdle();
        public StateController<WolfController> StateController { get; set; }
        public Timer AfraidDuration { get; set; }

        public override void _Ready()
        {
            AiBody2D = GetNode<WolfPawn>("WolfBody");
            Animation = GetNode<AnimationPlayer>("WolfBody/Animation");
            WalkDuration = GetNode<Timer>("WolfBody/WalkDuration");
            AfraidDuration = GetNode<Timer>("WolfBody/AfraidDuration");

            Speed = 30f;
            StateController = new StateController<WolfController>(this);
            StateController.SetCurrentState(Rest);
            WalkDuration.Timeout += ChooseDirection;
        }

        public override void _PhysicsProcess(double delta)
        {
            Vector2 velocity = AiBody2D.Velocity;
            if (!AiBody2D.IsOnFloor())
                velocity.Y += gravity * (float)delta;

                AiBody2D.Velocity = velocity;
                AiBody2D.MoveAndSlide();
                StateController.Update();
        }

        public override void ChangeState()
        {
            if (isAggresive)
            {
                StateController.ChangeState(Afraid);
            }
            else
                StateController.ChangeState(Rest);
        }

        public void ChooseDirection()
        {
            var rnd = new Random();
            var direction = rnd.Next(0, 3);

            if (direction > 1)
            {
                AiBody2D.FlipCharacter(1);
                StateController.ChangeState(Rest);
            }
            else if (direction < 1)
            {
                AiBody2D.FlipCharacter(-1);
                StateController.ChangeState(Rest);
            }
            else if (direction == 1)
                StateController.ChangeState(Idle);
        }

        public void StopAfraid()
        {
            isAggresive = false;
            ChangeState();
            WalkDuration.Start();
        }

        #region CallMethod

        public void Hurt()
        {
            WalkDuration.Stop();
            isAggresive = true;
            AfraidDuration.Start(0);
            ChangeState();
            AiBody2D.isHurt = false;
        }

        #endregion
    }
}
