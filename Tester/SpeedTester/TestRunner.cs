using MyChess.SpeedTester.Tests;
using CS_MyConsole;

namespace MyChess.SpeedTester
{
    class TestRunner
    {
        public static void Run()
        {
            //PrintReport(Test.TestGeneratePossibleMoves());
            PrintReport(Test.OutOfBoundsCheck());
        }

        private static void PrintReport(TestReport report)
        {
            MyConsole.WriteLine(report.strReport);
            MyConsole.WriteLine(report.ElaspedMS + "ms");
            MyConsole.WriteLine("");
        }

        private static void PrintCompareReports(TestReport r1, TestReport r2)
        {

        }
    }
}