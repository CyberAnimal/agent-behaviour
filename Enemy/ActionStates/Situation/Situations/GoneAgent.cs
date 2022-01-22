namespace Game.Enemy
{
    public class GoneAgent : Situation
    {
        public GoneAgent() : base()
        {
            Variations.Add(new StateIdle(), new Variation(�heckType.Distance, new ActionSleep()));
            Variations.Add(new StateSeek(), new Variation(�heckType.Distance, new ActionWalk()));
            Variations.Add(new StateAttack(), new Variation(�heckType.DistanceAndHealth, new ActionPursue()));
        }
    }
}
