using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StateTracker.StateMachine
{
    [Serializable]
    public class State : IEquatable<State>
    {
        public string name;
        public Vector2 position;
        public List<Transition> transitions = new List<Transition>();

        public bool Equals(State other)
        {
            return name == other.name;
        }
    }

    [Serializable]
    public class Transition
    {
        public string destinationName;
        public Event action;
    }


    public class Tracker : ScriptableObject
    {
        public List<State> states = new List<State>();
        public IEnumerable<Transition> Transitions
        {
            get
            {
                var sources = states.SelectMany(s => s.transitions);
                var destinations = states.SelectMany(t => t.transitions);
                return sources.Union(destinations).Distinct();
            }
        }

    }
}