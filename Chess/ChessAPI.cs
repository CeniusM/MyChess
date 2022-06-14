using MyChess.ChessBoard;
using MyChess.FEN;
using MyChess.PossibleMoves;

namespace MyChess
{
    class ChessGame
    {
        public Board board;
        public ChessGame()
        {
            board = new Board();
        }
        public ChessGame(string FEN)
        {
            board = MyFEN.GetBoardFromFEN(FEN);
        }

        public void MakeMove(Move move)
        {
            board.MakeMove(move);
        }

        public List<Move> GetPossibleMoves() => PossibleMovesGenerator.GetMoves(board);
    }
}