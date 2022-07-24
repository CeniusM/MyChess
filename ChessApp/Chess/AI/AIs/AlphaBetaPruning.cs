

namespace MyChess.ChessBoard.AIs
{
    public class AlphaBetaPruning : ChessAIBase
    {
        public const int Depth = 4;
        public AlphaBetaPruning(ChessGame chessGame) : base(chessGame)
        {
        }

        public override Move GetMove()
        {
            return new Move(0,0,0,0);
        }

        public int alphabeta(int depth, int moveCount)
        {
            return 1;
        }

        public override void SetChessGame()
        {
            throw new NotImplementedException();
        }
    }
}