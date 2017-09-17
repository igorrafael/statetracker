using UnityEditor;
using UnityEngine;

namespace StateTracker
{
    public class MenuItems
    {
        [MenuItem("Assets/Create/My Scriptable Object")]
        public static void CreateMyAsset()
        {
            var asset = ScriptableObject.CreateInstance<StateMachine.Tracker>();

            AssetDatabase.CreateAsset(asset, "Assets/NewTracker.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}
