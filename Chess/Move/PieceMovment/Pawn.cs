using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Pawn
    {
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
                                posssibleMoves.Add(new Move(square, square - 16, Move.Flag.PawnTwoForward));

                    if (board.board[square - 8] == 0)
                        posssibleMoves.Add(new Move(square, square - 8));

                    if (((square - 7) >> 3) - (square >> 3) == -1)
                    {
                        if (Board.IsPieceOpposite(board.board[square], board.board[square - 7]))
                            posssibleMoves.Add(new Move(square, square - 7));
                    }

                    if (((square - 9) >> 3) - (square >> 3) == -1)
                    {
                        if (Board.IsPieceOpposite(board.board[square], board.board[square - 9]))
                            posssibleMoves.Add(new Move(square, square - 9));
                    }
                }
                else // black
                {
                    if (square >> 3 == 1)
                        if (board.board[square + 8] == 0)
                            if (board.board[square + 16] == 0)
                                posssibleMoves.Add(new Move(square, square + 16, Move.Flag.PawnTwoForward));

                    if (board.board[square + 8] == 0)
                        posssibleMoves.Add(new Move(square, square + 8));

                    if (((square + 7) >> 3) - (square >> 3) == 1)
                    {
                        if (Board.IsPieceOpposite(board.board[square], board.board[square + 7]))
                            posssibleMoves.Add(new Move(square, square + 7));
                    }

                    if (((square + 9) >> 3) - (square >> 3) == 1)
                    {
                        if (Board.IsPieceOpposite(board.board[square], board.board[square + 9]))
                            posssibleMoves.Add(new Move(square, square + 9));
                    }

                    // promotions
                    if (square + 8 < 64 && square + 8 > 56)
                    {
                        posssibleMoves.Add(new Move(square, square + 8, Move.Flag.PromoteToQueen));
                    }
                }
            }

            return posssibleMoves;
        }
    }
}