

namespace MyChess.ChessBoard.AIs
{
    public interface IChessAI
    {
        void SetChessGame();
        Move GetMove();
    }
}