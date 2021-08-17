using System;
using Microsoft.Extensions.DependencyInjection;
using static MazeApp.MazeCreator;
using System.Collections.Generic;

namespace MazeApp
{
    public class MazeSolver : IMazeSolver
    {
        private static void PrintStep(string[,] maze, int currentX, int currentY)
        {
            var mazeCreator = new MazeCreator();
            if (maze[currentY, currentX] != "S")
            {
                maze[currentY, currentX] = "@";
                mazeCreator?.PrintMaze(maze);
                maze[currentY, currentX] = " ";
            }
            else
            {
                maze[currentY, currentX] = "S";
                mazeCreator?.PrintMaze(maze);
            }
        }

        private static Dictionary<string,object> Search(string[,] maze, bool[,] visited, int x, int y, Dictionary<string,object> thisDict, IList<int> xLoc, IList<int> yLoc)
        {
            thisDict["foundExit"] = false;
            if (maze[y, x] == "E")
            {
                thisDict["foundExit"] = true;
                return thisDict;
            }
            visited[y, x] = true;
            if (ValidMove(maze, visited, x, y - 1))
            {
                thisDict = Search(maze, visited, x, y - 1, thisDict, xLoc, yLoc);
            }
            if ((bool)thisDict["foundExit"] == false && ValidMove(maze, visited, x, y + 1))
            {
                thisDict = Search(maze, visited, x, y + 1, thisDict, xLoc, yLoc);
            }
            if ((bool)thisDict["foundExit"] == false && ValidMove(maze, visited, x - 1, y))
            {
                thisDict = Search(maze, visited, x - 1, y, thisDict, xLoc, yLoc);
            }
            if ((bool)thisDict["foundExit"] == false && ValidMove(maze, visited, x + 1, y))
            {
                thisDict = Search(maze, visited, x + 1, y, thisDict, xLoc, yLoc);
            }

            if (!(bool)thisDict["foundExit"]) return thisDict;
            xLoc[(int)thisDict["moveNum"]] = x;
            yLoc[(int)thisDict["moveNum"]] = y;
            thisDict["moveNum"] = (int)thisDict["moveNum"] + 1;
            return thisDict;
        }

        private static bool ValidMove(string[,] maze, bool[,] visited, int newX, int newY)
        {
            if (newX is < 0 or > Width)
            {
                return false;
            }
            if (newY is < 0 or > Height)
            {
                return false;
            }
            if (maze[newY, newX] == "X")
            {
                return false;
            }
            return !visited[newY, newX];
        }

        private void PlaceStart(string[,] maze, IList<int> xLoc, IList<int> yLoc)
        {
            var rand = new Random();
            var startI = rand.Next(1, Height - 1);
            var startJ = rand.Next(1, Width - 1);
            while (maze[startI, startJ] == "X" || maze[startI,startJ] == "E")
            {
                startI = rand.Next(1, Height - 1);
                startJ = rand.Next(1, Width - 1);
            }
            maze[startJ, startI] = "S";
            xLoc[0] = startI;
            yLoc[0] = startJ;
            PrintStep(maze, xLoc[0], yLoc[0]);
        }

        private static bool[,] InitializeVisited()
        {
            var visited = new bool[Height,Width];
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    visited[i,j] = false;
                }
            }
            return visited;
        }
        
        public void Run(string[,] maze)
        {
            var thisDict = new Dictionary<string, object>
            {
                { "foundExit", false },
                { "moveNum", 0 }
            };
            var visited = InitializeVisited();
            var xLoc = new int[1000];
            var yLoc = new int[1000];
            PlaceStart(maze, xLoc, yLoc);
            Console.WriteLine(Search(maze, visited, xLoc[0], yLoc[0], thisDict, xLoc, yLoc));
            for (var i = (int)thisDict["moveNum"] - 1; i >= 0; i--)
            {
                PrintStep(maze, xLoc[i], yLoc[i]);
                Console.WriteLine("Move Number: " + ((int)thisDict["moveNum"] - i - 1) + "\n" + "Coordinates: (" + xLoc[i] + "," + yLoc[i] + ")");
            }
            if ((int)thisDict["moveNum"] <= 0)
            {
                Console.WriteLine("Maze was not solvable. Please try again.\n");
            }
        }
    }
}