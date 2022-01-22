using UnityEngine;

namespace Game.Enemy
{
    public class InterestWithThreeSense : InterestWithTwoSense
    {
        private readonly Sense _thirdSense;
        public Sense ThirdSense => _thirdSense;

        public InterestWithThreeSense(Sense firstSense, Sense secondSense,
                                      Sense thirdSense, Priority priority,
                                      GameObject interestObject) :
                               base(firstSense, secondSense, priority, interestObject)
        {
            _thirdSense = thirdSense;
        }
    }
}
