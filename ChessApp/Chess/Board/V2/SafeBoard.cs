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
    }
}