using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    [Serializable]
    public class InterestSources : MonoBehaviour
    {
        private List<Interest> _interests = new List<Interest>();
        public List<Interest> Interests => _interests;

        private InterestSorter _sorter;
        public InterestSorter Sorter => _sorter;

        public Interest ExtractInterest(GameObject interestObject)
        {
            foreach (var interest in _interests)
                if (interest.InterestObject == interestObject)
                    return interest;

            return default;
        }

        public void AddInterest(Interest newInterest)
        {
            int indexOfInterest = -1;

            foreach (var interest in _interests)
                if (interest == newInterest)
                    indexOfInterest = _interests.IndexOf(interest);

            if (indexOfInterest < 0)
                _interests.Add(newInterest);

            else
                _interests[indexOfInterest] = newInterest;
        }

        public void RemoveInterest(Interest interestForDelete)
        {
            foreach (var interest in _interests)
                if (interestForDelete == interest)
                {
                    int indexOfInterest = _interests.IndexOf(interest);
                    _interests.RemoveAt(indexOfInterest);
                }
        }

        public void AddSence(Interest curentInterest, Sense sense)
        {
            foreach (var interest in _interests)
                if (curentInterest == interest)
                {
                    Interest interestForAdd = interest;
                    int indexOfInterest = _interests.IndexOf(interestForAdd);

                    if (interestForAdd is InterestWithTwoSense)
                    {
                        InterestWithTwoSense twoSense = interestForAdd as InterestWithTwoSense;
                        InterestWithThreeSense threeSense = new InterestWithThreeSense(twoSense.Sense, twoSense.SecondSense, sense,
                                                                                       twoSense.Priority, twoSense.InterestObject);
                        _interests[indexOfInterest] = threeSense;
                    }

                    else
                    {
                        InterestWithTwoSense twoSense = new InterestWithTwoSense(interestForAdd.Sense, sense,
                                                                                 interestForAdd.Priority,
                                                                                 interestForAdd.InterestObject);
                        _interests[indexOfInterest] = twoSense;
                    }
                }
        }

        public void RemoveSence(Interest curentInterest, Sense sense)
        {
            foreach (var interest in _interests)
                if (curentInterest == interest)
                {
                    Interest newInterest = interest;
                    int indexOfInterest = _interests.IndexOf(newInterest);

                    if (newInterest is InterestWithThreeSense)
                    {
                        InterestWithThreeSense threeSense = newInterest as InterestWithThreeSense;
                        Sense firstSense;
                        Sense secondSense;

                        if (sense != threeSense.Sense)
                            firstSense = threeSense.Sense;

                        else firstSense = threeSense.SecondSense;

                        if (sense != threeSense.ThirdSense)
                            secondSense = threeSense.ThirdSense;

                        else secondSense = threeSense.SecondSense;

                        InterestWithTwoSense twoSense = new InterestWithTwoSense(firstSense, secondSense,
                                                                                 threeSense.Priority,
                                                                                 threeSense.InterestObject);
                        _interests[indexOfInterest] = threeSense;
                    }

                    else if (newInterest is InterestWithTwoSense)
                    {
                        InterestWithTwoSense twoSense = newInterest as InterestWithTwoSense;
                        Sense newSense;

                        if (sense != twoSense.Sense)
                            newSense = twoSense.Sense;

                        else
                            newSense = twoSense.SecondSense;

                        newInterest = new Interest(newSense, twoSense.Priority,
                                                            twoSense.InterestObject);

                        _interests[indexOfInterest] = newInterest;
                    }

                    else
                    {
                        InterestWithTwoSense twoSense = new InterestWithTwoSense(newInterest.Sense, sense,
                                                                                 newInterest.Priority,
                                                                                 newInterest.InterestObject);
                        _interests[indexOfInterest] = twoSense;
                    }
                }
        }

        public bool IsPresent(Interest interest)
        {
            foreach (var curentInterest in _interests)
                if (curentInterest == interest)
                    return true;

            return false;
        }

        public bool IsActive(Interest interest)
        {
            foreach (var curentInterest in _interests)
                if (interest == curentInterest)
                {
                    int indexOfInterest = _interests.IndexOf(curentInterest);

                    if ((int)_interests[indexOfInterest].Priority > 0)
                        return true;
                }

            return false;
        }

        public bool IsRelevant(Interest newInterest)
        {
            foreach (var interest in _interests)
                if (interest == newInterest)
                    if (interest >= newInterest)
                        return true;

            return false;
        }
    }
}
