using Godot;
using GodotProject.Content.Scripts.Characters;
using GodotProject.Content.Scripts.Controllers;
using GodotProject.Content.Scripts.enums;

namespace GodotProject.Content.Scripts.Ai.AiComponents
{
    public partial class AttackComponent : Area2D
    {
        public AiAggresivePawn Pawn { get; set; }
        public Timer BackToNormalTimer { get; set; }
        public bool IsSlide { get; set; }
        public bool VictimOut { get; set; } = true;

        public override void _PhysicsProcess(double delta)
        {
            if (IsSlide)
            {
                Pawn.MoveAndSlide();
            }

            if (Pawn.isHurt)
            {
                Pawn.Controller.isAttack = false;
            }
        }

        public void Init(AiAggresivePawn pawn)
        {
            Pawn = pawn;
            this.BodyEntered += Attack;
            this.BodyExited += StopAttack;

            BackToNormalTimer = new Timer();
            BackToNormalTimer.WaitTime = Pawn.BackToNormalTime;
            BackToNormalTimer.OneShot = true;
            BackToNormalTimer.Autostart = false;
            AddChild(BackToNormalTimer);
            BackToNormalTimer.Timeout += EndSlide;
        }

        public void Attack(Node2D victim)
        {
            if (victim is player)
            {
                VictimOut = false;

                if (!Pawn.isHurt)
                    Pawn.Controller.isAttack = true;

                Pawn.ChooseAttack();
            }
        }

        public void StopAttack(Node2D victim)
        {
            if (victim is player)
            {
                VictimOut = true;
            }
        }

        public void EndSlide()
        {
            IsSlide = false;
        }


        #region CallMethod

        public void FinishAttack()
        {
            Pawn.FinishAttack();

            BackToNormalTimer.Start(0);

            IsSlide = true;
        }

        public void AttackStart()
        {
            if (VictimOut || Pawn.HealthComponent.IsDead)
                Pawn.Controller.isAttack = false;
            else
            {
                Pawn.Controller.isAttack = true;

                Pawn.ChooseAttack();
            }
        }

        #endregion
    }
}