// namespace MyChess.ChessBoard
// {
//     class PiecePoses
//     {
//         /*
//         PiecePoses,
//         less to loop through

//         0 = white King
//         1 = black King
//         2 - 5 bishop
//         6 - 9 knights
//         10 - 13 rooks
//         14 - 29 Pawns
//         30 & 31 queens
//         */
//         private int[] piecePoses = new int[32];
//         private int[] square = new int[64];

//         //
//         private int[] map = new int[64];

//         public PiecePoses(Board board)
//         {
//             InitBoard(board);
//         }

//         public int this[int key]
//         {
//             get => piecePoses[key];
//             set => throw new NotImplementedException();
//         }

//         public void MakeMove(Move move)
//         {

//         }

//         public void InitBoard(Board board)
//         {
//             // init board
//             for (int i = 0; i < 32; i++)
//             {
//                 piecePoses[i] = -1;
//             }
//             square = board.Square;
//             map = new int[64];

//             // init posestions
//             void SetPawnToPieceSquare(int square)
//             {
//                 for (int i = 14; i < 30; i++)
//                 {
//                     if (piecePoses[i] == -1)
//                     {
//                         piecePoses[i] = square;
//                         return;
//                     }
//                 }
//             }

//             void SetOficer(int firstPos, int secondPos, int square)
//             {
//                 if (piecePoses[firstPos] == -1)
//                     piecePoses[firstPos] = square;
//                 else if (piecePoses[secondPos] == -1)
//                     piecePoses[secondPos] = square;
//                 else // if 2 places has been ocupied
//                     SetPawnToPieceSquare(square);
//             }

//             for (int i = 0; i < 64; i++)
//             {
//                 int piece = square[i];
//                 if (piece == Piece.WKing)
//                 {
//                     piecePoses[0] = i;
//                     continue;
//                 }
//                 else if (piece == Piece.BKing)
//                 {
//                     piecePoses[1] = i;
//                     continue;
//                 }
//                 else if (piece == Piece.WBishop)
//                 {
//                     SetOficer(2, 3, i);
//                 }
//                 else if (piece == Piece.BBishop)
//                 {
//                     SetOficer(4, 5, i);
//                 }
//                 else if (piece == Piece.WKnight)
//                 {
//                     SetOficer(6, 7, i);
//                 }
//                 else if (piece == Piece.BKnight)
//                 {
//                     SetOficer(8, 9, i);
//                 }
//                 else if (piece == Piece.WRook)
//                 {
//                     SetOficer(10, 11, i);
//                 }
//                 else if (piece == Piece.BRook)
//                 {
//                     SetOficer(12, 13, i);
//                 }
//                 else if (piece == Piece.WQueen)
//                 {
//                     if (piecePoses[30] == -1)
//                         piecePoses[30] = i;
//                     else // if 2 places has been ocupied
//                         SetPawnToPieceSquare(i);
//                 }
//                 else if (piece == Piece.BQueen)
//                 {
//                     if (piecePoses[31] == -1)
//                         piecePoses[31] = i;
//                     else // if 2 places has been ocupied
//                         SetPawnToPieceSquare(i);
//                 }
//                 else if ((piece & Piece.ColorBits) == Piece.Pawn)
//                 {
//                     for (int j = 14; j < 30; j++)
//                     {
//                         if (piecePoses[j] == -1)
//                         {
//                             piecePoses[j] = i;
//                             break;
//                         }
//                     }
//                 }
//             }
//         }
//     }
// }