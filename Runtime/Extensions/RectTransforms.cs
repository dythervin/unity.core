#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Dythervin.Core.Extensions
{
    public static class RectTransforms
    {
        // Add a menu item called "Double Mass" to a Rigidbody's context menu.
        [MenuItem("CONTEXT/RectTransform/SetAnchors")]
        private static void SetAnchors(MenuCommand command)
        {
            RectTransform target = (RectTransform)command.context;
            if (target.parent == null || !(target.parent is RectTransform parent))
                return;

            Undo.RecordObject(target, nameof(SetAnchors));
            Rect parentSize = parent.rect;
            if (target.anchorMax != Vector2.one || target.anchorMin != Vector2.zero)
            {
                target.parent = null;
                target.anchorMin = Vector2.zero;
                target.anchorMax = Vector2.one;
                target.parent = parent;
                return;
            }

            Vector2 min = target.offsetMin;
            Vector2 max = target.offsetMax * -1;
            target.anchorMin = new Vector2(min.x / parentSize.width, min.y / parentSize.height);
            target.anchorMax = new Vector2(1 - max.x / parentSize.width, 1 - max.y / parentSize.height);
            target.offsetMax = Vector2.zero;
            target.offsetMin = Vector2.zero;
            target.Dirty();
        }
    }
}
#endif