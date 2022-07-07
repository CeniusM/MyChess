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
        public static TestReport NumberOfPositionsAfter5plies(string FEN, int ExpectedValue, int Depth)
            => NumberOfPositionsAfter5plies(new ChessGame(FEN), ExpectedValue, Depth);

        public static TestReport NumberOfPositionsAfter5plies(ChessGame chessGame, int ExpectedValue, int Depth)
        {
            int SearchDepth = Depth;

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
            return new(moveCount + " amount of combinations after " + SearchDepth + " moves" + ", Expected: " + ExpectedValue + "\n"
            + MyFEN.GetFENFromBoard(chessGame.board),
            moveCount == ExpectedValue ? TestReport.SuccesFlag.Succes : TestReport.SuccesFlag.Failed); // succes flag
        }
    }
}