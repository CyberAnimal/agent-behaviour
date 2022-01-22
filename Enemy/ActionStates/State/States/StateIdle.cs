namespace Game.Enemy
{
    public class StateIdle : State
    {
        public StateIdle() : base()
        {
            Transitions.Add(new TransitionSeek());
            Transitions.Add(new TransitionAttack());
        }
    }
}
