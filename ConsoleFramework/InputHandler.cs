using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFramework.Essentials
{
    static partial class InputHandler
    {
        public static List<Enum> Properties = new List<Enum> { Options.DebugModes.ShowDirectionalSelectables };
        internal static List<Viewport> Viewports = new List<Viewport>();
        static Viewport defaultDialog = new Viewport();
        public static Tuple<int, int> FinalCursorPosition = Tuple.Create(0, 0);
        public static ConsoleKeyInfo KeyPressed;

        public static Action OnInit = () => { };
        static void InitDefaultDialog()
        {
            new TextInstance(defaultDialog, $"There {(Viewports.Count is int c && c == 1 ? "is" : "are")} {c} viewport{(c == 1 ? "" : "s")}, {(c == 1 ? "which is the current one." : "and none are active:")}", 1, 1, ConsoleColor.White, ConsoleColor.Red);
            if (c == 1)
                new TextInstance(defaultDialog, "You must add at least one other viewport", 1, 2, ConsoleColor.Gray, ConsoleColor.Black);
            for (int i = 0; i < c && Viewports[i] is Viewport viewport; i++)
            {
                TextList viewportList = new TextList(defaultDialog, new string[] { $"Viewport {i + 1}:", $"{(viewport.Instances.Count is int count ? count : 0)} instance{(count == 1 ? "" : "s")}", viewport == defaultDialog ? "(this viewport)" : "" }, 1, 3 + i, ConsoleColor.White, ConsoleColor.Black, new Enum[] { Options.Direction.Horizontal });
                viewportList.Spacing = 2;
                viewportList.Items[2].Foreground = ConsoleColor.DarkGray;
            }
        }

        static void Init()
        {
            defaultDialog.Initializer = InitDefaultDialog;
            Viewports.ForEach(viewport => viewport.Initializer());
        }

        public static void WaitForInput()
        {
            Console.CursorVisible = false;
            Init();
            while (Viewports.Find(v => v.Active) is Viewport activeViewport)
            {
                if (!activeViewport.Initialized)
                    activeViewport.Initializer();
                OnInit();
                foreach (Instance instance in activeViewport.Instances)
                    instance.Update();
                activeViewport.Draw();
                Console.SetCursorPosition(Math.Min(FinalCursorPosition.Item1, Console.BufferWidth - 1), FinalCursorPosition.Item2);
                KeyPressed = Console.ReadKey(true);
                activeViewport?.ActiveSelectable?.TryAction(KeyPressed.Key);
            }
            defaultDialog.Draw();
            Console.SetCursorPosition(0, 3 + Viewports.Count);
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
