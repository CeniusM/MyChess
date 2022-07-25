// using System.ComponentModel;
// using MyChess.ChessBoard;


// // https://en.wikipedia.org/wiki/Portable_Game_Notation

// // note:
// // gotta make the GetboardFromFEN faster becous it will be called alot and alot more then GetFenFromBoard

// namespace MyChess.PGN
// {
//     public class PGNReader
//     {
//         public static readonly string StartPostion = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
//         private enum PGNState
//         {
//             None,
//             Event,
//             Site,
//             Date,
//             Round,
//             White,
//             Black,
//             Result,
//             //optinals, or rather if its setup
//             Setup,
//             FEN,
//             ReadingMoves // last
//         }
//         /// dosent work with Optional tags, and only works with postiotions that start from the initial one
//         public static ChessGame GetChessGame(List<string>) // and is probely slow becous of all the string manipulations, hopefully MyFEN is way faster
//         {
//             try
//             {
//                 PGNState state = PGNState.None;
//                 ChessGame cg = new ChessGame();

//                 // for (int i = 0; i < PGN.Length; i++)
//                 // {
//                 //     char c = PGN[i];
//                 //     if (c == '\n' && PGN[i + 1] == '\n')
//                 //         state = PGNState.ReadingMoves;


//                 // }







//                 return cg;
//             }
//             catch
//             {
//                 return new ChessGame();
//             }
//         }

//         private static Move GetMove(ChessGame cg, string move)
//         {
//             throw new NotImplementedException();
//         }

//         private static bool IsPieceWhite(int p) => (p & Piece.ColorBits) == Piece.White;
//         private static bool IsPieceThisPiece(int p, int pType) => (p & Piece.PieceBits) == pType;
//     }
// }