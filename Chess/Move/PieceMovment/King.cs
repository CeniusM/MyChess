using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class King
    {
        private static int[] KingMoves =
        {
            DirectionOffSets.North,
            DirectionOffSets.South,
            DirectionOffSets.East,
            DirectionOffSets.West,
            DirectionOffSets.NorthEast,
            DirectionOffSets.SouthEast,
            DirectionOffSets.SouthWest,
            DirectionOffSets.NothWest
        };

        // public static bool IsMovePossible(Board board, Move move) // old
        // {
        //     if (KingMoves.Contains((move.StartSquare - move.TargetSquare)))
        //         if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
        //             return true;
        //     return false;
        // }

        public static bool IsMovePossible(Board board, Move move)
        {
            int diffAmount = move.TargetSquare - move.StartSquare;

            if (diffAmount == -8) // north
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == -1)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                    {
                        return true;
                    }
            }
            else if (diffAmount == -7) // NorthEast
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == -1)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                    {
                        return true;
                    }
            }
            else if (diffAmount == 1) // East
            {
                if ((move.StartSquare >> 3 == move.TargetSquare >> 3))
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                    {
                        return true;
                    }
            }
            else if (diffAmount == 9) // SouthEast
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == 1)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                    {
                        return true;
                    }
            }
            else if (diffAmount == 8) // South
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == 1)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                    {
                        return true;
                    }
            }
            else if (diffAmount == 7) // SouthWest
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == 1)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                    {
                        return true;
                    }
            }
            else if (diffAmount == -1) // West
            {
                if ((move.StartSquare >> 3 == move.TargetSquare >> 3))
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                    {
                        return true;
                    }
            }
            else if (diffAmount == -9) // NorthWest
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == -1)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                    {
                        return true;
                    }
            }

            return false;
        }

        public static List<Move> GetPossibleMoves(Board board, int[] pos)
        {
            int playerTurn = board.PlayerTurn; // so i dont need to get it each time
            List<Move> posssibleMoves = new List<Move>();

            for (int i = 0; i < pos.Length; i++)
            {
                for (int j = 0; j < KingMoves.Count(); j++)
                {
                    if (Board.IsPieceOppositeOrNone(board.board[pos[i]], board.board[pos[i] + KingMoves[j]]))
                        posssibleMoves.Add(new Move(pos[i], KingMoves[j]));
                }
            }
            return posssibleMoves;
        }
    }
}