

namespace MyChess.ChessBoard.AIs
{
    public class OnlyMinMax : ChessAIBase
    {
        public OnlyMinMax(ChessGame chessGame) : base(chessGame)
        {
        }

        public override Move GetMove()
        {
            const int Depth = 3;
            int searchDepth = Depth - 1; // becous serch in the func as well
            Move move = new Move(0, 0, 0, 0);



            chessGame.possibleMoves.GenerateMoves();
            List<Move> movesRef = chessGame.GetPossibleMoves();
            int Count = movesRef.Count();
            Move[] moves = new Move[Count];
            movesRef.CopyTo(moves);
            if (Count == 0)
                return new(0, 0, 0, board.Square[0]);


            int bestMove = 0;
            int bestMoveEval = (board.playerTurn == 8) ? int.MinValue : int.MaxValue;
            int eval = 0;
            for (int i = 0; i < Count; i++)
            {
                board.MakeMove(moves[i]);

                eval = minimax(Depth, -1);//(board.playerTurn == 8) ? true : false

                if (board.playerTurn == 8) // max
                {
                    if (eval > bestMoveEval)
                    {
                        bestMoveEval = eval;
                        bestMove = i;
                    }
                }
                else    // min
                {
                    if (eval < bestMoveEval)
                    {
                        bestMoveEval = eval;
                        bestMove = i;
                    }
                }

                board.UnMakeMove();
            }



            return moves[bestMove];

            // if (chessGame.GetPossibleMoves().Count == 0)
            //     return new Move(0, 0, 0, board.Square[0]);
            // return chessGame.GetPossibleMoves()[new Random().Next(0, chessGame.GetPossibleMoves().Count)];
        }

        public int minimax(int depth, int moveCount)
        {
            if (depth == 0)
                return evaluator.EvaluateBoardLight(moveCount);


            chessGame.possibleMoves.GenerateMoves();
            List<Move> movesRef = chessGame.GetPossibleMoves();
            int Count = movesRef.Count();
            Move[] moves = new Move[Count];
            movesRef.CopyTo(moves);
            if (Count == 0)
                return evaluator.EvaluateBoardLight(moveCount);



            if (board.playerTurn == 8) // white
            {
                int maxEval = int.MinValue;
                int eval = 0;
                for (int i = 0; i < Count; i++)
                {
                    board.MakeMove(moves[i]);
                    eval = minimax(depth - 1, Count);
                    maxEval = Math.Max(maxEval, eval);
                    board.UnMakeMove();
                }
                return maxEval;
            }
            else // black
            {
                int minEval = int.MaxValue;
                int eval = 0;
                for (int i = 0; i < Count; i++)
                {
                    board.MakeMove(moves[i]);
                    eval = minimax(depth - 1, Count);
                    minEval = Math.Min(minEval, eval);
                    board.UnMakeMove();
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