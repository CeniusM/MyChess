
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

    const int DoublePenelty = -20;
    const int TriplePenelty = -50;
    const int IsolationPenelty = -15;

    const int KingCloseDefender = 10;

    public static float GetLateGameMultiplier(Board board)
    {
        float lateGameMultiplier = (float)(32 - board.piecePoses.Count) / 32;

        if (lateGameMultiplier < 0.3f)
            lateGameMultiplier = 0;

        return lateGameMultiplier;
    }

    public static int GetMaterial(Board board, float LateGameMultiplier)
    {
        int eval = 0;

        for (int i = 0; i < board.piecePoses.Count; i++)
            eval += Indexed[board.Square[board.piecePoses[i]]];

        return eval;
    }

    public static int GetPiecePosses(Board board, float LateGameMultiplier)
    {
        int eval = 0;

        for (int i = 0; i < board.piecePoses.Count; i++)
        {
            int square = board.piecePoses[i];
            int piece = board.Square[square];
            int color = piece & Piece.ColorBits;
            if (color == Piece.White)
                eval += PiecePosesBonus.PieceBonuses[piece, square] / 2;
            else
                eval -= PiecePosesBonus.PieceBonuses[piece, square] / 2;
        }
        
        return (int)(eval * (1 - LateGameMultiplier));
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
                    BlackPawnPushedBonus += PawnPushedLateGameBonuses[(square >> 3) - 1];
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

        eval += (int)(KingBonus[board.GetKingsPos(Piece.Black)] * lateGameMultiplier);
        eval -= (int)(KingBonus[board.GetKingsPos(Piece.White)] * lateGameMultiplier);

        // Should also try and get kings closer to each other if the side is winning

        return eval;
    }

    public static int GetKingSafty(Board board, float lateGameMultiplier)
    {
        // give bonus for having our pieces closer to the enemy king

        //int[] KingMovmentBonus = { -15, -5, 0, 5, -10, -20, -25, -30};

        int eval = 0;

        int whiteKingPos = board.GetKingsPos(Piece.White);
        int blackKingPos = board.GetKingsPos(Piece.Black);

        int whiteKingFile = whiteKingPos % 8;
        int blackKingFile = blackKingPos % 8;

        int whiteKingRank = whiteKingPos >> 3;
        int blackKingRank = blackKingPos >> 3;

        // Check for pawns the 6 square in front
        // This only count if the king is close to the back part of the board
        if (whiteKingRank > 5)
        {
            // Infront
            if (Piece.IsColor(board.Square[whiteKingPos - 8], Piece.White))
                eval += KingCloseDefender;
            if (Piece.IsColor(board.Square[whiteKingPos - 16], Piece.White))
                eval += KingCloseDefender >> 1;

            // Rigth
            if (whiteKingFile != 0)
            {
                if (Piece.IsColor(board.Square[whiteKingPos - 8 + 1], Piece.White))
                    eval += KingCloseDefender;
                if (Piece.IsColor(board.Square[whiteKingPos - 16 + 1], Piece.White))
                    eval += KingCloseDefender >> 1;
            }

            // Left
            if (whiteKingFile != 7)
            {
                if (Piece.IsColor(board.Square[whiteKingPos - 8 - 1], Piece.White))
                    eval += KingCloseDefender;
                if (Piece.IsColor(board.Square[whiteKingPos - 16 - 1], Piece.White))
                    eval += KingCloseDefender >> 1;
            }
        }


        if (blackKingRank < 2)
        {
            // Infront
            if (Piece.IsColor(board.Square[blackKingPos + 8], Piece.Black))
                eval -= KingCloseDefender;
            if (Piece.IsColor(board.Square[blackKingPos + 16], Piece.Black))
                eval -= KingCloseDefender >> 1;

            // Right
            if (blackKingFile != 0)
            {
                if (Piece.IsColor(board.Square[blackKingPos + 8 + 1], Piece.Black))
                    eval -= KingCloseDefender;
                if (Piece.IsColor(board.Square[blackKingPos + 16 + 1], Piece.Black))
                    eval -= KingCloseDefender >> 1;
            }

            // Left
            if (blackKingFile != 7)
            {
                if (Piece.IsColor(board.Square[blackKingPos + 8 - 1], Piece.Black))
                    eval -= KingCloseDefender;
                if (Piece.IsColor(board.Square[blackKingPos + 16 - 1], Piece.Black))
                    eval -= KingCloseDefender >> 1;
            }
        }

        // Advanced
        // Check for bishops hitting on the diagonels
        // And check for rooks on the file, and knight close by
        // And the king only need as much defence as there is attacking

        return (int)(eval * (1 - lateGameMultiplier)); // Becomes less important
    }

    readonly static int[] BonusSquaresWhite =
    {
        0,0,0,0,0,0,0,0,0,0,2,2,2,2,0,0,0,2,10,11,11,10,2,0,-3,4,11,12,12,11,4,-3,-3,6,11,13,12,11,7,-3,-5,3,10,11,11,10,3,-5,-3,6,-3,1,1,-3,6,-3,-10,-12,-10,-5,-5,-10,-12,-10
    };

    readonly static int[] BonusSquaresBlack =
    {
        -10,-12,-10,-5,-5,-10,-12,-10
        ,-3,6,-3,1,1,-3,6,-3
        ,-5,3,10,11,11,10,3,-5
        ,-3,6,11,13,12,11,7,-3
        ,-3,4,11,12,12,11,4,-3
        ,0,2,10,11,11,10,2,0
        ,0,0,2,2,2,2,0,0
        ,0,0,0,0,0,0,0,0
    };

    public static int GetSpace(Board board, float lateGameMultiplier)
    {
        int eval = 0;

        // Create a map for good square to control ( in this case be on... )
        // Pawns only count as half for this and king for 0
        for (int i = 2; i < board.piecePoses.Count; i++)
        {
            int pos = board.piecePoses[i];
            int piece = board.Square[pos];
            int color = piece & Piece.ColorBits;
            int type = piece & Piece.PieceBits;
            if (type == Piece.Pawn)
            {
                if (color == Piece.White)
                    eval += BonusSquaresWhite[pos] / 2;
                else
                    eval -= BonusSquaresWhite[pos] / 2;
            }
            else
            {
                if (color == Piece.White)
                    eval += BonusSquaresWhite[pos];
                else
                    eval -= BonusSquaresWhite[pos];
            }
        }

        return eval;
    }

    public static int GetMobility(Board board, float lateGameMultiplier)
    {
        int eval = 0;



        return eval;
    }
}
