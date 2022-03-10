using Chess.Moves;
using Chess.ChessBoard;

namespace Chess
{
    partial class ChessGame
    {
        public void StartOver() => _board.Reset();
        public void LoadFENString(string FEN) => _board = MyFEN.GetBoardFromFEN(FEN);
        public string GetFENBoard() => MyFEN.GetFENFromBoard(_board);
        public Board GetBoard() => _board;
        public int GetEvaluation() => myEvaluater.Evaluate();
        public List<Move> GetPossibleMoves() => _PossibleMoves.possibleMoves;
        public List<Move> GetPossibleMoves(int startSquare, int targetSquare) => _PossibleMoves.GetMovesFromMove(startSquare, targetSquare);
    }
}