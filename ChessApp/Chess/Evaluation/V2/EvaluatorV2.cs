﻿using MyChess.Chess.Board;

namespace MyChess.ChessBoard.Evaluators;

public class EvaluatorV2
{
    public static int EvaluateBoard(ChessGame game, int moveCount)
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

        eval += Evaluations.GetMaterial(board, lateGameMultiplier);
        eval += Evaluations.GetPiecePosses(board, lateGameMultiplier);
        eval += Evaluations.GetKingToEdgeLateGame(board, lateGameMultiplier);
        eval += Evaluations.GetKingSafty(board, lateGameMultiplier);
        eval += Evaluations.GetPawnStructure(board, lateGameMultiplier);
        eval += Evaluations.GetSpace(board, lateGameMultiplier);
        eval += Evaluations.GetMobility(board, lateGameMultiplier);


        return eval;
    }
}