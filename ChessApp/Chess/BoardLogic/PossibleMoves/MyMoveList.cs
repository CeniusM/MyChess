using MyChess.ChessBoard;

namespace MyChess.PossibleMoves
{
    /// <summary>
    /// WARNING: this list only goes up to 80
    /// </summary>
    public class MoveList
    {
        private int Capacity;
        public int Count = 0;
        Move[] moves;
        public MoveList(int Capacity = 40) // needs to be tested for speed
        {
            this.Capacity = Capacity;
            moves = new Move[Capacity];
        }

        public void Add(Move move)
        {
            if (Count == 40)
            {
                Capacity = 80;
                Move[] temp = new Move[Capacity];
                for (int i = 0; i < 40; i++)
                    temp[i] = moves[i];
                moves = temp;
            }

            moves[Count] = move;
            Count++;
        }
    }
}