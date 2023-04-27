using GodotProject.Content.Scripts.Ai.AiComponents.Stans;

namespace GodotProject.Content.Scripts.Ai.AiComponents
{
    public class StateController<T>
    {
        private T Owner;
        private State<T> CurrentState;
        private State<T> PreviousState;
        private State<T> GlobalState;

        public StateController(T owner)
        {
            Owner = owner;
            CurrentState = null;
            PreviousState = null;
            GlobalState = null;
        }
        public void SetCurrentState(State<T> state) { CurrentState = state; }
        public void SetGlobalState(State<T> state) { GlobalState = state; }
        public void SetPreviousState(State<T> state) { PreviousState = state; }

        public void Update()
        {         
            if (GlobalState != null)
            {
                GlobalState.Execute(Owner);
            }
            if (CurrentState != null)
            {
                CurrentState.Execute(Owner);
            }
        }
        public void ChangeState(State<T> NewState)
        {
            if (NewState != null)
            {
                PreviousState = CurrentState;
                CurrentState.Exit(Owner);
                CurrentState = NewState;
                CurrentState.Enter(Owner);
            }
        }
        public void RevertToPreviousState()
        {
            ChangeState(PreviousState);
        }

        public bool isinState(State<T> st)
        {
            if (st.GetType() == CurrentState.GetType())
            {
                return true;
            }
            else
                return false;
        }
        public State<T> currentState()
        {
            return CurrentState;
        }
        public State<T> globalState()
        {
            return GlobalState;
        }
        public State<T> previousState()
        {
            return PreviousState;
        }
    }
}
