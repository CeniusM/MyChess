

using System.Diagnostics;

namespace MyChess.ChessBoard.AIs
{
    public class AlphaBetaIterativeDeepening : ChessAIBase
    {
        private Dictionary<ulong, int> TransportationTable = new Dictionary<ulong, int>();

        public const int MAXDEPTH_Debuging = 10000;
        public const int Depth = 999999999;
        //public const int TimeToThinkMS = 1;
        public const int TimeToThinkMS = 600_000;
        private bool AllowedToThink = true;
        public void StopClock() => AllowedToThink = false;
        public AlphaBetaIterativeDeepening(ChessGame chessGame) : base(chessGame)
        {
        }
        //public override (Move move, int Eval) GetMove()
        public override Move GetMove()
        {
            AllowedToThink = true;
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
            int bestMoveIndex = 0;

            while (AllowedToThink)
            {
                DepthReached++;
                var results = Search(DepthReached, moves, bestMoveIndex);
                //Console.WriteLine("Time Spent: " + thinkTime.ElapsedMilliseconds + "ms. Detph: " + DepthReached +
                //        " Best. eval: " + results.Values[results.BestMoveIndex] + " MovesFinished: " + results.MovesFinished + "/" + Count +
                //        " Alpha Beta Snips: " + ABSnips + ". Nodes: " + Nodes);
                Console.WriteLine("Time: " + thinkTime.ElapsedMilliseconds + "ms. Detph: " + DepthReached +
                        ". eval: " + results.Values[results.BestMoveIndex] + ". MovesFinished: " + results.MovesFinished + "/" + Count +
                        ". HashCollisions: " + HashKeyCollisuions + ". Nodes: " + Nodes);
                Nodes = 0;
                ABSnips = 0;
                HashKeyCollisuions = 0;

                bestMoveIndex = results.BestMoveIndex;
                //Console.WriteLine();
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
                    //Console.WriteLine("Time Spent: " + thinkTime.ElapsedMilliseconds + "ms. Detph: " + DepthReached + " Best. eval: " + results.Values[results.BestMoveIndex] + ". Nodes " + Nodes);

                }
                else
                {
                    //Array.Copy(results.Values, be);
                    break;
                }
                if (DepthReached == MAXDEPTH_Debuging)
                    break;
                // If half of the time have allready gone by, we just stop it there
                //if (thinkTime.Elapsed.TotalMilliseconds > TimeToThinkMS / 2)
                //    break;
            }
            chessGame.possibleMoves.GenerateMoves();

            //Console.WriteLine("Time spent thinking: " + thinkTime.Elapsed.TotalSeconds + "s And reached a depth of: " + DepthReached);
            //Console.WriteLine("Time: " + thinkTime.Elapsed.TotalSeconds + "s to depth of: " + DepthReached);

            return bestMove;
            //return (moves[bestMove], bestMoveEval);
        }

        /// <summary>
        /// Return the scores of the difrent moves, and take in the moves 
        /// </summary>
        public (int[] Values, bool Finished, int BestMoveIndex, int MovesFinished) Search(int depth, Move[] moves, int previousBestMove)
        {
            int moveCount = moves.Length;
            int bestMove = 0;
            int bestMoveEval = (board.playerTurn == 8) ? int.MinValue : int.MaxValue;
            int eval = 0;
            int[] values = new int[moveCount];
            for (int i = 0; i < moveCount; i++)
            {
                if (!AllowedToThink)
                {
                    TransportationTable.Clear();
                    return (values, false, previousBestMove, i);
                }
                board.MakeMove(moves[i]);
                eval = AlphaBeta(depth - 1, moveCount, (board.playerTurn == 8), int.MinValue, int.MaxValue);//(board.playerTurn == 8) ? true : false
                board.UnMakeMove();
                if (!AllowedToThink)
                {
                    TransportationTable.Clear();
                    return (values, false, previousBestMove, i);
                }

                if (board.playerTurn == 8) // max
                {
                    if (eval > bestMoveEval && eval != bestMoveEval)
                    {
                        bestMoveEval = eval;
                        bestMove = i;
                    }
                }
                else    // min
                {
                    if (eval < bestMoveEval && eval != bestMoveEval)
                    {
                        bestMoveEval = eval;
                        bestMove = i;
                    }
                }
                values[i] = eval;
            }

            //Console.WriteLine(TransportationTable.Count);
            TransportationTable.Clear();
            //Console.WriteLine(TransportationTable.Count);
            return (values, true, bestMove, moveCount);
        }

        long Nodes = 0;
        long ABSnips = 0;
        long HashKeyCollisuions = 0;
        public int AlphaBeta(int depth, int LASTMOVECOUNT, bool maxPlayer, int alpha, int beta)
        {
            Nodes++;
            if (!AllowedToThink)
                return 0;
            if (depth == 0)
                return evaluator.EvaluateBoardLight(LASTMOVECOUNT, true);
            //if (depth == 0)
            //    return AlphaBetaOnlyCaptures(LASTMOVECOUNT, maxPlayer);

            if (TransportationTable.ContainsKey(board.HashKey))
                return TransportationTable[board.HashKey];

            chessGame.possibleMoves.GenerateMoves();
            List<Move> movesList = chessGame.GetPossibleMoves();
            int TotalCount = movesList.Count();
            if (TotalCount == 0)
                return evaluator.EvaluateBoardLight(0);
            Move[] moves = new Move[movesList.Count];
            movesList.CopyTo(moves);

            if (maxPlayer)
            {
                int maxEval = int.MinValue;
                foreach (Move move in moves)
                {
                    board.MakeMove(move);
                    int eval = AlphaBeta(depth - 1, TotalCount, false, alpha, beta);
                    board.UnMakeMove();
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                    {
                        ABSnips++;
                        break;
                    }
                }

                if (!TransportationTable.ContainsKey(board.HashKey))
                {
                    TransportationTable.Add(board.HashKey, maxEval);
                }
                else
                {
                    HashKeyCollisuions++;
                    board.PrintMoves();
                    AllowedToThink = false;
                    return 0;
                }

                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (Move move in moves)
                {
                    board.MakeMove(move);
                    int eval = AlphaBeta(depth - 1, TotalCount, true, alpha, beta);
                    board.UnMakeMove();
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                    {
                        ABSnips++;
                        break;
                    }
                }

                if (!TransportationTable.ContainsKey(board.HashKey))
                {
                    TransportationTable.Add(board.HashKey, minEval);
                }
                else
                {
                    HashKeyCollisuions++;
                    board.PrintMoves();
                    AllowedToThink = false;
                    return 0;
                }

                return minEval;
            }
        }

        public int AlphaBetaOnlyCaptures(int LASTMOVECOUNT, bool maxPlayer)
        {
            Nodes++;
            if (!AllowedToThink)
                return 0;

            chessGame.possibleMoves.GenerateMoves();
            List<Move> movesList = chessGame.GetPossibleMoves();
            int TotalCount = movesList.Count();
            if (TotalCount == 0)
                return evaluator.EvaluateBoardLight(0);
            Move[] moves = new Move[movesList.Count];
            movesList.CopyTo(moves);
            bool Ran = false;
            if (maxPlayer)
            {
                int maxEval = int.MinValue;
                foreach (Move move in moves)
                {
                    if (move.CapturedPiece == 0)
                        continue;
                    Ran = true;
                    //if (onlyCaptures && move.CapturedPiece == 0)
                    //    continue;
                    board.MakeMove(move);
                    int eval = AlphaBetaOnlyCaptures(TotalCount, false);
                    board.UnMakeMove();
                    maxEval = Math.Max(maxEval, eval);
                }
                if (!Ran)
                    return evaluator.EvaluateBoardLight(LASTMOVECOUNT, true);
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (Move move in moves)
                {
                    if (move.CapturedPiece == 0)
                        continue;
                    //if (onlyCaptures && move.CapturedPiece == 0)
                    //    continue;
                    board.MakeMove(move);
                    int eval = AlphaBetaOnlyCaptures(TotalCount, true);
                    board.UnMakeMove();
                    minEval = Math.Min(minEval, eval);
                }
                if (!Ran)
                    return evaluator.EvaluateBoardLight(LASTMOVECOUNT, true);
                return minEval;
            }
        }

        public override void SetChessGame()
        {
            throw new NotImplementedException();
        }
    }
}



//public int AlphaBeta(int depth, int LASTMOVECOUNT, bool maxPlayer, int alpha, int beta, bool onlyCaptures = false)
//{
//    //if (depth == 0)
//    //    return evaluator.EvaluateBoardLight(LASTMOVECOUNT, true);
//    if (depth == 0 && onlyCaptures)
//        return evaluator.EvaluateBoardLight(LASTMOVECOUNT, true);
//    if (depth == 0)
//        return AlphaBeta(100, LASTMOVECOUNT, maxPlayer, alpha, beta, true);

//    chessGame.possibleMoves.GenerateMoves();
//    List<Move> movesList = chessGame.GetPossibleMoves();
//    int TotalCount = movesList.Count();
//    if (TotalCount == 0)
//        return evaluator.EvaluateBoardLight(0);
//    Move[] moves;
//    if (onlyCaptures)
//    {
//        int count = movesList.Count(x => x.CapturedPiece == 0);
//        count = TotalCount - count;
//        if (count == 0)
//            return evaluator.EvaluateBoardLight(LASTMOVECOUNT);
//        moves = new Move[count];
//        int index = 0;
//        for (int i = 0; i < TotalCount; i++)
//        {
//            if (movesList[i].CapturedPiece != 0)
//            {
//                moves[index] = movesList[i];
//                index++;
//            }
//        }
//        //moves = new Move[1];
//        //moves[0] = movesList[0];
//    }
//    else
//    {
//        moves = new Move[movesList.Count];
//        movesList.CopyTo(moves);
//    }

//    if (maxPlayer)
//    {
//        int maxEval = int.MinValue;
//        foreach (Move move in moves)
//        {
//            //if (onlyCaptures && move.CapturedPiece == 0)
//            //    continue;
//            board.MakeMove(move);
//            int eval = AlphaBeta(depth - 1, TotalCount, false, alpha, beta, onlyCaptures);
//            board.UnMakeMove();
//            maxEval = Math.Max(maxEval, eval);
//            alpha = Math.Max(alpha, eval);
//            if (beta <= alpha)
//                break;
//        }
//        return maxEval;
//    }
//    else
//    {
//        int minEval = int.MaxValue;
//        foreach (Move move in moves)
//        {
//            //if (onlyCaptures && move.CapturedPiece == 0)
//            //    continue;
//            board.MakeMove(move);
//            int eval = AlphaBeta(depth - 1, TotalCount, true, alpha, beta, onlyCaptures);
//            board.UnMakeMove();
//            minEval = Math.Min(minEval, eval);
//            beta = Math.Min(beta, eval);
//            if (beta <= alpha)
//                break;
//        }
//        return minEval;
//    }
//}





/*
 * 

using System.Diagnostics;

namespace MyChess.ChessBoard.AIs
{
    public class AlphaBetaIterativeDeepening : ChessAIBase
    {
        public const int MAXDEPTH_Debuging = 100;
        public const int Depth = 999999999;
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
                    Console.WriteLine("Time Spent: " + thinkTime.ElapsedMilliseconds + "ms. Detph: " + DepthReached + " Best. eval: " + results.Values[results.BestMoveIndex]);
                }
                else break;
                if (DepthReached == MAXDEPTH_Debuging)
                    break;
                // If half of the time have allready gone by, we just stop it there
                if (thinkTime.Elapsed.TotalMilliseconds > TimeToThinkMS / 2)
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

        public int AlphaBeta(int depth, int LASTMOVECOUNT, bool maxPlayer, int alpha, int beta)
        {
            if (depth == 0)
                return AlphaBetaCapturesOnly(LASTMOVECOUNT, maxPlayer, alpha, beta);

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

        public int AlphaBetaCapturesOnly(int LASTMOVECOUNT, bool maxPlayer, int alpha, int beta)
        {
            chessGame.possibleMoves.GenerateMoves();
            List<Move> movesList = new List<Move>(chessGame.GetPossibleMoves());
            int originalMovesCount = movesList.Count();
            if (movesList.Count == 0)
                return evaluator.EvaluateBoardLight(0);
            for (int i = 0; i < movesList.Count; i++)
            {
                if (movesList[i].CapturedPiece == 0)
                {
                    movesList.RemoveAt(i);
                    i--;
                }
            }
            int Count = movesList.Count();
            if (movesList.Count == 0)
                return evaluator.EvaluateBoardLight(LASTMOVECOUNT, true);
            Move[] moves = new Move[Count];
            movesList.CopyTo(moves);

            if (maxPlayer)
            {
                int maxEval = int.MinValue;
                foreach (Move move in moves)
                {
                    board.MakeMove(move);
                    int eval = AlphaBetaCapturesOnly(originalMovesCount, false, alpha, beta);
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
                    int eval = AlphaBetaCapturesOnly(originalMovesCount, true, alpha, beta);
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
*/