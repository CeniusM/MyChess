namespace Chess.ChessBoard
{
    partial class MyFEN
    {
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

                else if (FEN[FENIndex] == 'K') // King
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

            board.castle = 0;
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
            FENIndex += addedNextTime + 1;

            if (board.enPassantPiece == 64)
            {
                FEN[FENIndex] = '-';
                FENIndex += 2;
            }
            else
            {
                int enPassantPiecePlacement = (8 - FEN[FENIndex + 1]) * 8; // the number
                if (FEN[FENIndex] == 'a')
                    enPassantPiecePlacement += 1;
                else if (FEN[FENIndex] == 'b')
                    enPassantPiecePlacement += 2;
                else if (FEN[FENIndex] == 'c')
                    enPassantPiecePlacement += 3;
                else if (FEN[FENIndex] == 'd')
                    enPassantPiecePlacement += 4;
                else if (FEN[FENIndex] == 'e')
                    enPassantPiecePlacement += 5;
                else if (FEN[FENIndex] == 'f')
                    enPassantPiecePlacement += 6;
                else if (FEN[FENIndex] == 'g')
                    enPassantPiecePlacement += 7;
                else if (FEN[FENIndex] == 'h')
                    enPassantPiecePlacement += 8;

                board.enPassantPiece = enPassantPiecePlacement;

                FENIndex += 3;
            }

            return board;
        }
    }
}