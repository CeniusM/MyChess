// does checks and stuff like that so it dosent crash, tho shoudlent be used for speedy algorithms
// only used for player interactions

namespace ChessV1
{
    class SafeBoard
    {
        private UnsafeBoard _board;
        private PossibleMovesGenerator _movesGenerator;

        public SafeBoard()
        {
            _board = new UnsafeBoard();
            _movesGenerator = new PossibleMovesGenerator(_board);
        }

        public bool MakeMove(Move move)
        {
            throw new NotImplementedException();

            // if _mGenerator.Contains(move)
            // _board.makemove()
            // return true
            // else
            // return false
        }

        public bool UnMakeMove()
        {
            throw new NotImplementedException();

            // if _board.gamestatehistory.Count != 0
            // _board.UnmakeMove
            // return true
            // else 
            // return false
        }

        public int this[int key]
        {
            get => _board.square[key];
            set { throw new NotSupportedException("SafeBoard not able to set board varibles"); }
        }

        public int PlayerTurn { get => _board.playerTurn; }
    }
}