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
        /// Specify the direction in which the items of a <see cref="TextList"/> will be ordered.
        /// </summary>
        public enum Direction
        { 
            Horizontal,
            Vertical
        }

        /// <summary>
        /// Specify when the viewports get initialized.
        /// </summary>
        public enum InitAtRuntime
        {
            /// <summary>
            /// Viewports get initialized at application launch.
            /// </summary>
            no,
            /// <summary>
            /// Viewports get initialized before they are drawn.
            /// </summary>
            yes
        }

        static partial class InputHandler
        {
            public static List<Options.InitAtRuntime> Properties = new List<Options.InitAtRuntime>();
        }
    }
}
