namespace Game.Enemy
{
    public class GoneObject : Situation
    {
        public GoneObject() : base()
        {
            Variations.Add(new StateIdle(), new Variation(ÑheckType.Distance, new ActionSleep()));
            Variations.Add(new StateSeek(), new Variation(ÑheckType.Distance, new ActionWalk()));
            Variations.Add(new StateAttack(), new Variation(ÑheckType.DistanceAndHealth, new ActionEscape()));
        }
    }
}
