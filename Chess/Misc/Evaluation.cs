namespace MyChess.ChessBoard.Evaluation
{
    class MyEvaluater
    {
        const int pawnValue = 100;
        const int knightValue = 300;
        const int bishopValue = 300;
        const int rookValue = 500;
        const int queenValue = 900;

        /// <summary>
        /// This returns a positiv number for white winning and a negativ number if black is winning
        /// </summary>
        public static int Evaluate(Board board)
        {
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
                if (board[i] == Piece.Pawm + color) material += pawnValue;
                else if (board[i] == Piece.Knight + color) material += knightValue;
                else if (board[i] == Piece.Bishop + color) material += bishopValue;
                else if (board[i] == Piece.Rook + color) material += rookValue;
                else if (board[i] == Piece.Queen + color) material += queenValue;
            }
            return material;
        }
    }
}