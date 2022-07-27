

namespace ChessV1
{
    public unsafe class UnsafeBoard
    {
        // --- by Sebastian Lague ---
        // Bits 0-3 store castles
        // Bits 4-7 store file of ep square (starting at 1, so 0 = no ep square)
        // Bits 8-13 captured piece
        // Bits 14-... fifty mover counter
        public Stack<uint> gameStateHistory = new Stack<uint>(100);

        // ideer
        // public List<List<Move>> possibleMoves = new List<List<Move>>(100) { new List<Move>(100) };

        public PieceList piecePoses = new PieceList();
        public byte[] square = new byte[64];
        public int castle = 0b1111;
        public int enPassantPiece = 64;
        public int playerTurn = 8; // 8 = white, 16 = black;

        public UnsafeBoard()
        {
            InitFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
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
                if (Piece.PieceType(square[i]) == Piece.King)
                    piecePoses.AddPieceAtSquare(i);
            }
            for (int i = 0; i < 64; i++)
            {
                if (square[i] != 0 && Piece.PieceType(square[i]) != Piece.King)
                    piecePoses.AddPieceAtSquare(i);
            }
        }
        public void MakeMove(Move move)
        {

        }
        public void UnMakeMove()
        {

        }
        public void ChangePlayer() => playerTurn ^= Piece.colourMask;
        public void InitFEN(string FEN)
        {
            string[] sections = FEN.Split(' ');
            int boardPtr = 0;
            foreach (char c in sections[0])
            {
                if (c == '/')
                    continue;
                else if (c < 58) // a num
                    boardPtr += c - '0';
                else
                    square[boardPtr] = PreInitializeData.CharToPiece.GetPiece(c);
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