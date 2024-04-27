using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Dythervin
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IGameObjectAttached
    {
        GameObject gameObject { get; }

        Transform transform { get; }
    }
}