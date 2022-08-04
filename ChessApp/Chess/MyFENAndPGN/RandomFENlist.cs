namespace MyChess.FEN
{
    readonly struct RandomFENList
    {
        // public static string this[int index]
        // {
        //     get
        //     {
        //         if (FENList.Length > index && index < -1)
        //         throw new IndexOutOfRangeException();
        //         else
        //             return FENList[index];
        //     }
        //     set { throw new Exception("This list can not be set");}
        // }
        public static string GetFEN(int index)
        {
                if (FENList.Length > index && index < -1)
                throw new IndexOutOfRangeException();
                else
                    return FENList[index];
        }
        public static int GetLenght() => FENList.Length;
        private static string[] FENList =
        {
            "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1",
            "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0",
            "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1",
            "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1",
            "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8",
            "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10",
            "rn1b1rk1/ppp3p1/3p1p2/1N1Pp1Pp/4P3/3Q1N2/PPP2P1P/R3K2R w KQ h6 0 15",
            "8/5k2/3p4/1p1Pp2p/pP2Pp1P/P4P1K/8/8 b - - 99 50",
            "4B3/3nk1p1/5r2/3b1p1Q/8/4p3/p1p1pPR1/4K1n1 w - - 0 1",
            "K1npQ1qp/r1q1B1N1/2r1kq2/RB1p2b1/8/p1BN3N/NNR1B3/1R3NB1 w - - 0 1",
            "3NQ1p1/P3q1KN/QQPQ1Q2/bkbN4/3q4/4r3/3bnN2/PBbqRq2 w - - 0 1",
            "7k/1b3pr1/n2R4/8/8/3r4/8/1K6 b - - 0 1",
            "k5r1/3K4/1P2N3/n6r/p4r2/1p2RpB1/n7/r7 w - - 0 1",
            "2p1k1nn/8/2q1q1K1/p5p1/8/5r2/2r2n2/1b3b2 w - - 0 1",
            "8/1Q1p1Q2/b1P3Nn/nqp1Q1Rb/b3Bk2/Q1P2nb1/2r1B1Qn/K1q3r1 b - - 0 1", 
            "4qq2/8/q5p1/8/n2n3P/2k2rrQ/5nb1/1K6 b - - 0 1",
            "7b/6Pn/B2K4/4P3/1r5R/4b3/3k4/B6B b - - 0 1", 
            "2k3B1/5nP1/1K3B2/2p5/4b3/3r4/8/8 w - - 0 1",
            "6k1/4r1q1/8/6B1/8/3K4/6B1/3R1N2 w - - 0 1",
            "3b1Rr1/8/1B4R1/3p4/2Ppk2N/2P5/7B/n1n3K1 b - - 0 1",
            "3R4/3R2p1/k7/7P/K7/4b1p1/1nb4P/3N4 w - - 0 1",
            "8/8/N6n/1p1R4/8/6r1/4k3/K7 b - - 0 1",
            "8/2k5/7b/3p2pr/B3Nn1P/4B3/2P4R/K3N1b1 w - - 0 1",
            "5knq/1rbp2N1/1p2B1n1/2K5/2q4p/1p2Rq2/3pN3/2N4q w - - 0 1",
            "8/5pkp/4b1p1/4n3/8/KB6/N7/8 w - - 1 1",
            "k7/2p1qp2/pp6/8/8/P7/1PPRB3/1K6 b - - 0 1"
        };
    }
}