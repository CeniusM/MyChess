using System.Diagnostics;

namespace CS_MyConsole
{
    class MyStopwatch
    {
        private static Stopwatch stopWatch = new Stopwatch();
        private static Stopwatch stopWatch2 = new Stopwatch();
        public static string Measure(Action action)
        {
            stopWatch.Start();

            action.Invoke();

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = ts.TotalSeconds.ToString() + "s";
//ts.Seconds.ToString() + 
            stopWatch.Reset();

            return ("RunTime " + elapsedTime);
        }
        public static string Measure(Action action, int i)
        {
            stopWatch2.Start();

            action.Invoke();

            stopWatch2.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch2.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = ts.TotalSeconds.ToString() + "s";
//"minutes" + ts.Minutes + " and ms " +ts.Seconds.ToString() + 
            stopWatch2.Reset();

            return ("RunTime " + elapsedTime);
        }
    }
}

// timeSpend += MyStopwatch.Measure(() =>
//                 {
//                     // code
//                 });