using System.Collections.Generic;
using System.Linq;

namespace Game.Enemy
{
    public class InterestSorter
    {
        private readonly InterestSources _sources;

        public InterestSorter(InterestSources sources) => _sources = sources;

        public List<Interest> SortToDistance() =>
              (List<Interest>)_sources.Interests.OrderByDescending(x => x.Position);

        public List<Interest> SortToDistance(List<Interest> interests) =>
              (List<Interest>)interests.OrderByDescending(x => x.Position);

        public List<InterestWithTwoSense> SortToDistanceWithTwoSense() =>
              (List<InterestWithTwoSense>)_sources.Interests.Where(x => x is InterestWithTwoSense)
                                                            .OrderByDescending(x => x.Position);

        public List<InterestWithTwoSense> SortToDistanceWithTwoSense(List<InterestWithTwoSense> interests) =>
              (List<InterestWithTwoSense>)interests.Where(x => x is InterestWithTwoSense)
                                                   .OrderByDescending(x => x.Position);

        public List<InterestWithThreeSense> SortToDistanceWithThreeSense() =>
              (List<InterestWithThreeSense>)_sources.Interests.Where(x => x is InterestWithThreeSense)
                                                              .OrderByDescending(x => x.Position);

        public List<InterestWithThreeSense> SortToDistanceWithThreeSense(List<InterestWithThreeSense> interests) =>
              (List<InterestWithThreeSense>)interests.Where(x => x is InterestWithThreeSense)
                                                     .OrderByDescending(x => x.Position);

        public List<Interest> SortToPriority() =>
              (List<Interest>)_sources.Interests.OrderByDescending(x => x.Priority);

        public List<Interest> SortToPriority(List<Interest> interests) =>
              (List<Interest>)interests.OrderByDescending(x => x.Priority);

        public List<InterestWithTwoSense> SortToPriorityWithTwoSense() =>
              (List<InterestWithTwoSense>)_sources.Interests.OrderByDescending(x => x.Priority);

        public List<InterestWithTwoSense> SortToPriorityWithTwoSense(List<InterestWithTwoSense> interests) =>
              (List<InterestWithTwoSense>)interests.OrderByDescending(x => x.Priority);

        public List<InterestWithThreeSense> SortToPriorityWithThreeSense() =>
              (List<InterestWithThreeSense>)_sources.Interests.OrderByDescending(x => x.Priority);

        public List<InterestWithThreeSense> SortToPriorityWithThreeSense(List<InterestWithThreeSense> interests) =>
              (List<InterestWithThreeSense>)interests.OrderByDescending(x => x.Priority);

        public List<Interest> ExtractInterestWithOneSense(Sense sense)
        {
            var newInterests = _sources.Interests.Where(x => x.Sense == sense).Select(x => x);

            return (List<Interest>)newInterests;
        }

        public List<InterestWithTwoSense> ExtractInterestWithTwoSense(Sense firstSense,
                                                                      Sense secondSense)
        {
            List<InterestWithTwoSense> interestWithTwoSense = (List<InterestWithTwoSense>)_sources.Interests
                                                              .Where(x => x is InterestWithTwoSense);

            var newInterests = interestWithTwoSense.Where(x => x.Sense == firstSense)
                                                   .Where(x => x.SecondSense == secondSense);


            return (List<InterestWithTwoSense>)newInterests;
        }
    }
}
