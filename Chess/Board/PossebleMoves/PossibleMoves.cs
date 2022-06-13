using MyChess.ChessBoard;
using MyChess.PossibleMoves.Pieces;

namespace MyChess.PossibleMoves
{
    class PossibleMovesGenerator
    {
        public static List<Move> GetMoves(Board board)
        {
            List<Move> moves = new List<Move>(30); // avg moves for an pos



            return moves;
        }
        public static List<Move> GetMoves(Board board, int selecktedPiece)
        {
            List<Move> moves = GetMoves(board);
            List<Move> newMoves = new List<Move>(moves.Capacity);
            for (int i = 0; i < moves.Count; i++)
            {
                if (moves[i].StartSquare == selecktedPiece)
                {
                    newMoves.Add(moves[i]);
                }
            }
            return moves;
        }
    }
}