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

        // public static bool IsMovePossible(Board board, Move move) // dosnet WORK, if the peice is on the right side it can just move to the left side, by doing -6
        // {
        //     if (KnightMoves.Contains((move.StartSquare - move.TargetSquare)))
        //         if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
        //             return true;
        //     return false;
        // }

        public static bool IsMovePossible(Board board, Move move)
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

        public static List<Move> GetPossibleMoves(Board board)
        {
            int playerTurn = board.PlayerTurn; // so i dont need to get it each time
            List<Move> posssibleMoves = new List<Move>();


            void TryMove(int square, int move, int lineDiff)
            {
                if (square + move < 64 && square + move > -1)
                    if (((square + move) >> 3) - (square >> 3) == lineDiff)
                        if (Board.IsPieceOppositeOrNone(board.board[square], (board.board[square + move])))
                            posssibleMoves.Add(new Move(square, square + move));
            };
            for (int square = 0; square < 64; square++)
            {
                if (!(board.board[square] == Piece.Knight + playerTurn))
                    continue;

                TryMove(square, -6, -1);
                TryMove(square, -10, -1);
                TryMove(square, -15, -2);
                TryMove(square, -17, -2);
                TryMove(square, 6, 1);
                TryMove(square, 10, 1);
                TryMove(square, 15, 2);
                TryMove(square, 17, 2);
            }

            return posssibleMoves;
        }
    }
}


// for (int i = 0; i < pos.Length; i++)
// {
//     for (int j = 0; j < KnightMoves.Count(); j++)
//     {
//         if (Board.IsPieceOppositeOrNone(board.board[pos[i]], board.board[pos[i] + KnightMoves[i]]))
//             posssibleMoves.Add(new Move(pos[i], KnightMoves[i]));
//     }
// }