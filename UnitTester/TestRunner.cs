using Foo.Tests;
using CS_MyConsole;

namespace Foo
{
    class TestRunner
    {
        public static void Run()
        {
            PrintReport(Test.TestGetFEN());
            PrintReport(Test.TestKing());
        }

        private static void PrintReport(TestReport report)
        {
            if (report.succesStatus == TestReport.SuccesFlag.Succes)
            {
                MyConsole.WriteLine("Succes!");
            }
            else if (report.succesStatus == TestReport.SuccesFlag.Undetermined)
            {
                MyConsole.WriteLine("Undetermined-");
            }
            else if (report.succesStatus == TestReport.SuccesFlag.Failed)
            {
                MyConsole.WriteLine("Failed...");
            }
        
            MyConsole.WriteLine(report.strReport);
            MyConsole.WriteLine("");
        }
    }
}