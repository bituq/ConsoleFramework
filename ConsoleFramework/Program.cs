using System;
using System.Collections.Generic;
using ConsoleFramework.Essentials;

namespace ConsoleFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Tuple<int, int> pos = new Tuple<int, int>(1, 1);
            var viewport = new Viewport(true);
            var matrix = new List<SelectableTextList>();
            int size = 6;
            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++)
                {
                    var hor = new TextList(viewport, new string[] { "-", "+" }, pos.Item1 + (2 * row), pos.Item2 + 1 + (2 * col), ConsoleColor.White, ConsoleColor.Black, new Enum[] { Options.Direction.Horizontal });
                    hor.Spacing = 1;
                    var ver = new TextList(viewport, new string[] { "|", "+", "|" }, pos.Item1 + 1 + (2 * col), pos.Item2 + (2 * row), ConsoleColor.White, ConsoleColor.Black, new Enum[] { Options.Direction.Vertical });
                    ver.Spacing = 1;
                    var block = new SelectableTextList(viewport, new string[] { " ", " " }, pos.Item1 + (2 * col), pos.Item2 + (2 * row), ConsoleColor.Yellow, ConsoleColor.Black, new Enum[] { Options.Direction.Vertical });
                    block.SelectedBackground = ConsoleColor.White;
                    foreach (SelectableTextInstance item in block.Items)
                        item.KeyActionPairs[ConsoleKey.Spacebar] = () =>
                        {
                            if (item.Text == " ")
                                item.Text = "0";
                            else
                            {
                                int n = ;
                                item.Text = 
                            }
                        };
                    matrix.Add(block);
                }
            InputHandler.WaitForInput();
        }
    }
}
