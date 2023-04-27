using Godot;
using GodotProject.Content.Scripts.Characters.CharacterComponents.UI.Interfaces;

public partial class UI_HpAndArmor : Control,IHUD
{
    public HBoxContainer HpContainer { get; set; }
    public HBoxContainer ArmorContainer { get; set; }
    public Texture2D Hp { get; set; }
    public Texture2D Hp_Empty { get; set; }
    public Texture2D Armor { get; set; }

    public override void _Ready()
    {
        ArmorContainer = GetNode<HBoxContainer>("ArmorContainer");
        HpContainer = GetNode<HBoxContainer>("ArmorContainer/HpContainer");
        Hp = GD.Load<Texture2D>("res://Content/Components/Player/UI/Items/Heart.png");
        Hp_Empty = GD.Load<Texture2D>("res://Content/Components/Player/UI/Items/Heart_empty.png");
        Armor = GD.Load<Texture2D>("res://Content/Components/Player/UI/Items/Armor.png");
    }

    public void Refresh(int currentHp, int maxHp, int armor)
    {
        maxHp -= currentHp;
        RemoveAllChildHp();
        RemoveAllChildArmor();

        SetComponent(HpContainer, currentHp, Hp);
        SetComponent(HpContainer, maxHp, Hp_Empty);
        SetComponent(ArmorContainer, armor, Armor);
    }

    public void RemoveAllChildHp()
    {
        if(HpContainer.GetChildCount() != 0)
        {
            for (int i = HpContainer.GetChildCount() - 1; i >= 0; i--)
            {
                var child = HpContainer.GetChild<TextureRect>(i);
                HpContainer.RemoveChild(child);
                child.QueueFree();
            }
        }   
    }

    public void RemoveAllChildArmor()
    {
        if(ArmorContainer.GetChildCount() != 0)
        { 
            for (int i = ArmorContainer.GetChildCount() - 1; i >= 1; i--)
            {
                var child = ArmorContainer.GetChild<TextureRect>(i);
                ArmorContainer.RemoveChild(child);
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
