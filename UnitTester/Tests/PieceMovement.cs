using MyChess;

namespace Foo.Tests
{
    partial class Test
    {
        public static TestReport TestKing()
        {
            int succes = TestReport.SuccesFlag.Undetermined;

            ChessGame board = new("k7/8/8/8/8/8/2Kp4/3N4 w - - 0 1");

            return new("King Test", succes);
        }
    }
}