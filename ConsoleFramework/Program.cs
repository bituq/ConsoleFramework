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
            var matrix = new List<Dictionary<SelectableTextInstance, int>>();
            int size = 12;
            for (int row = 0; row < size; row++)
            {
                matrix.Add(new Dictionary<SelectableTextInstance, int>());
                for (int col = 0; col < size; col++)
                {
                    var hor = new TextList(viewport, new string[] { "-", "+" }, pos.Item1 + (2 * row), pos.Item2 + 1 + (2 * col), ConsoleColor.DarkGray, ConsoleColor.Black, new Enum[] { Options.Direction.Horizontal });
                    hor.Spacing = 1;
                    var ver = new TextList(viewport, new string[] { "|", "+" }, pos.Item1 + 1 + (2 * col), pos.Item2 + (2 * row), ConsoleColor.DarkGray, ConsoleColor.Black, new Enum[] { Options.Direction.Vertical });
                    ver.Spacing = 1;
                    var item = new SelectableTextInstance(viewport, " ", pos.Item1 + (2 * col), pos.Item2 + (2 * row), ConsoleColor.Yellow, ConsoleColor.Black);
                    matrix[row].Add(item, 0);
                    item.SelectionBackground = ConsoleColor.White;
                    item.SelectionForeground = ConsoleColor.Black;
                    if (matrix.Count == 1)
                        item.Active = true;
                    item.Variables.Add(row);
                    item.KeyActionPairs[ConsoleKey.Spacebar] = () =>
                    {
                        int current = (int)item.Variables[0];
                        if (item.Text == " ")
                            item.Text = matrix[current][item].ToString();
                        else if (matrix[current][item] == 9)
                        {
                            item.Text = " ";
                            matrix[current][item] = 0;
                        }
                        else
                        {
                            matrix[current][item]++;
                            item.Text = matrix[current][item].ToString();
                        }
                    };
                    item.KeyActionPairs[ConsoleKey.Delete] = () =>
                    {
                        item.Text = " ";
                        matrix[(int)item.Variables[0]][item] = 0;
                    };
                }
                var rowSum = new TextInstance(viewport, "Sum:", pos.Item1 + 1 + (size * 2), pos.Item2 + (2 * row), ConsoleColor.DarkGray, ConsoleColor.Black);
                rowSum.Variables.Add(row);
                rowSum.Update = () =>
                {
                    int sum = 0;
                    foreach (int n in matrix[(int)rowSum.Variables[0]].Values)
                        sum += n;
                    rowSum.Text = $"Sum: {sum}";
                };
            }
            InputHandler.WaitForInput();
        }
    }
}
