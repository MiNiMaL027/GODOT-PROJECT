namespace GodotProject.Content.Scripts.Ai.AiComponents.Stans
{
    public abstract class State<T>
    {
        public abstract void Enter(T Owner);
        public abstract void Execute(T Owner);
        public abstract void Exit(T Owner);
    }
}
