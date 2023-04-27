using Godot;
using GodotProject.Content.Scripts.Controllers;
using GodotProject.Content.Scripts.enums;

namespace GodotProject.Content.Scripts.Characters
{
    public partial class Pawn : CharacterBody2D
    {
        public HealthComponent HealthComponent { get; set; }
        public int Damage { get; set; } = 1;
        public bool isHurt { get; set; }
        public Controller Controller { get; set; }
        public DamageArea DamageArea { get; set; }
        public MoveDirection MoveDirection { get; set; }
    }
}
