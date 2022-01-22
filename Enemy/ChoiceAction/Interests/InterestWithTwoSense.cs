using UnityEngine;

namespace Game.Enemy
{
    public class InterestWithTwoSense : Interest
    {
        protected readonly Sense _secondSense;
        public Sense SecondSense => _secondSense;

        public InterestWithTwoSense(Sense firstSense, Sense secondSense,
                                    Priority priority, GameObject interestObject) :
                               base(firstSense, priority, interestObject)
        {
            _secondSense = secondSense;
        }
    }
}
