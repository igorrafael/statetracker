using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace StateTracker
{
    public class TrackableBehaviour<T> : MonoBehaviour, ITrackable<T>
        where T : struct, IConvertible
    {
        public T[] States
        {
            get
            {
                Array values = Enum.GetValues(typeof(T));
                return values as T[];
            }
        }

        public Dictionary<T, T[]> Transitions
        {
            get
            {
                return _transitions;
            }
        }

        private Dictionary<T, T[]> _transitions = new Dictionary<T, T[]>();
    }
}