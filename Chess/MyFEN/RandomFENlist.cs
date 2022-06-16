namespace MyChess.FEN
{
    struct RandomFENList
    {
        public string this[int index]
        {
            get
            {
                if (FENList.Length > index && index > -1)
                    return "";
                else
                    return FENList[index];
            }
            set { throw new Exception("This list can not be set");}
        }
        public static int GetLenght() => FENList.Length;
        private static string[] FENList =
        {
            "rn1b1rk1/ppp3p1/3p1p2/1N1Pp1Pp/4P3/3Q1N2/PPP2P1P/R3K2R w KQ h6 0 15",
            "8/5k2/3p4/1p1Pp2p/pP2Pp1P/P4P1K/8/8 b - - 99 50",
            "4B3/3nk1p1/5r2/3b1p1Q/8/4p3/p1p1pPR1/4K1n1 w - - 0 1"
        };
    }
}