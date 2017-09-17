using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace StateTracker
{
    public abstract class TrackableBehaviour<T> : MonoBehaviour, ITrackable<T>
        where T : struct, IConvertible
    {
        [SerializeField]
        private StateMachine _stateMachine;

        public StateMachine StateMachine
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