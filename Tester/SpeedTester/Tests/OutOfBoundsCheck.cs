using MyChess.FEN;
using MyChess.ChessBoard;

namespace MyChess.SpeedTester.Tests
{
    partial class Test
    {
        // public static TestReport OutOfBoundsCheck()
        // {
        //     int reps = 10;
        //     int repsPerRep = 100000000; // 100 mil
        //     ulong avg = 0;

        //     //Action<int> action = () => Board.IsPieceOutOfBounce;

        //     //SpeedTester.MyStopwatch.Measure<int>(action, 1, reps, repsPerRep);

        //     //SpeedTester.MyStopwatch.Measure(() => Board.IsPieceOutOfBounce, reps, repsPerRep);

        //     Action a = Test1;

        //     avg = (ulong)SpeedTester.MyStopwatch.Measure(a, reps, repsPerRep);

        //     return new("Time of OutOfBoundsCheck",
        //                 (long)avg);
        // }
        // private static void Test1()
        // {
        //     bool i = (1 & 0xFFC0) != 0;
        // }
        // private static void Test2()
        // {
        //     // faster i think but we dont have dynamic values
        //     // bool i = !(1 < 0 || 1 > 63)
        //     bool i = 1 > -1 && 1 < 64;
        // }
    }
}