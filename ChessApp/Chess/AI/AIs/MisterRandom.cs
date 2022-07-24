

namespace MyChess.ChessBoard.AIs
{
    public class MisterRandom : ChessAIBase
    {
        public MisterRandom(ChessGame chessGame) : base(chessGame)
        {
        }

        public override Move GetMove()
        {
            if (chessGame.GetPossibleMoves().Count() == 0)
                return new Move(0, 0, 0, chessGame.board.Square[0]);
            return chessGame.GetPossibleMoves()[new Random().Next(0, chessGame.GetPossibleMoves().Count())];
        }

        public override void SetChessGame()
        {
            throw new NotImplementedException();
        }
    }
}