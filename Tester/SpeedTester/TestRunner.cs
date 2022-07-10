using MyChess.SpeedTester.Tests;
using MyLib;

namespace MyChess.SpeedTester
{
    class TestRunner
    {
        public static void Run()
        {
            PrintReport(Test.TestGeneratePossibleMoves());
            //PrintReport(Test.OutOfBoundsCheck());
        }

        private static void PrintReport(TestReport report)
        {
            FileWriter.WriteLine(report.strReport);
            FileWriter.WriteLine(report.ElaspedMS + "ms");
            FileWriter.WriteLine("");
        }

        private static void PrintCompareReports(TestReport r1, TestReport r2)
        {

        }
    }
}