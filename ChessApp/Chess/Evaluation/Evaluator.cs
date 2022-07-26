using MyChess.ChessBoard.Evaluators.Methods;

namespace MyChess.ChessBoard.Evaluators
{
    public class Evaluator
    {
        public ChessGame chessGame;
        public Board board;
        public MaterialCounterV1 matCounter;
        public Evaluator(ChessGame chessGame)
        {
            this.chessGame = chessGame;
            this.board = chessGame.board;
            matCounter = new(board);
        }

        public int EvaluateBoardLight(int moveCount, bool evaluateMatPlacement = false)
        {
            if (moveCount == 0)
            {
                // king in check
                if (chessGame.possibleMoves.IsSquareAttacked(chessGame.possibleMoves.GetKingsPos(board.playerTurn), board.playerTurn ^ Board.ColorMask))
                    return (board.playerTurn == 8) ? int.MinValue : int.MaxValue;
                else
                    return 0;
            }

            // later game give king bonus for being close to each other i think
            // later game give bonus for pawn movement to encurage pawn promotions

            // make it so the king prefer spots where it cant get checked?

            // idk if this a good way of gaginng if its late game
            // gives you a range between 0 - 1
            float lateGameBonusMultiplyer = (float)(32 - board.piecePoses.Count) / 32f;

            int evaluation = matCounter.GetMaterialAdvantage(chessGame, evaluateMatPlacement);

            return evaluation;
        }

        public int TestMiniMaxEval(ChessGame chessGame, int DEPTH)
        {
            var evaluator = chessGame.evaluator;

            return minimax(DEPTH, chessGame.GetPossibleMoves().Count(), (chessGame.board.playerTurn == 8));

            int minimax(int depth, int LASTMOVECOUNT, bool maxPlayer)
            {
                if (depth == 0)
                    return evaluator!.EvaluateBoardLight(LASTMOVECOUNT, true);

                chessGame.possibleMoves.GenerateMoves();
                List<Move> movesRef = chessGame.GetPossibleMoves();
                int Count = movesRef.Count();
                Move[] moves = new Move[Count];
                movesRef.CopyTo(moves);
                if (Count == 0)
                    return evaluator!.EvaluateBoardLight(0, true);

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
        }
    }
}