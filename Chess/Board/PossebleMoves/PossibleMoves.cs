using MyChess.ChessBoard;
using MyChess.PossibleMoves.Pieces;

// maby put all the piece movement code in here for speed?
// implement and test the loop inside Generate the loop


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

            // so we dont need to loop over all square everytime we try and fint the right piece
            // for (int i = 0; i < board.piecePoses.Count; i++)
            // {
            //     int piece = board[board.piecePoses[i]] & Piece.PieceBits;

            //     if (piece == Piece.None)
            //         continue;
            //     else if (piece == Piece.Pawn)
            //         {}// stuff
            //     else if (piece == Piece.Bishop)
            //         Bishop.AddMoves(board, moves, board.piecePoses[i]);
            // }

            King.AddMoves(board, moves);
            Knight.AddMoves(board, moves);
            Bishop.AddMoves(board, moves);
            Rook.AddMoves(board, moves);
            Queen.AddMoves(board, moves);
            Pawn.AddMoves(board, moves);

            TerribleFilterForChecks();
        }

        private void TerribleFilterForChecks()
        {
            List<Move> newMoves = new List<Move>(moves.Count);

            for (int i = 0; i < moves.Count; i++)
            {
                board.MakeMove(moves[i]);

                moves = new List<Move>(30);

                King.AddMoves(board, moves);
                Knight.AddMoves(board, moves);
                Bishop.AddMoves(board, moves);
                Rook.AddMoves(board, moves);
                Queen.AddMoves(board, moves);
                Pawn.AddMoves(board, moves);

                bool notInCheck = true;
                int kingPos = -1;

                for (int j = 0; j < board.piecePoses.Count; j++)
                {
                    if (board[board.piecePoses[j]] == (Piece.King | (board.playerTurn ^ Board.ColorMask)))
                    {
                        kingPos = board.piecePoses[j];
                        break;
                    }
                }

                for (int j = 0; j < moves.Count; j++)
                {
                    if ()
                }

                board.UnMakeMove();
            }

            moves = newMoves;
        }
    }
}