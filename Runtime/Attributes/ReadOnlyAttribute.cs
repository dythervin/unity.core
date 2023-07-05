using System;
using System.Diagnostics;
using Dythervin.Core.Utils;
using UnityEngine;

namespace Dythervin.Core.Attributes
{
    /// <summary>
    /// Draws attribute as read only
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional(Symbols.UNITY_EDITOR)]
    public class ReadOnlyAttribute : PropertyAttribute
    {
    }
}