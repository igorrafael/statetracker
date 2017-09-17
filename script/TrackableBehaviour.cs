using System;
using UnityEngine;
using StateTracker.StateMachine;

namespace StateTracker
{
    public abstract class TrackableBehaviour<T> : MonoBehaviour, ITrackable<T>
        where T : struct, IConvertible
    {
        [SerializeField]
        private Tracker _stateMachine;

        public Tracker StateMachine
        {
            get
            {
                return _stateMachine;
            }
        }

        public abstract T CurrentState
        {
            get;
        }
    }
}