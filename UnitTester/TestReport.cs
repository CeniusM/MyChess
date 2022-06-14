

namespace Foo
{
    class TestReport
    {
        public readonly struct SuccesFlag
        {
            public const int Failed = -1;
            public const int Undetermined = 0;
            public const int Succes = 1;
        }
        public string strReport;
        public int succesStatus;
        public TestReport(string ReportString, int SuccesF)
        {
            strReport = ReportString;
            succesStatus = SuccesF;
        }
    }
}