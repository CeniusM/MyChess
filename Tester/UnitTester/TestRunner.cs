using MyChess.UnitTester.Tests;
using CS_MyConsole;

namespace MyChess.UnitTester
{
    class TestRunner
    {
        public static void Run()
        {
            // heavy tests
            // PrintReport(Test.NumberOfPositionsAfter5plies("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 14, 1));
            // PrintReport(Test.NumberOfPositionsAfter5plies("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 191, 2));
            // PrintReport(Test.NumberOfPositionsAfter5plies("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 2812, 3));
            // PrintReport(Test.NumberOfPositionsAfter5plies("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 43238, 4));
            // PrintReport(Test.NumberOfPositionsAfter5plies("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 674624, 5));
            // PrintReport(Test.NumberOfPositionsAfter5plies("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 11030083, 6));
            // PrintReport(Test.NumberOfPositionsAfter5plies("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 178633661, 7));

            // PrintReport(Test.NumberOfPositionsAfter5plies("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 706045033, 6));
            // PrintReport(Test.NumberOfPositionsAfter5plies("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 4865609, 5));
            // PrintReport(Test.NumberOfPositionsAfter5plies("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 164075551, 5));
            // PrintReport(Test.NumberOfPositionsAfter5plies("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 89941194, 5));
            // PrintReport(Test.NumberOfPositionsAfter5plies("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 10 11", 193690690, 5));


            // PrintReport(Test.NumberOfPositionsAfter5plies("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 6, 1));
            // PrintReport(Test.NumberOfPositionsAfter5plies("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 264, 2));


            // PrintReport(Test.NumberOfPositionsAfter5plies("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8  ", 44, 1));
            // PrintReport(Test.NumberOfPositionsAfter5plies("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8  ", 1486, 2));


            // PrintReport(Test.NumberOfPositionsAfter5plies("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10 ", 46, 1));
            // PrintReport(Test.NumberOfPositionsAfter5plies("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10 ", 2079, 2));


            // PrintReport(Test.NumberOfPositionsAfter5plies("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 20, 1));
            // PrintReport(Test.NumberOfPositionsAfter5plies("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 400, 2));
            // PrintReport(Test.NumberOfPositionsAfter5plies("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 8902, 3));



            CS_MyConsole.MyConsole.WriteLine("");
            CS_MyConsole.MyConsole.WriteLine("Final Test");

            // PrintReport(Test.NumberOfPositionsAfter5plies("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 44, 1));
            // PrintReport(Test.NumberOfPositionsAfter5plies("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 1486, 2));
            // PrintReport(Test.NumberOfPositionsAfter5plies("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 62379, 3));
            // PrintReport(Test.NumberOfPositionsAfter5plies("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 2103487, 4));
            // PrintReport(Test.NumberOfPositionsAfter5plies("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 89941194, 5));

            // PrintReport(Test.NumberOfPositionsAfter5plies("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 6, 1));
            // PrintReport(Test.NumberOfPositionsAfter5plies("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 264, 2));
            // PrintReport(Test.NumberOfPositionsAfter5plies("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 9467, 3));
            // PrintReport(Test.NumberOfPositionsAfter5plies("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 422333, 4));
            // PrintReport(Test.NumberOfPositionsAfter5plies("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 15833292, 5));

            PrintReport(Test.NumberOfPositionsAfter5plies("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 14, 1));
            PrintReport(Test.NumberOfPositionsAfter5plies("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 191, 2));
            PrintReport(Test.NumberOfPositionsAfter5plies("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 2812, 3));
            PrintReport(Test.NumberOfPositionsAfter5plies("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 43238, 4));
            PrintReport(Test.NumberOfPositionsAfter5plies("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 674624, 5));
            PrintReport(Test.NumberOfPositionsAfter5plies("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 11030083, 6));




            // light tests
            //PrintReport(Test.TestGetBoardFromFEN());
            //PrintReport(Test.TestGetFENFromBoard());
            //PrintReport(Test.TestKing());
            //PrintReport(Test.TestKingsgeneratedPossibleMoves());
            
            CS_MyConsole.MyConsole.WriteLine("");
            CS_MyConsole.MyConsole.WriteLine("Done");
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