using Godot;
using GodotProject.Content.Scripts.Characters.CharacterComponents.UI.Interfaces;

namespace GodotProject.Content.Scripts.Characters.CharacterComponents.UI
{
    public partial class UI_MonsterHUD : Control,IHUD
    {
        public HBoxContainer HpContainer { get; set; }

        [Export]
        public Texture2D Hp { get; set; }

        public override void _Ready()
        {
            HpContainer = GetNode<HBoxContainer>("HpContainer");
        }

        public void Refresh(int currentHp, int maxHp, int armor)
        {
            maxHp -= currentHp;
            RemoveAllChildHp();

            SetComponent(HpContainer, currentHp, Hp);
        }

        public void RemoveAllChildHp()
        {
            if (HpContainer.GetChildCount() != 0)
            {
                for (int i = HpContainer.GetChildCount() - 1; i >= 0; i--)
                {
                    var child = HpContainer.GetChild<TextureRect>(i);
                    HpContainer.RemoveChild(child);
                    child.QueueFree();
                }
            }
        }

        public void SetComponent(HBoxContainer container, int count, Texture2D texture)
        {
            while (count > 0)
            {
                container.AddChild(new TextureRect() { Texture = texture });
                count--;
            }
        }
    }
}
