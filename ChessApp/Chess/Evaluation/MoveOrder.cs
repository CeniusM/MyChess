using MyChess.ChessBoard;
using MyChess.ChessBoard.Evaluators.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChess.Chess.Evaluation
{
    internal class MoveOrder
    {
        private static bool IsPieceInBound(int pos) => (pos & 0xFFC0) == 0;

        public static void OrderMoves(ChessBoard.Board board, Move[] moves)
        {
            // could not make it work
            //throw new NotImplementedException();

            int length = moves.Length;
            int[] values = new int[length];
            int playerTurn = board.playerTurn;

            for (int i = 0; i < length; i++)
            {
                //  Rate moves
                Move move = moves[i];
                int value = 0;

                int movingPiece = board.Square[move.StartSquare];
                //int movingPieceValue = MaterialCounterBase.PieceValue.Indexed[movingPiece];

                // Check if we walk into pawns
                bool walkingIntoPawn = false;
                if (playerTurn == 8)
                {
                    if (IsPieceInBound(move.TargetSquare - 7))
                        if (board.Square[move.TargetSquare - 7] == Piece.BPawn)
                            walkingIntoPawn = true;
                    if (IsPieceInBound(move.TargetSquare - 9))
                        if (board.Square[move.TargetSquare - 9] == Piece.BPawn)
                            walkingIntoPawn = true;
                }
                else
                {
                    if (IsPieceInBound(move.TargetSquare + 7))
                        if (board.Square[move.TargetSquare + 7] == Piece.WPawn)
                            walkingIntoPawn = true;
                    if (IsPieceInBound(move.TargetSquare + 9))
                        if (board.Square[move.TargetSquare + 9] == Piece.WPawn)
                            walkingIntoPawn = true;
                }

                if (walkingIntoPawn)
                {
                    value -= Math.Min(100 - MaterialCounterBase.PieceValue.Indexed[movingPiece] >> 2, 0);
                }

                if ((move.MoveFlag & 0b100) == 0b100) // Promotions
                {
                    value += MaterialCounterBase.PieceValue.Indexed[move.PromotionPiece() | playerTurn];
                }
                else if (move.MoveFlag == Move.Flag.EnPassantCapture)
                {
                    value += 80;
                }

                if (move.CapturedPiece != 0)
                {
                    value += MaterialCounterBase.PieceValue.Indexed[move.CapturedPiece];
                }

                values[i] = -value;
            }

            Array.Sort(values, moves);
        }
    }
}
