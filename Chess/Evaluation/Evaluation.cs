

namespace MyChess.ChessBoard.Evaluation
{
    class MyEvaluater
    {
        const int PawnValue = 100;
        const int knightValue = 300;
        const int bishopValue = 300;
        const int rookValue = 500;
        const int queenValue = 900;


        public readonly struct PieceValue
        {
            const int Pawn = 100;
            const int Knight = 300;
            const int Bishop = 300;
            const int Rook = 500;
            const int Queen = 900;
            public static readonly int[] Indexed =
            {
                // so we dont need if statements too get he piece type
                // becous just use the piece as an idexer to get the value
                0, // nothing
                0, // nothing
                0, // nothing
                0, // nothing
                0, // nothing
                0, // nothing
                0, // nothing
                0, // nothing
                0, // nothing
                Pawn, // wPawn
                Rook, // wRook
                Knight, // wKnight
                Bishop, // wBishop
                Queen, // wQueen
                0, // wKing
                0, // nothing
                0, // nothing
                Pawn, // bPawn
                Rook, // bRook
                Knight, // bKnight
                Bishop, // bBishop
                Queen, // bQueen
                0, // bKing
            };
        }

        /// <summary>
        /// This returns a positiv number for white winning and a negativ number if black is winning
        /// </summary>
        public static int EvaluateBoard(Board board, List<Move> moves)
        {
            if (moves.Count == 0)
            {
                // if King in check
                if (false)
                    return 0;
                else
                {
                    if (board.playerTurn == 8)
                        return int.MinValue;
                    else
                        return int.MaxValue;
                }
            }

            int whiteEval = CountMaterial(board, Piece.White);
            int blackEval = CountMaterial(board, Piece.Black);

            int evaluation = whiteEval - blackEval;

            return evaluation;
        }

        private static int CountMaterial(Board board, int color)
        {
            int material = 0;
            for (int i = 0; i < board.piecePoses.Count; i++)
            {
                if ((board[board.piecePoses[i]] & Piece.ColorBits) == color)
                    material += PieceValue.Indexed[board[board.piecePoses[i]]];
            }
            return material;
        }

        public static int EvaluateBoardMinMax(Board board)
        { throw new NotImplementedException(""); } // this will evaluate with a min max search instead of just a postion
    }
}

/*
Ideers:

Multiply the piece by some amount depending on where on the board it is so it will gain more value if standing in the middle

Get possible moves and if the moves amount is 0 its either stalemate or chackmate depending on if the king is in check

?
Give pawn extra value if they defend eachother?

Get a list of all the possible moves and give maby like 10? for all the possible moves one side have
And maby give point for if the piece attacks a piece or defends a pikece?

*/