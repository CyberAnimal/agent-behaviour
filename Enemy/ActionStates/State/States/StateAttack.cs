
namespace Game.Enemy
{
    public class StateAttack : State
    {
        public StateAttack() : base()
        {
            Transitions.Add(new TransitionSeek());
            Transitions.Add(new TransitionIdle());
        }
    }
}
