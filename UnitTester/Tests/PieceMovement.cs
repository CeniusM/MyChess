using MyChess;

namespace MyChess.UnitTester.Tests
{
    partial class Test
    {
        private int[] move = {
            1
        };
        public static TestReport TestKing()
        {
            const int Failed = TestReport.SuccesFlag.Failed;
            int succes = TestReport.SuccesFlag.Succes;

            ChessGame board = new("k7/8/8/8/8/8/2Kp4/3N4 w - - 0 1");
            

            board.MakeMove(new(50, 59, Move.Flag.None)); // capture own knight
            if (board.board.halfMove == 1)
                succes = Failed;

            board.MakeMove(new(50, 51, Move.Flag.None)); // capture pawn
            if (board.board.halfMove != 1)
                succes = Failed;

            board = new("k7/8/8/8/8/8/2Kp4/3N4 w - - 0 1");

            board.MakeMove(new(50, 42, Move.Flag.None)); // move to nothing
            if (board.board.halfMove != 1)
                succes = Failed;


            return new("King moveing test", succes);
        }
    }
}