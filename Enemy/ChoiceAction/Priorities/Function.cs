using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public readonly struct Function
    {
        private readonly PrioritiesReckoner.FunctionType _type;
        public readonly PrioritiesReckoner.FunctionType Type => _type;

        private readonly PrioritiesReckoner.FunctionForm _form;
        public readonly PrioritiesReckoner.FunctionForm Form => _form;


        public Function(PrioritiesReckoner.FunctionType type,
                        PrioritiesReckoner.FunctionForm form)
        {
            _type = type;
            _form = form;
        }
    }
}
