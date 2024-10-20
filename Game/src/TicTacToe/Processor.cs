namespace TicTacToe
{
    public struct Side
    {
        public const bool O = true;
        public const bool X = false;
    }

    public enum CellState
    {
        Empty,
        O,
        X
    }

    public enum GameState
    {
        NotOver,
        O_wins,
        X_wins,
        Draw
    }
    public class Processor
    {
        /*----------------------------------------*/
        public CellState[][] Board { get; private set; }
        // these bros are same
        public static readonly int BOARD_DIMENSION_LENGTH = 3;
        public static readonly int ROWS = BOARD_DIMENSION_LENGTH;
        public static readonly int COLS = BOARD_DIMENSION_LENGTH;
        /*----------------------------------------*/
        public Processor()
        {
            Board = new CellState[ROWS][];
            for(int i = 0; i < Board.GetLength(0); ++i)
            {
                Board[i] = new CellState[COLS];
                Board[i] = Enumerable.Repeat(CellState.Empty, ROWS).ToArray();
            }
        }
        /*----------------------------------------*/
        public Processor(Processor processor)
        {
            this.Board = new CellState[ROWS][];
            for(int i = 0; i < Board.GetLength(0); ++i)
            {
                this.Board[i] = new CellState[COLS];
                Array.Copy(processor.Board[i], this.Board[i], COLS);
            }
        }
        /*----------------------------------------*/
        public bool TryToMove(bool side, int y, int x)
        {
            if(Board[y][x] != CellState.Empty)
            {
                return false;
            }
            else
            {
                CellState cell = side ? CellState.O : CellState.X;
                Board[y][x] = cell;
            }
            return true;
        }
        /*----------------------------------------*/
        public GameState CurrentGameState()
        {
            if(this.CheckLines(Side.O) || this.CheckDiagonal(Side.O))
            {
                return GameState.O_wins;
            }
            if(this.CheckLines(Side.X) || this.CheckDiagonal(Side.X))
            {
                return GameState.X_wins;
            }
            if(this.IsThereEmptyCells())
            {
                return GameState.NotOver;
            }
            return GameState.Draw;
        }
        /*----------------------------------------*/
        public void Reset()
        {
            for(int i = 0; i < COLS; ++i)
            {
                Board[i] = Enumerable.Repeat(CellState.Empty, ROWS).ToArray();
            }
        }
        /*----------------------------------------*/
        private bool CheckLines(bool side)
        {
            CellState cell = side ? CellState.O : CellState.X;
            for(int i = 0; i < BOARD_DIMENSION_LENGTH; ++i)
            {
                if(((Board[i][0] == cell) && (Board[i][1] == cell) && (Board[i][2] == cell))
                || ((Board[0][i] == cell) && (Board[1][i] == cell) && (Board[2][i] == cell)))
                {
                    return true;
                }
            }
            return false;
        }
        /*----------------------------------------*/
        private bool CheckDiagonal(bool side)
        {
            CellState cell = side ? CellState.O : CellState.X;
            return (((Board[0][0] == cell) && (Board[1][1] == cell) && (Board[2][2] == cell))
                 || ((Board[0][2] == cell) && (Board[1][1] == cell) && (Board[2][0] == cell)));
        }
        /*----------------------------------------*/
        private bool IsThereEmptyCells()
        {
            for(int i = 0; i < ROWS; ++i)
            {
                if(Board[i].Contains(CellState.Empty))
                    return true;
            }
            return false;
        }
    }
}
