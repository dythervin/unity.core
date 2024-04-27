using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Dythervin.Editor
{
    [CreateAssetMenu(fileName = "PreprocessorDirectives", menuName = "PreprocessorDirectives", order = 0)]
    public class PreprocessorDirective : ScriptableObject
    {
        [SerializeField] private Define[] defines;

        public void ImportAll()
        {
            var definesMap = new Dictionary<string, Define>();
            foreach (Define define in this.defines)
            {
                definesMap.Add(define.value, define);
            }

            foreach (BuildTargetGroup buildTargetGroup in BuildTargetGroup.Unknown.GetValues())
            {
                string[] scriptingDefineSymbolsForGroup;
                try
                {
                    PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup,
                        out scriptingDefineSymbolsForGroup);
                }
                catch (ArgumentException)
                {
                    continue;
                }

                foreach (string value in scriptingDefineSymbolsForGroup)
                {
                    if (definesMap.TryGetValue(value, out Define define))
                    {
                        define.platforms |= buildTargetGroup.ToBuildTargetGroups();
                    }
                    else
                    {
                        definesMap[value] = new Define()
                        {
                            value = value,
                            enabled = true,
                            platforms = buildTargetGroup.ToBuildTargetGroups()
                        };
                    }
                }
            }

            defines = definesMap.Values.ToArray();
            this.Dirty();
        }

        public void Apply()
        {
            var dict = new Dictionary<BuildTargetGroup, List<string>>();

            foreach (Define define in defines)
            {
                string value = define.Value;
                if (string.IsNullOrEmpty(value))
                    continue;

                BuildTargetGroups definePlatforms = define.Platforms;
                foreach (BuildTargetGroups platform in definePlatforms.ToEnumerable())
                {
                    if (!platform.TryToBuildTargetGroup(out BuildTargetGroup buildTargetGroup) ||
                        buildTargetGroup == BuildTargetGroup.Unknown)
                        continue;

                    if (!dict.TryGetValue(buildTargetGroup, out var defineSymbols))
                    {
                        PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup, out string[] symbols);
                        defineSymbols = symbols.ToList();
                        dict[buildTargetGroup] = defineSymbols;
                    }

                    int index = defineSymbols.IndexOf(value);
                    if (define.Enabled)
                    {
                        if (index >= 0)
                            continue;

                        defineSymbols.Add(value);
                    }
                    else
                    {
                        if (index == -1)
                            continue;

                        defineSymbols.RemoveAt(index);
                    }
                }
            }

            foreach (var platform in dict)
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(platform.Key, platform.Value.ToArray());
            }
        }
    }
}