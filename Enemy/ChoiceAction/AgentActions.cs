using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class AgentActions : MonoBehaviour
    {
        private DesicionsMaker _desicions;

        private InterestSources _interests;
        private Handlers _handlers;
        private InterestHandler _handler;

        private BehaviourType _behaviourType;
        private FunctionType _functionType;

        private Function _function;
        private Sense _first;
        private Sense _second;

        private double _radius;

        private void Start()
        {
            _handlers = new Handlers();
            _handler = _handlers.GetHandler(_behaviourType);

            ChoiseFunction choise = new ChoiseFunction();
            _function = choise.GetFunction(_functionType);
        }
        private void OnEnable()  => _desicions.CanEnable(AddInterest);

        private void OnDisable() => _desicions.CanDisable(AddInterest);

        void AddInterest(GameObject source, Sense sense)
        {
            if (_first == sense)
                _second = sense;

            else _first = sense;

            Priority priority = AssignPriority(source);
            Interest interest = new Interest(sense, priority, source);

            _interests.AddInterest(interest);
        }

        Priority AssignPriority(GameObject source) => _handler.GetPriority(source, gameObject, _radius,
                                                                          _function.Type, _function.Form);
        public Interest CanUpdate() => UpdateInterests(_first, _second);

        Interest UpdateInterests(Sense first, Sense second)
        {
            List<InterestWithThreeSense> interestsWithThree = UpdateInterestsThreeSense();
            List<InterestWithTwoSense> interestsWithTwoSense = UpdateInterestsTwoSense(first, second);
            List<Interest> interestsOneSense = UpdateInterestsOneSense(first);

            List<Interest> allInterests = new List<Interest>(interestsWithThree.Count +
                                                  interestsWithTwoSense.Count +
                                                  interestsOneSense.Count);

            allInterests.AddRange(interestsWithThree);
            allInterests.AddRange(interestsWithTwoSense);
            allInterests.AddRange(interestsOneSense);

            Interest interest = ChooseMainInterest(allInterests);

            return interest;
        }

        List<InterestWithThreeSense> UpdateInterestsThreeSense()
        {
            List<InterestWithThreeSense> interestsThreeSense = _interests.Sorter.SortToDistanceWithThreeSense();

            for (int i = 0; i < interestsThreeSense.Count; i++)
            {
                Interest inter = interestsThreeSense[i];
                interestsThreeSense[i] = (InterestWithThreeSense)_handler.UpdateAffected(inter, gameObject, _radius,
                                                                                         _function.Type, _function.Form);
            }

            return _interests.Sorter.SortToPriorityWithThreeSense(interestsThreeSense);
        }

        List<InterestWithTwoSense> UpdateInterestsTwoSense(Sense first, Sense second)
        {
            List<InterestWithTwoSense> interestsTwoSense =
                _interests.Sorter.ExtractInterestWithTwoSense(first, second);

            interestsTwoSense = _interests.Sorter.SortToDistanceWithTwoSense();

            for (int i = 0; i < interestsTwoSense.Count; i++)
            {
                Interest inter = interestsTwoSense[i];
                interestsTwoSense[i] = (InterestWithTwoSense)_handler.UpdateAffected(inter, gameObject, _radius,
                                                                                     _function.Type, _function.Form);
            }

            return _interests.Sorter.SortToPriorityWithTwoSense(interestsTwoSense);
        }

        List<Interest> UpdateInterestsOneSense(Sense sense)
        {
            List<Interest> interests = _interests.Sorter.ExtractInterestWithOneSense(sense);
            interests = _interests.Sorter.SortToDistance();

            for (int i = 0; i < interests.Count; i++)
            {
                Interest inter = interests[i];
                interests[i] = _handler.UpdateAffected(inter, gameObject, _radius,
                                                       _function.Type, _function.Form);
            }

            return _interests.Sorter.SortToPriority(interests);
        }

        Interest ChooseMainInterest(List<Interest> interests)
        {
            var newInterests = _interests.Sorter.SortToPriority(interests);

            return newInterests[0];
        }
    }
}
