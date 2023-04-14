
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

        return 0;
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

        return 0;
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



        return 0;
    }
}
