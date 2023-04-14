using MyChess.Chess.Board;

namespace MyChess.ChessBoard.Evaluators;

public class EvaluatorV2
{
    public int EvaluateBoardLight(ChessGame game, int moveCount)
    {
        Board board = game.board;
        
        if (moveCount == 0)
        {
            // king in check
            if (game.possibleMoves.IsSquareAttacked(game.possibleMoves.GetKingsPos(board.playerTurn), board.playerTurn ^ Board.ColorMask))
                return (board.playerTurn == 8) ? int.MinValue + board.gameData.Count : int.MaxValue - board.gameData.Count;
            else
                return 0;
        }

        int eval = 0;



        return eval;
    }
}