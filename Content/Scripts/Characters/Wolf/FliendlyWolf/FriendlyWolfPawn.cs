using Godot;
using GodotProject.Content.Scripts.Characters.CharacterComponents.UI;
using GodotProject.Content.Scripts.enums;

namespace GodotProject.Content.Scripts.Characters.Wolf
{
    public partial class FriendlyWolfPawn : AiPawn
    {
        public HitBoxCollision BodyCollision;
        public UI_MonsterHUD Widget { get; set; }

        public override void _Ready()
        {
            MoveDirection = MoveDirection.Left;
            Controller = GetParent<FriendlyWolfController>();
            Sprite = GetNode<Sprite2D>("WolfSprite");

            BodyCollision = GetNode<HitBoxCollision>("Body");
            BodyCollision.Init(this);

            Widget = GetNode<UI_MonsterHUD>("MonsterHpWidget");
            Widget.Visible = false;
            HealthComponent = GetNode<HealthComponent>("HealthComponent");
            HealthComponent.MaxHp = 2;
            HealthComponent.Init(Widget, Controller);
        }      
    }
}
