using MyChess.UnitTester.Tests;
using CS_MyConsole;

namespace MyChess.UnitTester
{
    class TestRunner
    {
        public static void Run()
        {
            // heavy tests
            PrintReport(Test.NumberOfPositionsAfter5plies("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1"));
            

            // light tests
            //PrintReport(Test.TestGetBoardFromFEN());
            //PrintReport(Test.TestGetFENFromBoard());
            //PrintReport(Test.TestKing());
            //PrintReport(Test.TestKingsgeneratedPossibleMoves());

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