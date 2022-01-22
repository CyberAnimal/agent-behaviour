namespace Game.Enemy
{
    public class GoneObject : Situation
    {
        public GoneObject() : base()
        {
            Variations.Add(new StateIdle(), new Variation(�heckType.Distance, new ActionSleep()));
            Variations.Add(new StateSeek(), new Variation(�heckType.Distance, new ActionWalk()));
            Variations.Add(new StateAttack(), new Variation(�heckType.DistanceAndHealth, new ActionEscape()));
        }
    }
}
