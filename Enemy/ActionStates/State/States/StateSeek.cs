namespace Game.Enemy
{
    public class StateSeek : State
    {
        public StateSeek() : base()
        {
            Transitions.Add(new TransitionIdle());
            Transitions.Add(new TransitionAttack());
        }
    }
}
