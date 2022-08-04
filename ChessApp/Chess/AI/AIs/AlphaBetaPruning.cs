

namespace MyChess.ChessBoard.AIs
{
    public class AlphaBetaPruning : ChessAIBase
    {
        public const int Depth = 4;
        public AlphaBetaPruning(ChessGame chessGame) : base(chessGame)
        {
        }
        public override Move GetMove()
        {
            chessGame.possibleMoves.GenerateMoves();
            List<Move> movesRef = chessGame.GetPossibleMoves();
            int Count = movesRef.Count();
            Move[] moves = new Move[Count];
            movesRef.CopyTo(moves);
            if (Count == 0)
                return new(0, 0, 0, board.Square[0]);


            // maby needs to be a ref? prb not tho
            int alpha = int.MinValue;
            int beta = int.MinValue;

            int bestMove = 0;
            int bestMoveEval = (board.playerTurn == 8) ? int.MinValue : int.MaxValue;
            int eval = 0;
            for (int i = 0; i < Count; i++)
            {
                board.MakeMove(moves[i]);
                eval = AlphaBeta(Depth - 1, Count, (board.playerTurn == 8), int.MinValue, int.MaxValue);//(board.playerTurn == 8) ? true : false
                board.UnMakeMove();

                if (board.playerTurn == 8) // max
                {
                    // alpha = Math.Max(alpha, eval);
                    // if (beta <= alpha)
                    //     break;
                    if (eval > bestMoveEval)
                    {
                        bestMoveEval = eval;
                        bestMove = i;
                    }
                }
                else    // min
                {
                    // beta = Math.Min(beta, eval);
                    // if (alpha <= beta)
                    //     break;
                    if (eval < bestMoveEval)
                    {
                        bestMoveEval = eval;
                        bestMove = i;
                    }
                }
            }

            chessGame.possibleMoves.GenerateMoves();
            return moves[bestMove];
        }

        public int AlphaBeta(int depth, int LASTMOVECOUNT, bool maxPlayer, int alpha, int beta)
        {
            if (depth == 0)
                return evaluator.EvaluateBoardLight(LASTMOVECOUNT, true);

            chessGame.possibleMoves.GenerateMoves();
            List<Move> movesRef = chessGame.GetPossibleMoves();
            int Count = movesRef.Count();
            Move[] moves = new Move[Count];
            movesRef.CopyTo(moves);
            if (Count == 0)
                return evaluator.EvaluateBoardLight(0);

            if (maxPlayer)
            {
                int maxEval = int.MinValue;
                foreach (Move move in moves)
                {
                    board.MakeMove(move);
                    int eval = AlphaBeta(depth - 1, Count, false, alpha, beta);
                    board.UnMakeMove();
                    maxEval = Math.Max(maxEval, eval);
                    // alpha = Math.Max(alpha, eval);
                    // if (beta >= alpha)
                    //     break;
                }
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (Move move in moves)
                {
                    board.MakeMove(move);
                    int eval = AlphaBeta(depth - 1, Count, true, alpha, beta);
                    board.UnMakeMove();
                    minEval = Math.Min(minEval, eval);
                    // beta = Math.Min(beta, eval);
                    // if (alpha >= beta)
                    //     break;
                }
                return minEval;
            }
        }

        public override void SetChessGame(ChessGame chessGame)
        {
            this.chessGame = chessGame;
            board = chessGame.board;
            evaluator = new(chessGame);
        }
    }
}