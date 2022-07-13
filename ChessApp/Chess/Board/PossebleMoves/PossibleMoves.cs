using MyChess.ChessBoard;
using MyChess.PossibleMoves.Pieces;

// maby put all the piece movement code in here for speed?
// implement and test the loop inside Generate the loop


namespace MyChess.PossibleMoves
{
    public class PossibleMovesGenerator
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
            //moves.AddRange(King.GetPossibleMoves(board));
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
                if (!IsSquareAttacked(GetKingsPos(board.playerTurn ^ Board.ColorMask), board.playerTurn ^ Board.ColorMask))
                    ValidMoves.Add(moves[i]);

                // if (moves[i].MoveFlag != 0) // for debuging
                // {

                // }

                board.UnMakeMove();
            }

            moves = ValidMoves;
        }

        public bool IsSquareAttacked(int square, int opponentColor)
        {
            // test, and shouldent acktullay be needed
            if (square == -1)
                return true;








            /*
                for v2 note, also add in castle
                here you can use magic bitboard, you can just AND operate a long that has bits on all the locations that the knight
                can jump to from the square is on
                so fx

                    Long                   Magic bitboard with the knights
                if ((KinghtMoves[square] & KnightPositions) != 0)
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
                if (MovesFromSquare.KnightMoves[square, i] == MovesFromSquare.InvalidMove)
                    continue;
                if (board[square + MovesFromSquare.KnightMoves[square, i]] == (Piece.Knight | board.playerTurn))
                    return true;
            }

            // check King
            for (int i = 0; i < 8; i++)
            {
                if (MovesFromSquare.KingMoves[square, i] == MovesFromSquare.InvalidMove)
                    continue;
                if (board[square + MovesFromSquare.KingMoves[square, i]] == (Piece.King | board.playerTurn))
                    return true;
            }

            // check pawns
            if (opponentColor == Board.BlackMask)
            {
                if (Board.IsPieceInBound(square - 7))
                    if ((square - 7) >> 3 == (square >> 3) - 1)
                        if (board[square - 7] == Piece.BPawn)
                            return true;
                if (Board.IsPieceInBound(square - 9))
                    if ((square - 9) >> 3 == (square >> 3) - 1)
                        if (board[square - 9] == Piece.BPawn)
                            return true;
            }
            else
            {
                if (Board.IsPieceInBound(square + 7))
                    if ((square + 7) >> 3 == (square >> 3) + 1)
                        if (board[square + 7] == Piece.WPawn)
                            return true;
                if (Board.IsPieceInBound(square + 9))
                    if ((square + 9) >> 3 == (square >> 3) + 1)
                        if (board[square + 9] == Piece.WPawn)
                            return true;
            }

            // check Sliding Pieces
            for (int dir = 0; dir < 8; dir++)
            {
                int move = square;
                for (int moveCount = 0; moveCount < 7; moveCount++)
                {
                    if (MovesFromSquare.SlidingpieceMoves[square, dir, moveCount] == MovesFromSquare.InvalidMove)
                        break;
                    move = MovesFromSquare.SlidingpieceMoves[square, dir, moveCount];


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