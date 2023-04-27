using Godot;
using GodotProject.Content.Scripts.Characters;
using GodotProject.Content.Scripts.enums;
using GodotProject.Content.Scripts.Player.PlayerComponent;
using System;
using System.Collections.Generic;

public partial class DamageArea : Area2D
{
    public Pawn HitBoxOwner { get; set; }

    public AttackType CollisionType;

    public CollisionShape2D CollisionShape;

    public Timer Timer { get; set; }

    public List<HitBoxCollision> EnteredHitBoxs = new List<HitBoxCollision>();

    public float StartAreaRotation;
    public Vector2 StartAreaPosition;

    public void Init(Pawn pawn)
    {
        HitBoxOwner = pawn;
        CollisionShape = HitBoxOwner.DamageArea.GetChild<CollisionShape2D>(0);
        StartAreaPosition = CollisionShape.Position;
        StartAreaRotation = CollisionShape.Rotation;
        Timer = new Timer();
        Timer.WaitTime = 0.1;
        AddChild(Timer);
        Timer.Timeout += Attack;
        this.AreaEntered += HitBoxCollision_AreaEntered;
    }

    public void ChangeDamageArea(AttackTransform attackTransform)
    {    
        if(HitBoxOwner.MoveDirection == MoveDirection.Right)
        {
            CollisionShape.Position = attackTransform.Position;
            CollisionShape.Rotation = attackTransform.Rotation;
        }
        else if(HitBoxOwner.MoveDirection == MoveDirection.Left)
        {
            CollisionShape.Position = new Vector2(-attackTransform.Position.X, attackTransform.Position.Y);
            CollisionShape.Rotation = -attackTransform.Rotation;
        }
       
        StartAreaPosition = attackTransform.Position;
        StartAreaRotation = attackTransform.Rotation;
        (CollisionShape.Shape as CapsuleShape2D).Radius = attackTransform.Transform.X;
        (CollisionShape.Shape as CapsuleShape2D).Height = attackTransform.Transform.Y;
    }

    private void HitBoxCollision_AreaEntered(Area2D area)
    {
        if (area is HitBoxCollision damageArea)
        {
            if(!damageArea.HitBoxOwner.isHurt)
                EnteredHitBoxs.Add(damageArea);
        }
    }

    public void Attack()
    {
        if (EnteredHitBoxs.Count != 0)
        {
            EnteredHitBoxs[0].TakeDamage(HitBoxOwner.Damage);
            EnteredHitBoxs.Clear();
        }
            
        Timer.Stop();
    }

    #region CallMethod
    public void ActiveDamageArea()
    {
        CollisionShape.Disabled = false;
        Timer.Start();
    }

    public void DisableDamageArea()
    {
        CollisionShape.Disabled = true;
    }

    #endregion
}
