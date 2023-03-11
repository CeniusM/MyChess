

namespace MyChess.ChessBoard.Evaluators.Methods
{
    public class LateGameKingToEadge
    {
        // wanna get king to side early, and later get the oponenet to the each
        public static int GetBonus(ChessGame board, float lateGameMultiplier, int color)
        {
            int moveNum = board.board.moves.Count;

            int kingPos = board.possibleMoves.GetKingsPos(color ^ Piece.ColorBits);
            int bonusMultiplyer = (int)(KingBonus[kingPos] * lateGameMultiplier * 0.5f);
            // MyLib.DebugConsole.WriteLine(bonusMultiplyer + " . " + (float)(32 - Material) / 32 + " . " + Material);

            return bonusMultiplyer;
        }

        readonly static float[] KingBonus =
        {
            150, 150, 150, 150, 150, 150, 150, 150,
            150, 100, 100, 100, 100, 100, 100, 150,
            150, 100, 050, 050, 050, 050, 100, 150,
            150, 100, 050, 000, 000, 050, 100, 150,
            150, 100, 050, 000, 000, 050, 100, 150,
            150, 100, 050, 050, 050, 050, 100, 150,
            150, 100, 100, 100, 100, 100, 100, 150,
            150, 150, 150, 150, 150, 150, 150, 150
        };
    }
}