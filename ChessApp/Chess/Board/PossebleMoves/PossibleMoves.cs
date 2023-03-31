using MyChess.ChessBoard;
using MyChess.PossibleMoves.Pieces;

// maby put all the piece movement code in here for speed?
// implement and test the loop inside Generate the loop


namespace MyChess.PossibleMoves
{
    public class PossibleMovesGenerator
    {
        enum AttackType
        {
            Sliding, // Rook, Bishop, Queen
            Step, // King, Pawn
            Jump // Knight
        }
        // private struct kingAttacker
        // {
        //     int pieceType;
        //     int pos;
        // }
        // private List<kingAttacker> kingAttackers = new List<kingAttacker>();

        private int ThisKing = 0;
        private int OpponentKing = 0;
        private Board board;
        public List<Move> moves;
        Chess.Board.PossebleMoves.FastIsSquareAttacked checkChecker;
        public PossibleMovesGenerator(Board board)
        {
            moves = new List<Move>();
            this.board = board;
            checkChecker = new(board);
            GenerateMoves();
        }

        public void GenerateMoves(bool removeNonCaptures = false)
        {
            moves = new List<Move>(64); // avg moves for random pos

            // so we dont need to loop over all square everytime we try and fint the right piece
            // for (int i = 0; i < board.piecePoses.Count; i++)
            // {
            //     int piece = board.Square[board.piecePoses[i]] & Piece.PieceBits;

            //     if (piece == Piece.None)
            //         continue;
            //     else if (piece == Piece.Pawn)
            //         {}// stuff
            //     else if (piece == Piece.Bishop)
            //         Bishop.AddMoves(board, moves, board.piecePoses[i]);
            // }

            if (board.playerTurn == 8)
            {
                ThisKing = Piece.WKing;
                OpponentKing = Piece.BKing;
            }
            else
            {
                ThisKing = Piece.BKing;
                OpponentKing = Piece.WKing;
            }

            AddCastleMoves();
            AddPawnMoves();
            AddKnightMoves();
            AddSlidingPieces();
            AddKingMoves();

            KingCheckCheck(removeNonCaptures);
        }

        public int GetKingsPos(int color)
        {
            int piece = (color | Piece.King);
            for (int i = 0; i < board.piecePoses.Count; i++)
                if (board.Square[board.piecePoses[i]] == piece)
                    return board.piecePoses[i];
            return -1;
        }

        private void KingCheckCheck(bool removeNonCaptures)
        {
            List<Move> ValidMoves = new List<Move>(moves.Count);
            checkChecker.Init(board.playerTurn);

            // go through each move
            //int kingPos = GetKingsPos(board.playerTurn);
            int kingPos = board.piecePoses[0];
            if ((board.Square[kingPos] & Piece.ColorBits) != board.playerTurn)
                kingPos = board.piecePoses[1];
            int kingPosTemp = kingPos;
            int kingPiece = Piece.King | board.playerTurn;
            for (int i = 0; i < moves.Count; i++)
            {
                // This does not work with enpassant yet

                Move move = moves[i];

                if (removeNonCaptures)
                    if (board.Square[move.TargetSquare] != 0)
                        continue;

                if (move.MoveFlag == Move.Flag.EnPassantCapture)
                {
                    board.MakeMove(move);
                    if (board.Square[move.TargetSquare] == kingPiece)
                        kingPosTemp = move.TargetSquare;
                    if (!IsSquareAttacked(kingPosTemp, board.playerTurn))
                        ValidMoves.Add(move);
                    kingPosTemp = kingPos;
                    board.UnMakeMove();
                }
                else
                {
                    int movingPiece = board.Square[move.StartSquare];
                    int capturedPiece = board.Square[move.TargetSquare];
                    board.Square[move.StartSquare] = 0;
                    board.Square[move.TargetSquare] = movingPiece;

                    // change the FastCheckCheckers Bitboards
                    checkChecker.poses[movingPiece] ^= (1UL << move.StartSquare) | (1UL << move.TargetSquare);
                    checkChecker.poses[capturedPiece] ^= 1UL << move.TargetSquare;

                    if (board.Square[move.TargetSquare] == kingPiece)
                        kingPosTemp = move.TargetSquare;
                    if (!checkChecker.IsSquareAttacked(kingPosTemp))
                        ValidMoves.Add(move);
                    kingPosTemp = kingPos;

                    checkChecker.poses[movingPiece] ^= (1UL << move.StartSquare) | (1UL << move.TargetSquare);
                    checkChecker.poses[capturedPiece] ^= 1UL << move.TargetSquare;

                    board.Square[move.StartSquare] = movingPiece;
                    board.Square[move.TargetSquare] = capturedPiece;
                }

            }

            moves = ValidMoves;
        }

        public bool IsSquareAttacked(int square, int opponentColor)
        {
            // check Knights
            for (int i = 0; i < 8; i++)
            {
                if (MovesFromSquare.KnightMoves[square, i] == MovesFromSquare.InvalidMove)
                    continue;
                if (board.Square[square + MovesFromSquare.KnightMoves[square, i]] == (Piece.Knight | opponentColor))
                    return true;
            }

            // check King
            for (int i = 0; i < 8; i++)
            {
                if (MovesFromSquare.KingMoves[square, i] == MovesFromSquare.InvalidMove)
                    continue;
                if (board.Square[square + MovesFromSquare.KingMoves[square, i]] == (Piece.King | opponentColor))
                    return true;
            }

            // check pawns
            if (opponentColor == Board.BlackMask)
            {
                if (Board.IsPieceInBound(square - 7))
                    if ((square - 7) >> 3 == (square >> 3) - 1)
                        if (board.Square[square - 7] == Piece.BPawn)
                            return true;
                if (Board.IsPieceInBound(square - 9))
                    if ((square - 9) >> 3 == (square >> 3) - 1)
                        if (board.Square[square - 9] == Piece.BPawn)
                            return true;
            }
            else
            {
                if (Board.IsPieceInBound(square + 7))
                    if ((square + 7) >> 3 == (square >> 3) + 1)
                        if (board.Square[square + 7] == Piece.WPawn)
                            return true;
                if (Board.IsPieceInBound(square + 9))
                    if ((square + 9) >> 3 == (square >> 3) + 1)
                        if (board.Square[square + 9] == Piece.WPawn)
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


                    if (board.Square[move] != 0)
                    {
                        if ((board.Square[move] & Board.ColorMask) == opponentColor)
                        {
                            switch (board.Square[move] & Piece.PieceBits)
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

        public bool IsSquareAttackedBySlidingPieces(int square, int opponentColor)
        {
            // check Sliding Pieces
            for (int dir = 0; dir < 8; dir++)
            {
                int move = square;
                for (int moveCount = 0; moveCount < 7; moveCount++)
                {
                    if (MovesFromSquare.SlidingpieceMoves[square, dir, moveCount] == MovesFromSquare.InvalidMove)
                        break;
                    move = MovesFromSquare.SlidingpieceMoves[square, dir, moveCount];


                    if (board.Square[move] != 0)
                    {
                        if ((board.Square[move] & Board.ColorMask) == opponentColor)
                        {
                            switch (board.Square[move] & Piece.PieceBits)
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

        public bool IsSquareAttackedByKnights(int square, int opponentColor)
        {
            // check Knights
            for (int i = 0; i < 8; i++)
            {
                if (MovesFromSquare.KnightMoves[square, i] == MovesFromSquare.InvalidMove)
                    continue;
                if (board.Square[square + MovesFromSquare.KnightMoves[square, i]] == (Piece.Knight | opponentColor))
                    return true;
            }

            // check King
            for (int i = 0; i < 8; i++)
            {
                if (MovesFromSquare.KingMoves[square, i] == MovesFromSquare.InvalidMove)
                    continue;
                if (board.Square[square + MovesFromSquare.KingMoves[square, i]] == (Piece.King | opponentColor))
                    return true;
            }

            return false;
        }
        public bool IsSquareAttackedByKing(int square, int opponentColor)
        {
            // check King
            for (int i = 0; i < 8; i++)
            {
                if (MovesFromSquare.KingMoves[square, i] == MovesFromSquare.InvalidMove)
                    continue;
                if (board.Square[square + MovesFromSquare.KingMoves[square, i]] == (Piece.King | opponentColor))
                    return true;
            }

            return false;
        }

        public void AddKingMoves()
        {
            for (int i = 0; i < board.piecePoses.Count; i++)
            {
                if (board.Square[board.piecePoses[i]] == ThisKing)
                {
                    int kingPos = board.piecePoses[i];
                    for (int j = 0; j < 8; j++)
                    {
                        int kingMove = kingPos + MovesFromSquare.KingMoves[kingPos, j];
                        if (MovesFromSquare.KingMoves[kingPos, j] == MovesFromSquare.InvalidMove)
                            continue;
                        else if ((board.Square[kingMove] & Piece.ColorBits) != board.playerTurn)
                        {
                            moves.Add(new(kingPos, kingMove, 0, board.Square[kingMove]));
                        }
                    }
                }
            }
        }

        public void AddKnightMoves()
        {
            int thisKnight = (Piece.Knight | board.playerTurn);
            for (int i = 0; i < board.piecePoses.Count; i++)
            {
                if (board.Square[board.piecePoses[i]] == thisKnight)
                {
                    int knightPos = board.piecePoses[i];
                    for (int j = 0; j < 8; j++)
                    {
                        int knightMove = knightPos + MovesFromSquare.KnightMoves[knightPos, j];
                        if (MovesFromSquare.KnightMoves[knightPos, j] == MovesFromSquare.InvalidMove)
                            continue;
                        else if ((board.Square[knightMove] & Piece.ColorBits) != board.playerTurn)
                        {
                            moves.Add(new(knightPos, knightMove, 0, board.Square[knightMove]));
                        }
                    }
                }
            }
        }

        public void AddSlidingPieces()
        {
            int ThisQueen = Piece.Queen | board.playerTurn;
            int ThisRook = Piece.Rook | board.playerTurn;
            int ThisBishop = Piece.Bishop | board.playerTurn;

            int fromDir = 0;
            int toDir = 0;
            for (int i = 0; i < board.piecePoses.Count; i++)
            {
                int piece = board.Square[board.piecePoses[i]];
                if (piece == 0)
                    continue;
                else if (piece == ThisQueen)
                {
                    fromDir = 0;
                    toDir = 8;
                }
                else if (piece == ThisRook)
                {
                    fromDir = 0;
                    toDir = 4;
                }
                else if (piece == ThisBishop)
                {
                    fromDir = 4;
                    toDir = 8;
                }
                else
                    continue;

                int pos = board.piecePoses[i];
                for (int dir = fromDir; dir < toDir; dir++)
                {
                    int move = pos;
                    for (int moveCount = 0; moveCount < 7; moveCount++)
                    {
                        if (MovesFromSquare.SlidingpieceMoves[pos, dir, moveCount] == MovesFromSquare.InvalidMove)
                            break;
                        move = MovesFromSquare.SlidingpieceMoves[pos, dir, moveCount];

                        if (board.Square[move] == 0)
                        {
                            moves.Add(new(pos, move, 0, board.Square[move]));
                        }
                        else if ((board.Square[move] & Piece.ColorBits) != board.playerTurn)
                        {
                            moves.Add(new(pos, move, 0, board.Square[move]));
                            break;
                        }
                        else
                            break;
                    }
                }
            }
        }

        public void AddPawnMoves() // what a disaster
        {
            bool IsOponent(int p1, int p2) => ((p1 & Piece.ColorBits) != (p2 & Piece.ColorBits) && (p2 & Piece.PieceBits) != 0);
            for (int i = 0; i < board.piecePoses.Count; i++)
            {
                int pos = board.piecePoses[i];
                if (board.Square[pos] == (Piece.Pawn | board.playerTurn))
                {
                    if (board.playerTurn == Board.WhiteMask)
                    {
                        // two forward
                        if (56 > pos && pos > 47)
                            if (board.Square[pos - 16] == 0)
                                if (board.Square[pos - 8] == 0)
                                    moves.Add(new(pos, pos - 16, Move.Flag.PawnTwoForward));



                        if (Board.IsPieceInBound(pos - 7)) // left
                        {
                            int inBoundPos = pos - 7;
                            if ((inBoundPos) >> 3 == (pos >> 3) - 1)
                            {
                                if (inBoundPos == board.enPassantPiece)
                                    moves.Add(new(pos, inBoundPos, Move.Flag.EnPassantCapture, Piece.BPawn));

                                if (IsOponent(board.Square[pos], board.Square[inBoundPos]))
                                {
                                    if (inBoundPos > -1 && inBoundPos < 8) // check if its on promotion
                                    {
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToQueen, board.Square[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToKnight, board.Square[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToRook, board.Square[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToBishop, board.Square[inBoundPos]));
                                    }
                                    else if ((board.Square[inBoundPos] & Piece.PieceBits) != 0)
                                        moves.Add(new(pos, inBoundPos, 0, board.Square[inBoundPos]));
                                }
                            }
                        }
                        if (Board.IsPieceInBound(pos - 8)) // foward
                        {
                            int inBoundPos = pos - 8;
                            if (board.Square[inBoundPos] == 0)
                            {
                                if (inBoundPos > -1 && inBoundPos < 8) // check if its on promotion
                                {
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToQueen));
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToKnight));
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToRook));
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToBishop));
                                }
                                else
                                    moves.Add(new(pos, inBoundPos, 0));
                            }
                        }
                        if (Board.IsPieceInBound(pos - 9)) // right
                        {
                            int inBoundPos = pos - 9;
                            if ((inBoundPos) >> 3 == (pos >> 3) - 1)
                            {
                                if (inBoundPos == board.enPassantPiece)
                                    moves.Add(new(pos, inBoundPos, Move.Flag.EnPassantCapture, Piece.BPawn));

                                if (IsOponent(board.Square[pos], board.Square[inBoundPos]))
                                {
                                    if (inBoundPos > -1 && inBoundPos < 8) // check if its on promotion
                                    {
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToQueen, board.Square[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToKnight, board.Square[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToRook, board.Square[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToBishop, board.Square[inBoundPos]));
                                    }
                                    else if ((board.Square[inBoundPos] & Piece.PieceBits) != 0)
                                        moves.Add(new(pos, inBoundPos, 0, board.Square[inBoundPos]));
                                }
                            }
                        }
                    }
                    else
                    {
                        // two forward
                        if (16 > pos && pos > 7)
                            if (board.Square[pos + 16] == 0)
                                if (board.Square[pos + 8] == 0)
                                    moves.Add(new(pos, pos + 16, Move.Flag.PawnTwoForward));



                        if (Board.IsPieceInBound(pos + 7)) // left
                        {
                            int inBoundPos = pos + 7;
                            if ((inBoundPos) >> 3 == (pos >> 3) + 1)
                            {
                                if (inBoundPos == board.enPassantPiece)
                                    moves.Add(new(pos, inBoundPos, Move.Flag.EnPassantCapture, Piece.WPawn));

                                if (IsOponent(board.Square[pos], board.Square[inBoundPos]))
                                {
                                    if (inBoundPos > 55 && inBoundPos < 64) // check if its on promotion
                                    {
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToQueen, board.Square[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToKnight, board.Square[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToRook, board.Square[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToBishop, board.Square[inBoundPos]));
                                    }
                                    else if ((board.Square[inBoundPos] & Piece.PieceBits) != 0)
                                        moves.Add(new(pos, inBoundPos, 0, board.Square[inBoundPos]));
                                }
                            }
                        }
                        if (Board.IsPieceInBound(pos + 8)) // foward
                        {
                            int inBoundPos = pos + 8;
                            if (board.Square[inBoundPos] == 0)
                            {
                                if (inBoundPos > 55 && inBoundPos < 64) // check if its on promotion
                                {
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToQueen));
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToKnight));
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToRook));
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToBishop));
                                }
                                else
                                    moves.Add(new(pos, inBoundPos, 0, 0));
                            }
                        }
                        if (Board.IsPieceInBound(pos + 9)) // right
                        {
                            int inBoundPos = pos + 9;
                            if ((inBoundPos) >> 3 == (pos >> 3) + 1)
                            {
                                if (inBoundPos == board.enPassantPiece)
                                    moves.Add(new(pos, inBoundPos, Move.Flag.EnPassantCapture, Piece.WPawn));

                                if (IsOponent(board.Square[pos], board.Square[inBoundPos]))
                                {
                                    if (inBoundPos > 55 && inBoundPos < 64) // check if its on promotion
                                    {
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToQueen, board.Square[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToKnight, board.Square[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToRook, board.Square[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToBishop, board.Square[inBoundPos]));
                                    }
                                    else if ((board.Square[inBoundPos] & Piece.PieceBits) != 0)
                                        moves.Add(new(pos, inBoundPos, 0, board.Square[inBoundPos]));
                                }
                            }
                        }
                    }
                }
            }
        } // AddPawnMoves()

        public void AddCastleMoves() // still uses the IsSquareAttacked
        {
            int castle = board.castle;

            if (castle == 0)
                return;

            if (board.playerTurn == 8)
            {
                // only check the first and second square, the third will be checked later
                if ((castle & CASTLE.W_King_Side) == CASTLE.W_King_Side)
                {
                    if (board.Square[61] == 0)
                    {
                        if (board.Square[62] == 0)
                        {
                            if (!IsSquareAttacked(60, 16))
                            {
                                if (!IsSquareAttacked(61, 16))
                                {
                                    moves.Add(new(60, 62, Move.Flag.Castling)); // check at 62 will be checked later
                                }
                            }
                        }
                    }
                }
                if ((castle & CASTLE.W_Queen_Side) == CASTLE.W_Queen_Side)
                {
                    if (board.Square[59] == 0)
                    {
                        if (board.Square[58] == 0)
                        {
                            if (board.Square[57] == 0)
                            {
                                if (!IsSquareAttacked(60, 16))
                                {
                                    if (!IsSquareAttacked(59, 16))
                                    {
                                        moves.Add(new(60, 58, Move.Flag.Castling)); // check at 62 will be checked later
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if ((castle & CASTLE.B_King_Side) == CASTLE.B_King_Side)
                {
                    if (board.Square[5] == 0)
                    {
                        if (board.Square[6] == 0)
                        {
                            if (!IsSquareAttacked(4, 8))
                            {
                                if (!IsSquareAttacked(5, 8))
                                {
                                    moves.Add(new(4, 6, Move.Flag.Castling)); // check at 62 will be checked later
                                }
                            }
                        }
                    }
                }
                if ((castle & CASTLE.B_Queen_Side) == CASTLE.B_Queen_Side)
                {
                    if (board.Square[1] == 0)
                    {
                        if (board.Square[2] == 0)
                        {
                            if (board.Square[3] == 0)
                            {
                                if (!IsSquareAttacked(4, 8))
                                {
                                    if (!IsSquareAttacked(3, 8))
                                    {
                                        moves.Add(new(4, 2, Move.Flag.Castling)); // check at 62 will be checked later
                                    }
                                }
                            }
                        }
                    }
                }
            }
        } // AddCaslteMoves
    }
}



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