namespace Chess.ChessBoard.Evaluation
{
    class MyEvaluater
    {
        const int pawnValue = 100;
        const int knightValue = 300;
        const int bishopValue = 300;
        const int rookValue = 500;
        const int queenValue = 900;

        private Board _board;

        public MyEvaluater(Board board)
        {
            _board = board;
        }

        /// <summary>
        /// This returns a positiv number for white winning and a negativ number if black is winning
        /// </summary>
        public int Evaluate()
        {
            int whiteEval = CountMaterial(Piece.White);
            int blackEval = CountMaterial(Piece.Black);

            int evaluation = whiteEval - blackEval;

            return evaluation;
        }

        private int CountMaterial(int color)
        {
            int material = 0;
            for (var i = 0; i < 64; i++)
            {
                if (_board.board[i] == Piece.Pawm + color) material += pawnValue;
                else if (_board.board[i] == Piece.Knight + color) material += knightValue;
                else if (_board.board[i] == Piece.Bishop + color) material += bishopValue;
                else if (_board.board[i] == Piece.Rook + color) material += rookValue;
                else if (_board.board[i] == Piece.Queen + color) material += queenValue;
            }
            return material;
        }
    }
}