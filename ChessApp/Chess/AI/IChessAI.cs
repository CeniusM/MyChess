

namespace MyChess.ChessBoard.AIs
{
    public interface IChessAI
    {
        void SetChessGame(ChessGame chessGame);
        Move GetMove();
    }
}