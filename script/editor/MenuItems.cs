using UnityEditor;
using UnityEngine;

namespace StateTracker
{
    public class MenuItems
    {
        [MenuItem("Assets/Create/My Scriptable Object")]
        public static void CreateMyAsset()
        {
            var asset = ScriptableObject.CreateInstance<StateMachine>();

            AssetDatabase.CreateAsset(asset, "Assets/NewScripableObject.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}