using MyChess.PossibleMoves;
using MyChess.PossibleMoves.Pieces;

namespace MyChess.UnitTester.Tests
{
    partial class Test
    {
        public static TestReport TestKingsgeneratedPossibleMoves()
        {

            
            return new("Possible piece movement generator", TestReport.SuccesFlag.Undetermined);
        }
    }
}