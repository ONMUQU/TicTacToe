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
        public Processor Processor { get => _processor; }
        /*----------------------------------------*/
        public void Update()
        {
            if(_processor.CurrentGameState() != Processor.GameState.NotOver)
            {
                Processor.GameState game_state = _processor.CurrentGameState();
                this.ClearAndDraw();
                switch(game_state)
                {
                    case (Processor.GameState.O_wins):
                        this.ClearAndDraw();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("--GAME OVER O WINS--");
                        break;
                    case (Processor.GameState.X_wins):
                        this.ClearAndDraw();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("--GAME OVER X WINS--");
                        break;
                    default: // if draw
                        this.ClearAndDraw();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("--GAME OVER DRAW--");
                        break;
                }
                Console.ResetColor();
                Console.Write("Restart(Y/n): ");
                char sym = Console.ReadKey().KeyChar;
                if((sym == 'Y') || (sym == 'y'))
                {
                    _processor.Reset();
                    _side_to_move = Side.X;
                    return;
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            //----------------------------------------------------
            ConsoleColor color;
            if(_side_to_move)
            {
                color = ConsoleColor.Blue;
            }
            else
            {
                color = ConsoleColor.Red;
            }
            char number;
            int[] move = new int[2];
            {
                int i = 0;
                while(true)
                {
                    this.ClearAndDraw();

                    Console.ForegroundColor = color;
                    if(i == 1)
                    {
                        Console.WriteLine("--ENTER XPOS OF MOVE--");
                    }
                    else
                    {
                        Console.WriteLine("--ENTER YPOS OF MOVE--");
                    }
                    Console.ResetColor();

                    number = Console.ReadKey().KeyChar;
                    this.ResizeIfSmall();

                    if(char.IsNumber(number))
                    {
                        move[i] = int.Parse(number.ToString());
                        if((move[i] <= 3) && (move[i] >= 1))
                        {
                            ++i;
                        }
                        else
                        { // if move is out of bounds
                            i = 0;
                            InvalidInputMessage();
                        }
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
                    { // if input is not number
                        InvalidInputMessage();
                    }

                }
            }
            this.ClearAndDraw();
            _side_to_move = !_side_to_move;

            // ----------- Local methods ----------- //
            void InvalidInputMessage()
            {
                Console.Clear();
                Console.WriteLine("  Invalid input!  ");
                Thread.Sleep(1000);
                this.ResizeIfSmall();
            }
        }
        /*----------------------------------------*/
        private void ResizeIfSmall()
        {
            if((Console.WindowWidth < _WIDTH) || (Console.WindowHeight < _HEIGHT))
            {
                Console.SetWindowSize(_WIDTH, _HEIGHT);
            }
        }
        /*----------------------------------------*/
        private void ClearAndDraw()
        {


            this.ResizeIfSmall();
            Processor.CellState[][] board = _processor.Board;

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
            char CellToChar(Processor.CellState cell)
            {
                switch(cell)
                {
                    case (Processor.CellState.O):
                        return 'O';
                    case (Processor.CellState.X):
                        return 'X';
                }
                return ' '; // if CellState.Empty
            };
            //----------------------------------------
            void ColorCell(Processor.CellState cell)
            {
                switch(cell)
                {
                    case (Processor.CellState.O):
                        Console.ForegroundColor = ConsoleColor.Blue;
                        return;
                    case (Processor.CellState.X):
                        Console.ForegroundColor = ConsoleColor.Red;
                        return;
                }
            };
            //----------------------------------------
        }
    }
}


