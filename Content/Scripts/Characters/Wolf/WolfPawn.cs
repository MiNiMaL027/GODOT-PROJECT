using Godot;
using GodotProject.Content.Scripts.Characters.CharacterComponents.UI;
using GodotProject.Content.Scripts.enums;

namespace GodotProject.Content.Scripts.Characters.Wolf
{
    public partial class WolfPawn : AiPawn
    {
        public HitBoxCollision BodyCollision;
        public Sprite2D Sprite { get; set; }

        public UI_MonsterHUD Widget { get; set; }

        public override void _Ready()
        {
            MoveDirection = MoveDirection.Left;
            Controller = GetParent<WolfController>();
            Sprite = GetNode<Sprite2D>("WolfSprite");

            BodyCollision = GetNode<HitBoxCollision>("Body");
            BodyCollision.Init(this);

            Widget = GetNode<UI_MonsterHUD>("MonsterHpWidget");
            Widget.Visible = false;
            HealthComponent = GetNode<HealthComponent>("HealthComponent");
            HealthComponent.MaxHp = 2;
            HealthComponent.Init(Widget, Controller);
        }
        public override void FlipCharacter(float velocity)
        {
            if (velocity > 0)
            {
                MoveDirection = MoveDirection.Right;
                Sprite.FlipH = true;
            }
            else if (velocity < 0)
            {
                MoveDirection = MoveDirection.Left;
                Sprite.FlipH = false;            
            }
        }
    }
}
