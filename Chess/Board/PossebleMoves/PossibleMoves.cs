using MyChess.ChessBoard;
using MyChess.PossibleMoves.Pieces;

namespace MyChess.PossibleMoves
{
    class PossibleMovesGenerator
    {
        private Board board;
        public List<Move> moves;
        public PossibleMovesGenerator(Board board)
        {
            moves = new List<Move>();
            this.board = board;
            GenerateMoves();
        }
        
        public void GenerateMoves()
        {
            moves = new List<Move>(30); // avg moves for random pos
            King.AddMoves(board, moves);
            Knight.AddMoves(board, moves);
        }

        public List<Move> GetMoves(Board board, int selecktedPiece, bool reGenerateMoves = false)
        {
            if (reGenerateMoves)
                GenerateMoves();

            List<Move> newMoves = new List<Move>(moves.Capacity);

            for (int i = 0; i < moves.Count; i++)
            {
                if (moves[i].StartSquare == selecktedPiece)
                {
                    newMoves.Add(moves[i]);
                }
            }
            
            return newMoves;
        }
    }
}