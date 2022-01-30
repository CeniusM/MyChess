using Chess.Moves;
using Chess.ChessBoard; // idk how to name

namespace Chess
{
    class ChessGame
    {
        private Board _board;
        private PosebleMoves posebleMoves;
        public ChessGame()
        {
            _board = new Board();
            posebleMoves = new PosebleMoves(_board);
        }

        /// <summary>
        /// This returnes a list of the poseble moves
        /// </summary>
        public List<PosebleMoves.Move> StartSquare(int StartSquare)
        {
            return posebleMoves.ReturnPosebleMoves(StartSquare);
        }

        /// <summary>
        /// This takes a move and returns if it was valid
        /// </summary>
        public bool MakeMove(PosebleMoves.Move Move)
        {
            return false;
        }

        public string GetFENBoard()
        {
            string FEN = ""; // returns a FEN string

            return FEN;
        }
    }
}