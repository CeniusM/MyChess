// This code is by Sebastian Lague

/* 
To preserve memory during search, moves are stored as 16 bit numbers.
The format is as follows:
bit 0-5: from square (0 to 63)
bit 6-11: to square (0 to 63)
bit 12-15: flag
*/

using Chess.ChessBoard;

namespace Chess.Moves
{

    public readonly struct GameMove
    {
        public readonly struct Flag
        {
            public const int None = 0;
            public const int EnPassantCapture = 1;
            public const int Castling = 2;
            public const int PromoteToQueen = 3;
            public const int PromoteToKnight = 4;
            public const int PromoteToRook = 5;
            public const int PromoteToBishop = 6;
            public const int PawnTwoForward = 7;
        }

        readonly ushort moveValue;
        readonly int capturedPiece = 0;

        const ushort startSquareMask = 0b0000000000111111;
        const ushort targetSquareMask = 0b0000111111000000;
        const ushort flagMask = 0b1111000000000000;

        public GameMove(ushort moveValue)
        {
            this.moveValue = moveValue;
        }

        public GameMove(int startSquare, int targetSquare)
        {
            moveValue = (ushort)(startSquare | targetSquare << 6);
        }

        public GameMove(int startSquare, int targetSquare, int flag)
        {
            moveValue = (ushort)(startSquare | targetSquare << 6 | flag << 12);
        }
        public GameMove(int startSquare, int targetSquare, int flag, int capturedPiece)
        {
            moveValue = (ushort)(startSquare | targetSquare << 6 | flag << 12);
            this.capturedPiece = capturedPiece;
        }

        public int StartSquare
        {
            get
            {
                return moveValue & startSquareMask;
            }
        }

        public int TargetSquare
        {
            get
            {
                return (moveValue & targetSquareMask) >> 6;
            }
        }

        public int CapturedPiece
        {
            get
            {
                return capturedPiece;
            }
        }

        public bool IsPromotion
        {
            get
            {
                int flag = MoveFlag;
                return flag == Flag.PromoteToQueen || flag == Flag.PromoteToRook || flag == Flag.PromoteToKnight || flag == Flag.PromoteToBishop;
            }
        }

        public int MoveFlag
        {
            get
            {
                return moveValue >> 12;
            }
        }

        public int PromotionPieceType
        {
            get
            {
                switch (MoveFlag)
                {
                    case Flag.PromoteToRook:
                        return Piece.Rook;
                    case Flag.PromoteToKnight:
                        return Piece.Knight;
                    case Flag.PromoteToBishop:
                        return Piece.Bishop;
                    case Flag.PromoteToQueen:
                        return Piece.Queen;
                    default:
                        return Piece.None;
                }
            }
        }

        public static Move InvalidMove
        {
            get
            {
                return new Move(0);
            }
        }
    }
}