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
            

            return false;
        }

        // public bool MakeMove(Move move) // old
        // {
        //     if ((_board.board[move.StartSquare] & Piece.ColorBits) != _board.PlayerTurn)
        //         return false;

        //     //for now
        //     // if (_PossibleMoves.ReturnPossibleMoves(64).Contains(move))
        //     if (DoesListContainMove(_PossibleMoves.possibleMoves, move))
        //     {
        //         if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Pawm)) // pawn promotian check, make it its own method
        //         {
        //             _board.enPassantPiece = 64;
        //             if ((_board.board[move.StartSquare] & Piece.PieceBits) == Piece.Pawm)
        //                 if (Math.Abs(move.StartSquare - move.TargetSquare) == 16)
        //                     _board.enPassantPiece = move.TargetSquare;
        //             // else if (move.TargetSquare )
        //         }

        //         gameMoves.Add(new GameMove(move.StartSquare, move.TargetSquare, move.MoveFlag, _board.board[move.TargetSquare]));

        //         if (move.MoveFlag == Move.Flag.EnPassantCapture)
        //         {
        //             if (Board.IsPieceWhite(_board.board[move.StartSquare]))
        //                 _board.board[move.TargetSquare + 8] = 0;
        //             else
        //                 _board.board[move.TargetSquare - 8] = 0;
        //         }


        //         _board.board[move.TargetSquare] = _board.board[move.StartSquare];
        //         _board.board[move.StartSquare] = Piece.None;
        //         _board.ChangePlayer();
        //         return true;
        //     }
        //     return false;
        // }

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

        private bool DoesListContainMove(List<Move> moves, Move move)
        {
            int startSquare = move.StartSquare;
            int targetSquare = move.TargetSquare;

            for (int i = 0; i < moves.Count; i++)
            {
                if (moves[i].StartSquare == startSquare)
                    if (moves[i].TargetSquare == move.TargetSquare)
                        return true;
            }

            return false;
        }
    }
}