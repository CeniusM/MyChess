using Chess.Moves;
using Chess.ChessBoard; // idk how to name

namespace Chess
{
    class ChessGame
    {
        private Board _board;
        public PosebleMoves posebleMoves;
        public ChessGame()
        {
            _board = new Board();
            posebleMoves = new PosebleMoves(_board);
        }

        public void MakeMove()
        {

        }

        public string GetBoard()
        {
            string FEN = ""; // returns a FEN string

            return FEN;
        }
    }
}