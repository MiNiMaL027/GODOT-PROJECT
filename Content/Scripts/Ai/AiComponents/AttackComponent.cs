using Godot;
using GodotProject.Content.Scripts.Characters;
using GodotProject.Content.Scripts.enums;

public partial class AttackComponent : Area2D
{
    public AiPawn Pawn { get; set; }
    public Timer BackToNormalTimer { get; set; }
    public bool IsSlide { get; set; }
    public bool VictimOut { get; set; } = true;
    public string _animAttack {get; set;}

    public override void _PhysicsProcess(double delta)
    {
        if(IsSlide)
        {
            Pawn.MoveAndSlide();
        }

        if (Pawn.isHurt)
        {
            Pawn.Controller.isAttack = false;
        }
    }

    public void Init(AiPawn pawn)
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
        if(victim is player)
        {
            VictimOut = false;

            if(!Pawn.isHurt)
                Pawn.Controller.isAttack = true;

            ChooseAttack();
        }
    }

    public void StopAttack(Node2D victim)
    {
        if(victim is player)
        {
            VictimOut = true;
        }
    }

    public void ChooseAttack()
    {
        if (Pawn.ObservationComponent.PawnEnemy.HealthComponent.IsDead || !Pawn.Controller.isAttack)
            return;

        if(Pawn.ObservationComponent.PawnEnemy.GlobalPosition.Y < Pawn.GlobalPosition.Y - 10)
        {
;            _animAttack = "UpAttack";
            Pawn.DamageArea.ChangeDamageArea(Pawn.Controller.UpAttack);
        }
        else
        {
            _animAttack = "Attack";
            Pawn.DamageArea.ChangeDamageArea(Pawn.Controller.Attack);
        }

        Pawn.Controller.Animation.Play(_animAttack);
    }

    public void EndSlide()
    {
        IsSlide = false;
    }


    #region CallMethod

    public void FinishAttack()
    {
        if (_animAttack == "UpAttack")
            return;
        else if (Pawn.MoveDirection == MoveDirection.Right)
            Pawn.Velocity = new Vector2(-5 * Pawn.Controller.Speed, Pawn.Velocity.Y);
        else if (Pawn.MoveDirection == MoveDirection.Left)
            Pawn.Velocity = new Vector2(5 * Pawn.Controller.Speed, Pawn.Velocity.Y);

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
            ChooseAttack();
        }                  
    }

    #endregion
}
