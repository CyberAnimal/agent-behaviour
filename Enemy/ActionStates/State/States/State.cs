using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Enemy
{
    public class State
    {
        protected List<Transition> Transitions;
        protected ActionHighLevel Actions;
        protected float CountSize;

        public State()
        {
            Transitions = new List<Transition>();
            Actions = new ActionHighLevel();
        }

        public virtual async Task<State> Start(ConditionData data)
        {
            Func<Task<State>> returnedFunction = await Continue(data);

            Task<State> finishedTask = await Task.FromResult(returnedFunction.Invoke());

            while (finishedTask.IsCompleted == false)
                await Task.Yield();

            return finishedTask.Result;
        }

        protected virtual async Task<Func<Task<State>>> Continue(ConditionData data)
        {
            Transition newTransition = default;

            Actions.Execute(data.TargetPosition, this, data);

            foreach (var transition in Transitions)
                if (transition.CanActive(data))
                    newTransition = transition;

            while (newTransition == null)
                await Task.Yield();

            return
                () => Stop(newTransition.Target, newTransition, data);
        }

        protected virtual async Task<State> Stop(State newState, Transition newTransition, ConditionData data)
        {
            float count = 0;

            Task<bool> isTurn = await Task.FromResult(TurnOnNewState(count, newTransition, data));

            while (isTurn.IsCompleted == false)
                await Task.Yield();

            if (isTurn.Result)
                return newState;

            else return this;
        }

        protected virtual async Task<bool> TurnOnNewState(float count, Transition newTransition, ConditionData data)
        {
            count += Time.deltaTime;

            while (count <= 60)
                await Task.Yield();

            if (newTransition.CanActive(data))
                return true;

            else return false;
        }
    }
}
