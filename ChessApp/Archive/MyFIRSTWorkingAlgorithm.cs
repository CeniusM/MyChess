

// namespace MyChess.ChessBoard.AIs
// {
//     public class OnlyMinMax1 : ChessAIBase
//     {
//         public int Depth = 4; // rather have id even so the last move is from the oppenent
//         public OnlyMinMax1(ChessGame chessGame, int depth = 4) : base(chessGame)
//         {
//             this.Depth = depth;
//         }

//         public override Move GetMove()
//         {
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
//                 eval = minimax(Depth - 1, Count, (board.playerTurn == 8));//(board.playerTurn == 8) ? true : false
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

//             chessGame.possibleMoves.GenerateMoves();
//             return moves[bestMove];
//         }

//         public int minimax(int depth, int LASTMOVECOUNT, bool maxPlayer)
//         {
//             if (depth == 0)
//                 return evaluator.EvaluateBoardLight(LASTMOVECOUNT, true);

//             chessGame.possibleMoves.GenerateMoves();
//             List<Move> movesRef = chessGame.GetPossibleMoves();
//             int Count = movesRef.Count();
//             Move[] moves = new Move[Count];
//             movesRef.CopyTo(moves);
//             if (Count == 0)
//                 return evaluator.EvaluateBoardLight(0, true);

//             if (maxPlayer)
//             {
//                 int maxEval = int.MinValue;
//                 foreach (Move move in moves)
//                 {
//                     board.MakeMove(move);
//                     int eval = minimax(depth - 1, Count, false);
//                     board.UnMakeMove();
//                     maxEval = Math.Max(maxEval, eval);
//                 }
//                 return maxEval;
//             }
//             else
//             {
//                 int minEval = int.MaxValue;
//                 foreach (Move move in moves)
//                 {
//                     board.MakeMove(move);
//                     int eval = minimax(depth - 1, Count, true);
//                     board.UnMakeMove();
//                     minEval = Math.Min(minEval, eval);
//                 }
//                 return minEval;
//             }
//         }

//         public override void SetChessGame(ChessGame chessGame)
//         {
//             this.chessGame = chessGame;
//             board = chessGame.board;
//             evaluator = new(chessGame);
//         }
//     }
// }




























/// ---------BaseClass-----------
// using MyChess.ChessBoard.Evaluators;

// namespace MyChess.ChessBoard.AIs
// {
//     public abstract class ChessAIBase : IChessAI
//     {
//         public ChessGame chessGame;
//         public Board board;
//         public Evaluator evaluator;
//         public ChessAIBase(ChessGame chessGame)
//         {
//             this.chessGame = chessGame;
//             if (chessGame is not null)
//             {
//                 board = chessGame.board;
//                 evaluator = new(chessGame);
//             }
//         }
//         public abstract Move GetMove();
//         public abstract void SetChessGame(ChessGame chessGame);
//     }
// }


























/// ---------Interface-----------
// namespace MyChess.ChessBoard.AIs
// {
//     public interface IChessAI
//     {
//         void SetChessGame(ChessGame chessGame);
//         Move GetMove();
//     }
// }