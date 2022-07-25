using MyChess.ChessBoard;

namespace MyChess
{
    public readonly struct Move
    {
        public readonly struct Flag
        {
            // in order of what is most likly to happend
            public const byte None = 0;
            public const byte PawnTwoForward = 1;
            public const byte EnPassantCapture = 2;
            public const byte Castling = 3;
            public const byte PromoteToQueen = 4;
            public const byte PromoteToKnight = 5;
            public const byte PromoteToRook = 6;
            public const byte PromoteToBishop = 7;
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

        public int PromotionPiece()
        {
            switch (MoveFlag)
            {
                case 4:
                    return Piece.Queen;
                case 5:
                    return Piece.Knight;
                case 6:
                    return Piece.Rook;
                case 7:
                    return Piece.Bishop;
                default:
                    return 0;
            }
        }

        public static bool operator ==(Move m1, Move m2)
        {
            return (m1.StartSquare == m2.StartSquare &&
            m1.TargetSquare == m2.TargetSquare &&
            m1.MoveFlag == m2.MoveFlag &&
            m1.CapturedPiece == m2.CapturedPiece
            );
        }

        public static bool operator !=(Move m1, Move m2)
        {
            return (m1.StartSquare != m2.StartSquare ||
            m1.TargetSquare != m2.TargetSquare ||
            m1.MoveFlag != m2.MoveFlag ||
            m1.CapturedPiece != m2.CapturedPiece
            );
        }

        public override string ToString()
        {
            return "{S:" + StartSquare + ", T:" + TargetSquare + ", F:" + MoveFlag + ", C:" + CapturedPiece + "}";
        }
    }
}