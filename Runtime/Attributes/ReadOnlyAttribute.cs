using System;
using UnityEngine;

namespace Dythervin.Core.Attributes
{
    /// <summary>
    /// Draws attribute as read only
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : PropertyAttribute { }
}