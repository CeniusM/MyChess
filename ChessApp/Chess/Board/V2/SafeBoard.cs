// does checks and stuff like that so it dosent crash, tho shoudlent be used for speedy algorithms
// only used for player interactions

namespace ChessV2
{
    public unsafe class SafeBoard
    {
        private UnsafeBoard _board;
        private PossibleMovesGenerator _movesGenerator;
        private Move[] _moves;

        public SafeBoard()
        {
            _board = new UnsafeBoard();
            _movesGenerator = new PossibleMovesGenerator(_board);
            _moves = new Move[0];
            _movesGenerator.GenerateMoves();
            _moves = _movesGenerator.GetMoves();
        }

        public SafeBoard(string FEN)
        {
            _board = new UnsafeBoard(FEN);
            _movesGenerator = new PossibleMovesGenerator(_board);
            _moves = new Move[0];
            _movesGenerator.GenerateMoves();
            _moves = _movesGenerator.GetMoves();
        }

        public bool MakeMove(Move move)
        {
            if (_moves.Contains(move))
            {
                _board.MakeMove(move);
                _movesGenerator.GenerateMoves();
                _moves = _movesGenerator.GetMoves();
                return true;
            }
            else
                return false;
        }

        public bool UnMakeMove()
        {
            if (_board.gameStateHistoryCount != 0)
            {
                _board.UnMakeMove();
                _movesGenerator.GenerateMoves();
                _moves = _movesGenerator.GetMoves();
                return true;
            }
            else return false;
        }

        public int this[int key]
        {
            get
            {
                if (key > 63 || key < 0)
                    return int.MinValue;
                return _board.boardPtr[key];
            }
            set { throw new NotSupportedException("SafeBoard not able to set board varibles"); }
        }

        public int PlayerTurn { get => _board.playerTurn; }

        public Move[] GetMoves() => _movesGenerator.GetMoves();

        public bool IsKingInCheck() => _movesGenerator.IsKingInCheck;

        public UnsafeBoard GetUnsafeBoard() => _board;
        public PossibleMovesGenerator GetPossibleMovesGenerator() => _movesGenerator;









        // vedy bad code, need update at some point
        public string GetFen()
        {
            char GetCharFromPiece(int i)
            {
                switch (i)
                {
                    case Piece.WPawn:
                        return 'P';
                    case Piece.WKnight:
                        return 'N';
                    case Piece.WBishop:
                        return 'B';
                    case Piece.WRook:
                        return 'R';
                    case Piece.WQueen:
                        return 'Q';
                    case Piece.WKing:
                        return 'K';
                    case Piece.BPawn:
                        return 'p';
                    case Piece.BKnight:
                        return 'n';
                    case Piece.BBishop:
                        return 'b';
                    case Piece.BRook:
                        return 'r';
                    case Piece.BQueen:
                        return 'q';
                    case Piece.BKing:
                        return 'k';
                    default:
                        return char.MaxValue;
                }

            }

            char[] CString = new char[100];
            int CStringPointer = 0;
            void SetChar(char c)
            {
                CString[CStringPointer] = c;
                CStringPointer++;
            }

            int gap = 0;
            for (int i = 0; i < 64; i++)
            {
                if (i % 8 == 0 && i != 0)
                {
                    if (gap > 0)
                    {
                        SetChar((char)(gap + '0'));
                        gap = 0;
                    }
                    SetChar('/');
                }

                if (_board.boardPtr[i] == 0)
                {
                    gap++;
                }
                else
                {
                    if (gap > 0)
                    {
                        SetChar((char)(gap + '0'));
                        gap = 0;
                    }
                    CString[CStringPointer] = GetCharFromPiece(_board.boardPtr[i]);
                    CStringPointer++;
                }
            }
            if (gap > 0)
                SetChar((char)(gap + '0'));
            SetChar(' ');

            if (_board.playerTurn == 8)
                SetChar('w');
            else
                SetChar('b');
            SetChar(' ');

            if ((_board.castle & 0b1000) == 0b1000)
                SetChar('K');
            if ((_board.castle & 0b0100) == 0b0100)
                SetChar('Q');
            if ((_board.castle & 0b0010) == 0b0010)
                SetChar('k');
            if ((_board.castle & 0b0001) == 0b0001)
                SetChar('q');
            if (CString[CStringPointer - 1] == ' ') // check if anything has acually placed
                SetChar('-');
            SetChar(' ');

            if (_board.EPSquare != 64)
            {
                // set the collum with letter
                SetChar((char)((int)'a' + _board.EPSquare % 8));

                // set the rank
                SetChar((char)(8 - (_board.EPSquare >> 3) + '0'));
            }
            else
                SetChar('-');
            SetChar(' ');

            SetChar('0');
            SetChar(' ');
            SetChar('0');

            char[] temp = new char[CStringPointer];
            for (int i = 0; i < CStringPointer; i++)
            {
                temp[i] = CString[i];
            }
            string FENString = new string(temp);
            return FENString;
        }
    }
}