namespace GodotProject.Content.Scripts.Characters.CharacterComponents
{
    public class DefenseComponent
    {
        public int Defense { get; set; }

        public int Spikes { get; set; } = 0;

        public DefenseComponent()
        {
            Defense = 0;
        }
    }
}
