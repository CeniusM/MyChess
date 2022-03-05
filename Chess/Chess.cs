using System.Globalization;
using System.IO.MemoryMappedFiles;
using System;
using System.Net.NetworkInformation;
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
        public List<GameMove> gameMoves
        { get; private set; }
        public bool isGameOver
        { get; private set; }

        public ChessGame()
        {
            _board = new Board();
            _PossibleMoves = new PossibleMoves(_board);
            myEvaluater = new MyEvaluater(_board);
            gameMoves = new List<GameMove>();
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
            gameMoves = new List<GameMove>();
            isGameOver = false;
        }

        public List<Move> GetPossibleMoves()
        {
            return _PossibleMoves.ReturnPossibleMoves();
        }

        public bool MakeMove(Move move)
        {
            if ((_board.board[move.StartSquare] & Piece.ColorBits) != _board.PlayerTurn)
                return false;
            if (move.StartSquare > 63 || move.StartSquare < 0 || move.TargetSquare > 63 || move.TargetSquare < 0)
                return false;

            int indexOfMove = GetIndexOfMove(_PossibleMoves.possibleMoves, move);

            if (indexOfMove != -1)
            {
                List<Move> moves = _PossibleMoves.possibleMoves; // just making a refrence right?
                
                int startSquare = moves[indexOfMove].StartSquare;
                int targetSquare = moves[indexOfMove].TargetSquare;

                // gotta later on make it so it dosent need the index and just use the input "move"


                if (moves[indexOfMove].MoveFlag == Move.Flag.None)
                {
                    _board.board[targetSquare] = _board.board[startSquare];
                    _board.board[startSquare] = 0;

                }
                else if (moves[indexOfMove].MoveFlag == Move.Flag.PawnTwoForward)
                {
                    _board.board[targetSquare] = _board.board[startSquare];
                    _board.board[startSquare] = 0;
                    _board.enPassantPiece = targetSquare;
                }
                else if (moves[indexOfMove].MoveFlag == Move.Flag.EnPassantCapture)
                {
                    _board.board[targetSquare] = _board.board[startSquare];
                    _board.board[startSquare] = 0;
                    if (Board.IsPieceWhite(_board.board[targetSquare]))
                        _board.board[targetSquare + 8] = 0;
                    else
                        _board.board[targetSquare - 8] = 0;
                }
                else if (moves[indexOfMove].MoveFlag == Move.Flag.Castling)
                {
                    // (☞ﾟヮﾟ)☞  ☜(ﾟヮﾟ☜)
                }
                else // promotions
                {
                    _board.board[targetSquare] = move.PromotionPieceType + _board.PlayerTurn;
                    _board.board[startSquare] = 0;
                }
                _board.ChangePlayer();
            }
            return false;
        }

        public void UnmakeMove()
        {
            GameMove move = gameMoves[gameMoves.Count - 1];
            gameMoves.RemoveAt(gameMoves.Count - 1);

            _board.board[move.StartSquare] = _board.board[move.TargetSquare];
            _board.board[move.TargetSquare] = move.CapturedPiece;

            if (move.MoveFlag == Move.Flag.PromoteToQueen)
                _board.board[move.StartSquare] = Piece.Pawm + _board.PlayerTurn;

            _board.ChangePlayer();
        }


        public void StartOver() => _board.Reset();

        public void LoadFENString(string FEN) => _board = MyFEN.GetBoardFromFEN(FEN);

        public string GetFENBoard() => MyFEN.GetFENFromBoard(_board);

        public Board GetBoard() => _board;

        public int GetEvaluation() => myEvaluater.Evaluate();

        private int GetIndexOfMove(List<Move> moves, Move move)
        {
            int startSquare = move.StartSquare;
            int targetSquare = move.TargetSquare;

            for (int i = 0; i < moves.Count; i++)
            {
                if (moves[i].StartSquare == startSquare)
                    if (moves[i].TargetSquare == move.TargetSquare)
                        return i;
            }

            return -1;
        }
    }
}