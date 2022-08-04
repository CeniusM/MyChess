// does checks and stuff like that so it dosent crash, tho shoudlent be used for speedy algorithms
// only used for player interactions

namespace ChessV1
{
    public class SafeBoard
    {
        private UnsafeBoard _board;
        private PossibleMovesGenerator _movesGenerator;
        private Move[] _moves;

        public SafeBoard()
        {
            _board = new UnsafeBoard();
            _movesGenerator = new PossibleMovesGenerator(_board);
            _moves = new Move[0];
        }

        public bool MakeMove(Move move)
        {
            throw new NotImplementedException();

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
            throw new NotImplementedException();

            if (_board.gameStateHistory.Count != 0)
            {
                _board.UnMakeMove();
                _movesGenerator.GenerateMoves();
                _moves = _movesGenerator.GetMoves();
                return true;
            }
        }

        public int this[int key]
        {
            get
            {
                if (key > 63 || key < 0)
                    return int.MinValue;
                return _board.square[key];
            }
            set { throw new NotSupportedException("SafeBoard not able to set board varibles"); }
        }

        public int PlayerTurn { get => _board.playerTurn; }

        public Move[] GetMoves() => _movesGenerator.GetMoves();

        public bool IsKingInCheck() => _movesGenerator.IsKingInCheck();

        public UnsafeBoard GetUnsafeBoard() => _board;
    }
}