using Godot;
using GodotProject.Content.Scripts.Characters.CharacterComponents;
using GodotProject.Content.Scripts.Characters.CharacterComponents.UI.Interfaces;
using GodotProject.Content.Scripts.Controllers;

public partial class HealthComponent : Node
{
    private Controller Controller { get; set; }

    private int _maxHp;
    public int MaxHp
    {
        get => _maxHp;

        set
        {
            _maxHp = value;
            CurrentHp = _maxHp;
        }
    }

    public int CurrentHp { get; set; }

    public bool IsDead { get; set; }

    public DefenseComponent DefenseComponent { get; set; }

    public IHUD HUDWidget { get; set; }

    public override void _Ready()
    {
        MaxHp = 3;
        DefenseComponent = new DefenseComponent();
    }

    public void TakeDamage(int damage = 1,bool ignoreArmor = false)
    {       
        if (ignoreArmor)
        {
            CurrentHp -= damage;
        }
        else
        {
            if(DefenseComponent.Defense > 0)
            {
                DefenseComponent.Defense -= damage;

                if(DefenseComponent.Defense < 0)
                {
                    CurrentHp -= (DefenseComponent.Defense * -1);
                    DefenseComponent.Defense = 0;
                }
            }             
            else
                CurrentHp -= damage;
        }

        if (CurrentHp <= 0)
        {
            Controller.Animation.Play("Dead");
            IsDead = true;
        }         
        else
           Controller.Animation.Play("Hurt");
            
        HUDWidget.Refresh(CurrentHp, MaxHp, DefenseComponent.Defense);
    }

    public void HealHp(int hp)
    {
        CurrentHp += hp;

        if (CurrentHp > MaxHp)
            CurrentHp = MaxHp;

        HUDWidget.Refresh(CurrentHp, MaxHp, DefenseComponent.Defense);
    }

    public void AddHp(int hp)
    {
        MaxHp += hp;
        HUDWidget.Refresh(CurrentHp, MaxHp, DefenseComponent.Defense);
    }

    public void SubtractionHp(int hp)
    {
        _maxHp -= hp;

        if (_maxHp <= 0)
            _maxHp = 1;

        if (_maxHp < CurrentHp)
            CurrentHp = _maxHp;

        HUDWidget.Refresh(CurrentHp, MaxHp, DefenseComponent.Defense);
    }

    public void Init(IHUD widget, Controller Owner)
    {
        HUDWidget = widget;
        HUDWidget.Refresh(CurrentHp, MaxHp, DefenseComponent.Defense);
        Controller = Owner;
    }
}

