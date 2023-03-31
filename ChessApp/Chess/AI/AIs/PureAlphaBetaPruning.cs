

namespace MyChess.ChessBoard.AIs
{
    public class PureAlphaBetaPruning : ChessAIBase
    {
        public const int Depth = 5;
        public PureAlphaBetaPruning(ChessGame chessGame) : base(chessGame)
        {
        }
        //public override (Move move, int Eval) GetMove()
        public override Move GetMove()
        {
            chessGame.possibleMoves.GenerateMoves();
            List<Move> movesRef = chessGame.GetPossibleMoves();
            int Count = movesRef.Count();

            if (true) // -- Move ordering --
            {
                List<Move> newMoves = new List<Move>(movesRef.Count);
                bool gettingAttacs = true;
                while (movesRef.Count > 0)
                {
                    for (int i = 0; i < movesRef.Count; i++)
                    {
                        if (movesRef[i].CapturedPiece != 0)
                        {
                            newMoves.Add(movesRef[i]);
                            movesRef.RemoveAt(i);
                        }
                        else if (!gettingAttacs)
                        {
                            newMoves.Add(movesRef[i]);
                            movesRef.RemoveAt(i);
                        }
                    }
                    gettingAttacs = false;
                }

                movesRef = newMoves;
            }

            Move[] moves = new Move[Count];
            movesRef.CopyTo(moves);
            if (Count == 0)
                //return (new(0, 0, 0, board.Square[0]), 0);
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
            //return (moves[bestMove], bestMoveEval);
        }

        public int AlphaBeta(int depth, int LASTMOVECOUNT, bool maxPlayer, int alpha, int beta)
        {
            if (depth == 0)
                return evaluator.EvaluateBoardLight(LASTMOVECOUNT);

            chessGame.possibleMoves.GenerateMoves();
            List<Move> movesRef = chessGame.GetPossibleMoves();
            int Count = movesRef.Count();
            if (Count == 0)
                return evaluator.EvaluateBoardLight(0);
            Move[] moves = new Move[Count];
            movesRef.CopyTo(moves);

            if (maxPlayer)
            {
                int maxEval = int.MinValue;
                foreach (Move move in moves)
                {
                    board.MakeMove(move);
                    int eval = AlphaBeta(depth - 1, Count, false, alpha, beta);
                    board.UnMakeMove();
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                        break;
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
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break;
                }
                return minEval;
            }
        }

        public override void SetChessGame()
        {
            throw new NotImplementedException();
        }
    }
}