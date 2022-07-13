using MyChess.UnitTester.Tests;
using MyLib;

namespace MyChess.UnitTester
{
    public class TestRunner
    {
        public static void Run()
        {
            Test.PerftResults();
        }

        private static void PrintReport(TestReport report)
        {
            switch (report.succesStatus)
            {
                case TestReport.SuccesFlag.Succes:
                    FileWriter.WriteLine("Succes!");
                    break;
                case TestReport.SuccesFlag.Undetermined:
                    FileWriter.WriteLine("Undetermined-");
                    break;
                case TestReport.SuccesFlag.Failed:
                    FileWriter.WriteLine("Failed...");
                    break;
                default:
                    break;
            }
            FileWriter.WriteLine(report.strReport);
            FileWriter.WriteLine("");
        }
    }
}