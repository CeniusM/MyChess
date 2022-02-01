using Chess.Moves;
using Chess.ChessBoard; // idk how to name

namespace Chess
{
    class ChessGame
    {
        private Board _board;
        private PosebleMoves _posebleMoves;
        public ChessGame()
        {
            _board = new Board();
            _posebleMoves = new PosebleMoves(_board);
        }
        public ChessGame(int[] board, int castle)
        {
            _board = new Board(board, castle);
            _posebleMoves = new PosebleMoves(_board);
        }
        public ChessGame(Board board)
        {
            _board = new Board(board.board, board.castle);
            _posebleMoves = new PosebleMoves(_board);
        }
        public ChessGame(string FENboard)
        {
            _board = new Board(FENboard);
            _posebleMoves = new PosebleMoves(_board);
        }

        /// <summary>
        /// This returnes a list of the poseble moves
        /// </summary>
        public List<PosebleMoves.Move> StartSquare(int StartSquare)
        {
            return _posebleMoves.ReturnPosebleMoves(StartSquare);
        }

        /// <summary>
        /// This takes a move and returns if it was valid
        /// </summary>
        public bool MakeMove(PosebleMoves.Move move)
        {
            //for now
            if (_posebleMoves.IsMovePoseble(move))
            {
                if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Pawm)) // queen check, make it its own method
                {
                    if (Board.IsPieceWhite(_board.board[move.StartSquare]))
                    {
                        if (move.TargetSquare > -1 && move.TargetSquare < 9)
                            _board.board[move.StartSquare] = Piece.WQueen;
                    }
                    else
                    {
                        if (move.TargetSquare > 57 && move.TargetSquare < 64)
                            _board.board[move.StartSquare] = Piece.BQueen;
                    }
                }

                _board.board[move.TargetSquare] = _board.board[move.StartSquare];
                _board.board[move.StartSquare] = Piece.None;
            }

            return true;
        }

        public void StartOver()
        {
            _board.SetUpToStanderd();
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