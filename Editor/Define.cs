using System;
using UnityEditor;
using UnityEngine;

namespace Dythervin.Editor
{
    [Serializable]
    public class Define
    {
        public bool enabled = true;
        public string value;
        public BuildTargetGroups platforms = BuildTargetGroups.All;

        public bool Enabled => enabled;

        public string Value => value;

        public BuildTargetGroups Platforms => platforms;

        [CustomPropertyDrawer(typeof(Define))]
        internal class DefineDrawer : PropertyDrawer
        {
            private const int CheckboxWidth = 20;
            private const int SpacingWidth = 2;
            private const int PropertyHeight = 20;
            private const int ElementCount = 3;

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                Rect initialPosition = position;
                float leftWidth = position.width;

                Define define = (Define)property.boxedValue;
                bool changed = false;

                position.width = CheckboxWidth;
                {
                    bool prevValue = define.enabled;
                    define.enabled = EditorGUI.Toggle(position, define.enabled);
                    changed |= prevValue != define.enabled;
                }

                position.x += CheckboxWidth + SpacingWidth;
                leftWidth -= CheckboxWidth + SpacingWidth;

                float textWidth = (initialPosition.width - SpacingWidth * (ElementCount - 1) - CheckboxWidth * 2) / 2;

                position.width = textWidth;
                {
                    string prevValue = define.value;
                    define.value = EditorGUI.DelayedTextField(position, define.value);
                    changed |= prevValue != define.value;
                }

                position.x += textWidth + SpacingWidth;
                leftWidth -= textWidth + SpacingWidth;

                position.width = textWidth;

                {
                    BuildTargetGroups prevValue = define.platforms;
                    define.platforms = (BuildTargetGroups)EditorGUI.EnumFlagsField(position, define.platforms);
                    changed |= prevValue != define.platforms;
                }

                position.x += textWidth + SpacingWidth;
                leftWidth -= textWidth + SpacingWidth;

                if (changed)
                {
                    property.boxedValue = define;
                    property.serializedObject.ApplyModifiedProperties();
                }
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return PropertyHeight;
            }
        }
    }
}