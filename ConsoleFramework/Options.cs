﻿using System;
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
            /// <summary>
            /// The items will be ordered horizontally.
            /// </summary>
            Horizontal,
            /// <summary>
            /// The items will be ordered vertically.
            /// </summary>
            Vertical
        }
    }
}
