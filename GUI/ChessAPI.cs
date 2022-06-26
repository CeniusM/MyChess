using MyChess.ChessBoard;
using winForm;
using MyChess;
using CS_Math;
using MyChess.ChessBoard.Evaluation;

using MyChess.PossibleMoves;

namespace MyChessGUI
{
    class ChessAPI // gets the instruction from the chess game and gives the FormGUI instructions on what to print
    {
        private FormGUI _formGUI;
        private List<Bitmap> _sprites;
        private ChessGame chessGame;
        private Form1 _form;
        private bool _isPrinting = false;
        public ChessAPI(Form1 form, ChessGame chessGame)
        {
            _formGUI = new FormGUI(form);
            this.chessGame = chessGame;
            _sprites = Sprites.SpriteFetcher.GetSprites(); // learn relativ path
            _form = form;
            form.Paint += (s, e) => PrintBoard();
        }

        public void PrintBoard(int selecktedPiece)
        // make it so when it checks for wich peice it needs to print, 
        // make it check if there is an empty square first, then the color, and then do the Pawn, rook and so on
        {
            if (_isPrinting)
                return;

            _isPrinting = true;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i + (j * 8) == selecktedPiece)
                    {
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.Red);
                    }
                    else if ((i + j) % 2 == 0) // white squares, mnake it its own method
                    {
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.WhiteSmoke);
                    }
                    else
                    {
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.LimeGreen);
                    }

                    if ((chessGame.board[i + (j * 8)] & 31) != 0) // checks if there is a peice
                        PrintPeice(i, j, chessGame.board[i + (j * 8)]);
                }
            }

            PrintPosebleSquaresForSelecktedSquare(selecktedPiece);

            PrintEvalBar();

            _formGUI.Print();

            _isPrinting = false;
        }

        public void PrintPosebleSquaresForSelecktedSquare(int selecktedPiece) // like the name?
        {
            //for debugging
            chessGame.possibleMoves.GenerateMoves();

            List<Move> listOfMoves = chessGame.GetPossibleMoves();
            List<Move> moves = new List<Move>();
            for (int i = 0; i < listOfMoves.Count; i++)
            {
                if (listOfMoves[i].StartSquare == selecktedPiece)
                    moves.Add(listOfMoves[i]);
            }

            for (int moveIndex = 0; moveIndex < moves.Count; moveIndex++)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (i + (j * 8) == selecktedPiece)
                        {
                            _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, 100, 255, 0));
                        }
                        else if ((i + j) % 2 == 0 && moves[moveIndex].TargetSquare == i + (j * 8)) // white squares, mnake it its own method
                        {
                            _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.Orange);
                        }
                        else if (moves[moveIndex].TargetSquare == i + (j * 8))
                        {
                            _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.Red);
                        }

                        if ((chessGame.board[i + (j * 8)] & 31) != 0) // checks if there is a peice
                            PrintPeice(i, j, chessGame.board[i + (j * 8)]);
                    }
                }
            }
        }

        public void PrintEvalBar()
        {
            _formGUI.DrawSquare(800, 0, 800, 100, Color.White);

            float evaluation = MyEvaluater.EvaluateBoard(chessGame.board, chessGame.GetPossibleMoves()) / 300f; // idk 

            float evalHeight = MyMath.LogisticCurve((float)evaluation, 30, 0.3f, 15); // returs a num between -15 and 15

            int evalHeightpx = (int)((-evalHeight / 15f + 1) * 400f); // make eval height into a range between 0, 800

            _formGUI.DrawSquare(800, 0, evalHeightpx, 100, Color.Black);
        }

        public void PrintBoard()
        // make it so when it checks for wich peice it needs to print, 
        // make it check if there is an empty square first, then the color, and then do the Pawn, rook and so on
        {
            PrintBoard(64);
        }

        public void PrintBoard(string FENboard)
        {

        }

        private void PrintPeice(int x, int y, int piece) // later add it so it does the background, or at list be able to
        {
            void PrintWSprite(int sprite) => _formGUI.DrawBitmap(_sprites[(sprite - 1) << 1], x * 100, y * 100);
            void PrintBSprite(int sprite) => _formGUI.DrawBitmap(_sprites[((sprite - 1) << 1) + 1], x * 100, y * 100);

            if ((piece & Piece.White) == Piece.White)
                PrintWSprite((piece & 0b111));
            else if ((piece & Piece.Black) == Piece.Black)
                PrintBSprite((piece & 0b111));
            else
                throw new NotImplementedException("Cant use Piceses with no color value");
        }


        public void TestTheDirections() // just for testing
        {
            //for (int i = 0; i < 8; i++)
            //{
            //    for (int j = 0; j < 8; j++)
            //    {
            //        int num = Directions.DirectionValues[i + (j * 8), 0 /*North*/];
            //
            //        if (num == 0)
            //            _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
            //        if (num == 1)
            //            _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
            //        if (num == 2)
            //            _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
            //        if (num == 3)
            //            _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
            //        if (num == 4)
            //            _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
            //        if (num == 5)
            //            _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
            //        if (num == 6)
            //            _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
            //        if (num == 7)
            //            _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
            //    }
            //}
            //_formGUI.Print();
        }

        public void PrintMoves(int square, int direction)
        {
            // reset
            for (int col = 0; col < 8; col++)
            {
                for (int rank = 0; rank < 8; rank++)
                {
                    if ((col + rank) % 2 == 0)
                    {
                        _formGUI.DrawSquare(col * 100, rank * 100, 100, 100, Color.WhiteSmoke);
                    }
                    else
                    {
                        _formGUI.DrawSquare(col * 100, rank * 100, 100, 100, Color.LimeGreen);
                    }
                }
            }

            // directions
            _formGUI.DrawSquare(square % 8 * 100, square / 8 * 100, 100, 100, Color.Yellow);
            for (int i = 0; i < 7; i++)
            {
                int s = MovesFromSquare.SlidingpieceMoves[square, direction, i];
                if (s == MovesFromSquare.InvalidMove)
                    break;
                _formGUI.DrawSquare(s % 8 * 100, s / 8 * 100, 100, 100, Color.Red);
            }

            _formGUI.Print();
        }
    }
}