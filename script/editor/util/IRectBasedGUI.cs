using UnityEngine;

namespace StateTracker.Editor
{
    internal interface IRectBasedGUI
    {
        Rect rect
        {
            get;
        }
        void OnGUI(Rect rect);
    }
}