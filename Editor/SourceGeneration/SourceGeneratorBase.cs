using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[assembly: Guid("CB292017-9777-47DB-9593-501BE3BC8E84")]

namespace Dythervin.Editor
{
    public abstract class SourceGeneratorBase : ScriptableObject
    {
        [Tooltip("If null, will try to find AssemblyDefinitionAsset in the same folder as path")]
        [SerializeField] private AssemblyDefinitionAsset? _targetAssembly;

        [HideIf(nameof(_targetAssembly))]
        [SerializeField] private string _path = "Assets/Scripts/Generated";

        [ShowIf(nameof(_targetAssembly))]
        [Tooltip("Local path to the assembly folder")]
        [SerializeField] private string _localPath = "Generated";

        [SerializeField] private string? _name;
        [SerializeField] private bool _prefixAssemblyNamespace = true;
        [SerializeField] private string? _namespace;

        [ContextMenu("Generate")]
        public void Generate()
        {
            if (!TryGetPath(out string? fullPath,
                    out string? folderPath,
                    out AssemblyDefinitionAsset? assemblyDefinitionAsset,
                    out Assembly? assembly))
            {
                return;
            }

            Generate(fullPath, folderPath, assembly, assemblyDefinitionAsset);
        }

        protected abstract bool Generate(string fullPath, string folderPath, Assembly assembly,
            AssemblyDefinitionAsset? assemblyDefinitionAsset);

        public string? Name => _name;

        public string? Namespace
        {
            get
            {
                if (_targetAssembly != null && _prefixAssemblyNamespace)
                {
                    string? rootNamespace = _targetAssembly.GetData().rootNamespace;
                    if (!string.IsNullOrWhiteSpace(rootNamespace))
                    {
                        if (!string.IsNullOrWhiteSpace(_namespace))
                            return rootNamespace + "." + _namespace;

                        return rootNamespace;
                    }
                }

                return _namespace;
            }
        }

        public bool TryGetPath([NotNullWhen(true)] out string? fullPath, [NotNullWhen(true)] out string? folderPath)
        {
            return TryGetPath(out fullPath, out folderPath, out _, out _);
        }

        public bool TryGetPath([NotNullWhen(true)] out string? fullPath, [NotNullWhen(true)] out string? folderPath,
            out UnityEditorInternal.AssemblyDefinitionAsset? assemblyDefinitionAsset,
            [NotNullWhen(true)] out Assembly? assembly)
        {
            if (_targetAssembly != null)
            {
                assemblyDefinitionAsset = _targetAssembly;
                assembly = _targetAssembly.GetAssembly();
                string? assemblyFolder = Path.GetDirectoryName(AssetDatabase.GetAssetPath(_targetAssembly));
                if (!string.IsNullOrWhiteSpace(_localPath))
                {
                    folderPath = assemblyFolder != null ? Path.Combine(assemblyFolder, _localPath) : _localPath;
                    assemblyDefinitionAsset = AssemblyExt.GetAssemblyDefinitionAsset(folderPath);
                    if (assemblyDefinitionAsset != null)
                    {
                        var localAssembly = assemblyDefinitionAsset.GetAssembly();
                        assembly = localAssembly;
                    }
                }
                else if (assemblyFolder != null)
                {
                    folderPath = assemblyFolder;
                }
                else
                {
                    fullPath = null;
                    folderPath = null;
                    Debug.LogError("Could not find assembly folder");
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(_path) || string.IsNullOrWhiteSpace(Name))
                {
                    fullPath = null;
                    folderPath = null;
                    assembly = null;
                    Debug.LogError("Path or Name is null");
                    assemblyDefinitionAsset = null;
                    return false;
                }

                folderPath = _path;
                assemblyDefinitionAsset = AssemblyExt.GetAssemblyDefinitionAsset(folderPath);
                assembly = null;
                if (assemblyDefinitionAsset != null)
                {
                    assembly = assemblyDefinitionAsset.GetAssembly();
                }

                if (assembly == null)
                {
                    var regex = new System.Text.RegularExpressions.Regex(@"^Assets[/\\]");
                    if (regex.IsMatch(folderPath))
                    {
                        assembly = Assembly.Load("Assembly-CSharp");
                    }
                    else
                    {
                        throw new FileNotFoundException($"Could not find assembly for {folderPath}");
                    }
                }
            }

            fullPath = Path.Combine(folderPath, $"{Name}.g.cs");
            return true;
        }
    }

    [CustomEditor(typeof(SourceGeneratorBase), true)]
    public class SourceGeneratorBaseEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Generate"))
            {
                ((SourceGeneratorBase)target).Generate();
            }
        }
    }
}