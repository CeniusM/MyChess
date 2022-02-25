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

            bool IsMoveValid(int lineDiff)
            {
                if ((move.TargetSquare >> 3) - (move.StartSquare >> 3) == lineDiff)
                    if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                        return true;
                return false;
            }

            if (diffAmount == -8) // north
                if (IsMoveValid(-1))
                    return true;

            if (diffAmount == -7) // NorthEast
            {
                if (IsMoveValid(-1))
                    return true;
            }
            if (diffAmount == 1) // East
            {
                if (IsMoveValid(0))
                    return true;
            }
            if (diffAmount == 9) // SouthEast
            {
                if (IsMoveValid(1))
                    return true;
            }
            if (diffAmount == 8) // South
            {
                if (IsMoveValid(1))
                    return true;
            }
            if (diffAmount == 7) // SouthWest
            {
                if (IsMoveValid(1))
                    return true;
            }
            if (diffAmount == -1) // West
            {
                if (IsMoveValid(0))
                    return true;
            }
            if (diffAmount == -9) // NorthWest
            {
                if (IsMoveValid(-1))
                    return true;
            }

            return false;
        }

        public static List<Move> GetPossibleMoves(Board board)
        {
            int playerTurn = board.PlayerTurn; // so i dont need to get it each time
            List<Move> posssibleMoves = new List<Move>();

            void TryMove(int square, int move, int lineDiff)
            {
                if (((square - 6) >> 3) - (square >> 3) == lineDiff) // -6
                    if (Board.IsPieceOppositeOrNone(board.board[square >> 3], (board.board[(square - 6) >> 3])))
                        posssibleMoves.Add(new Move(square >> 3, (square - 6) >> 3));
            };
            for (int square = 0; square < 64; square++)
            {
                if (!(board.board[square] == Piece.Bishop + playerTurn))
                    continue;

                TryMove(square, -8, -1);
                TryMove(square, -7, -1);
                TryMove(square, 1, 0);
                TryMove(square, 9, 1);
                TryMove(square, 8, 1);
                TryMove(square, 7, 1);
                TryMove(square, -1, 0);
                TryMove(square, -9, -1);
            }

            return posssibleMoves;
        }
    }
}