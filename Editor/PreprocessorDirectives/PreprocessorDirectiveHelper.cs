using System.Collections.Generic;
using UnityEditor;

namespace Dythervin.Editor
{
    internal class PreprocessorDirectiveHelper : ScriptableSingleton<PreprocessorDirectiveHelper>
    {
        private readonly HashSet<PreprocessorDirective> _directives = new HashSet<PreprocessorDirective>();

        private void OnEnable()
        {
            
        }
    }
}