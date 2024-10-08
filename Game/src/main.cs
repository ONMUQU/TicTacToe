using TicTacToe;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Game game = Game.Instance;
        while(true)
        {
            game.Update();
        }
    }
}

