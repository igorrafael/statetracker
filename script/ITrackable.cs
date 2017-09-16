using System.Collections.Generic;

namespace StateTracker
{
    public interface ITrackable<T>
    {
        T[] States
        {
            get;
        }
        Dictionary<T, T[]> Transitions
        {
            get;
        }
    }
}