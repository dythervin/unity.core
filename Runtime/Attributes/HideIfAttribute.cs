using System;
using System.Diagnostics;
using UnityEngine;

namespace Dythervin
{
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional(Symbols.UNITY_EDITOR)]
    public class HideIfAttribute : PropertyAttribute
    {
        public readonly string value;

        public HideIfAttribute(string value)
        {
            this.value = value;
        }
    }
}