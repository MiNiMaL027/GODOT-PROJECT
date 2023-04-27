using Godot;
using GodotProject.Content.Scripts.Characters;
using GodotProject.Content.Scripts.enums;
using static Godot.TextServer;

public partial class player : Pawn
{
    public Sprite2D Sprite;

    public CollisionShape2D CollisionShape;

    public HitBoxCollision BodyCollision;

    public HitBoxCollision HeadCollision;

    public override void _Ready()
    {
        Controller = GetParent<PlayerController>();
        MoveDirection = MoveDirection.Right;

        Sprite = GetNode<Sprite2D>("PlayerSprite");

        BodyCollision = GetNode<HitBoxCollision>("Body");
        BodyCollision.Init(this);
        HeadCollision = GetNode<HitBoxCollision>("Head");
        HeadCollision.Init(this);
        CollisionShape = GetNode<CollisionShape2D>("PhithicCollision");
        DamageArea = GetNode<DamageArea>("DamageArea");
        DamageArea.Init(this);

        HealthComponent = GetNode<HealthComponent>("HealthComponent");
        HealthComponent.Init(GetNode<UI_HpAndArmor>("HUD/HpAndManaWidget"), Controller);
    }

    public void StartCrouch()
    {
        HeadCollision.GetChild<CollisionShape2D>(0).Disabled = true;
        CollisionShape.Position = new Vector2(0, 5);
        (CollisionShape.Shape as CapsuleShape2D).Height = 20;
    }

    public void StopCrouch()
    {
        HeadCollision.GetChild<CollisionShape2D>(0).Disabled = false;
        CollisionShape.Position = new Vector2(0, 0);
        (CollisionShape.Shape as CapsuleShape2D).Height = 30;
    }

    public void FlipCharacter(Vector2 velocity)
    {
        if (velocity.X > 0)
        {
            MoveDirection = MoveDirection.Right;
            Sprite.FlipH = false;
            Sprite.Offset = new Vector2(0, 0);
            DamageArea.CollisionShape.Position = new Vector2(DamageArea.StartAreaPosition.X, DamageArea.StartAreaPosition.Y);
            DamageArea.CollisionShape.Rotation = DamageArea.StartAreaRotation;           
        }
        else if(velocity.X < 0)
        {
            MoveDirection = MoveDirection.Left;
            Sprite.FlipH = true;
            Sprite.Offset = new Vector2(-15, 0);
            DamageArea.CollisionShape.Position = new Vector2(-DamageArea.StartAreaPosition.X, DamageArea.StartAreaPosition.Y);
            DamageArea.CollisionShape.Rotation = -DamageArea.StartAreaRotation;
        }
    } 
}
