using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFramework.Data
{
    class Cell
    {
        ConsoleColor defaultForeground => ConsoleColor.White;
        ConsoleColor defaultBackground => ConsoleColor.Black;

        public Cell(Char Character, int X, int Y, ConsoleColor Foreground, ConsoleColor Background)
        {
            this.Character = Character;
            this.X = X;
            this.Y = Y;
            this.Foreground = Foreground;
            this.Background = Background;
        }
        public Cell(int X, int Y)
        {
            this.Character = ' ';
            this.X = X;
            this.Y = Y;
            this.Foreground = ConsoleColor.White;
            this.Background = ConsoleColor.Black;
        }

        public ConsoleColor Foreground;
        public ConsoleColor Background;
        public Char Character;
        public int X;
        public int Y;

        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
            Console.Write(Character);
        }

        public void Clean()
        {
            Foreground = defaultForeground;
            Background = defaultBackground;
            Character = ' ';
        }

        public bool PositionEquals(Cell cell) => X == cell.X && Y == cell.Y;
    }
}
