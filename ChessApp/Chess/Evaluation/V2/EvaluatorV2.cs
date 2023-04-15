using MyChess.Chess.Board;

namespace MyChess.ChessBoard.Evaluators;

public class EvaluatorV2
{
    public static int EvaluateBoard(ChessGame game, int moveCount, bool UseAlphaBetaPrunningMargine = false, int alpha = 0, int beta = 0, bool maxPlayer = false)
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
        float lateGameMultiplier = Evaluations.GetLateGameMultiplier(board);

        eval += Evaluations.GetMaterial(board, lateGameMultiplier); // Important

        // I have added theese two before because they arent really slow
        eval += Evaluations.GetKingToEdgeLateGame(board, lateGameMultiplier);
        eval += Evaluations.GetKingSafty(board, lateGameMultiplier);

        //if (UseAlphaBetaPrunningMargine)
        //{
        //    // So if eihter side is a Margine outside AlphaBeta it most likly will not have an impact to leave early
        //    const int Margine = 50;

        //    if (maxPlayer)
        //    {
        //        if (beta <= eval - Margine)
        //            return eval;
        //    }
        //    else
        //    {
        //        if (beta + Margine <= alpha)
        //            return eval;
        //    }
        //}

        eval += Evaluations.GetPiecePosses(board, lateGameMultiplier);
        eval += Evaluations.GetPawnStructure(board, lateGameMultiplier);
        eval += Evaluations.GetSpace(board, lateGameMultiplier);
        eval += Evaluations.GetMobility(board, lateGameMultiplier);


        return eval;
    }
}