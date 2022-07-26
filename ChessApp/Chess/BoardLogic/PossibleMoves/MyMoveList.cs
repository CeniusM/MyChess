using MyChess.ChessBoard;

namespace MyChess.PossibleMoves
{
    /// <summary>
    /// WARNING: this list only goes up to 80
    /// </summary>
    public class MoveList
    {
        private int Capacity = 40; // needs to be tested for speed
        public int Count = 0;
        public Move[] MoveArr;
        public MoveList()
        {
        //     this.Capacity = Capacity;
            MoveArr = new Move[Capacity];
        }

        public void Add(Move move)
        {
            if (Count == 40)
            {
                Capacity = 80;
                Move[] temp = new Move[Capacity];
                for (int i = 0; i < 40; i++)
                    temp[i] = MoveArr[i];
                MoveArr = temp;
            }

            MoveArr[Count] = move;
            Count++;
        }
    }
}