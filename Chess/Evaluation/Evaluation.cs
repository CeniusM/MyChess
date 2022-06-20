namespace MyChess.ChessBoard.Evaluation
{
    class MyEvaluater
    {
        const int PawnValue = 100;
        const int knightValue = 300;
        const int bishopValue = 300;
        const int rookValue = 500;
        const int queenValue = 900;

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
            for (var i = 0; i < 64; i++)
            {
                if (board[i] == Piece.Pawn + color) material += PawnValue;
                else if (board[i] == Piece.Knight + color) material += knightValue;
                else if (board[i] == Piece.Bishop + color) material += bishopValue;
                else if (board[i] == Piece.Rook + color) material += rookValue;
                else if (board[i] == Piece.Queen + color) material += queenValue;
            }
            return material;
        }

        public static int EvaluateBoardMinMax(Board board)
        {throw new NotImplementedException("");} // this will evaluate with a min max search instead of just a postion
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