namespace Game.Enemy
{
    public class DiscoveredObject : Situation
    {
        public DiscoveredObject() : base()
        {
            Variations.Add(new StateIdle(), new Variation(�heckType.Distance, new ActionPursue()));
            Variations.Add(new StateSeek(), new Variation(�heckType.Distance, new ActionPursue()));
            Variations.Add(new StateAttack(), new Variation(�heckType.DistanceAndHealth, new ActionEscape()));
        }
    }
}