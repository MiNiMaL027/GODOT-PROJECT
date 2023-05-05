using Godot;

namespace GodotProject.Content.Scripts.Characters.Interfaces
{
    public interface IHaveSound
    {
        public AudioStreamPlayer2D Audio { get; set; }

        public AudioStreamMP3 WalkSound { get; set; }
    }
}
