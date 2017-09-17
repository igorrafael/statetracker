using System;

namespace StateTracker
{
    public interface ITrackable<T> where T : struct, IConvertible
    {
        StateMachine.Tracker StateMachine
        {
            get;
        }

        T CurrentState
        {
            get;
        }
    }
}