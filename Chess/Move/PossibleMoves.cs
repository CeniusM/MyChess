using Chess.ChessBoard;
using Chess.Moves.PieceMovment;

namespace Chess.Moves
{
    class PossibleMoves
    {
        private Board _board;
        public List<Move> possibleMoves
        { get; private set; }
        public PossibleMoves(Board board)
        {
            _board = board;
            possibleMoves = ReturnPossibleMoves();
        }

        public void GeneratePossibleMoves()
        {
            ReturnPossibleMoves();
        }

        public List<Move> ReturnPossibleMoves()
        {
            possibleMoves = new List<Move>();

            possibleMoves.AddRange(Pawn.GetPossibleMoves(_board)); // dosent queit work since just moving the pawn to the back line also counts
            possibleMoves.AddRange(Knight.GetPossibleMoves(_board));
            possibleMoves.AddRange(Bishop.GetPossibleMoves(_board));
            possibleMoves.AddRange(Rook.GetPossibleMoves(_board));
            possibleMoves.AddRange(Queen.GetPossibleMoves(_board));
            possibleMoves.AddRange(King.GetPossibleMoves(_board));

            if (_board.castle != 0)
                possibleMoves.AddRange(Castle.GetPossibleMoves(_board));

            if (_board.enPassantPiece != 64)
                possibleMoves.AddRange(EnPassant.GetPossibleMoves(_board));

            RemoveAllInvalidMoves();

            return possibleMoves;
        }

        private void RemoveAllInvalidMoves()
        {
            List<Move> validMoves = new List<Move>();
            for (int i = 0; i < possibleMoves.Count; i++)
            {
                List<Move> tempMoves = GetMovesFromMove(_board, possibleMoves[i]);
                bool validMove = true;
                for (int j = 0; j < tempMoves.Count; j++)
                {
                    if (_board.board[tempMoves[j].TargetSquare] == (Piece.King | _board.playerTurn))
                    {
                        validMove = false;
                        break;
                    }
                }
                if (validMove)
                    validMoves.Add(possibleMoves[i]);
            }
            possibleMoves = validMoves;
        }

        public List<Move> GetMovesFromMove(int startSquare, int targetSquare)
        {
            List<Move> moves = new List<Move>(4);

            for (int i = 0; i < possibleMoves.Count; i++)
            {
                if (possibleMoves[i].StartSquare == startSquare && possibleMoves[i].TargetSquare == targetSquare)
                    moves.Add(possibleMoves[i]);
            }

            return moves;
        }

        private List<Move> ReturnPossibleMoves(Board board, Move move)
        {
            List<Move> tempMoves = new List<Move>();

            tempMoves.AddRange(Pawn.GetPossibleMoves(board)); // dosent queit work since just moving the pawn to the back line also counts
            tempMoves.AddRange(Knight.GetPossibleMoves(board));
            tempMoves.AddRange(Bishop.GetPossibleMoves(board));
            tempMoves.AddRange(Rook.GetPossibleMoves(board));
            tempMoves.AddRange(Queen.GetPossibleMoves(board));
            tempMoves.AddRange(King.GetPossibleMoves(board));

            if (board.castle != 0)
                tempMoves.AddRange(Castle.GetPossibleMoves(board));

            if (board.enPassantPiece != 64)
                tempMoves.AddRange(EnPassant.GetPossibleMoves(board));

            RemoveAllInvalidMoves();

            return tempMoves;
        }
    }
}