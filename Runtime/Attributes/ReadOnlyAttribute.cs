using System;
using System.Diagnostics;
using UnityEngine;

namespace Dythervin
{
    /// <summary>
    /// Draws attribute as read only
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional(Symbols.UNITY_EDITOR)]
    public class ReadOnlyAttribute : PropertyAttribute
    {
    }
    
    /// <summary>
    /// Draws attribute as read only
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional(Symbols.UNITY_EDITOR)]
    public class ReadOnlyIfAttribute : PropertyAttribute
    {
        public readonly string value;

        public ReadOnlyIfAttribute(string value)
        {
            this.value = value;
        }
    }
}