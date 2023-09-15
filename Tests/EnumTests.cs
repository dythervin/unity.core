using System;
using System.Linq;
using System.Reflection;
using Dythervin.Core.Extensions;
using NUnit.Framework;

namespace Dythervin.Core.Tests
{
    [TestFixture]
    public static class EnumTests
    {
        [TestCase(BindingFlags.ExactBinding, BindingFlags.Instance, BindingFlags.Public, BindingFlags.Static)]
        [TestCase(BindingFlags.FlattenHierarchy, BindingFlags.SetField, BindingFlags.NonPublic, BindingFlags.DeclaredOnly)]
        public static void TestEnumerator(params BindingFlags[] array)
        {
            BindingFlags flags = default;
            foreach (BindingFlags bindingFlags in array)
            {
                if (flags.HasFlagFast(bindingFlags))
                    throw new Exception("No duplicates allowed");

                flags |= bindingFlags;
            }

            var newArray = flags.ToEnumerable().ToArray();
            foreach (BindingFlags flag in newArray)
            {
                Assert.IsTrue(array.Contains(flag));
            }
        }
    }
}