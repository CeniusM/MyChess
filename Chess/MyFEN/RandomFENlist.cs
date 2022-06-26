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
            "rn1b1rk1/ppp3p1/3p1p2/1N1Pp1Pp/4P3/3Q1N2/PPP2P1P/R3K2R w KQ h6 0 15",
            "8/5k2/3p4/1p1Pp2p/pP2Pp1P/P4P1K/8/8 b - - 99 50",
            "4B3/3nk1p1/5r2/3b1p1Q/8/4p3/p1p1pPR1/4K1n1 w - - 0 1",
            "K1npQ1qp/r1q1B1N1/2r1kq2/RB1p2b1/8/p1BN3N/NNR1B3/1R3NB1 w - - 0 1",
            "3NQ1p1/P3q1KN/QQPQ1Q2/bkbN4/3q4/4r3/3bnN2/PBbqRq2 w - - 0 1",
            "K1kpR2n/QN6/3P4/5q1n/3b1PQr/B1pq2r1/5n2/r2Pq3 b - - 0 1",
            "7k/1b1q1pr1/nn1R3p/5p2/1q6/2r5/6pb/rK6 b - - 0 1",
            "k5r1/3K4/1P2N3/n6r/p4r2/1p2RpB1/n7/r7 w - - 0 1",
            "2p1k1nn/8/2q1q1K1/p5p1/8/5r2/2r2n2/1b3b2 w - - 0 1",
            "8/1Q1p1Q2/b1P3Nn/nqp1Q1Rb/b3Bk2/Q1P2nb1/2r1B1Qn/K1q3r1 b - - 0 1", 
            "4qq2/8/q5p1/8/n2n3P/2k2rrQ/5nb1/1K6 b - - 0 1",
            "1n1p3b/1bK2QPn/BR2n1Q1/bbQ1PQ2/3rn2R/1R2b3/2r2r2/B3k2B b - - 0 1", 
            "2k1p1B1/3B1nPQ/P1r1nB2/2pK4/4P3/1Rr1Pnrr/1R2P1r1/3nPq1n w - - 0 1",
            "3R2k1/4r3/8/6B1/n7/2RK3Q/5RB1/3R1N2 w - - 0 1",
            "1p6/1K6/7b/k2P2P1/3p2R1/8/5Q1Q/3P4 w - - 0 1",
            "2pb1Rr1/5q2/qB4R1/8/2PpkR1N/1QP5/1Q5B/n1n3K1 b - - 0 1",
            "2NR1B2/1bK1N3/n3BbB1/1kBNq3/1q1b3r/3qb2p/1Qn1NB2/1Nq1q3 b - - 0 1",
            "3R4/6p1/k7/3b1q1P/K5p1/3q2p1/1nb4P/3Nr3 w - - 0 1",
            "2pN1Q2/B2Rb3/pp4NK/3pBB2/5p2/1Q1qB1Qk/Qb3P2/4P1n1 b - - 0 1",
            "1rNNn3/1p2BrN1/1N2Nn2/r2B2p1/1b2QN2/RQp2rK1/1BB1p1p1/2Q1nkQ1 w - - 0 1",
            "8/8/Nq1Q3n/1p1R4/8/6r1/4k3/K1r5 b - - 0 1",
            "8/1Nk5/8/2Np4/B3N2P/4B3/2P4R/K3n1b1 w - - 0 1",
            "5knq/1rbp2N1/1p2B1n1/2K5/2q4p/1p2Rq2/3pN3/2N4q w - - 0 1",
            "1K6/1P4k1/8/4n3/5B2/5B2/N7/2Q5 w - - 0 1",
            "4rb2/K4R2/6nb/q2nr3/b7/1r4k1/4n1nn/1R6 b - - 0 1"
        };
    }
}