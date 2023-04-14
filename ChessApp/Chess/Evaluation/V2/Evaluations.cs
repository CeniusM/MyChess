
using MyChess.ChessBoard.Evaluators.Methods;

namespace MyChess.ChessBoard.Evaluators;

internal class Evaluations
{
    const int Pawn = 100;
    const int Knight = 300;
    const int Bishop = 300;
    const int Rook = 500;
    const int Queen = 900;
    public static readonly int[] Indexed =
    {   
        // so we dont need if statements too get he piece type
        // becous just use the piece as an idexer to get the value
        0, // NULL
        0, // NULL
        0, // NULL
        0, // NULL
        0, // NULL
        0, // NULL
        0, // NULL
        0, // NULL
        0, // NULL
        Pawn, // wPawn
        Rook, // wRook
        Knight, // wKnight
        Bishop, // wBishop
        Queen, // wQueen
        0, // wKing
        0, // NULL
        0, // NULL
        -Pawn, // bPawn
        -Rook, // bRook
        -Knight, // bKnight
        -Bishop, // bBishop
        -Queen, // bQueen
        0, // bKing
    };

    // This counts towardss pawns hitting other friendlies
    const float PawnAttachted = 5;
    // This counts towardss pawns hitting other enemies
    const float PawnAttackEnemy = 10;

    const int DoublePenelty = -15;
    const int TriplePenelty = -40;
    const int IsolationPenelty = -10;

    public static float GetLateGameMultiplier(Board board)
    {
        float lateGameMultiplier = (float)(32 - board.piecePoses.Count) / 32;

        if (lateGameMultiplier < 0.3f)
            lateGameMultiplier = 0;

        return lateGameMultiplier;
    }

    public static int GetMaterial(Board board)
    {
        int eval = 0;

        for (int i = 0; i < board.piecePoses.Count; i++)
            eval += Indexed[board.Square[board.piecePoses[i]]];

        return eval;
    }

    public static int GetPiecePosses(Board board)
    {
        int eval = 0;

        for (int i = 0; i < board.piecePoses.Count; i++)
        {
            int square = board.piecePoses[i];
            int piece = board.Square[square];
            int color = piece & Piece.ColorBits;
            if (color == Piece.White)
                eval += PiecePosesBonus.PieceBonuses[piece, square];
            else
                eval -= PiecePosesBonus.PieceBonuses[piece, square];
        }

        return eval;
    }

    static readonly int[] PawnPushedLateGameBonuses =
    {
        0, 3, 7, 15, 54, 86
    };

    /// <summary>
    /// Checks double and tripled pawns.
    /// Check for isolated pawns.
    /// Try and push pawns with LateGameMultiplier.
    /// </summary>
    public static int GetPawnStructure(Board board, float LateGameMultiplier)
    {
        int eval = 0;

        int[] WhitePawnCounts = new int[8];
        int[] BlackPawnCounts = new int[8];

        // Tells us how much the pawns for each side have moved forward
        // Gets more important late game
        int WhitePawnPushedBonus = 0;
        int BlackPawnPushedBonus = 0;

        for (int i = 0; i < board.piecePoses.Count; i++)
        {
            int square = board.piecePoses[i];
            int piece = board.Square[square];
            if ((piece & Piece.PieceBits) == Piece.Pawn)
            {
                int collum = square % 8;
                int color = piece & Piece.ColorBits;

                if (color == Piece.White)
                {
                    WhitePawnCounts[collum]++;
                    WhitePawnPushedBonus += PawnPushedLateGameBonuses[6 - (square >> 3)]; // [0, 5]
                }
                else
                {
                    BlackPawnCounts[collum]++;
                    WhitePawnPushedBonus += PawnPushedLateGameBonuses[(square >> 3) - 1];
                }
            }
        }

        eval += (int)(WhitePawnPushedBonus * LateGameMultiplier);
        eval -= (int)(BlackPawnPushedBonus * LateGameMultiplier);

        for (int i = 0; i < 8; i++)
        {
            if (WhitePawnCounts[i] == 2)
                eval += DoublePenelty;
            else if (WhitePawnCounts[i] == 3)
                eval += TriplePenelty;

            if (BlackPawnCounts[i] == 2)
                eval -= DoublePenelty;
            else if (BlackPawnCounts[i] == 3)
                eval -= TriplePenelty;

            // Isolated pawns
            bool NonLeft = true;
            bool NonRight = true;
            if (i != 0)
                if (WhitePawnCounts[i - 1] != 0)
                    NonLeft = false;
            if (i != 7)
                if (WhitePawnCounts[i + 1] != 0)
                    NonRight = false;

            if (NonRight && NonLeft)
                eval += IsolationPenelty * WhitePawnCounts[i];


            NonLeft = true;
            NonRight = true;
            if (i != 0)
                if (BlackPawnCounts[i - 1] != 0)
                    NonLeft = false;
            if (i != 7)
                if (BlackPawnCounts[i + 1] != 0)
                    NonRight = false;

            if (NonRight && NonLeft)
                eval -= IsolationPenelty * BlackPawnCounts[i];
        }

        return eval;
    }

    readonly static float[] KingBonus =
    {
        90, 80, 80, 80, 80, 80, 80, 90,
        80, 60, 60, 50, 50, 60, 60, 80,
        80, 60, 30, 20, 20, 30, 60, 80,
        80, 50, 20, 00, 00, 20, 50, 80,
        80, 50, 20, 00, 00, 20, 50, 80,
        80, 60, 30, 20, 20, 30, 60, 80,
        80, 60, 60, 50, 50, 60, 60, 80,
        90, 80, 80, 80, 80, 80, 80, 90
    };

    public static int GetKingToEdgeLateGame(Board board, float lateGameMultiplier)
    {
        int eval = 0;

        eval += (int)(KingBonus[board.GetKingsPos(Piece.White)] * lateGameMultiplier);
        eval -= (int)(KingBonus[board.GetKingsPos(Piece.Black)] * lateGameMultiplier);

        return eval;
    }

    public static int GetSpace(Board board, float lateGameMultiplier)
    {
        int eval = 0;



        return eval;
    }
}
