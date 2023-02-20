

using System.Diagnostics;

namespace MyChess.ChessBoard.AIs
{
    public class AlphaBetaIterativeDeepening : ChessAIBase
    {
        public const int MAXDEPTH_Debuging = 100;
        public const int Depth = 5;
        public const int TimeToThinkMS = 10_000;
        private bool ALlowedToThink = true;
        private void StopClock() => ALlowedToThink = false;
        public AlphaBetaIterativeDeepening(ChessGame chessGame) : base(chessGame)
        {
        }
        //public override (Move move, int Eval) GetMove()
        public override Move GetMove()
        {
            ALlowedToThink = true;
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

            Task.Delay(new TimeSpan(0, 0, 0, 0, TimeToThinkMS)).ContinueWith(o => { StopClock(); });

            Stopwatch thinkTime = Stopwatch.StartNew();

            int DepthReached = 0;
            Move bestMove = new Move(0, 0, 0);

            while (ALlowedToThink)
            {
                DepthReached++;
                var results = Search(DepthReached, moves);
                if (results.Finished)
                {
                    bestMove = moves[results.BestMoveIndex];

                    //for (int i = 0; i < Count; i++)
                    //{
                    //    Console.WriteLine("val: " + results.Values[i] + " Move: " + moves[i].ToString());
                    //}
                    // Sort moves
                    Array.Sort(results.Values, moves);

                    //for (int i = 0; i < Count; i++)
                    //{
                    //    Console.WriteLine("val: " + results.Values[i] + " Move: " + moves[i].ToString());
                    //}
                    Console.WriteLine("Time Spent: " + thinkTime.ElapsedMilliseconds + "ms Detph: " + DepthReached);
                }
                else break;
                if (DepthReached == MAXDEPTH_Debuging)
                    break;
            }

            //Console.WriteLine("Time spent thinking: " + thinkTime.Elapsed.TotalSeconds + "s And reached a depth of: " + DepthReached);
            //Console.WriteLine("Time: " + thinkTime.Elapsed.TotalSeconds + "s to depth of: " + DepthReached);

            return bestMove;
            //return (moves[bestMove], bestMoveEval);
        }

        /// <summary>
        /// Return the scores of the difrent moves, and take in the moves 
        /// </summary>
        public (int[] Values, bool Finished, int BestMoveIndex, int MovesEvaluated) Search(int depth, Move[] moves)
        {
            int moveCount = moves.Length;
            int bestMove = 0;
            int bestMoveEval = (board.playerTurn == 8) ? int.MinValue : int.MaxValue;
            int eval = 0;
            int[] values = new int[moveCount];
            for (int i = 0; i < moveCount; i++)
            {
                if (!ALlowedToThink)
                    return (values, false, -1, i);
                board.MakeMove(moves[i]);
                eval = AlphaBeta(depth - 1, moveCount, (board.playerTurn == 8), int.MinValue, int.MaxValue);//(board.playerTurn == 8) ? true : false
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
                values[i] = eval;
            }

            return (values, true, bestMove, moveCount);
        }

        public int AlphaBeta(int depth, int LASTMOVECOUNT, bool maxPlayer, int alpha, int beta, bool onlyCaptures = false)
        {
            if (depth == 0)
                return evaluator.EvaluateBoardLight(LASTMOVECOUNT, true);

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
                    int eval = AlphaBeta(depth - 1, Count, false, alpha, beta, onlyCaptures);
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
                    int eval = AlphaBeta(depth - 1, Count, true, alpha, beta, onlyCaptures);
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