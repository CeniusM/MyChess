using MyChess.ChessBoard.Evaluators;

namespace MyChess.ChessBoard.AIs
{
    public abstract class ChessAIBase : IChessAI
    {
        public ChessGame chessGame;
        public Board board;
        public Evaluator evaluator;
        public ChessAIBase(ChessGame chessGame)
        {
            this.chessGame = chessGame;
            if (chessGame is not null)
            {
                board = chessGame.board;
                evaluator = new(chessGame);
            }
        }
        public abstract Move GetMove();
        public abstract void SetChessGame(ChessGame chessGame);
    }
}