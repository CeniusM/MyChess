namespace Chess.ChessBoard
{
    class MyFEN
    {
        public static string GetFENBoard(Board board)
        {
            string FENString = string.Empty;

            return FENString;
        }

        public static Board GetBoardFromFEN(string FENString)
        {
            Board board = new Board();
            char[] FEN = FENString.ToCharArray();

            int boardIndexer = 0;
            void SetPiece(int piece)
            {
                board.board[boardIndexer] = piece;
                boardIndexer++;
            }

            int FENIndex = 0;

            for (FENIndex = 0; FENIndex < FEN.Length; FENIndex++)
            {
                if (char.IsNumber(FEN[FENIndex]))
                    boardIndexer += Convert.ToInt32(new string(FEN[FENIndex], 1));
                else if (FEN[FENIndex] == 'P') // pawn
                    SetPiece(Piece.WPawm);
                else if (FEN[FENIndex] == 'p')
                    SetPiece(Piece.BPawm);

                else if (FEN[FENIndex] == 'R') // Rook
                    SetPiece(Piece.WRook);
                else if (FEN[FENIndex] == 'r')
                    SetPiece(Piece.BRook);

                else if (FEN[FENIndex] == 'N') // Knight
                    SetPiece(Piece.WKnight);
                else if (FEN[FENIndex] == 'n')
                    SetPiece(Piece.BKnight);

                else if (FEN[FENIndex] == 'B') // Bishop
                    SetPiece(Piece.WBishop);
                else if (FEN[FENIndex] == 'b')
                    SetPiece(Piece.BBishop);

                else if (FEN[FENIndex] == 'Q') // Queen
                    SetPiece(Piece.WQueen);
                else if (FEN[FENIndex] == 'q')
                    SetPiece(Piece.BQueen);

                else if (FEN[FENIndex] == 'K') // Kin
                    SetPiece(Piece.WKing);
                else if (FEN[FENIndex] == 'k')
                    SetPiece(Piece.BKing);

                else if (FEN[FENIndex] == ' ')
                    break;
            }
            FENIndex++;
            if (FEN[FENIndex] == 'b')
                board.ChangePlayer();
            FENIndex += 2;

            int addedNextTime = 0;
            for (int i = 0; i < 4; i++)
            {
                if (FEN[FENIndex + i] == 'K')
                {
                    board.castle = (board.castle | 0b1000);
                    addedNextTime++;
                }
                else if (FEN[FENIndex + i] == 'Q')
                {
                    board.castle = (board.castle | 0b0100);
                    addedNextTime++;
                }
                else if (FEN[FENIndex + i] == 'k')
                {
                    board.castle = (board.castle | 0b0010);
                    addedNextTime++;
                }
                else if (FEN[FENIndex + i] == 'q')
                {
                    board.castle = (board.castle | 0b0001);
                    addedNextTime++;
                }
                else if (FEN[FENIndex + i] == ' ')
                    break;
            }
            FENIndex += addedNextTime;







            return board;
        }
    }
}