using MyChess.ChessBoard;
using MyChess.FEN;
using MyChess.PossibleMoves;
using MyChess.ChessBoard.Evaluators;

namespace MyChess
{
    public class ChessGame
    {
        private const string InitialPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        public Board board;
        public PossibleMovesGenerator possibleMoves;
        public Evaluator evaluator;
        public Move lastMove = new Move(-1, -1, -1, -1);
        private bool CapturesOnly = false;
        public ChessGame()
        {
            MovesFromSquare.Init();
            board = MyFEN.GetBoardFromFEN(InitialPosition);
            possibleMoves = new PossibleMovesGenerator(board);
            evaluator = new(this);
        }
        public ChessGame(string FEN)
        {
            MovesFromSquare.Init();
            board = MyFEN.GetBoardFromFEN(FEN);
            possibleMoves = new PossibleMovesGenerator(board);
            evaluator = new(this);
        }

        public void SetCapturesOnlyOn()
        {
            CapturesOnly = true;
            possibleMoves.GenerateMoves(CapturesOnly);
        }

        public void SetCapturesOnlyOff()
        {
            CapturesOnly = false;
            possibleMoves.GenerateMoves(CapturesOnly);
        }

        public void MakeMove(Move move)
        {
            if (possibleMoves.moves.Count == 0)
                return;
            board.MakeMove(move);
            possibleMoves.GenerateMoves(CapturesOnly);
        }

        public void UnMakeMove()
        {
            if (board.moves.Count == 0)
                return;
            board.UnMakeMove();
            possibleMoves.GenerateMoves(CapturesOnly);
        }

        public List<Move> GetPossibleMoves() => possibleMoves.moves;
    }
}