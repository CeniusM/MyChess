using MyChess.ChessBoard;
using MyChess.FEN;
using MyChess.PossibleMoves;

namespace MyChess
{
    class ChessGame
    {
        public Board board;
        public PossibleMovesGenerator possibleMoves;
        public ChessGame()
        {
            MovesFromSquare.Init();
            board = new Board();
            possibleMoves = new PossibleMovesGenerator(board);
        }
        public ChessGame(string FEN)
        {
            MovesFromSquare.Init();
            board = MyFEN.GetBoardFromFEN(FEN);
            possibleMoves = new PossibleMovesGenerator(board);
        }

        public void MakeMove(Move move)
        {
            board.MakeMove(move);
            possibleMoves.GenerateMoves();
        }

        public List<Move> GetPossibleMoves() => possibleMoves.moves;
    }
}