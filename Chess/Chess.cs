using Chess.Moves;
using Chess.ChessBoard; // idk how to name
using Chess.ChessBoard.Evaluation;

namespace Chess
{
    class ChessGame
    {
        public Board _board { get; private set; }
        private PossibleMoves _PossibleMoves;
        private MyEvaluater myEvaluater;
        public bool isGameOver
        { get; private set; }

        public ChessGame()
        {
            _board = new Board();
            _PossibleMoves = new PossibleMoves(_board);
            myEvaluater = new MyEvaluater(_board);
            isGameOver = false;
        }
        // public ChessGame(Board board)
        // {
        //     _board = new Board(board.board, board.castle);
        //     _PossibleMoves = new PossibleMoves(_board);
        // }
        public ChessGame(string FENboard)
        {
            _board = new Board(FENboard);
            _PossibleMoves = new PossibleMoves(_board);
            myEvaluater = new MyEvaluater(_board);
            isGameOver = false;
        }

        public List<Move> GetPossibleMoves(int StartSquare)
        {
            return _PossibleMoves.ReturnPossibleMoves(StartSquare);
        }

        public bool MakeMove(Move move)
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

        public void UnmakeMove(Move move, int capturedPiece)
        {
            _board.board[move.StartSquare] = _board.board[move.TargetSquare];
            _board.board[move.TargetSquare] = capturedPiece;
        }

        public void StartOver()
        {
            _board.Reset();
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

        public int GetEvaluation()
        {
            return myEvaluater.Evaluate();
        }
    }
}