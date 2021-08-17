using Microsoft.Extensions.DependencyInjection;

namespace MazeApp
{
    internal static class Program
    {
        private static void Main()
        {
            var serviceCollection = new ServiceCollection(); // create new ServiceCollection object
            serviceCollection.AddSingleton<IMazeSolver, MazeSolver>(); // add MazeSolver class to service collection and include the interface
            serviceCollection.AddSingleton<IMazeCreator, MazeCreator>();
            var serviceProvider = serviceCollection.BuildServiceProvider(); // build the service provider
            var mazeSolver = serviceProvider.GetService<IMazeSolver>(); // initialize mazeSolver as the MazeSolver class through the IMazeSolver interface in serviceProvider
            var mazeCreator = serviceProvider.GetService<IMazeCreator>();
            var maze = mazeCreator?.Create();
            mazeSolver?.Run(maze); 
        }
    }
}
