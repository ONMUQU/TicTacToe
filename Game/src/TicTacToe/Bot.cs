namespace TicTacToe
{
    public class Bot
    {
        // Move is int[2], [0] is y pos of move, [1] x pos of move
        public int[] Move{ get; private set; }
        /*----------------------------------------*/
        private enum Scores
        {
            Minimizing = -1,
            Tie = 0,
            Maximizing = 1,
        }
        /*----------------------------------------*/
        public Bot()
        {
            Move = [-1, -1];
        }
        /*----------------------------------------*/
        // sets Move to best move
        public int MiniMax(ref CellState[][] board, int depth, bool is_maximizing)
        {
            if((board.GetLength(0) != Processor.ROWS) && (board.GetLength(1) != Processor.COLS))
            {
                throw new ArgumentException("TicTacToe.Bot.MiniMax, board length is invalid");
            }
            if(IsTerminalState(board))
            {
                Processor processor = new(board);
                switch (processor.CurrentGameState())
                {
                    case GameState.Draw:
                        return (int)Scores.Tie;
                    case GameState.X_wins:
                        return (int)Scores.Maximizing;
                    case GameState.O_wins:
                        return (int)Scores.Minimizing;
                }
            }

            // TODO: fix bug with diagonal moves
            // capital letter is last move
            // when position like that
            // x__
            // Xo_
            // ___
            // and i play like that
            // x__
            // xo_
            // O__
            // bot brokes and plays this
            // xX_
            // xo_
            // o__
            // when should play this
            // x_X
            // xo_
            // o__
            if(is_maximizing)
            {
                int best_score = int.MinValue;
                for(int i = 0; i < Processor.ROWS; ++i)
                {
                    for(int j = 0; j < Processor.COLS; ++j)
                    {
                        if(board[i][j] == CellState.Empty)
                        {
                            board[i][j] = CellState.X;
                            int score = MiniMax(ref board, depth + 1, false);
                            board[i][j] = CellState.Empty;
                            if(score > best_score)
                            {
                                Move = [i, j];
                                best_score = score;
                            }
                        }
                    }
                }
                return best_score;
            }
            /* is minimizing */
            {
                int best_score = int.MaxValue;
                for(int i = 0; i < Processor.ROWS; ++i)
                {
                    for(int j = 0; j < Processor.COLS; ++j)
                    {
                        if(board[i][j] == CellState.Empty)
                        {
                            board[i][j] = CellState.O;
                            int score = MiniMax(ref board, depth + 1, true);
                            board[i][j] = CellState.Empty;
                            if(score < best_score)
                            {
                                Move = [i, j];
                                best_score = score;
                            }
                        }
                    }
                }
                return best_score;
            }

        }
        /*----------------------------------------*/
        public bool IsTerminalState(CellState[][] board)
        {
            Processor processor = new(board);
            if(processor.CurrentGameState() != GameState.NotOver)
            {
                return true;
            }
            return false;
        }
    } 
}
