using MyChess.ChessBoard;

namespace MyChess.Chess.Evaluation.EvaluationTechniques
{

    internal class PawnStructure
    {
        public static bool IsPieceInBound(int pos) => (pos & 0xFFC0) == 0;
        private static int GetRank(int square) => square >> 3;
        private static int GetCollum(int square) => square % 8;

        private static float TryPromotion(ChessBoard.Board board)
        {
            float eval = 0f;
            for (int i = 0; i < 64; i++)
            {
                int piece = board.Square[i];
                if ((piece & Piece.Pawn) == Piece.Pawn)
                {
                    if ((piece & Piece.ColorBits) == Piece.White)
                        eval += ((float)GetRank(i) / 7) * 350;
                    else
                        eval -= ((float)(8 - GetRank(i)) / 7) * 350;
                }
            }
            return eval;
        }

        private static int Structure(ChessBoard.Board board)
        {
            const int DoublePenelty = -25;
            const int TriplePenelty = -70;
            const int IsolationPenelty = -35;

            int[] whitePawns = new int[8];
            int[] blackPawns = new int[8];

            ulong whiteBitboard = 0;
            ulong blackBitboard = 0;

            // Gotta move this into make and unmake move...

            for (int i = 0; i < 64; i++)
            {
                int piece = board.Square[i];
                if (piece == Piece.WPawn)
                {
                    whiteBitboard |= 0b1UL << i;
                    whitePawns[GetCollum(i)]++;
                }
                else if (piece == Piece.BPawn)
                {
                    blackBitboard |= 0b1UL << i;
                    blackPawns[GetCollum(i)]++;
                }
            }

            int eval = 0;

            for (int i = 0; i < 8; i++)
            {
                if (whitePawns[i] == 2)
                    eval += DoublePenelty;
                else if (whitePawns[i] == 3)
                    eval += TriplePenelty;

                if (blackPawns[i] == 2)
                    eval -= DoublePenelty;
                else if (blackPawns[i] == 3)
                    eval -= TriplePenelty;
            }

            ulong[] isolationChecks = { 144680345676153346, 361700864190383365, 723401728380766730, 1446803456761533460, 2893606913523066920, 5787213827046133840, 11574427654092267680, 4629771061636907072 };

            for (int i = 0; i < 64; i++)
            {
                // Check for isolation, and passed pawns
                int piece = board.Square[i];
                if (piece == Piece.WPawn)
                {
                    if ((whiteBitboard & isolationChecks[i % 8]) == 0)
                        eval += IsolationPenelty;
                }
                else if (piece == Piece.BPawn)
                {
                    if ((blackBitboard & isolationChecks[i % 8]) == 0)
                        eval -= IsolationPenelty;
                }

                // Check for passed pawns
            }

            return eval;
        }

        public static int ProtectionAndAttack(ChessBoard.Board board)
        {
            //const int PawnProtection = 20;
            //const int PawnAttackingEnemy = 20;
            const int PassedPawnBonus = 30;
            int eval = 0;

            for (int i = 0; i < 64; i++)
            {
                int square = board.Square[i];
                if ((square & Piece.Pawn) == Piece.Pawn)
                {
                    if ((square & Piece.ColorBits) == Piece.White)
                    {
                        //int piece1 = 0;
                        //int piece2 = 0;
                        //if (IsPieceInBound(i - 7))
                        //piece1 = board.Square[i - 7];
                        //if (IsPieceInBound(i - 9))
                        //piece2 = board.Square[i - 9];

                        if (IsPieceInBound(i - 7))
                            if (board.Square[i - 7] != 0)
                                eval += 10;
                        if (IsPieceInBound(i - 9))
                            if (board.Square[i - 9] != 0)
                                eval += 10;
                    }
                    else
                    {
                        //int piece1 = board.Square[i + 7];
                        //int piece2 = board.Square[i + 9];
                        //if (IsPieceInBound(i + 7))
                        //piece1 = board.Square[i + 7];
                        //if (IsPieceInBound(i + 9))
                        //piece2 = board.Square[i + 9];

                        if (IsPieceInBound(i + 7))
                            if (board.Square[i + 7] != 0)
                                eval -= 10;
                        if (IsPieceInBound(i + 9))
                            if (board.Square[i + 9] != 0)
                                eval -= 10;
                    }
                }
            }

            return eval;
        }

        /// <summary>
        /// Looks at pawn structure, defence and pawn structure attack.
        /// Tries to get the pawn closer to promotion in the late game.
        /// Penelize double and triple pawns
        /// </summary>
        public static int GetEval(ChessBoard.Board board, float LateGameMultiplier)
        {
            //return 0;
            const float PawnLink = 20;
            const float PawnDefendingFellows = 25;
            const float NonPawnAttackedByPawnPenelty = -20;

            float eval = 0;

            if (LateGameMultiplier != 0f)
                eval += TryPromotion(board) * LateGameMultiplier;

            return (int)eval + Structure(board) + ProtectionAndAttack(board);// 
        }
    }
}
