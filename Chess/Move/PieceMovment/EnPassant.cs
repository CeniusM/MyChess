// not used yet, the pawn just checks but later on do it in here instead of doing it everytime with each pawn
using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class EnPassant
    {
        public static List<Move> GetPossibleMoves(Board board)
        {
            int playerTurn = board.PlayerTurn;
            List<Move> posssibleMoves = new List<Move>(1);

            int enPassant = board.enPassantPiece;

            // Right
            if (board.board[1 + enPassant] == Piece.Pawm + playerTurn)
            {
                if (playerTurn == Piece.White)
                    posssibleMoves.Add(new Move(1 + enPassant, enPassant - 8, Move.Flag.EnPassantCapture));
                else
                    posssibleMoves.Add(new Move(1 + enPassant, enPassant + 8, Move.Flag.EnPassantCapture));
            }

            // Left
            if (board.board[enPassant - 1] == Piece.Pawm + playerTurn)
            {
                if (playerTurn == Piece.White)
                    posssibleMoves.Add(new Move(enPassant - 1, enPassant - 8, Move.Flag.EnPassantCapture));
                else
                    posssibleMoves.Add(new Move(enPassant - 1, enPassant + 8, Move.Flag.EnPassantCapture));
            }


            return posssibleMoves;
        }
    }
}