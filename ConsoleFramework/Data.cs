using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

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
        public char Character;
        public int X;
        public int Y;
        public int MemoryLength = 0;

        public void Draw()
        {
            Console.SetCursorPosition(Math.Min(X, Console.BufferWidth - 1), Y);
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
            Console.Write(Character);
        }

        public void Clean()
        {
            Foreground = defaultForeground;
            Background = defaultBackground;
            Character = ' ';
            MemoryLength = 0;
        }

        public bool PositionEquals(Cell cell) => X == cell.X && Y == cell.Y;
        public static bool operator ==(Cell a, Cell b) => a.Foreground == b.Foreground && a.Background == b.Background && a.Character == b.Character && a.PositionEquals(b);
        public static bool operator !=(Cell a, Cell b) => !(a == b);
        public override string ToString() => Character.ToString();
    }
}
