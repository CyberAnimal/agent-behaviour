using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class Interest : IEquatable<Interest>
    {
        private readonly Sense _sense;
        private readonly Attraction _attraction;
        protected Priority _priority;

        public Sense Sense => _sense;
        public Attraction Attraction => _attraction;
        public Priority Priority => _priority;

        private readonly GameObject _interestObject;
        public GameObject InterestObject => _interestObject;

        public Vector3 Position => _interestObject.transform.position;

        public Interest(Sense sense, Priority priority,
                        GameObject interestObject)
        {
            _sense = sense;
            _priority = priority;
            _interestObject = interestObject;
        }

        public void AppendPriority(int priorityForAppend)
        {
            _priority = (Priority)((int)_priority + priorityForAppend);
            ShiftPriority(0);
        }

        public void SubtractPriority(int priorityForSubtract)
        {
            _priority = (Priority)((int)_priority - priorityForSubtract);
            ShiftPriority(0);
        }

        protected void ShiftPriority(int shiftCount)
        {
            int priorityValue = (int)_priority;

            List<Priority> priorities = new List<Priority>()
            {
                Priority.Lowest,
                Priority.Broken,
                Priority.Missing,
                Priority.Suspect,
                Priority.Smoke,
                Priority.Box,
                Priority.DistractionFlare,
                Priority.Terror
            };

            Dictionary<bool, (Priority, List<Priority>)> shiftTable =
                new Dictionary<bool, (Priority, List<Priority>)>
                {
                    [priorityValue < (int)Priority.Lowest] = (Priority.Lowest, priorities),

                    [priorityValue >= (int)Priority.Lowest &&
                     priorityValue <  (int)Priority.Broken] = (Priority.Broken, priorities),

                    [priorityValue >= (int)Priority.Broken &&
                     priorityValue <  (int)Priority.Missing] = (Priority.Missing, priorities),

                    [priorityValue >= (int)Priority.Missing &&
                     priorityValue <  (int)Priority.Suspect] = (Priority.Suspect, priorities),

                    [priorityValue >= (int)Priority.Suspect &&
                     priorityValue <  (int)Priority.Smoke] = (Priority.Smoke, priorities),

                    [priorityValue >= (int)Priority.Smoke &&
                     priorityValue <  (int)Priority.Box] = (Priority.Box, priorities),

                    [priorityValue >= (int)Priority.Box &&
                     priorityValue <  (int)Priority.DistractionFlare] = (Priority.DistractionFlare, priorities),

                    [priorityValue >= (int)Priority.DistractionFlare &&
                     priorityValue <  (int)Priority.Terror ||
                     priorityValue >= (int)Priority.Terror] = (Priority.Terror, priorities)
                };

            int indexOfPrioritiesValue = priorities.IndexOf(shiftTable[true].Item1);
            int newIndex = indexOfPrioritiesValue + shiftCount;

            _priority = shiftTable[true].Item2[newIndex];
        }

        public static bool operator >(Interest newInterest, Interest oldInterest) => (int)newInterest.Priority > (int)oldInterest.Priority;
        public static bool operator <(Interest newInterest, Interest oldInterest) => (int)newInterest.Priority < (int)oldInterest.Priority;

        public static bool operator >=(Interest newInterest, Interest oldInterest) => (int)newInterest.Priority >= (int)oldInterest.Priority;
        public static bool operator <=(Interest newInterest, Interest oldInterest) => (int)newInterest.Priority <= (int)oldInterest.Priority;

        public static bool operator ==(Interest newInterest, Interest oldInterest) => newInterest.InterestObject == oldInterest.InterestObject;
        public static bool operator !=(Interest newInterest, Interest oldInterest) => newInterest.InterestObject != oldInterest.InterestObject;

        public override int GetHashCode() => ((int)_priority + 2) ^ ((int)_sense + 2);

        public override bool Equals(object obj)
        {
            if ((obj is Interest) == false)
                return false;

            return Equals((Interest)obj);
        }

        bool IEquatable<Interest>.Equals(Interest other) => _sense == other.Sense &&
                                                            _priority == other.Priority &&
                                                            _interestObject == other.InterestObject;
    }
}
