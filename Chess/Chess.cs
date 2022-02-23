using Chess.Moves;
using Chess.ChessBoard; // idk how to name

namespace Chess
{
    class ChessGame
    {
        public Board _board { get; private set; }
        private PossibleMoves _PossibleMoves;
        public bool isGameOver { get; private set; } = false;

        public ChessGame()
        {
            _board = new Board();
            _PossibleMoves = new PossibleMoves(_board);
        }
        public ChessGame(int[] board, int castle)
        {
            _board = new Board(board, castle);
            _PossibleMoves = new PossibleMoves(_board);
        }
        public ChessGame(Board board)
        {
            _board = new Board(board.board, board.castle);
            _PossibleMoves = new PossibleMoves(_board);
        }
        public ChessGame(string FENboard)
        {
            _board = new Board(FENboard);
            _PossibleMoves = new PossibleMoves(_board);
        }

        /// <summary>
        /// This returnes a list of the Possible moves
        /// </summary>
        public List<PossibleMoves.Move> StartSquare(int StartSquare)
        {
            return _PossibleMoves.ReturnPossibleMoves(StartSquare);
        }

        /// <summary>
        /// This takes a move and returns if it was valid
        /// </summary>
        public bool MakeMove(PossibleMoves.Move move)
        {
            if ((_board.board[move.StartSquare] & Piece.ColorBits) != _board.PlayerTurn)
                return false;

            //for now
            if (_PossibleMoves.IsMovePossible(move))
            {
                if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Pawm)) // pawn promotian check, make it its own method
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
                _board.ChangePlayer();
                // CS_MyConsole.MyConsole.WriteLine(move.StartSquare + "." + move.TargetSquare + ". piece = " + (_board.board[move.TargetSquare] & Piece.PieceBits)); // debuging
                return true;
            }
            return false;
        }

        public void StartOver()
        {
            _board.SetUpToStanderd();
        }

        public Board GetBoard()
        {
            return _board;
        }

        public void LoadFENString(string FEN)
        {
            _board = MyFEN.GetBoardFromFEN(FEN);
        }

        public string GetFENBoard()
        {
            return MyFEN.GetFENFromBoard(_board);
        }
    }
}