using System;
using System.Collections.Generic;
using System.Text;
using ConsoleFramework.Essentials;

namespace ConsoleFramework
{
    /// <summary>
    /// A static class which contains enums to describe the properties of an instance.
    /// </summary>
    public static class Options
    {
        /// <summary>
        /// <list type="table">
        /// <item>
        /// <term><see cref="TextInstanceList{T}"/></term>
        /// <description>The order in which the items will be displayed.</description>
        /// </item>
        /// </list>
        /// </summary>
        public enum Direction
        {
            /// <summary>
            /// The items will be ordered horizontally.
            /// </summary>
            Horizontal,
            /// <summary>
            /// The items will be ordered vertically.
            /// </summary>
            Vertical
        }

        /// <summary>
        /// <list type="table"
        /// <item>
        /// <term><see cref="InputHandler"/></term>
        /// <description>Passive debug modes that apply to inputhandler.</description>
        /// </item>
        /// </summary>
        public enum DebugModes
        {
            /// <summary>
            /// When a selectable is active, the next selectable to be selected will be shown.
            /// </summary>
            ShowDirectionalSelectables,
            /// <summary>
            /// When a selectable is active, the distances to all other selectables will be color coded.
            /// </summary>
            ShowDistanceValues
        }
    }
}
