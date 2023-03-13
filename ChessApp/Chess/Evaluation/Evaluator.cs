using MyChess.Chess.Evaluation.EvaluationTechniques;
using MyChess.ChessBoard.Evaluators.Methods;

namespace MyChess.ChessBoard.Evaluators
{
    public class Evaluator
    {
        public ChessGame chessGame;
        public Board board;
        public MaterialCounterV1 matCounter;
        public Evaluator(ChessGame chessGame)
        {
            this.chessGame = chessGame;
            this.board = chessGame.board;
            matCounter = new(board);
        }

        public int EvaluateBoardLight(int moveCount, bool evaluateMatPlacement = false)
        {
            if (moveCount == 0)
            {
                // king in check
                if (chessGame.possibleMoves.IsSquareAttacked(chessGame.possibleMoves.GetKingsPos(board.playerTurn), board.playerTurn ^ Board.ColorMask))
                    return (board.playerTurn == 8) ? int.MinValue : int.MaxValue;
                else
                    return 0;
            }

            // idk if this a good way of gaginng if its late game
            // gives you a range between 0 - 1
            //float lateGameBonusMultiplyer = (float)(32 - board.piecePoses.Count) / 32f;


            // Try and get the ai to move up the pawns, at the cost of pawn structure, try and push pawns
            //chessGame.board.piecePoses





            // Need king safty
            // and piece activity
            int evaluation = matCounter.GetMaterialAdvantage(chessGame, evaluateMatPlacement);
            

            return evaluation;
        }
    }
}