using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents;
using System;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans.WolfStans.FriendlyWolfStans;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans;

namespace GodotProject.Content.Scripts.Characters.Wolf
{
    public partial class FriendlyWolfController : RestController<FriendlyWolfController>
    { 
        public Timer AfraidDuration { get; set; }

        public State<FriendlyWolfController> Afraid = new WolfAfraid();

        public override void _Ready()
        {
            AfraidDuration = GetNode<Timer>("WolfBody/AfraidDuration");
            AiBody2D = GetNode<FriendlyWolfPawn>("WolfBody");
            Animation = GetNode<AnimationPlayer>("WolfBody/Animation");
            WalkDuration = GetNode<Timer>("WolfBody/WalkDuration");

            Speed = 30f;
            StateController = new StateController<FriendlyWolfController>(this);
            StateController.SetCurrentState(Rest);
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

        public override void ChangeState(bool Aggresive = false)
        {
            if (isAggresive || Aggresive)
            {
                StateController.ChangeState(Afraid);
            }
            else
                StateController.ChangeState(Rest);
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

        public void Dead()
        {
            this.QueueFree();
        }

        #endregion
    }
}
