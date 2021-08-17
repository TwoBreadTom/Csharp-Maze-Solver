using System;
using Microsoft.Extensions.DependencyInjection;

namespace MazeApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection(); // create new ServiceCollection object
            serviceCollection.AddSingleton<IMazeSolver, MazeSolver>(); // add DiceGame class to service collection and include the interface
            serviceCollection.AddSingleton<IMazeCreator, MazeCreator>();
            var serviceProvider = serviceCollection.BuildServiceProvider(); // build the service provider
            var mazeSolver = serviceProvider.GetService<IMazeSolver>(); // initialize diceGame as the DiceGame class through the IDiceGame interface in serviceProvider
            var mazeCreator = serviceProvider.GetService<IMazeCreator>();
            var maze = mazeCreator?.Create();
            mazeSolver?.Run(maze); 
        }
    }
}