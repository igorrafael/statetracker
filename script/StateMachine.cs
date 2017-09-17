using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StateTracker
{
    public class StateMachine : ScriptableObject
    {
        [Serializable]
        public class State : IEquatable<State>
        {
            public string name;

            public bool Equals(State other)
            {
                return name == other.name;
            }
        }

        [Serializable]
        public class Transition
        {
            public State source, destination;
            public Event action;
        }

        public List<Transition> Transitions = new List<Transition>();
        public IEnumerable<State> States
        {
            get
            {
                var sources = Transitions.Select(t => t.source);
                var destinations = Transitions.Select(t => t.destination);
                return sources.Union(destinations).Distinct();
            }
        }

    }
}