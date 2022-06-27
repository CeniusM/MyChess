using System.Diagnostics;
using System.Collections.Generic;


namespace MyChess.SpeedTester
{
    class MyStopwatch
    {
        /// <summary>
        /// returns the avg time of the reps and not the timesPerRep
        /// </summary>
        public static long Measure(Action action, int reps, int timesPerRep)
        {
            action.Invoke();


            Stack<long> times = new Stack<long>(reps);

            Stopwatch sw = new Stopwatch();
            for (int rep = 0; rep < reps; rep++)
            {
                sw.Restart();

                for (int i = 0; i < timesPerRep; i++)
                {
                    action.Invoke();
                }

                sw.Stop();
                times.Push(sw.ElapsedMilliseconds);
            }

            long avg = 0;
            for (int i = 0; i < reps; i++)
            {
                avg += times.Pop();
            }
            avg /= reps;

            return avg;
        }
        /// <summary>
        /// returns the avg time of the reps and not the timesPerRep
        /// </summary>
        public static long Measure<T> (Action<T> action, T paramater, int reps, int timesPerRep)
        {
            action(paramater);


            Stack<long> times = new Stack<long>(reps);

            Stopwatch sw = new Stopwatch();
            for (int rep = 0; rep < reps; rep++)
            {
                sw.Restart();

                for (int i = 0; i < timesPerRep; i++)
                {
                    action(paramater);
                }

                sw.Stop();
                times.Push(sw.ElapsedMilliseconds);
            }

            long avg = 0;
            for (int i = 0; i < reps; i++)
            {
                avg += times.Pop();
            }
            avg /= reps;

            return avg;
        }
    }
}