using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents;
using GodotProject.Content.Scripts.Characters.CharacterComponents.UI;
using GodotProject.Content.Scripts.enums;

namespace GodotProject.Content.Scripts.Characters.Wolf.NeutralWolf
{
    public partial class NeutralWolfPawn : AiAggresivePawn
    {
        public HitBoxCollision BodyCollision;
        public UI_MonsterHUD Widget { get; set; }

        public override void _Ready()
        {
            MoveDirection = MoveDirection.Left;
            Controller = GetParent<NeutralWolfController>();
            Sprite = GetNode<Sprite2D>("WolfSprite");
            MemoryTime = 2;
            BackToNormalTime = 0.5f;

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
            _animAttack = "Attack";
            DamageArea.ChangeDamageArea(Controller.Attack);

            Controller.Animation.Play(_animAttack);
        }

        public override void FinishAttack()
        {
            return;
        }
    }   
}
