using MyChess.ChessBoard;

namespace ChessV1
{
    public unsafe class UnsafeBoard
    {
        // ---by Sebastian---
        // Bits 0-3 store castles
		// Bits 4-7 store file of ep square (starting at 1, so 0 = no ep square)
		// Bits 8-13 captured piece
		// Bits 14-... fifty mover counter
		Stack<uint> gameStateHistory;

        public PieceList piecePoses = new PieceList();
        public byte[] square = new byte[64];
        public int castle = 0b1111;
        public int enPassantPiece = 64;
        public int playerTurn = 8; // 8 = white, 16 = black;

        public UnsafeBoard()
        {
            InitFEN(MyChess.FEN.MyFEN.StartPostion);
        }
        public UnsafeBoard(string FEN)
        {
            InitFEN(FEN);
        }
        public void InitPiecePoses()
        {
            piecePoses = new PieceList(32);
            for (int i = 0; i < 64; i++) // put in kings first
            {
                if ((square[i] & Piece.PieceBits) == Piece.King)
                    piecePoses.AddPieceAtSquare(i);
            }
            for (int i = 0; i < 64; i++)
            {
                if (square[i] != 0 && (square[i] & Piece.PieceBits) != Piece.King)
                    piecePoses.AddPieceAtSquare(i);
            }
        }
        public void MakeMove(Move move)
        {
            
        }
        public void UnMakeMove()
        {

        }
        public void ChangePlayer() => playerTurn ^= Piece.ColorBits;
        public void InitFEN(string FEN)
        {
            try
            {
                int fPtr = 0; // fen ptr
                int bPtr = 0; // board ptr
                while (true)
                {
                    byte b = (byte)FEN[fPtr];
                    if (b != '/')
                    {
                        if (b == ' ')
                            break;
                        else if (b < 60) // num
                        {
                            bPtr += b - '0';
                        }
                        else // letter
                        {
                            square[bPtr] = PreInitlizedData.CharToPiece.GetPiece((char)b); // gives a num between 0 and 
                            bPtr++;
                        }
                    }

                    fPtr++;
                }
                fPtr++;

                if (FEN[fPtr] == 'b')
                    playerTurn = Piece.Black;

                fPtr += 2;

                for (int i = 0; i < 4; i++)
                {
                    if (FEN[fPtr] == 'K')
                        castle = (castle | 0b1000);
                    else if (FEN[fPtr] == 'Q')
                        castle = (castle | 0b0100);
                    else if (FEN[fPtr] == 'k')
                        castle = (castle | 0b0010);
                    else if (FEN[fPtr] == 'q')
                        castle = (castle | 0b0001);
                    else if (FEN[fPtr] == ' ')
                        break;
                    fPtr++;
                }
                fPtr++;

                if (FEN[fPtr] != '-')
                {
                    enPassantPiece += FEN[fPtr] - 'a';
                    fPtr++;
                    enPassantPiece += 64 - ((FEN[fPtr] - '0') * 8);
                }
                else
                    enPassantPiece = 64;

                InitPiecePoses();
            }
            catch
            {
                throw new Exception("Invalid FEN");
            }
        }
        public unsafe override int GetHashCode()
        {
            // note, remeber to test it to see how many collisions acure and speed

            // traditinal hashing
            long hash = 0;
            fixed (byte* ptr = square)
            {
                long a = (long)ptr;
                long b = (long)(ptr + 8);
                long c = (long)(ptr + 16);
                long d = (long)(ptr + 24);
                long e = (long)(ptr + 32);
                long f = (long)(ptr + 30);
                long g = (long)(ptr + 48);
                long h = (long)(ptr + 56);

                // hasing code
            }

            // zobist hashing
            // https://en.wikipedia.org/wiki/Zobrist_hashing

            throw new NotImplementedException("Hashing have not been implementet");
        }
    }
}