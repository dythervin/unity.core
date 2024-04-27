using System;
using System.Diagnostics;
using UnityEngine;

namespace Dythervin
{
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional(Symbols.UNITY_EDITOR)]
    public class ShowIfAttribute : PropertyAttribute
    {
        public readonly string value;

        public ShowIfAttribute(string value)
        {
            this.value = value;
        }
    }
}