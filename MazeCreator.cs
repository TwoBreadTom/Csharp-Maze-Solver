using System;
using System.Runtime.InteropServices;

namespace MazeApp
{
    public class MazeCreator : IMazeCreator
    {
        public const int Height = 40;
        public const int Width = 40;

        public void PrintMaze(string[,] maze)
        {
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    Console.Write(maze[i,j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void CreateBorder(string[,] maze)
        {
            for(var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    if (i is 0 or Height-1 || j is 0 or Width-1)
                    {
                        maze[i, j] = "X";
                    }
                }
            }
        }

        private static void FillMaze(string[,] maze, int h, int w, int x, int y)
        {
            while (true)
            {
                var rand = new Random();
                if (h <= 2 || w <= 2)
                {
                    return;
                }

                if (h >= w)
                {
                    var selectH1 = rand.Next(minValue: 0, maxValue: (h - 2)) + y + 1;
                    for (var i = 0; i < w; i++)
                    {
                        maze[selectH1, i + x] = "X";
                    }

                    if (maze[selectH1, x + w] == " ")
                    {
                        maze[selectH1, x + w - 1] = " "; // if hole cant me accessed, make it so it can be
                    }

                    if (maze[selectH1, x - 1] == " ")
                    {
                        maze[selectH1, x] = " "; // if hole that cant me accessed, make it so it can be
                    }

                    var selectW1 = rand.Next(minValue: 0, maxValue: w) + x;
                    maze[selectH1, selectW1] = " "; // poke hole in new line
                    FillMaze(maze, selectH1 - y, w, x, y);
                    h = h - selectH1 + y - 1;
                    y = selectH1 + 1;
                    continue;
                }

                if (w <= h) return;
                {
                    var selectW2 = rand.Next(minValue: 0, maxValue: (w - 2)) + (x + 1);
                    for (var i = 0; i < h; i++)
                    {
                        maze[i + y, selectW2] = "X";
                    }

                    if (maze[y + h, selectW2] == " ")
                    {
                        maze[y + h - 1, selectW2] = " ";
                    }

                    if (maze[y - 1, selectW2] == " ")
                    {
                        maze[y, selectW2] = " ";
                    }

                    var selectH2 = rand.Next(minValue: 0, maxValue: h) + y;
                    maze[selectH2, selectW2] = " ";
                    FillMaze(maze, h, selectW2 - x, x, y);
                    w = w - selectW2 + x - 1;
                    x = selectW2 + 1;
                }
            }
        }

        private static void PlaceExit(string[,] maze)
        {
            var rand = new Random();
            var exitI = rand.Next(1, Height - 1);
            var exitJ = rand.Next(1, Width - 1);
            while (maze[exitI, exitJ] == "X")
            {
                exitI = rand.Next(1, Height - 1);
                exitJ = rand.Next(1, Width - 1);
            }
            maze[exitI, exitJ] = "E";
        }
        
        
        public string[,] Create()
        {
            var maze = new string[Width, Height];
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    maze[i, j] = " ";
                }
            }
            CreateBorder(maze);
            FillMaze(maze, Height - 2, Width - 2, 1, 1);
            PlaceExit(maze);
            return maze;
        }
    }
}