namespace MazeApp
{
    public interface IMazeCreator
    {
        string[,] Create();
        void PrintMaze(string[,] maze);
    }
}