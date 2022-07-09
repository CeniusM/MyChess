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
            Castle.AddMoves(board, moves);

            KingCheckCheck();
        }
        
        public int GetKingsPos(int color)
        {
            for (int i = 0; i < board.piecePoses.Count; i++)
                if (board[board.piecePoses[i]] == (color | Piece.King))
                    return board.piecePoses[i];
            return -1;
        }

        private void KingCheckCheck()
        {
            List<Move> ValidMoves = new List<Move>(moves.Count);

            // go through each move
            for (int i = 0; i < moves.Count; i++)
            {
                board.MakeMove(moves[i]);


                // save the new possible moves
                if (!CheckKingInCheck(GetKingsPos(board.playerTurn ^ Board.ColorMask), board.playerTurn ^ Board.ColorMask))
                    ValidMoves.Add(moves[i]);

                // if (moves[i].MoveFlag != 0) // for debuging
                // {

                // }

                board.UnMakeMove();
            }

            moves = ValidMoves;
        }

        public bool CheckKingInCheck(int kingPos, int kingColor)
        {
            // test, and shouldent acktullay be needed
            if (kingPos == -1)
                return true;








            /*
                for v2 note, also add in castle
                here you can use magic bitboard, you can just AND operate a long that has bits on all the locations that the knight
                can jump to from the square is on
                so fx

                    Long                   Magic bitboard with the knights
                if ((KinghtMoves[KINGPOS] & KnightPositions) != 0)
                    KING IN CHECK

                    example of a KnightMoves long on square 8, x is the square
                    8 * 8 bits, 64 bit int
                    00010000
                    0x000000
                    00010000
                    10100000
                    00000000
                    00000000
                    00000000
                    00000000

                King will look like
                    00000000
                    00011100
                    0001x100
                    00011100
                    00000000
                    00000000
                    00000000
                    00000000

                can implement the exact same with atlist pawns and kings
                and a modifyed version with the sliding pieces
            */




            // check Knights
            for (int i = 0; i < 8; i++)
            {
                if (MovesFromSquare.KnightMoves[kingPos, i] == MovesFromSquare.InvalidMove)
                    continue;
                if (board[kingPos + MovesFromSquare.KnightMoves[kingPos, i]] == (Piece.Knight | board.playerTurn))
                    return true;
            }

            // check King
            for (int i = 0; i < 8; i++)
            {
                if (MovesFromSquare.KingMoves[kingPos, i] == MovesFromSquare.InvalidMove)
                    continue;
                if (board[kingPos + MovesFromSquare.KingMoves[kingPos, i]] == (Piece.King | board.playerTurn))
                    return true;
            }

            // check pawns
            if (kingColor == Board.WhiteMask)
            {
                if (Board.IsPieceInBound(kingPos - 7))
                    if ((kingPos - 7) >> 3 == (kingPos >> 3) - 1)
                        if (board[kingPos - 7] == Piece.BPawn)
                            return true;
                if (Board.IsPieceInBound(kingPos - 9))
                    if ((kingPos - 9) >> 3 == (kingPos >> 3) - 1)
                        if (board[kingPos - 9] == Piece.BPawn)
                            return true;
            }
            else
            {
                if (Board.IsPieceInBound(kingPos + 7))
                    if ((kingPos + 7) >> 3 == (kingPos >> 3) + 1)
                        if (board[kingPos + 7] == Piece.WPawn)
                            return true;
                if (Board.IsPieceInBound(kingPos + 9))
                    if ((kingPos + 9) >> 3 == (kingPos >> 3) + 1)
                        if (board[kingPos + 9] == Piece.WPawn)
                            return true;
            }

            // check Sliding Pieces
            for (int dir = 0; dir < 8; dir++)
            {
                int move = kingPos;
                for (int moveCount = 0; moveCount < 7; moveCount++)
                {
                    if (MovesFromSquare.SlidingpieceMoves[kingPos, dir, moveCount] == MovesFromSquare.InvalidMove)
                        break;
                    move = MovesFromSquare.SlidingpieceMoves[kingPos, dir, moveCount];


                    if (board[move] != 0)
                    {
                        if ((board[move] & Board.ColorMask) == board.playerTurn)
                        {
                            switch (board[move] & Piece.PieceBits)
                            {
                                case Piece.Queen:
                                    return true;
                                case Piece.Rook:
                                    if (dir < 4)
                                        return true;
                                    break;
                                case Piece.Bishop:
                                    if (dir > 3)
                                        return true;
                                    break;
                            }
                        }
                        break;
                    }
                }
            }


            return false;
        }
    }
}