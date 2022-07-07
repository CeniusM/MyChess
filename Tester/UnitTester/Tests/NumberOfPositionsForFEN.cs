using MyChess;
using MyChess.ChessBoard;
using MyChess.FEN;

// https://en.wikipedia.org/wiki/Shannon_number
// https://www.chessprogramming.org/Perft_Results

// can both be used for speed and testing of the system

namespace MyChess.UnitTester.Tests
{
    partial class Test
    {
        public static TestReport NumberOfPositionsAfter5plies(string FEN)
            => NumberOfPositionsAfter5plies(new ChessGame(FEN));
        public static TestReport NumberOfPositionsAfter5plies(ChessGame chessGame)
        {
            int SearchDepth = 5;

            int moveCount = 0;

            //List<Move> moves = chessGame.GetPossibleMoves();

            SearchMove(SearchDepth);

            void SearchMove(int depth)
            {
                if (depth == 0)
                    return;

                List<Move> moves = chessGame.GetPossibleMoves();

                for (int i = 0; i < moves.Count(); i++)
                {
                    chessGame.MakeMove(moves[i]);
                    moveCount++;
                    SearchMove(depth - 1);
                    chessGame.UnMakeMove();
                }
            }


            int ExpectedValue = 15833292;
            return new(moveCount + " amount of combinations after " + SearchDepth + " moves" + ", Expected: " + ExpectedValue,
            moveCount == ExpectedValue ? TestReport.SuccesFlag.Succes : TestReport.SuccesFlag.Failed); // succes flag
        }

        public static TestReport NumberOfPositionsAfter5pliesV2(ChessGame chessGame)
        {
            int SearchDepth = 5;

            int moveCount = 0;

            //List<Move> moves = chessGame.GetPossibleMoves();

            SearchMove(SearchDepth);

            void SearchMove(int depth)
            {
                if (depth == 0)
                    return;

                chessGame.possibleMoves.GenerateMoves();
                List<Move> movesRef = chessGame.GetPossibleMoves();
                int Count = movesRef.Count();
                Move[] moves = new Move[Count];
                movesRef.CopyTo(moves);

                for (int i = 0; i < Count; i++)
                {
                    chessGame.board.MakeMove(moves[i]);
                    moveCount++;
                    SearchMove(depth - 1);
                    chessGame.board.UnMakeMove();
                }
            }


            int ExpectedValue = 15833292;
            return new(moveCount + " amount of combinations after " + SearchDepth + " moves" + ", Expected: " + ExpectedValue,
            moveCount == ExpectedValue ? TestReport.SuccesFlag.Succes : TestReport.SuccesFlag.Failed); // succes flag
        }
    }
}