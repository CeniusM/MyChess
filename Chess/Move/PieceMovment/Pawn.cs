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

        public static bool IsMovePossible(Board board, PossibleMoves.Move move, int enpasantPieces)
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
                    if (((move.StartSquare - move.TargetSquare) % 8) != 1)
                        return false;
                    else if (Board.IsPieceBlack(board.board[move.TargetSquare]))
                        return true;
                }
                else if ((move.StartSquare - 9) == move.TargetSquare)
                {
                    if (((move.StartSquare - move.TargetSquare) % 8) != 1)
                        return false;
                    if (Board.IsPieceBlack(board.board[move.TargetSquare]))
                        return true;
                }

                //else if (enpasant left)
                //else if (enpasant right)
            }
            else if (Board.IsPieceBlack(board.board[move.StartSquare]))
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
                    if (((move.StartSquare - move.TargetSquare) % 8) != 1)
                        return false;
                    else if (Board.IsPieceWhite(board.board[move.TargetSquare]))
                        return true;
                }
                else if ((move.StartSquare + 9) == move.TargetSquare)
                {
                    if (((move.StartSquare - move.TargetSquare) % 8) != 1)
                        return false;
                    if (Board.IsPieceWhite(board.board[move.TargetSquare]))
                        return true;
                }

                //else if (enpasant left)
                //else if (enpasant right)
            }
            return false;
        }

        // public static bool IsMovePossibleNewTry(Board board, PossibleMoves.Move move)
        // {
        //     if ((move.StartSquare - 8) == move.TargetSquare)
        //     {
        //         if (board.board[move.TargetSquare] == 0)
        //             return true;
        //     }
        //     else if ((move.StartSquare - 16) == move.TargetSquare)
        //     {
        //         if (move.StartSquare < 56 && move.StartSquare > 47)
        //             if (board.board[move.TargetSquare] == 0)
        //                 if (board.board[move.TargetSquare + 8] == 0)
        //                     return true;
        //     }
        //     else if ((move.StartSquare - 7) == move.TargetSquare)
        //     {
        //         if (Board.IsPieceOpposite(board.board[move.StartSquare], board.board[move.TargetSquare]))
        //             return true;
        //     }
        //     else if ((move.StartSquare - 9) == move.TargetSquare)
        //     {
        //         if (Board.IsPieceOpposite(board.board[move.StartSquare], board.board[move.TargetSquare]))
        //             return true;
        //     }
        //     return false;
        // }

        public static List<PossibleMoves.Move> GetPossibleMoves(Board board)
        {
            int playerTurn = board.PlayerTurn; // so i dont need to get it each time
            List<PossibleMoves.Move> posssibleMoves = new List<PossibleMoves.Move>();

            return posssibleMoves;
        }
    }
}