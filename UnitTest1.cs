using System;
using Xunit;
using MazeApp;

namespace TestMaze
{
    public class UnitTest1
    {
        [Fact]
        public void TestCreateMethodUsesDesiredSizing()
        {
            var mazeCreator = new MazeCreator();
            var testMaze = mazeCreator.Create();
            const int height = MazeCreator.Height;
            const int width = MazeCreator.Width;
            Assert.NotEmpty(testMaze);
            Assert.True(testMaze.Length is height*width);
        }

        [Fact]
        public void TestCreateBorderMethod()
        {
            var mazeCreator = new MazeCreator();
            var testMaze = mazeCreator.Create();
            const int height = MazeCreator.Height;
            const int width = MazeCreator.Width;
            for(var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (i is 0 or height-1 || j is 0 or width-1)
                    {
                        Assert.Equal("X", testMaze[j, i]);
                    }
                }
            }
        }
        
        /*[Fact]
        public void TestThatStartAndExitExist()
        {
            var mazeCreator = new MazeCreator();
            var testMaze = mazeCreator.Create();
            var mazeSolver = new MazeSolver();
            mazeSolver?.Run(testMaze);
            Assert.Contains("S", mazeSolver.GetMaze()[mazeSolver.GetStartX()[0], mazeSolver.GetStartY()[0]]);
        }*/
    }
}
