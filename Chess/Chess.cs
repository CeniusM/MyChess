using Chess.Moves;
using Chess.ChessBoard; // idk how to name

namespace Chess
{
    class ChessGame
    {
        private Board _board;
        private PosebleMoves posebleMoves;
        public ChessGame()
        {
            _board = new Board();
            posebleMoves = new PosebleMoves(_board);

            //test
            _board.board[1] = Piece.Bishop + Piece.White;
            _board.board[45] = Piece.King + Piece.Black;
            _board.board[61] = Piece.King + Piece.White;
        }
        public ChessGame(int[] board, int castle)
        {
            _board = new Board(board, castle);
            posebleMoves = new PosebleMoves(_board);
        }
        public ChessGame(Board board)
        {
            _board = new Board(board.board, board.castle);
            posebleMoves = new PosebleMoves(_board);
        }
        public ChessGame(string FENboard)
        {
            _board = new Board(FENboard);
            posebleMoves = new PosebleMoves(_board);
        }

        /// <summary>
        /// This returnes a list of the poseble moves
        /// </summary>
        public List<PosebleMoves.Move> StartSquare(int StartSquare)
        {
            return posebleMoves.ReturnPosebleMoves(StartSquare);
        }

        /// <summary>
        /// This takes a move and returns if it was valid
        /// </summary>
        public bool MakeMove(PosebleMoves.Move Move)
        {
            //for now
            // _board.board[Move.TargetSquare] = _board.board[Move.StartSquare];

            return true;
        }

        public Board GetBoard()
        {
            return _board;
        }

        public string GetFENBoard()
        {
            string FEN = ""; // returns a FEN string

            return FEN;
        }
    }
}