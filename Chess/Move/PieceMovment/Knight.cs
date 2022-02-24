using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Knight
    {
        private static int[] KnightMoves =
        {
            -6,
            -10,
            -15,
            -17,
            6,
            10,
            15,
            17
        };

        // public static bool IsMovePossible(Board board, PossibleMoves.Move move) // dosnet WORK, if the peice is on the right side it can just move to the left side, by doing -6
        // {
        //     if (KnightMoves.Contains((move.StartSquare - move.TargetSquare)))
        //         if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
        //             return true;
        //     return false;
        // }

        public static bool IsMovePossible(Board board, PossibleMoves.Move move)
        {
            int start = move.StartSquare;
            int target = move.TargetSquare;
            int diff = target - start;

            if (diff == -6)
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == -1)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                        return true;
            }
            else if (diff == -10)
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == -1)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                        return true;
            }
            else if (diff == -15)
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == -2)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                        return true;
            }
            else if (diff == -17)
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == -2)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                        return true;
            }
            else if (diff == 6)
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == 1)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                        return true;
            }
            else if (diff == 10)
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == 1)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                        return true;
            }
            else if (diff == 15)
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == 2)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                        return true;
            }
            else if (diff == 17)
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == 2)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                        return true;
            }


            return false;
        }

        public static List<PossibleMoves.Move> GetPossibleMoves(Board board, int[] pos)
        {
            int playerTurn = board.PlayerTurn; // so i dont need to get it each time
            List<PossibleMoves.Move> posssibleMoves = new List<PossibleMoves.Move>();

            for (int i = 0; i < pos.Length; i++)
            {
                for (int j = 0; j < KnightMoves.Count(); j++)
                {
                    if (Board.IsPieceOppositeOrNone(board.board[pos[i]], board.board[pos[i] + KnightMoves[i]]))
                        posssibleMoves.Add(new PossibleMoves.Move(pos[i], KnightMoves[i]));
                }
            }
            return posssibleMoves;
        }
    }
}