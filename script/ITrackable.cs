using System;

namespace StateTracker
{
    public interface ITrackable<T> where T : struct, IConvertible
    {
        StateMachine StateMachine
        {
            get;
        }

        T CurrentState
        {
            get;
        }
    }
}