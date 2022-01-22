
namespace Game.Enemy
{
    public class DiscoveredAgent : Situation
    {
        public DiscoveredAgent() : base()
        {
            Variations.Add(new StateIdle(), new Variation(ÑheckType.Distance, new ActionPursue()));
            Variations.Add(new StateSeek(), new Variation(ÑheckType.DistanceOrHealth, new ActionEscape()));
            Variations.Add(new StateAttack(), new Variation(ÑheckType.Health, new ActionEscape()));
        }
    }
}
