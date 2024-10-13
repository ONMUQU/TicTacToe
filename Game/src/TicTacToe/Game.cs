using System.Diagnostics.CodeAnalysis;

namespace TicTacToe
{
    public class Game
    {
        private static readonly Game _instance = new();
        private Processor _processor = new();
        /* board representation
        *       1     2     3
        *    +-----+-----+-----+ width 22
        *  1 |  0  |  0  |  0  |
        *    +-----+-----+-----+
        *  2 |  0  |  0  |  0  |
        *    +-----+-----+-----+
        *  3 |  0  |  0  |  0  |
        *    +-----+-----+-----+ 
        *    {      OUTPUT     }
        *    {      INPUT      } height 10
        */
        private const int _WIDTH = 22, _HEIGHT = 10;
        private bool _side_to_move = Side.X;
        /*----------------------------------------*/
        private Game()
        {
            if((Console.LargestWindowWidth < _WIDTH) || (Console.LargestWindowHeight < _HEIGHT))
            {
                throw new SystemException("TicTacToe.Game.Game(): Console is so small");
            }
        }
        static Game() { }
        /*----------------------------------------*/
        public static Game Instance { get => _instance; }
        public bool ShouldClose { get; private set; }
        /*----------------------------------------*/
        public void Update()
        {
            if(_processor.CurrentGameState() != GameState.NotOver)
            { // if game is over
                ClearAndDraw();
                PrintGameOver();
                if(!RestartOrClose())
                    return;
            }

            int[] move = [0, 0];
            for(int i = 0; i < move.Length;)
            {
                ClearAndDraw();

                Console.ForegroundColor = GetColorBySide();
                if(i == 1)
                {
                    Console.WriteLine("--ENTER XPOS OF MOVE--");
                }
                else
                {
                    Console.WriteLine("--ENTER YPOS OF MOVE--");
                }
                Console.ResetColor();

                char input = Console.ReadKey().KeyChar;
                ResizeWindowIfSmall();
                if(int.TryParse(input.ToString(), out move[i]) && ((move[i] <= 3) && (move[i] >= 1)))
                {
                    ++i; // incrementing the i, it works only here, DON'T MOVE IT
                    if(i == 2)
                    { // if input is taken
                        if(!_processor.TryToMove(_side_to_move, (move[0] - 1), (move[1] - 1)))
                        { // if cell isn't empty
                            i = 0;
                            InvalidInputMessage();
                        }
                        else
                        {
                            break;
                        }
                    }
                    continue; // if cell isn't empty, get move again
                }
                else
                { // if input is not number or number isn't in range
                    InvalidInputMessage();
                }
            }
            ClearAndDraw();
            _side_to_move = !_side_to_move;

            // ----------- Local methods ----------- //
            void InvalidInputMessage()
            {
                Console.Clear();
                Console.WriteLine("  Invalid input!  ");
                Thread.Sleep(1000);
                ResizeWindowIfSmall();
            }
        }
        /*----------------------------------------*/
        private void RestartGame()
        {
            ResizeWindowIfSmall();

            _processor.Reset();
            _side_to_move = Side.X;
            ShouldClose = false;
        }
        /*----------------------------------------*/
        static private void ResizeWindowIfSmall()
        {
            if((Console.WindowWidth < _WIDTH) || (Console.WindowHeight < _HEIGHT))
            {
                Console.SetWindowSize(_WIDTH, _HEIGHT);
            }
        }
        /*----------------------------------------*/
        private ConsoleColor GetColorBySide()
        {
            if(_side_to_move)
            {
                return ConsoleColor.Blue;
            }
            return ConsoleColor.Red;
        }
        /*----------------------------------------*/
        private void PrintGameOver()
        {
            GameState game_state = _processor.CurrentGameState();

            switch(game_state)
            {
                case (GameState.O_wins):
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("--GAME OVER O WINS--");
                    return;
                case (GameState.X_wins):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("--GAME OVER X WINS--");
                    return;
                case (GameState.Draw):
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("--GAME OVER DRAW--");
                    return;
                default:
                    return;
            }
        }
        /*----------------------------------------*/
        private bool RestartOrClose()
        {
            ResizeWindowIfSmall();
            Console.ResetColor();
            Console.Write("Restart(Y/n): ");
            char sym = Console.ReadKey().KeyChar;
            if((sym == 'Y') || (sym == 'y'))
            {
                RestartGame();
                return true;
            }
            ShouldClose = true;
            return false;
        }
        /*----------------------------------------*/
        private void ClearAndDraw()
        {
            ResizeWindowIfSmall();
            CellState[][] board = _processor.Board;

            Console.Clear();

            Console.WriteLine("      1     2     3   ");
            for(int i = 0; i < 3; ++i)
            {
                Console.WriteLine("   +-----+-----+-----+");
                Console.Write($" {i + 1} |  ");

                ColorCell(board[i][0]); // coloring foreground depending on the CellState
                Console.Write($"{CellToChar(board[i][0])}"); // getting character depending on the CellState
                Console.ResetColor();
                Console.Write("  |  ");

                ColorCell(board[i][1]);
                Console.Write($"{CellToChar(board[i][1])}");
                Console.ResetColor();
                Console.Write("  |  ");

                ColorCell(board[i][2]);
                Console.Write($"{CellToChar(board[i][2])}");
                Console.ResetColor();
                Console.WriteLine("  |"); // next line
            }
            Console.WriteLine("   +-----+-----+-----+");
            // ----------- Local methods ----------- //
            char CellToChar(CellState cell)
            {
                switch(cell)
                {
                    case (CellState.O):
                        return 'O';
                    case (CellState.X):
                        return 'X';
                }
                return ' '; // if CellState.Empty
            };
            //----------------------------------------
            void ColorCell(CellState cell)
            {
                switch(cell)
                {
                    case (CellState.O):
                        Console.ForegroundColor = ConsoleColor.Blue;
                        return;
                    case (CellState.X):
                        Console.ForegroundColor = ConsoleColor.Red;
                        return;
                }
            };
            //----------------------------------------
        }
    }
}


