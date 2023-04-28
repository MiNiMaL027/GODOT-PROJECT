using Godot;
using GodotProject.Content.Scripts.Characters;
using GodotProject.Content.Scripts.enums;

public partial class HitBoxCollision : Area2D
{
    public Pawn HitBoxOwner { get; set; }

    public CollisionShape2D CollisionShape { get; set; }

    [Export]
    public BodyParts CollisionType;

    public void Init(Pawn pawn)
    {
        HitBoxOwner = pawn;
        CollisionShape = GetChild<CollisionShape2D>(0);
    }

    public void TakeDamage(int damage)
    {  
        HitBoxOwner.isHurt = true;

        if(CollisionType == BodyParts.Head)
            HitBoxOwner.HealthComponent.TakeDamage(damage: damage, true);
        else
            HitBoxOwner.HealthComponent.TakeDamage(damage: damage);
    }

    //public void DisableHitBox()
    //{
    //    for (int i = 0; i < HitBoxOwner.EnteredHitBoxs.Count - 1; i++)
    //    {
    //        HitBoxOwner.EnteredHitBoxs[i].CollisionShape.Disabled = true;
    //    }
    //}

    //public void EnableHitBox()
    //{
    //    for (int i = 0; i < HitBoxOwner.EnteredHitBoxs.Count - 1; i++)
    //    {
    //        HitBoxOwner.EnteredHitBoxs[i].CollisionShape.Disabled = false;
    //    }
    //}
}
