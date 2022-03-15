using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Castle
    {
        public static List<Move> GetPossibleMoves(Board board)
        {
            int playerTurn = board.PlayerTurn;
            List<Move> posssibleMoves = new List<Move>();

            if ((board.castle & 8) == 8)
            {
                if (board.board[62] == 0)
                    if (board.board[61] == 0)
                        posssibleMoves.Add(new Move(60, 62, Move.Flag.Castling)); // first num is the castle
            }
            if ((board.castle & 4) == 4)
            {
                if (board.board[59] == 0)
                    if (board.board[58] == 0)
                        if (board.board[57] == 0)
                            posssibleMoves.Add(new Move(60, 58, Move.Flag.Castling)); // first num is the castle
            }
            if ((board.castle & 2) == 2)
            {
                if (board.board[5] == 0)
                    if (board.board[6] == 0)
                        posssibleMoves.Add(new Move(4, 6, Move.Flag.Castling)); // first num is the castle
            }
            if ((board.castle & 1) == 1)
            {
                if (board.board[3] == 0)
                    if (board.board[2] == 0)
                        if (board.board[1] == 0)
                            posssibleMoves.Add(new Move(4, 2, Move.Flag.Castling)); // first num is the castle
            }

            return posssibleMoves;
        }
    }
}
/*

1000 = White KingSite  = (O-O)   8
0100 = White QueenSite = (O-O-O) 4
0010 = Black KingSite  = (O-O)   2
0001 = Black QueenSite = (O-O-O) 1

*/