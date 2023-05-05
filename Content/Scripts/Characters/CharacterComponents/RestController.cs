using Godot;
using GodotProject.Content.Scripts.Ai.AiComponents;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans;
using GodotProject.Content.Scripts.Ai.AiComponents.Stans.Common;
using GodotProject.Content.Scripts.Controllers;
using System;

namespace GodotProject.Content.Scripts.Characters.Wolf
{
    public abstract partial class RestController<T> : AiController
    {
        public State<T> Rest { get; set; } = new Rest<T>();
        public State<T> Idle { get; set; } = new Idle<T>();
        public StateController<T> StateController { get; set; } 

        public void ChooseDirection()
        {
            var rnd = new Random();
            var direction = rnd.Next(0, 3);

            if (direction > 1)
            {
                AiBody2D.FlipCharacter(1);
                StateController.ChangeState(Rest);
            }
            else if (direction < 1)
            {
                AiBody2D.FlipCharacter(-1);
                StateController.ChangeState(Rest);
            }
            else if (direction == 1)
                StateController.ChangeState(Idle);
        }
    }
}
