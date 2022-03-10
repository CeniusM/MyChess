using Chess.Moves;
using Chess.ChessBoard;
using Chess.ChessBoard.Evaluation;

namespace Chess
{
    partial class ChessGame
    {
        private PossibleMoves _PossibleMoves;
        private MyEvaluater myEvaluater;
        public Board _board
        { get; private set; }
        public List<GameMove> gameMoves
        { get; private set; }
        public bool isGameOver
        { get; private set; }
        public int winner
        { get; private set; }

        public ChessGame()
        {
            _board = new Board();
            _PossibleMoves = new PossibleMoves(_board);
            myEvaluater = new MyEvaluater(_board);
            gameMoves = new List<GameMove>();
            isGameOver = false;
        }
        // public ChessGame(Board board)
        // {
        //     _board = new Board(board.board, board.castle);
        //     _PossibleMoves = new PossibleMoves(_board);
        // }
        public ChessGame(string FENboard)
        {
            _board = new Board(FENboard);
            _PossibleMoves = new PossibleMoves(_board);
            myEvaluater = new MyEvaluater(_board);
            gameMoves = new List<GameMove>();
            isGameOver = false;
        }
    }
}