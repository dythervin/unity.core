using System;
using UnityEditor;

namespace Dythervin.Editor
{
    /// <summary>
    ///   <para>Build target groups.</para>
    /// </summary>
    [Flags]
    public enum BuildTargetGroups : uint
    {
        None = 0,

        /// <summary>
        ///   <para>PC (Windows, Mac, Linux) target.</para>
        /// </summary>
        Standalone = 1 << 0,

        /// <summary>
        ///   <para>Mac/PC webplayer target.</para>
        /// </summary>
        /// <summary>
        ///   <para>Apple iOS target.</para>
        /// </summary>
        iOS = 1 << 1,

        /// <summary>
        ///   <para>Android target.</para>
        /// </summary>
        Android = 1 << 2,

        /// <summary>
        ///   <para>WebGL.</para>
        /// </summary>
        WebGL = 1 << 3, // 0x0000000D

        /// <summary>
        ///   <para>Windows Store Apps target.</para>
        /// </summary>
        WSA = 1 << 4, // 0x0000000E

        /// <summary>
        ///   <para>Sony Playstation 4 target.</para>
        /// </summary>
        PS4 = 1 << 5, // 0x00000013

        /// <summary>
        ///   <para>Microsoft Xbox One target.</para>
        /// </summary>
        XboxOne = 1 << 6, // 0x00000015

        /// <summary>
        ///   <para>Nintendo 3DS target.</para>
        /// </summary>
        /// <summary>
        ///   <para>Apple's tvOS target.</para>
        /// </summary>
        tvOS = 1 << 7, // 0x00000019

        /// <summary>
        ///   <para>Nintendo Switch target.</para>
        /// </summary>
        Switch = 1 << 8, // 0x0000001B

        /// <summary>
        ///   <para>Google Stadia target.</para>
        /// </summary>
        Stadia = 1 << 9, // 0x0000001D

        /// <summary>
        ///   <para>CloudRendering target.</para>
        /// </summary>
        /// <summary>
        ///   <para>LinuxHeadlessSimulation target.</para>
        /// </summary>
        LinuxHeadlessSimulation = 1 << 10, // 0x0000001E
        GameCoreXboxSeries = 1 << 11, // 0x0000001F
        GameCoreXboxOne = 1 << 12, // 0x00000020

        /// <summary>
        ///   <para>Sony Playstation 5 target.</para>
        /// </summary>
        PS5 = 1 << 13, // 0x00000021
        EmbeddedLinux = 1 << 14, // 0x00000022
        QNX = 1 << 15, // 0x00000023

        /// <summary>
        ///   <para>Apple visionOS target.</para>
        /// </summary>
        VisionOS = 1 << 16, // 0x00000024
        All = uint.MaxValue,
    }

    public static class BuildTargetGroupsExtensions
    {
        public static bool HasFlagFast(this BuildTargetGroups value, BuildTargetGroups flag)
        {
            return (value & flag) != 0;
        }

        public static BuildTargetGroup ToBuildTargetGroup(this BuildTargetGroups value)
        {
            var buildTargetGroup = ToBuildTargetGroupInternal(value);
            if (buildTargetGroup != null)
                return buildTargetGroup.Value;

            throw new ArgumentOutOfRangeException(nameof(value), value, null);
        }

        public static bool TryToBuildTargetGroup(this BuildTargetGroups value, out BuildTargetGroup buildTargetGroup)
        {
            var buildTargetGroupInternal = ToBuildTargetGroupInternal(value);
            if (buildTargetGroupInternal != null)
            {
                buildTargetGroup = buildTargetGroupInternal.Value;
                return true;
            }

            buildTargetGroup = BuildTargetGroup.Unknown;
            return false;
        }

        private static BuildTargetGroup? ToBuildTargetGroupInternal(BuildTargetGroups value)
        {
            switch (value)
            {
                case BuildTargetGroups.None:
                    return BuildTargetGroup.Unknown;
                case BuildTargetGroups.Standalone:
                    return BuildTargetGroup.Standalone;
                case BuildTargetGroups.iOS:
                    return BuildTargetGroup.iOS;
                case BuildTargetGroups.Android:
                    return BuildTargetGroup.Android;
                case BuildTargetGroups.WebGL:
                    return BuildTargetGroup.WebGL;
                case BuildTargetGroups.WSA:
                    return BuildTargetGroup.WSA;
                case BuildTargetGroups.PS4:
                    return BuildTargetGroup.PS4;
                case BuildTargetGroups.XboxOne:
                    return BuildTargetGroup.XboxOne;
                case BuildTargetGroups.tvOS:
                    return BuildTargetGroup.tvOS;
                case BuildTargetGroups.Switch:
                    return BuildTargetGroup.Switch;
                case BuildTargetGroups.Stadia:
                    return BuildTargetGroup.Stadia;
                case BuildTargetGroups.LinuxHeadlessSimulation:
                    return BuildTargetGroup.LinuxHeadlessSimulation;
                case BuildTargetGroups.GameCoreXboxSeries:
                    return BuildTargetGroup.GameCoreXboxSeries;
                case BuildTargetGroups.GameCoreXboxOne:
                    return BuildTargetGroup.GameCoreXboxOne;
                case BuildTargetGroups.PS5:
                    return BuildTargetGroup.PS5;
                case BuildTargetGroups.EmbeddedLinux:
                    return BuildTargetGroup.EmbeddedLinux;
                case BuildTargetGroups.QNX:
                    return BuildTargetGroup.QNX;
                case BuildTargetGroups.VisionOS:
                    return BuildTargetGroup.VisionOS;
                default:
                    return null;
            }
        }

        public static BuildTargetGroups ToBuildTargetGroups(this BuildTargetGroup value)
        {
            switch (value)
            {
                case BuildTargetGroup.Unknown:
                    return BuildTargetGroups.None;
                case BuildTargetGroup.Standalone:
                    return BuildTargetGroups.Standalone;
                case BuildTargetGroup.iOS:
                    return BuildTargetGroups.iOS;
                case BuildTargetGroup.Android:
                    return BuildTargetGroups.Android;
                case BuildTargetGroup.WebGL:
                    return BuildTargetGroups.WebGL;
                case BuildTargetGroup.WSA:
                    return BuildTargetGroups.WSA;
                case BuildTargetGroup.PS4:
                    return BuildTargetGroups.PS4;
                case BuildTargetGroup.XboxOne:
                    return BuildTargetGroups.XboxOne;
                case BuildTargetGroup.tvOS:
                    return BuildTargetGroups.tvOS;
                case BuildTargetGroup.Switch:
                    return BuildTargetGroups.Switch;
                case BuildTargetGroup.Stadia:
                    return BuildTargetGroups.Stadia;
                case BuildTargetGroup.LinuxHeadlessSimulation:
                    return BuildTargetGroups.LinuxHeadlessSimulation;
                case BuildTargetGroup.GameCoreXboxSeries:
                    return BuildTargetGroups.GameCoreXboxSeries;
                case BuildTargetGroup.GameCoreXboxOne:
                    return BuildTargetGroups.GameCoreXboxOne;
                case BuildTargetGroup.PS5:
                    return BuildTargetGroups.PS5;
                case BuildTargetGroup.EmbeddedLinux:
                    return BuildTargetGroups.EmbeddedLinux;
                case BuildTargetGroup.QNX:
                    return BuildTargetGroups.QNX;
                case BuildTargetGroup.VisionOS:
                    return BuildTargetGroups.VisionOS;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}