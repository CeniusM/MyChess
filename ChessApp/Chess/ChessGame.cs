using MyChess.ChessBoard;
using MyChess.FEN;
using MyChess.PossibleMoves;

namespace MyChess
{
    public class ChessGame
    {
        private const string InitialPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        public Board board;
        public PossibleMovesGenerator possibleMoves;
        public Move lastMove = new Move(-1, -1, -1, -1);
        public ChessGame()
        {
            MovesFromSquare.Init();
            board = MyFEN.GetBoardFromFEN(InitialPosition);
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
            if (possibleMoves.moves.Count == 0)
                return;
            board.MakeMove(move);
            possibleMoves.GenerateMoves();
        }

        public void UnMakeMove()
        {
            if (board.moves.Count == 0)
                return;
            board.UnMakeMove();
            possibleMoves.GenerateMoves();
        }

        public List<Move> GetPossibleMoves() => possibleMoves.moves;
    }
}