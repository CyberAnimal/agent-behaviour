using System.Collections.Generic;
using System.Threading.Tasks;

namespace Game.Enemy
{
    public class StateHighLevel : State
    {
        private readonly List<State> _states = new List<State>()
        {
            new StateIdle(),
            new StateSeek(),
            new StateAttack()
        };

        public State StartState => new StateIdle();
        public State CurrentState;

        public StateHighLevel()
        {
            if (CurrentState == null)
                CurrentState = StartState;
        }

        public async Task CallState(ConditionData data)
        {
            Task<State> newState = await Task.FromResult(CurrentState.Start(data));

            while (newState.IsCompleted == false)
                await Task.Yield();

            CurrentState = newState.Result;
            await CallState(data);
        }
    }
}
