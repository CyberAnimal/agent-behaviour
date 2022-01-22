namespace Game.Enemy
{
    public class DiscoveredObject : Situation
    {
        public DiscoveredObject() : base()
        {
            Variations.Add(new StateIdle(), new Variation(ÑheckType.Distance, new ActionPursue()));
            Variations.Add(new StateSeek(), new Variation(ÑheckType.Distance, new ActionPursue()));
            Variations.Add(new StateAttack(), new Variation(ÑheckType.DistanceAndHealth, new ActionEscape()));
        }
    }
}