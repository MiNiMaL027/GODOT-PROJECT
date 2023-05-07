using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents;
using GodotProject.Content.Scripts.Characters;
using GodotProject.Content.Scripts.Characters.CharacterComponents.UI;
using GodotProject.Content.Scripts.enums;

public partial class LeafPawn : AiAggresivePawn
{

    public override void _Ready()
    {
        MemoryTime = 10;

        MoveDirection = MoveDirection.Left;
        Controller = GetParent<LeafController>();
        Sprite = GetNode<Sprite2D>("LeafSprite");
        Audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");

        WalkSound = GD.Load<AudioStreamMP3>("res://Content/Components/Sounds/GameSound/Leaf/LeafWalk.mp3");

        BodyCollision = GetNode<HitBoxCollision>("Body");
        BodyCollision.Init(this);

        DamageArea = GetNode<DamageArea>("DamageArea");
        DamageArea.Init(this);
        DamageArea.ActiveDamageArea();

        ObservationComponent = GetNode<ObservationComponent>("ObservationComponent");
        ObservationComponent.Init(this);

        HealthComponent = GetNode<HealthComponent>("HealthComponent");
        HealthComponent.Init(GetNode<UI_MonsterHUD>("MonsterHpWidget"), Controller);
        HealthComponent.SubtractionHp(1);
    }

    public void ChooseAttack(bool attack)
    {
        if (attack)
            DamageArea.ChangeDamageArea(Controller.Attack);
        else
            DamageArea.ChangeDamageArea(Controller.UpAttack);
    }

    public override void ChooseAttack()
    {
        return;
    }

    public override void FinishAttack()
    {
        return;
    }
}
