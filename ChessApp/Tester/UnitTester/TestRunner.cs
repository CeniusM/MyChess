using MyChess.UnitTester.Tests;
using MyLib;

namespace MyChess.UnitTester
{
    public class TestRunner
    {
        public static void Run()
        {
            Test.PerftResults();
            // Test.CompareBoardsWithDepthAndMoves("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 5);
            //Test.CompareBoardsWithDepthAndMoves("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 5);
        }

        private static void PrintReport(TestReport report)
        {
            switch (report.succesStatus)
            {
                case TestReport.SuccesFlag.Succes:
                    MyConsole.WriteLine("Succes!");
                    break;
                case TestReport.SuccesFlag.Undetermined:
                    MyConsole.WriteLine("Undetermined-");
                    break;
                case TestReport.SuccesFlag.Failed:
                    MyConsole.WriteLine("Failed...");
                    break;
                default:
                    break;
            }
            MyConsole.WriteLine(report.strReport);
            MyConsole.WriteLine("");
        }
    }
}