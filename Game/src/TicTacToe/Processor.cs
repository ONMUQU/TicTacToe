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
        private CellState[][] _data = new CellState[3][];
        /*----------------------------------------*/
        public Processor()
        {
            for(int i = 0; i < 3; ++i)
            {
                _data[i] = new CellState[3];
                _data[i] = Enumerable.Repeat(CellState.Empty, 3).ToArray();
            }
        }
        /*----------------------------------------*/
        public CellState[][] Board
        {
            get => _data;
        }
        /*----------------------------------------*/
        public bool TryToMove(bool side, int y, int x)
        {
            if(_data[y][x] != CellState.Empty)
            {
                return false;
            }
            else
            {
                CellState cell = side ? CellState.O : CellState.X;
                _data[y][x] = cell;
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
            for(int i = 0; i < 3; ++i)
            {
                _data[i] = Enumerable.Repeat(CellState.Empty, 3).ToArray();
            }
        }
        /*----------------------------------------*/
        private bool CheckLines(bool side)
        {
            CellState cell = side ? CellState.O : CellState.X;
            for(int i = 0; i < 3; ++i)
            {
                if(((_data[i][0] == cell) && (_data[i][1] == cell) && (_data[i][2] == cell))
                || ((_data[0][i] == cell) && (_data[1][i] == cell) && (_data[2][i] == cell)))
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
            return (((_data[0][0] == cell) && (_data[1][1] == cell) && (_data[2][2] == cell))
                 || ((_data[0][2] == cell) && (_data[1][1] == cell) && (_data[2][0] == cell)));
        }
        /*----------------------------------------*/
        private bool IsThereEmptyCells()
        {
            for(int i = 0; i < 3; ++i)
            {
                if(_data[i].Contains(CellState.Empty))
                    return true;
            }
            return false;
        }
    }
}
