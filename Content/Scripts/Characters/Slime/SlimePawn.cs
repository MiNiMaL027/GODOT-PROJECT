using Godot;
using GodotProject.Content.Scripts.Characters;
using GodotProject.Content.Scripts.Characters.CharacterComponents.UI;
using GodotProject.Content.Scripts.enums;

public partial class SlimePawn : AiPawn
{
    public HitBoxCollision BodyCollision;
    public Sprite2D Sprite { get; set; }
    public override void _Ready()
    {
        MemoryTime = 20;

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

    public override void FlipCharacter(float velocity)
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
        else if(velocity < 0)
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
