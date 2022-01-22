
namespace Game.Enemy
{
    public class DiscoveredAgent : Situation
    {
        public DiscoveredAgent() : base()
        {
            Variations.Add(new StateIdle(), new Variation(�heckType.Distance, new ActionPursue()));
            Variations.Add(new StateSeek(), new Variation(�heckType.DistanceOrHealth, new ActionEscape()));
            Variations.Add(new StateAttack(), new Variation(�heckType.Health, new ActionEscape()));
        }
    }
}
