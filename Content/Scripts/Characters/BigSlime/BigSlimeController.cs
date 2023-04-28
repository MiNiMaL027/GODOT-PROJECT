using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents;
using GodotProject.Content.Scripts.Player.PlayerComponent;
using System;

public partial class BigSlimeController : SlimeController
{
    public Node parent;

    public PackedScene slime;

    public override void _Ready()
    {
        Speed = 7f;
        AiBody2D = GetNode<SlimePawn>("AiBody");
        Animation = GetNode<AnimationPlayer>("AiBody/Animation");
        WalkDuration = GetNode<Timer>("AiBody/WalkDuration");
        StanController = new StateController<SlimeController>(this);
        StanController.SetCurrentState(Rest);
        WalkDuration.Timeout += ChooseDirection;

        Animation.SpeedScale = 0.8f;
        parent = GetParent<Node>();
        Attack = new AttackTransform() { Transform = new Vector2(19f, 36f), Rotation = 0f, Position = new Vector2(18f, -14f) };
        UpAttack = new AttackTransform() { Transform = new Vector2(12f, 40f), Rotation = 99f, Position = new Vector2(3f, -17f) };

        AiBody2D.HealthComponent.AddHp(4);
        slime = GD.Load<PackedScene>("res://Content/Scenes/Enemy/Slime.tscn");      
    }

    public override void Dead()
    {
        var slimeInstance = slime.Instantiate<SlimeController>();
        var slimeInstance2 = slime.Instantiate<SlimeController>();

        slimeInstance.GlobalPosition = new Vector2(AiBody2D.GlobalPosition.X - 10, AiBody2D.GlobalPosition.Y);
        parent.AddChild(slimeInstance);
        slimeInstance.AiBody2D.HealthComponent.SubtractionHp(1);

        slimeInstance2.GlobalPosition = new Vector2(AiBody2D.GlobalPosition.X + 10, AiBody2D.GlobalPosition.Y);
        parent.AddChild(slimeInstance2);
        slimeInstance2.AiBody2D.HealthComponent.SubtractionHp(1);

        this.QueueFree();
    }
}
