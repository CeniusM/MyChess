using Chess.ChessBoard;
using Chess.Moves;
using winForm;
using Chess;

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
        // make it check if there is an empty square first, then the color, and then do the pawn, rook and so on
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

                    if ((chessGame._board.board[i + (j * 8)] & 31) != 0) // checks if there is a peice
                        PrintPeice(i, j, chessGame._board.board[i + (j * 8)]);
                }
            }

            PrintPosebleSquareForSelecktedSquare(selecktedPiece);

            _formGUI.Print();

            _isPrinting = false;
        }

        public void PrintPosebleSquareForSelecktedSquare(int selecktedPiece) // like the name?
        {
            List<Move> listOfMoves = chessGame.GetPossibleMoves(selecktedPiece);
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

                        if ((chessGame._board.board[i + (j * 8)] & 31) != 0) // checks if there is a peice
                            PrintPeice(i, j, chessGame._board.board[i + (j * 8)]);
                    }
                }
            }
        }

        public void PrintBoard()
        // make it so when it checks for wich peice it needs to print, 
        // make it check if there is an empty square first, then the color, and then do the pawn, rook and so on
        {
            PrintBoard(-1);
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
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int num = Directions.DirectionValues[i + (j * 8), 0 /*North*/];

                    if (num == 0)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
                    if (num == 1)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
                    if (num == 2)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
                    if (num == 3)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
                    if (num == 4)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
                    if (num == 5)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
                    if (num == 6)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
                    if (num == 7)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(255, (240 / (num + 1)), 0, 0));
                }
            }
            _formGUI.Print();
        }
    }
}