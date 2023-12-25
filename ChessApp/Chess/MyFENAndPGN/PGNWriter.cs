// using System.ComponentModel;
// using MyChess.ChessBoard;


// // https://en.wikipedia.org/wiki/Portable_Game_Notation

// // note:
// // gotta make the GetboardFromFEN faster becous it will be called alot and alot more then GetFenFromBoard

// namespace MyChess.PGN
// {
//     public class PGNWriter
//     {
//         /// dosent work with Optional tags, and only works with postiotions that start from the initial one

//         /// <summary>
//         /// Can be used to convert into a .pgn file and saved / used somewhere
//         /// </summary>
//         public static List<string> GetChessGame(ChessGame chessGame) // and is probely slow becous of all the string manipulations, hopefully MyFEN is way faster
//         {
//             Move[] moves = new Move[chessGame.board.moves.Count];
//             chessGame.board.moves.CopyTo(moves, 0);
//             ChessGame cg = new();

//             List<string> list = new List<string>();

//             list.Add("[Event \"?\"]");
//             list.Add("[Site \"?\"]");
//             list.Add("[Date \"????.??.??\"]");
//             list.Add("[Round \"?\"]");
//             list.Add("[White \"?\"]");
//             list.Add("[Black \"?\"]");
//             list.Add("[Result \"*\"]");
//             list.Add("");
//             list.Add(""); // for all the moves i think


//             for (int i = 0; i < moves.Count(); i++)
//             {
//                 list[list.Count] += GetMove(cg, moves[i]);
//                 cg.MakeMove(moves[i]);
//             }

//             return list;
//         }

//         private static string GetMove(ChessGame cg, Move move)
//         {

//         }

//         private static bool IsPieceWhite(int p) => (p & Piece.ColorBits) == Piece.White;
//         private static bool IsPieceThisPiece(int p, int pType) => (p & Piece.PieceBits) == pType;
//     }
// }