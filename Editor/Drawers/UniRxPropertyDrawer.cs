#if UNIRX
using R3;
using UnityEditor;

namespace Dythervin.Editor
{
    [CustomPropertyDrawer(typeof(ReactiveProperty<>))]
    public class UniRxPropertyDrawer : SimpleGenericDrawer
    {
    }
}
#endif