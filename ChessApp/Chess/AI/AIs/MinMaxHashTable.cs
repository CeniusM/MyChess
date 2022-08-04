

namespace MyChess.ChessBoard.AIs
{
    public class MinMaxWithHashTable : ChessAIBase
    {
        public MinMaxWithHashTable(ChessGame chessGame) : base(chessGame)
        {
        }

        public override Move GetMove()
        {
            throw new NotImplementedException();
        }

        public override void SetChessGame(ChessGame chessGame)
        {
            this.chessGame = chessGame;
            board = chessGame.board;
            evaluator = new(chessGame);
        }
    }
}