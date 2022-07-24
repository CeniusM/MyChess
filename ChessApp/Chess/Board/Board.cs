// V2, no full or half moves or repetision will be considerd
// or maby make chessGame take care of that
// while this is only used for a quick engine

namespace MyChess.ChessBoard
{
    public readonly struct GameStatusFlag
    {
        public const int BlackWin = -1;
        public const int Draw = 0;
        public const int WhiteWin = 1;
        public const int Running = 2;
    }
    public readonly struct CASTLE
    {
        public const int W_King_Side = 0b1000;
        public const int W_Queen_Side = 0b0100;
        public const int B_King_Side = 0b0010;
        public const int B_Queen_Side = 0b0001;
    }

    public struct DataICouldentGetToWork
    {
        public readonly int castle;
        public readonly int enPassantPiece;
        public DataICouldentGetToWork(int Castle, int EnPassantPiece)
        {
            castle = Castle;
            enPassantPiece = EnPassantPiece;
        }
    }

    public class Board
    {
        public Stack<DataICouldentGetToWork> gameData = new Stack<DataICouldentGetToWork>();
        public Stack<Move> moves = new Stack<Move>();
        public PieceList piecePoses = new PieceList();
        public int[] Square = new int[64];

        // W KingSide = 0b1000, W QueenSide = 0b0100, B KingSide = 0b0010, B QueenSide = 0b0001
        public int castle = 0b1111;
        public int enPassantPiece = 64;
        public const int WhiteMask = 0b00001000;
        public const int BlackMask = 0b00010000;
        public const int ColorMask = WhiteMask | BlackMask;
        public int playerTurn = 8; // 8 = white, 16 = black;

        // 2 = running
        // 1 = White Won
        // 0 = Draw
        // -1 = Black Won
        public int GameStatus = GameStatusFlag.Running;

        public Board()
        {
            InitPiecePoses();
        }

        public void InitPiecePoses()
        {
            piecePoses = new PieceList(32);
            for (int i = 0; i < 64; i++) // put in kings first
            {
                if ((Square[i] & Piece.PieceBits) == Piece.King)
                    piecePoses.AddPieceAtSquare(i);
            }
            for (int i = 0; i < 64; i++)
            {
                if (Square[i] != 0 && (Square[i] & Piece.PieceBits) != Piece.King)
                    piecePoses.AddPieceAtSquare(i);
            }
        }

        // public int this[int key]
        // {
        //     get => Square[key];
        //     set => Square[key] = value;
        // }board[]  = board.Square[]

        /// <summary> works up too 65535 and down too -65472 </summary>
        public static bool IsPieceInBound(int pos) => (pos & 0xFFC0) == 0;
        public static string MoveToStr(Move move)
        {
            return IntToLetterNum(move.StartSquare) + IntToLetterNum(move.TargetSquare);
        }
        public static string IntToLetterNum(int pos)
        {
            return (char)((int)'a' + pos % 8) + "" + (char)(8 - (pos >> 3) + '0');
        }
        public static string LetterToIntNum(string pos)
        {
            throw new NotImplementedException();
        }



        // move methods need
        // half/fullMove
        //

        public void MakeMove(Move move)
        {

            // halfMove += 1;
            // // if move was a succes, adds a fullmove after black moved
            // if (playerTurn == BlackMask)
            //     fullMove += 1;

            moves.Push(move);
            gameData.Push(new(castle, enPassantPiece));



            // can avoid some of these if you dont put them in fx PawnTwoForward, EnPassantCapture, Castle
            if (castle != 0)
            {
                if (move.StartSquare == 60 || move.TargetSquare == 60) // WKing
                    castle &= 0b0011;
                else if (move.StartSquare == 4 || move.TargetSquare == 4) // BKing
                    castle &= 0b1100;

                else if (move.StartSquare == 63 || move.TargetSquare == 63) // WRook King Side
                    castle &= (0b1111 ^ CASTLE.W_King_Side);
                else if (move.StartSquare == 56 || move.TargetSquare == 56) // WRook Queen Side
                    castle &= (0b1111 ^ CASTLE.W_Queen_Side);

                else if (move.StartSquare == 7 || move.TargetSquare == 7) // BRook King Side
                    castle &= (0b1111 ^ CASTLE.B_King_Side);
                else if (move.StartSquare == 0 || move.TargetSquare == 0) // BRook Queen Side
                    castle &= (0b1111 ^ CASTLE.B_Queen_Side);
            }

            if (move.MoveFlag == Move.Flag.None)
            {
                // pawn move or capture
                // if ((Square[move.StartSquare] & Piece.PieceBits) == Piece.Pawn || Square[move.TargetSquare] != 0)
                //     halfMove = 0;

                if (move.CapturedPiece != 0)
                    piecePoses.RemovePieceAtSquare(move.TargetSquare);

                piecePoses.MovePiece(move.StartSquare, move.TargetSquare);
                Square[move.TargetSquare] = Square[move.StartSquare];
                Square[move.StartSquare] = 0;
                enPassantPiece = 64;
            }
            else if (move.MoveFlag == Move.Flag.PawnTwoForward)
            {
                piecePoses.MovePiece(move.StartSquare, move.TargetSquare);
                Square[move.TargetSquare] = Square[move.StartSquare];
                Square[move.StartSquare] = 0;

                if (playerTurn == WhiteMask)
                    enPassantPiece = move.TargetSquare + 8;
                else
                    enPassantPiece = move.TargetSquare - 8;
            }
            else if (move.MoveFlag == Move.Flag.EnPassantCapture)
            {
                if (playerTurn == WhiteMask)
                {
                    piecePoses.RemovePieceAtSquare(move.TargetSquare + 8);
                    Square[move.StartSquare] = 0;
                    Square[move.TargetSquare] = Piece.WPawn;
                    Square[move.TargetSquare + 8] = 0;
                }
                else
                {
                    piecePoses.RemovePieceAtSquare(move.TargetSquare - 8);
                    Square[move.StartSquare] = 0;
                    Square[move.TargetSquare] = Piece.BPawn;
                    Square[move.TargetSquare - 8] = 0;
                }
                piecePoses.MovePiece(move.StartSquare, move.TargetSquare);
                enPassantPiece = 64;
            }
            else if (move.MoveFlag == Move.Flag.Castling)
            {
                // // white king side
                if (move.TargetSquare == 62)
                {
                    piecePoses.MovePiece(60, 62);
                    piecePoses.MovePiece(63, 61);
                    Square[60] = 0;
                    Square[61] = Piece.WRook;
                    Square[62] = Piece.WKing;
                    Square[63] = 0;
                    castle ^= (CASTLE.W_King_Side & CASTLE.W_Queen_Side);
                }
                // white queen side
                else if (move.TargetSquare == 58)
                {
                    piecePoses.MovePiece(60, 58);
                    piecePoses.MovePiece(56, 59);
                    Square[60] = 0;
                    Square[59] = Piece.WRook;
                    Square[58] = Piece.WKing;
                    Square[56] = 0;
                }
                // black king side
                else if (move.TargetSquare == 6)
                {
                    piecePoses.MovePiece(4, 6);
                    piecePoses.MovePiece(7, 5);
                    Square[4] = 0;
                    Square[5] = Piece.BRook;
                    Square[6] = Piece.BKing;
                    Square[7] = 0;
                }
                // black queen side
                else if (move.TargetSquare == 2)
                {
                    piecePoses.MovePiece(4, 2);
                    piecePoses.MovePiece(0, 3);
                    Square[4] = 0;
                    Square[3] = Piece.BRook;
                    Square[2] = Piece.BKing;
                    Square[0] = 0;
                }
                enPassantPiece = 64;
            }
            else // promotion
            {
                if (move.CapturedPiece != 0)
                    piecePoses.RemovePieceAtSquare(move.TargetSquare);

                piecePoses.MovePiece(move.StartSquare, move.TargetSquare);
                Square[move.TargetSquare] = move.PromotionPiece() | playerTurn;
                Square[move.StartSquare] = 0;
                enPassantPiece = 64;
            }

            ChangePlayer();
        }

        public void UnMakeMove()
        {
            Move move = moves.Pop();
            DataICouldentGetToWork data = gameData.Pop();
            castle = data.castle;
            enPassantPiece = data.enPassantPiece;

            if ((move.CapturedPiece & Piece.King) == Piece.King)
            {

            }

            ChangePlayer();

            if (move.MoveFlag == Move.Flag.None)
            {
                piecePoses.MovePiece(move.TargetSquare, move.StartSquare);
                if (move.CapturedPiece != 0)
                    piecePoses.AddPieceAtSquare(move.TargetSquare);

                Square[move.StartSquare] = Square[move.TargetSquare];
                Square[move.TargetSquare] = move.CapturedPiece;
            }
            else if (move.MoveFlag == Move.Flag.PawnTwoForward)
            {
                piecePoses.MovePiece(move.TargetSquare, move.StartSquare);
                Square[move.StartSquare] = Square[move.TargetSquare];
                Square[move.TargetSquare] = 0;
                // enPassantPiece = 64;
            }
            else if (move.MoveFlag == Move.Flag.EnPassantCapture)
            {
                piecePoses.MovePiece(move.TargetSquare, move.StartSquare);
                Square[move.StartSquare] = Square[move.TargetSquare];
                Square[move.TargetSquare] = 0;
                if (playerTurn == WhiteMask)
                {
                    Square[move.TargetSquare + 8] = Piece.BPawn;
                    piecePoses.AddPieceAtSquare(move.TargetSquare + 8);
                }
                else
                {
                    Square[move.TargetSquare - 8] = Piece.WPawn;
                    piecePoses.AddPieceAtSquare(move.TargetSquare - 8);
                }
                // enPassantPiece = move.TargetSquare;
            }
            else if (move.MoveFlag == Move.Flag.Castling)
            {
                // white king side
                if (move.TargetSquare == 62)
                {
                    piecePoses.MovePiece(62, 60);
                    piecePoses.MovePiece(61, 63);
                    Square[60] = Piece.WKing;
                    Square[61] = 0;
                    Square[62] = 0;
                    Square[63] = Piece.WRook;
                }
                // white queen side
                else if (move.TargetSquare == 58)
                {
                    piecePoses.MovePiece(58, 60);
                    piecePoses.MovePiece(59, 56);
                    Square[60] = Piece.WKing;
                    Square[59] = 0;
                    Square[58] = 0;
                    Square[56] = Piece.WRook;
                }
                // black king side
                else if (move.TargetSquare == 6)
                {
                    piecePoses.MovePiece(6, 4);
                    piecePoses.MovePiece(5, 7);
                    Square[4] = Piece.BKing;
                    Square[5] = 0;
                    Square[6] = 0;
                    Square[7] = Piece.BRook;
                }
                // black queen side
                else if (move.TargetSquare == 2)
                {
                    piecePoses.MovePiece(2, 4);
                    piecePoses.MovePiece(3, 0);
                    Square[4] = Piece.BKing;
                    Square[3] = 0;
                    Square[2] = 0;
                    Square[0] = Piece.BRook;
                }
            }
            else // promotion
            {
                piecePoses.MovePiece(move.TargetSquare, move.StartSquare);
                // if (move.CapturedPiece != 0)
                if (move.CapturedPiece != 0)
                    piecePoses.AddPieceAtSquare(move.TargetSquare);

                Square[move.StartSquare] = Piece.Pawn | playerTurn;
                Square[move.TargetSquare] = move.CapturedPiece;
            }
        }

        public void ChangePlayer() => playerTurn ^= ColorMask;

        public static string GetPrettyBoard(Board board)
        {
            string output = "";
            const string line = "+---+---+---+---+---+---+---+---+";

            for (int i = 0; i < 8; i++)
            {
                output += "   " + line + "\n";
                output += " " + (8 - i) + " ";
                for (int j = 0; j < 8; j++)
                {
                    output += "| " + MyChess.FEN.MyFEN.GetCharFromPiece(board.Square[(i * 8) + j]) + " ";
                }
                output += "|\n";
            }
            output += "   " + line + "\n";
            output += "     A   B   C   D   E   F   G   H" + "\n";
            return output;
        }
        public static Board GetCopy(Board original)
        {
            Board board = new Board();
            for (int i = 0; i < 64; i++)
            {
                board.Square[i] = original.Square[i];
            }
            board.castle = original.castle;
            board.enPassantPiece = original.enPassantPiece;
            board.GameStatus = original.GameStatus;
            board.playerTurn = original.playerTurn;

            for (int i = 0; i < original.piecePoses.Count; i++)
                board.piecePoses.AddPieceAtSquare(original.piecePoses[i]);

            DataICouldentGetToWork[] dataArray = new DataICouldentGetToWork[original.gameData.Count];
            original.gameData.CopyTo(dataArray, 0);
            for (int i = original.gameData.Count - 1; i > -1; i--)
                board.gameData.Push(dataArray[i]);

            Move[] movesArray = new Move[original.moves.Count];
            original.moves.CopyTo(movesArray, 0);
            for (int i = original.moves.Count - 1; i > -1; i--)
                board.moves.Push(movesArray[i]);

            // MyChess.UnitTester.Tests.Test.CompareBoardsWithMove(original, board, new(0, 0, 0, 0));

            return board;
        }
    }
}