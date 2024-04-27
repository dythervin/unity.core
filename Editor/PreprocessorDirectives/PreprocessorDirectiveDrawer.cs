using UnityEditor;
using UnityEngine;

namespace Dythervin.Editor
{
    [CustomEditor(typeof(PreprocessorDirective), true)]
    public class PreprocessorDirectiveDrawer : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button(nameof(PreprocessorDirective.Apply)))
            {
                ((PreprocessorDirective)target).Apply();
            }
            if (GUILayout.Button(nameof(PreprocessorDirective.ImportAll)))
            {
                ((PreprocessorDirective)target).ImportAll();
            }
        }
    }
}