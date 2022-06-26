using MyChess;
using MyChess.ChessBoard;
using MyChess.FEN;

// https://en.wikipedia.org/wiki/Shannon_number

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

            int moves = 0;
            void SearchMove(int depth)
            {

                SearchMove(depth - 1);

                moves++;
            }

            SearchMove(SearchDepth);

            return new(moves + " amount of combinations after " + SearchDepth + " moves", -1);
        }
    }
}