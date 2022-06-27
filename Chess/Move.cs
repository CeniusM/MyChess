namespace MyChess
{
    public readonly struct Move
    {
        public readonly struct Flag
        {
            public const byte None = 0;
            public const byte EnPassantCapture = 1;
            public const byte Castling = 2;
            public const byte PromoteToQueen = 3;
            public const byte PromoteToKnight = 4;
            public const byte PromoteToRook = 5;
            public const byte PromoteToBishop = 6;
            public const byte PawnTwoForward = 7;
        }
        public readonly byte StartSquare;
        public readonly byte TargetSquare;
        public readonly byte MoveFlag;
        public readonly byte CapturedPiece;
        public Move(int s, int t, int f, int cp = 0)
        {
            StartSquare = (byte)s;
            TargetSquare = (byte)t;
            MoveFlag = (byte)f;
            CapturedPiece = (byte)cp;
        }
    }
}