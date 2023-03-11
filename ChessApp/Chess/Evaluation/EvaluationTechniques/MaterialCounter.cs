

using MyChess.Chess.Evaluation.EvaluationTechniques;

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
        public MaterialCounterBase(Board board, bool evaluateMatPlacement = false)
        {
            this.board = board;
        }

        public abstract int GetMaterialAdvantage(ChessGame chessGame, bool evaluateMatPlacement = false);
    }


    public class MaterialCounterV1 : MaterialCounterBase
    {
        public MaterialCounterV1(Board board) : base(board) { }

        public override int GetMaterialAdvantage(ChessGame chessGame, bool evaluateMatPlacement = false)
        {
            int whiteEval = CountMaterial(board, Piece.White, evaluateMatPlacement);
            int blackEval = CountMaterial(board, Piece.Black, evaluateMatPlacement);

            if (board.Square[59] == Piece.WQueen) // White queen
                whiteEval += Math.Max(10 - board.moves.Count, 0) * 20;
            if (board.Square[3] == Piece.BQueen) // Black queen
                blackEval += Math.Max(10 - board.moves.Count, 0) * 20;

            float lateGameMultiplier = (float)(32 - chessGame.board.piecePoses.Count) / 32;
            if (lateGameMultiplier < 0.5f)
                lateGameMultiplier = 0;

            whiteEval += LateGameKingToEadge.GetBonus(chessGame, lateGameMultiplier, 8);
            blackEval += LateGameKingToEadge.GetBonus(chessGame, lateGameMultiplier, 16);

            return whiteEval - blackEval + PawnStructure.GetEval(board, lateGameMultiplier);
        }

        private static int CountMaterial(Board board, int color, bool evaluateMatPlacement = false)
        {
            int material = 0;
            for (int i = 0; i < board.piecePoses.Count; i++)
            {
                int piece = board.Square[board.piecePoses[i]];
                if ((piece & Piece.ColorBits) == color)
                {
                    material += PieceValue.Indexed[piece];
                    if (evaluateMatPlacement)
                    {
                        material += PiecePosesBonus.PieceBonuses[piece, board.piecePoses[i]];
                        // if (color == 8 && piece == 9)
                        //     MyLib.DebugConsole.WriteLine("Piece: " + piece + " + " + PiecePosesBonus.PieceBonuses[piece, board.piecePoses[i]] + " Cord: " + board.piecePoses[i]);
                    }
                }
            }
            // if (evaluateMatPlacement)
            //     MyLib.DebugConsole.WriteLine("");
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