using MyChess.UnitTester.Tests;
using CS_MyConsole;

namespace MyChess.UnitTester
{
    class TestRunner
    {
        public static void Run()
        {
            // heavy tests
            

            // light tests
            PrintReport(Test.TestGetBoardFromFEN());
            PrintReport(Test.TestGetFENFromBoard());
            PrintReport(Test.TestKing());
            PrintReport(Test.TestKingsgeneratedPossibleMoves());
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