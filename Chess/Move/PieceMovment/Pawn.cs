using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Pawn
    {
        /*
        the 6th bit is used to see if it can do a double move
        the 7th bit is used to see if it can have used "En passant" on it
        but for now i just check if it is on the right row
        */

        public static bool IsMovePossible(Board board, Move move, int enpasantPieces)
        {
            if (Board.IsPieceWhite(board.board[move.StartSquare]))
            {
                if ((move.StartSquare - 8) == move.TargetSquare)
                {
                    if (board.board[move.TargetSquare] == 0)
                        return true;
                }
                else if ((move.StartSquare - 16) == move.TargetSquare)
                {
                    if (move.StartSquare < 56 && move.StartSquare > 47)
                        if (board.board[move.TargetSquare] == 0)
                            if (board.board[move.TargetSquare + 8] == 0)
                                return true;
                }
                else if ((move.StartSquare - 7) == move.TargetSquare)
                {
                    if ((move.StartSquare >> 3) - (move.TargetSquare >> 3) == -1)
                        return false;
                    else if (Board.IsPieceBlack(board.board[move.TargetSquare]))
                        return true;
                    else if ((move.TargetSquare + 8) == enpasantPieces) // endpasant
                        if (Board.IsPieceBlack(board.board[enpasantPieces]))
                            return true;
                }
                else if ((move.StartSquare - 9) == move.TargetSquare)
                {
                    if ((move.StartSquare >> 3) - (move.TargetSquare >> 3) == -1)
                        return false;
                    else if (Board.IsPieceBlack(board.board[move.TargetSquare]))
                        return true;
                    else if ((move.TargetSquare + 8) == enpasantPieces) // endpasant
                        if (Board.IsPieceBlack(board.board[enpasantPieces]))
                            return true;
                }
            }
            else
            {
                if ((move.StartSquare + 8) == move.TargetSquare)
                {
                    if (board.board[move.TargetSquare] == 0)
                        return true;
                }
                else if ((move.StartSquare + 16) == move.TargetSquare) // can only move if the pawn hasent moved once yet, indecated by the 32value bit
                {
                    if (move.StartSquare > 7 && move.StartSquare < 16)
                        if (board.board[move.TargetSquare] == 0)
                            if (board.board[move.TargetSquare - 8] == 0)
                                return true;
                }
                else if ((move.StartSquare + 7) == move.TargetSquare)
                {
                    if ((move.StartSquare >> 3) - (move.TargetSquare >> 3) == 1)
                        return false;
                    else if (Board.IsPieceWhite(board.board[move.TargetSquare]))
                        return true;
                    else if ((move.TargetSquare - 8) == enpasantPieces) // endpasant
                        if (Board.IsPieceWhite(board.board[enpasantPieces]))
                            return true;
                }
                else if ((move.StartSquare + 9) == move.TargetSquare)
                {
                    if ((move.StartSquare >> 3) - (move.TargetSquare >> 3) == 1)
                        return false;
                    else if (Board.IsPieceWhite(board.board[move.TargetSquare]))
                        return true;
                    else if ((move.TargetSquare - 8) == enpasantPieces) // endpasant
                        if (Board.IsPieceWhite(board.board[enpasantPieces]))
                            return true;
                }
            }
            return false;
        }

        public static List<Move> GetPossibleMoves(Board board)
        {
            int playerTurn = board.PlayerTurn; // so i dont need to get it each time
            List<Move> posssibleMoves = new List<Move>();

            for (int square = 0; square < 64; square++)
            {
                if (!(board.board[square] == Piece.Pawm + playerTurn))
                    continue;


                if (playerTurn == Piece.White) // white
                {
                    if (square >> 3 == 6)
                        if (board.board[square - 8] == 0)
                            if (board.board[square - 16] == 0)
                                posssibleMoves.Add(new Move(square, square - 16));

                    if (board.board[square - 8] == 0)
                        posssibleMoves.Add(new Move(square, square - 8));

                    if ((square - 7) >> 3 == square >> 3)
                        if (Board.IsPieceOpposite(board.board[square], board.board[square - 7]))
                            posssibleMoves.Add(new Move(square, square - 7));

                    if ((square - 9) >> 3 == square >> 3)
                        if (Board.IsPieceOpposite(board.board[square], board.board[square - 9]))
                            posssibleMoves.Add(new Move(square, square - 9));
                }

                else    // black
                {
                    if (square >> 3 == 1)
                        if (board.board[square + 8] == 0)
                            if (board.board[square + 16] == 0)
                                posssibleMoves.Add(new Move(square, square + 16));

                    if (board.board[square + 8] == 0)
                        posssibleMoves.Add(new Move(square, square + 8));

                    if ((square + 7) >> 3 == square >> 3)
                        if (Board.IsPieceOpposite(board.board[square], board.board[square + 7]))
                            posssibleMoves.Add(new Move(square, square + 7));

                    if ((square + 9) >> 3 == square >> 3)
                        if (Board.IsPieceOpposite(board.board[square], board.board[square + 9]))
                            posssibleMoves.Add(new Move(square, square + 9));
                }
            }

            return posssibleMoves;
        }
    }
}