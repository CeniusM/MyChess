using ChessV1;

namespace PreInitializeData
{
    public class King
    {
        public const int InvalidMove = int.MinValue;
        static King()
        {
            Init();
        }

        public static readonly int[,] KingAttacks = new int[64, 8]; // square, direction
        public static readonly ulong[] KingAttacksBitBoard = new ulong[64]; // square
        private static void Init()
        {
            // KingAttacks
            void TryMakeMove(int square, int value, int index, int lineDiff)
            {
                if (BoardRepresentation.IsPieceInBound(square + value))
                {
                    if (((square + value) >> 3) - (square >> 3) == lineDiff)
                    {
                        KingAttacks[square, index] = value;
                    }
                    else
                        KingAttacks[square, index] = InvalidMove;
                }
                else
                    KingAttacks[square, index] = InvalidMove;
            }
            for (int i = 0; i < 64; i++)
            {
                TryMakeMove(i, Directions.Value.North, Directions.Index.North, -1);
                TryMakeMove(i, Directions.Value.East, Directions.Index.East, 0);
                TryMakeMove(i, Directions.Value.South, Directions.Index.South, 1);
                TryMakeMove(i, Directions.Value.West, Directions.Index.West, 0);
                TryMakeMove(i, Directions.Value.NorthEast, Directions.Index.NorthEast, -1);
                TryMakeMove(i, Directions.Value.SouthEast, Directions.Index.SouthEast, 1);
                TryMakeMove(i, Directions.Value.SouthWest, Directions.Index.SouthWest, 1);
                TryMakeMove(i, Directions.Value.NorthWest, Directions.Index.NorthWest, -1);
            }

            // KingAttakcsBitBoard
            void MakeBitBoardMove(ref ulong atc, int square, int value, int index, int lineDiff)
            {
                if (BoardRepresentation.IsPieceInBound(square + value))
                {
                    if (((square + value) >> 3) - (square >> 3) == lineDiff)
                    {
                        atc |= ((ulong)1ul << (63 - (square + value)));
                    }
                }
            }


            for (int i = 0; i < 64; i++)
            {
                ulong atc = 0;

                MakeBitBoardMove(ref atc, i, Directions.Value.North, Directions.Index.North, -1);
                MakeBitBoardMove(ref atc, i, Directions.Value.East, Directions.Index.East, 0);
                MakeBitBoardMove(ref atc, i, Directions.Value.South, Directions.Index.South, 1);
                MakeBitBoardMove(ref atc, i, Directions.Value.West, Directions.Index.West, 0);
                MakeBitBoardMove(ref atc, i, Directions.Value.NorthEast, Directions.Index.NorthEast, -1);
                MakeBitBoardMove(ref atc, i, Directions.Value.SouthEast, Directions.Index.SouthEast, 1);
                MakeBitBoardMove(ref atc, i, Directions.Value.SouthWest, Directions.Index.SouthWest, 1);
                MakeBitBoardMove(ref atc, i, Directions.Value.NorthWest, Directions.Index.NorthWest, -1);

                // MyLib.DebugConsole.WriteLine("board at square:" + i + '\n' + BitBoardHelper.GetBitBoardString(atc) + '\n');
            }
        }
    }
}