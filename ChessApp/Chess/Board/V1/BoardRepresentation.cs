﻿namespace ChessV1
{

    public static class BoardRepresentation
    {
        public const string fileNames = "abcdefgh";
        public const string rankNames = "12345678";

        // Rank (0 to 7) of square 
        public static int RankIndex(int squareIndex)
        {
            return 7 - (squareIndex >> 3);
        }

        // File (0 to 7) of square 
        public static int FileIndex(int squareIndex)
        {
            return (squareIndex % 8);
        }

        public static string SquareNameFromCoordinate(int fileIndex, int rankIndex)
        {
            return fileNames[fileIndex] + "" + (rankIndex + 1);
        }

        /// <summary> works up too 65535 and down too -65472 </summary>
        public static bool IsPieceInBound(int pos) => (pos & 0xFFC0) == 0;
    }
}