using ChessV1;

namespace PreInitializeDataV2
{
    public class King
    {
        public const int InvalidMove = int.MinValue;
        static King()
        {
            Init();
        }


        // maby at some point make it so the kingattacks dosent store the values in the right directions
        // but just add on the valid moves so when you hit an invalidmove you can break for that square

        public static readonly int[,] KingAttacks = new int[64, 8]; // square, direction

        // stores the square that can be attacked instead of the value of the move
        public static readonly int[,] KingAttacksV2 = new int[64, 8]; // square, direction 
        public static readonly int[] KingAttacksV2D1 = new int[64 * 8];


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

            // KingAttacksV2
            void TryMakeMoveV2(int square, int value, int index, int lineDiff)
            {
                if (BoardRepresentation.IsPieceInBound(square + value))
                {
                    if (((square + value) >> 3) - (square >> 3) == lineDiff)
                    {
                        KingAttacksV2[square, index] = square + value;
                    }
                    else
                        KingAttacksV2[square, index] = InvalidMove;
                }
                else
                    KingAttacksV2[square, index] = InvalidMove;
            }
            for (int i = 0; i < 64; i++)
            {
                TryMakeMoveV2(i, Directions.Value.North, Directions.Index.North, -1);
                TryMakeMoveV2(i, Directions.Value.East, Directions.Index.East, 0);
                TryMakeMoveV2(i, Directions.Value.South, Directions.Index.South, 1);
                TryMakeMoveV2(i, Directions.Value.West, Directions.Index.West, 0);
                TryMakeMoveV2(i, Directions.Value.NorthEast, Directions.Index.NorthEast, -1);
                TryMakeMoveV2(i, Directions.Value.SouthEast, Directions.Index.SouthEast, 1);
                TryMakeMoveV2(i, Directions.Value.SouthWest, Directions.Index.SouthWest, 1);
                TryMakeMoveV2(i, Directions.Value.NorthWest, Directions.Index.NorthWest, -1);
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

                KingAttacksBitBoard[i] = atc;
                // MyLib.DebugConsole.WriteLine("board at square:" + i + '\n' + BitBoardHelper.GetBitBoardString(atc) + '\n');
            }

            for (int square = 0; square < 64; square++)
            {
                for (int dir = 0; dir < 8; dir++)
                {
                    KingAttacksV2D1[square + (dir * 64)] = KingAttacksV2[square, dir];
                }
            }
        }
    }
}