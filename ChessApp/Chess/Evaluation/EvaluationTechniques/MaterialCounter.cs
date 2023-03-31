

using Microsoft.VisualBasic.Devices;
using MyChess.Chess.Evaluation.EvaluationTechniques;
using MyChess.PossibleMoves;

namespace MyChess.ChessBoard.Evaluators.Methods
{
    public abstract class MaterialCounterBase
    {
        public readonly struct PieceValue
        {
            const int Pawn = 100;
            const int Knight = 300;
            const int Bishop = 300;
            const int Rook = 500;
            const int Queen = 900;
            public static readonly int[] Indexed =
            {   
                // so we dont need if statements too get he piece type
                // becous just use the piece as an idexer to get the value
                0, // NULL
                0, // NULL
                0, // NULL
                0, // NULL
                0, // NULL
                0, // NULL
                0, // NULL
                0, // NULL
                0, // NULL
                Pawn, // wPawn
                Rook, // wRook
                Knight, // wKnight
                Bishop, // wBishop
                Queen, // wQueen
                0, // wKing
                0, // NULL
                0, // NULL
                Pawn, // bPawn
                Rook, // bRook
                Knight, // bKnight
                Bishop, // bBishop
                Queen, // bQueen
                0, // bKing
            };
        }
        public Board board;
        public MaterialCounterBase(Board board)
        {
            this.board = board;
        }

        public abstract int GetMaterialAdvantage(ChessGame chessGame);
    }


    public class MaterialCounterV1 : MaterialCounterBase
    {
        // MobilityBonus values borrowed from stockfish, https://github.com/official-stockfish/Stockfish/blob/master/src/evaluate.cpp
        public static readonly int[] KnightMobilityBonus = { -62, -53, -12, -3, 3, 12, 21, 28, 37 };
        public static readonly int[] RookMobilityBonus = { -60, -24, 0, 3, 4, 14, 20, 30, 41, 41, 41, 45, 57, 58, 67 };
        public static readonly int[] BishopMobilityBonus = { -47, -20, 14, 29, 39, 53, 53, 60, 62, 69, 78, 83, 91, 96 };
        public static readonly int[] QueenMobilityBonus = { -29, -16, -8, -8, 18, 25, 23, 37, 41, 54, 65, 68, 69, 70, 70, 70, 71, 72, 74, 76, 90, 104, 105, 106, 112, 114, 114, 119 };



        public MaterialCounterV1(Board board) : base(board) { }

        public override int GetMaterialAdvantage(ChessGame chessGame)
        {
            int eval = CountMaterialAndMobility(board);

            if (board.Square[59] == Piece.WQueen) // White queen
                eval += Math.Max(12 - board.moves.Count, 0) * 20;
            if (board.Square[3] == Piece.BQueen) // Black queen
                eval -= Math.Max(12 - board.moves.Count, 0) * 20;

            float lateGameMultiplier = (float)(32 - chessGame.board.piecePoses.Count) / 32;
            if (lateGameMultiplier < 0.5f)
                lateGameMultiplier = 0;

            eval += LateGameKingToEadge.GetBonus(chessGame, lateGameMultiplier, 8);
            eval -= LateGameKingToEadge.GetBonus(chessGame, lateGameMultiplier, 16);

            eval += PawnStructure.GetEval(board, lateGameMultiplier);
            return eval;
        }

        private static int CountMaterialAndMobility(Board board)
        {
            int material = 0;
            bool foundOneBishop = false;
            for (int i = 0; i < 64; i++)
            {
                int piece = board.Square[i];
                if (piece != 0)
                {
                    int color = piece & Piece.ColorBits;
                    if (color == 8)
                    {
                        material += PieceValue.Indexed[piece];
                        material += PiecePosesBonus.PieceBonuses[piece, i];
                    }
                    else
                    {
                        material -= PieceValue.Indexed[piece];
                        material -= PiecePosesBonus.PieceBonuses[piece, i];
                    }




                    // Get activity for rooks, knights and bishops

                    int type = piece & Piece.PieceBits;

                    if (type == Piece.Rook)
                    {
                        int count = 0;
                        for (int dir = 0; dir < 4; dir++)
                        {
                            int move = i;
                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                if (MovesFromSquare.SlidingpieceMoves[i, dir, moveCount] == MovesFromSquare.InvalidMove)
                                    break;
                                move = MovesFromSquare.SlidingpieceMoves[i, dir, moveCount];

                                if (board.Square[move] == 0)
                                {
                                    count++;
                                }
                                else if ((board.Square[move] & Piece.ColorBits) != board.playerTurn)
                                {
                                    count++;
                                    break;
                                }
                                else
                                    break;
                            }
                        }
                        if (color == 8)
                            material += RookMobilityBonus[count];
                        else
                            material -= RookMobilityBonus[count];
                    }
                    else if (type == Piece.Bishop)
                    {
                        if (foundOneBishop)
                        {
                            material += 30;
                            foundOneBishop = false;
                        }
                        else
                            foundOneBishop = true;

                        int count = 0;
                        for (int dir = 4; dir < 8; dir++)
                        {
                            int move = i;
                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                if (MovesFromSquare.SlidingpieceMoves[i, dir, moveCount] == MovesFromSquare.InvalidMove)
                                    break;
                                move = MovesFromSquare.SlidingpieceMoves[i, dir, moveCount];

                                if (board.Square[move] == 0)
                                {
                                    count++;
                                }
                                else if ((board.Square[move] & Piece.ColorBits) != board.playerTurn)
                                {
                                    count++;
                                    break;
                                }
                                else
                                    break;
                            }
                        }
                        if (color == 8)
                            material += BishopMobilityBonus[count];
                        else
                            material -= BishopMobilityBonus[count];
                    }
                    else if (type == Piece.Queen)
                    {
                        int count = 0;
                        for (int dir = 0; dir < 8; dir++)
                        {
                            int move = i;
                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                if (MovesFromSquare.SlidingpieceMoves[i, dir, moveCount] == MovesFromSquare.InvalidMove)
                                    break;
                                move = MovesFromSquare.SlidingpieceMoves[i, dir, moveCount];

                                if (board.Square[move] == 0)
                                {
                                    count++;
                                }
                                else if ((board.Square[move] & Piece.ColorBits) != board.playerTurn)
                                {
                                    count++;
                                    break;
                                }
                                else
                                    break;
                            }
                        }
                        if (color == 8)
                            material += QueenMobilityBonus[count];
                        else
                            material -= QueenMobilityBonus[count];
                    }
                    else if (type == Piece.Knight)
                    {
                        int count = 0;

                        for (int j = 0; j < 8; j++)
                        {
                            int knightMove = i + MovesFromSquare.KnightMoves[i, j];
                            if (MovesFromSquare.KnightMoves[i, j] == MovesFromSquare.InvalidMove)
                                continue;
                            else if ((board.Square[knightMove] & Piece.ColorBits) != board.playerTurn)
                            {
                                count++;
                            }
                        }

                        if (color == 8)
                            material += QueenMobilityBonus[count];
                        else
                            material -= QueenMobilityBonus[count];
                    }
                }
            }

            return material;
        }


    }
}













/*












    public class MaterialCounterV2 : MaterialCounterBase // use some way of also getting the bonus of anygiven square for a piece
    {
        public MaterialCounterV2(Board board) : base(board) { }

        public override int GetMaterialAdvantage()
        {
            throw new NotImplementedException();
        }
    }
*/