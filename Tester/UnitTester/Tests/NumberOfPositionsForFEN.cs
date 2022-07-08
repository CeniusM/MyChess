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
        public static TestReport NumberOfPositionsAfter5plies(string FEN, long ExpectedValue, int Depth)
            => NumberOfPositionsAfter5plies(new ChessGame(FEN), ExpectedValue, Depth);

        public static TestReport NumberOfPositionsAfter5plies(ChessGame chessGame, long ExpectedValue, int Depth)
        {
            // debuging
            // int MoveNum = 0;




            int SearchDepth = Depth;

            // int moveCount = 0;




            // int[] moveCount = new int[Depth + 1];

            //List<Move> moves = chessGame.GetPossibleMoves();



            long moveCount = 0;

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
                    // moveCount[depth]++;
                    if (depth == 1)
                        moveCount++;
                    SearchMove(depth - 1);
                    chessGame.board.UnMakeMove();
                }
            }
            return new(moveCount/*[1]*/ + " amount of combinations after " + SearchDepth + " moves" + ", Expected: " + ExpectedValue + "\n"
            + MyFEN.GetFENFromBoard(chessGame.board),
            moveCount/*[1]*/ == ExpectedValue ? TestReport.SuccesFlag.Succes : TestReport.SuccesFlag.Failed); // succes flag
        }
    }
}


// if (depth == 1)
// {
//     MoveNum++;
//     Move theMove = chessGame.board.moves.Peek();
//     CS_MyConsole.MyConsole.WriteLine(MoveNum + ": " + moves.Length + 
//     ", S: " + Board.IntToLetterNum(theMove.StartSquare)
//     + ", T: " + Board.IntToLetterNum(theMove.TargetSquare)
//     + ", F: " + theMove.MoveFlag
//     + ", C: " + theMove.CapturedPiece);
//     CS_MyConsole.MyConsole.WriteLine("");
//     for (int i = 0; i < moves.Length; i++)
//     {
//         CS_MyConsole.MyConsole.Write("(" + Board.IntToLetterNum(moves[i].StartSquare) + 
//         " " + Board.IntToLetterNum(moves[i].TargetSquare) + ")");
//     }
//     CS_MyConsole.MyConsole.WriteLine("");
// }