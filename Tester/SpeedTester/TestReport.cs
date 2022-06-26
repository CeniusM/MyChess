

namespace MyChess.SpeedTester
{
    class TestReport
    {
        public string strReport;
        public long ElaspedMS
        { get; }
        public long ElaspedSeconds
        { get { return ElaspedMS / 1000; } }
        public float ElaspedSecondsF
        { get { return (float)ElaspedMS / 1000; } }

        public TestReport(string strReport, long ElaspedMS)
        {
            this.strReport = strReport;
            this.ElaspedMS = ElaspedMS;
        }
    }
}