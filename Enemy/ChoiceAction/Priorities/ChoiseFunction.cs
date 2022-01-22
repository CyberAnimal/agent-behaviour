using System.Collections.Generic;

namespace Game.Enemy
{
    public class ChoiseFunction
    {
        private readonly Dictionary<FunctionType, PrioritiesReckoner.FunctionType> _handlersWithType =
            new Dictionary<FunctionType, PrioritiesReckoner.FunctionType>()
            {
                [FunctionType.In] = PrioritiesReckoner.FunctionType.InFunction,
                [FunctionType.Out] = PrioritiesReckoner.FunctionType.OutFunction,
                [FunctionType.InOut] = PrioritiesReckoner.FunctionType.InOutFunction
            };

        private static PrioritiesReckoner.FunctionForm GetRandomFunctionForm() =>
            (PrioritiesReckoner.FunctionForm)UnityEngine.Random.Range(0, 4);

        public Function GetFunction(FunctionType type) => new Function(_handlersWithType[type],
                                                                       GetRandomFunctionForm());
    }
}
