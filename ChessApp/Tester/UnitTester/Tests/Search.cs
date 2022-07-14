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
        public static void Search(string FEN, int Depth)
            => Search(new ChessGame(FEN), Depth);

        public static void Search(ChessGame chessGame, int Depth)
        {
            int SearchDepth = Depth;

            long[] moveCount = new long[Depth + 1];

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
                    moveCount[depth]++;
                    SearchMove(depth - 1);
                    chessGame.board.UnMakeMove();
                }
            }

            for (int i = 1; i < Depth + 1; i++)
            {
                MyLib.MyConsole.WriteLine("" + moveCount[i]);
            }
        }
    }
}