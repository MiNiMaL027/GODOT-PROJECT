﻿using Godot;

namespace GodotProject.Content.Scripts.Controllers
{
    public partial  class Controller : Node2D
    {
        public const float Speed = 150.0f;
        public const float JumpVelocity = -200.0f;
        public AnimationPlayer Animation;
        public float _currentSpeed = Speed / 2;
        public const float _startSpeed = Speed / 2;
    }
}