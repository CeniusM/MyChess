

// namespace ChessV1
// {
//     public class PossibleMovesGenerator
//     {
//          Some how use the bitboard cauculations to find all the pieces that pin the king by the enemy
//          so check if king is in there total bitboard, after find the direction, then go all the way and see if a single white piece is in the way
//
//
//
//
//
//
//
//
//         UnsafeBoard board;
//         List<Move> moves;
//         int thisKing;
//         int oppisitKing;

//         // king check
//         bool kingHit;
//         bool kingHitBySlider;
//         int kingHitDirectoion;
//         bool doubleCheck; // set this to true if kingHit != 0

//         // list of the pieces that is hit by sliding piece and could be pinned to the king
//         // later loop over these and see if any of them acually cant move
//         struct HitPiece
//         {
//             int piecePos;
//             int directionFrom;
//         }
//         List<HitPiece> pieces;
//         List<int> pinnedPieces;


//         ulong bitboardOpponentAttacks;

//         public PossibleMovesGenerator(UnsafeBoard boardRef)
//         {
//             board = boardRef;
//         }

//         // only works if the Generation have acured
//         public bool KingInCheck() => kingHit;

//         public void Init()
//         {

//         }

//         public void GenerateMoves()
//         {
//             Init();
//         }

//         public void KingMoves()
//         {

//         }
//     }
// }