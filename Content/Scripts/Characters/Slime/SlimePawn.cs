using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents;
using GodotProject.Content.Scripts.Characters;
using GodotProject.Content.Scripts.Characters.CharacterComponents.UI;
using GodotProject.Content.Scripts.enums;

public partial class SlimePawn : AiAggresivePawn
{
    public HitBoxCollision BodyCollision;
    
    public override void _Ready()
    {
        MemoryTime = 10;

        MoveDirection = MoveDirection.Left;
        Controller = GetParent<SlimeController>();
        Sprite = GetNode<Sprite2D>("SlimeSprite");

        BodyCollision = GetNode<HitBoxCollision>("Body");
        BodyCollision.Init(this);
        DamageArea = GetNode<DamageArea>("DamageArea");
        DamageArea.Init(this);
        ObservationComponent = GetNode<ObservationComponent>("ObservationComponent");
        ObservationComponent.Init(this);
        AttackRangeComponent = GetNode<AttackComponent>("AttackComponent");
        AttackRangeComponent.Init(this);

        HealthComponent = GetNode<HealthComponent>("HealthComponent");
        HealthComponent.Init(GetNode<UI_MonsterHUD>("MonsterHpWidget"), Controller);
    }

    public override void ChooseAttack()
    {
        if (ObservationComponent.PawnEnemy.HealthComponent.IsDead || !Controller.isAttack)
            return;

        if (ObservationComponent.PawnEnemy.GlobalPosition.Y < GlobalPosition.Y - 10)
        {
            _animAttack = "UpAttack";
            DamageArea.ChangeDamageArea(Controller.UpAttack);
        }
        else
        {
            _animAttack = "Attack";
            DamageArea.ChangeDamageArea(Controller.Attack);
        }

        Controller.Animation.Play(_animAttack);
    }

    public override void FinishAttack()
    {
        if (_animAttack == "UpAttack")
            return;
        else if (MoveDirection == MoveDirection.Right)
            Velocity = new Vector2(-5 * Controller.Speed, Velocity.Y);
        else if (MoveDirection == MoveDirection.Left)
            Velocity = new Vector2(5 * Controller.Speed, Velocity.Y);
    }
}
