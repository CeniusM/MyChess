using MyChess;
using MyChess.ChessBoard;
using MyChess.FEN;

namespace MyChess.UnitTester.Tests
{
    partial class Test
    {
        public static TestReport CompareBoardsWithDepthAndMoves(string FEN, int Depth)
         => CompareBoardsWithDepthAndMoves(new ChessGame(FEN), Depth);
        public static TestReport CompareBoardsWithDepthAndMoves(ChessGame chessGame, int Depth)
        {
            long moveCount = 0;

            SearchMove(Depth);

            void SearchMove(int depth)
            {
                // if (depth == 0)
                //     return;

                chessGame.possibleMoves.GenerateMoves();
                List<Move> movesRef = chessGame.GetPossibleMoves();
                int Count = movesRef.Count();
                Move[] moves = new Move[Count];
                movesRef.CopyTo(moves);

                Board Original = Board.GetCopy(chessGame.board);

                for (int i = 0; i < Count; i++)
                {
                    if (depth == 1)
                    {
                        moveCount++;
                        continue;
                    }
                    chessGame.board.MakeMove(moves[i]);
                    SearchMove(depth - 1);
                    chessGame.board.UnMakeMove();

                    CompareBoardsWithMove(Original, chessGame.board, moves[i]);
                }
            }

            return new("Nothing to return", 0);
        }


        /// <summary>
        /// returns succes if they are equal
        /// </summary>
        public static TestReport CompareBoardsWithMove(Board before, Board after, Move move)
        {
            string str = "";

            // compare squares
            bool isBoardsEquel = true;
            for (int i = 0; i < 64; i++)
            {
                    if (move.StartSquare == 4)
                        continue;
                if (before.Square[i] != after.Square[i])
                {
                    isBoardsEquel = false;
                    break;
                }
            }

            // compare sinlge varibles
            bool isCatleEquel = before.castle == after.castle;
            bool isEnPassentEquel = before.enPassantPiece == after.enPassantPiece;
            bool isPlayerTurnEquel = before.playerTurn == after.playerTurn;

            // will get all the numbers and after sort them, then go though like with the boards        
            bool isPiecePosesEquel = true;
            int[] beforePos = new int[before.piecePoses.Count];
            int[] afterPos = new int[after.piecePoses.Count];
            if (before.piecePoses.Count == after.piecePoses.Count)
            {
                // sort so they can be compared
                Array.Sort(beforePos);
                Array.Sort(afterPos);

                // compare
                for (int i = 0; i < before.piecePoses.Count; i++)
                {
                    if (beforePos[i] != afterPos[i])
                    {
                        isPiecePosesEquel = false;
                        break;
                    }
                }
            }
            else // if count not equel
                isPiecePosesEquel = false;

            // compare last move history
            bool isMovesEquel = true;
            if (before.moves.Count == after.moves.Count)
            {
                if (before.moves.Count != 0)
                {
                    Move beforeMove = before.moves.Peek();
                    Move afterMove = after.moves.Peek();
                    if (beforeMove.StartSquare != afterMove.StartSquare)
                        isMovesEquel = false;
                    if (beforeMove.TargetSquare != afterMove.TargetSquare)
                        isMovesEquel = false;
                    if (beforeMove.MoveFlag != afterMove.MoveFlag)
                        isMovesEquel = false;
                    if (beforeMove.CapturedPiece != afterMove.CapturedPiece)
                        isMovesEquel = false;
                }
            }
            else
                isMovesEquel = false;

            // compare last gameData history
            bool isGameDataEquel = true;
            if (before.gameData.Count == after.gameData.Count)
            {
                if (before.gameData.Count != 0)
                {
                    DataICouldentGetToWork beforeMove = before.gameData.Peek();
                    DataICouldentGetToWork afterMove = after.gameData.Peek();
                    if (beforeMove.castle != afterMove.castle)
                        isGameDataEquel = false;
                    if (beforeMove.enPassantPiece != afterMove.enPassantPiece)
                        isGameDataEquel = false;
                }
            }
            else
                isMovesEquel = false;


            if (!isBoardsEquel)
            {
                str += "\n" + Board.GetPrettyBoard(before);
                str += Board.GetPrettyBoard(after);
            }
            if (!isPiecePosesEquel)
            {
                if (before.piecePoses.Count == after.piecePoses.Count)
                {
                    for (int i = 0; i < before.piecePoses.Count; i++)
                    {
                        str += "before: " + beforePos[i] + ", after: " + afterPos[i] + "\n";
                    }
                }
                else
                {
                    str += "PiecePoses Count Dosent Match: " + before.piecePoses.Count + " != " + after.piecePoses.Count + "\n";
                }
            }


            if (isBoardsEquel && isPiecePosesEquel && isCatleEquel && isEnPassentEquel && isPlayerTurnEquel && isMovesEquel)
                return new("", 1);
            else
                return new(str, -1);
        }
    }
}