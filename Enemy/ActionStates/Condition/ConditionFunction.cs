
namespace Game.Enemy
{
    public class ConditionFunction
    {
        public bool Increasestatic(float originValue, float valueRatio) => originValue < valueRatio;

        public bool IncreaseDynamic(float oldValue, float newValue) => newValue < oldValue;

        public bool IncreaseDynamic(float oldValue, float newValue, float ratio) => newValue < oldValue - ratio;

        public bool AccordanceFightPotential(float health, float armor)
        {
            float ratio = health - 1000 / ((int)armor ^ 2);

            return health < ratio;
        }
    }
}
