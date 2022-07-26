

namespace MyChess.ChessBoard.AIs
{
    public class OnlyMinMax1 : ChessAIBase
    {
        public int Depth = 4; // rather have id even so the last move is from the oppenent
        public OnlyMinMax1(ChessGame chessGame, int depth = 4) : base(chessGame)
        {
            this.Depth = depth;
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


            int bestMove = 0;
            int bestMoveEval = (board.playerTurn == 8) ? int.MinValue : int.MaxValue;
            int eval = 0;
            for (int i = 0; i < Count; i++)
            {
                board.MakeMove(moves[i]);
                eval = minimax(Depth - 1, Count, (board.playerTurn == 8));//(board.playerTurn == 8) ? true : false
                board.UnMakeMove();

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
            }

            chessGame.possibleMoves.GenerateMoves();
            return moves[bestMove];
        }

        public int minimax(int depth, int LASTMOVECOUNT, bool maxPlayer)
        {
            if (depth == 0)
                return evaluator.EvaluateBoardLight(LASTMOVECOUNT, true);

            chessGame.possibleMoves.GenerateMoves();
            List<Move> movesRef = chessGame.GetPossibleMoves();
            int Count = movesRef.Count();
            Move[] moves = new Move[Count];
            movesRef.CopyTo(moves);
            if (Count == 0)
                return evaluator.EvaluateBoardLight(0, true);

            if (maxPlayer)
            {
                int maxEval = int.MinValue;
                foreach (Move move in moves)
                {
                    board.MakeMove(move);
                    int eval = minimax(depth - 1, Count, false);
                    board.UnMakeMove();
                    maxEval = Math.Max(maxEval, eval);
                }
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (Move move in moves)
                {
                    board.MakeMove(move);
                    int eval = minimax(depth - 1, Count, true);
                    board.UnMakeMove();
                    minEval = Math.Min(minEval, eval);
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








// namespace MyChess.ChessBoard.AIs
// {
//     public class OnlyMinMax : ChessAIBase
//     {
//         public const int Depth = 6;
//         public OnlyMinMax(ChessGame chessGame) : base(chessGame)
//         {
//         }

//         public override Move GetMove()
//         {
//             Move move = new Move(0, 0, 0, 0);



//             chessGame.possibleMoves.GenerateMoves();
//             List<Move> movesRef = chessGame.GetPossibleMoves();
//             int Count = movesRef.Count();
//             Move[] moves = new Move[Count];
//             movesRef.CopyTo(moves);
//             if (Count == 0)
//                 return new(0, 0, 0, board.Square[0]);


//             int bestMove = 0;
//             int bestMoveEval = (board.playerTurn == 8) ? int.MinValue : int.MaxValue;
//             int eval = 0;
//             for (int i = 0; i < Count; i++)
//             {
//                 board.MakeMove(moves[i]);
//                 eval = minimax(Depth - 1, Count);//(board.playerTurn == 8) ? true : false
//                 board.UnMakeMove();

//                 if (board.playerTurn == 8) // max
//                 {
//                     if (eval > bestMoveEval)
//                     {
//                         bestMoveEval = eval;
//                         bestMove = i;
//                     }
//                 }
//                 else    // min
//                 {
//                     if (eval < bestMoveEval)
//                     {
//                         bestMoveEval = eval;
//                         bestMove = i;
//                     }
//                 }
//             }



//             return moves[bestMove];

//             // if (chessGame.GetPossibleMoves().Count == 0)
//             //     return new Move(0, 0, 0, board.Square[0]);
//             // return chessGame.GetPossibleMoves()[new Random().Next(0, chessGame.GetPossibleMoves().Count)];
//         }

//         public int minimax(int depth, int moveCount)
//         {
//             if (depth == 0)
//                 return evaluator.EvaluateBoardLight(moveCount);


//             chessGame.possibleMoves.GenerateMoves();
//             List<Move> movesRef = chessGame.GetPossibleMoves();
//             int Count = movesRef.Count();
//             Move[] moves = new Move[Count];
//             movesRef.CopyTo(moves);
//             if (Count == 0)
//                 return evaluator.EvaluateBoardLight(Count);



//             if (board.playerTurn == 8) // white
//             {
//                 int maxEval = int.MinValue;
//                 int eval = 0;
//                 for (int i = 0; i < Count; i++)
//                 {
//                     if (depth == 5)
//                     {

//                     }
//                     else if (depth == 3)
//                     {

//                     }
//                     else if (depth == 1)
//                     {

//                     }
//                     board.MakeMove(moves[i]);
//                     eval = minimax(depth - 1, Count);
//                     maxEval = Math.Max(maxEval, eval);
//                     board.UnMakeMove();
//                 }
//                 return maxEval;
//             }
//             else // black
//             {
//                 int minEval = int.MaxValue;
//                 int eval = 0;
//                 for (int i = 0; i < Count; i++)
//                 {
//                     if (depth == 4)
//                     {

//                     }
//                     else if (depth == 2)
//                     {

//                     }
//                     board.MakeMove(moves[i]);
//                     eval = minimax(depth - 1, Count);
//                     board.UnMakeMove();
//                     minEval = Math.Min(minEval, eval);
//                 }
//                 return minEval;
//             }
//         }

//         public override void SetChessGame()
//         {
//             throw new NotImplementedException();
//         }
//     }
// }