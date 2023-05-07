using Godot;
using GodotProject.Content.Scripts.Characters;
using GodotProject.Content.Scripts.enums;

public partial class HitBoxCollision : Area2D
{
    public Pawn HitBoxOwner { get; set; }

    public bool Block { get; set; }

    public CollisionShape2D CollisionShape { get; set; }

    public CollisionShape2D PreviousCollisionShape { get; set; }

    [Export]
    public BodyParts CollisionType;

    public void Init(Pawn pawn)
    {
        HitBoxOwner = pawn;
        CollisionShape = GetChild<CollisionShape2D>(0);
    }

    public void TakeDamage(int damage)
    {
        if (Block)
            return;

        HitBoxOwner.isHurt = true;      

        if(CollisionType == BodyParts.Head)
            HitBoxOwner.HealthComponent.TakeDamage(damage: damage, true);
        else
            HitBoxOwner.HealthComponent.TakeDamage(damage: damage);
    }

    public void ChangeCollisionShape(float radius, float height)
    {
        PreviousCollisionShape = CollisionShape;
        (CollisionShape.Shape as CapsuleShape2D).Radius = radius;
        (CollisionShape.Shape as CapsuleShape2D).Height = height;
    }

    public void RevertToPreviousShape()
    {
        CollisionShape = PreviousCollisionShape;
    }
}
