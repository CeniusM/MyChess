using Chess.ChessBoard;
using Chess.Moves;
using winForm;

namespace MyChessGUI
{
    class ChessAPI // gets the instruction from the chess game and gives the FormGUI instructions on what to print
    {
        private FormGUI _formGUI;
        private List<Bitmap> _sprites;
        private Board _board;
        private Form1 _form;
        private bool _isPrinting = false;
        public ChessAPI(Form1 form, Board board)
        {
            _formGUI = new FormGUI(form);
            _board = board;
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

                    if ((_board.board[i + (j * 8)] & 31) != 0) // checks if there is a peice
                        PrintPeice(i, j, _board.board[i + (j * 8)]);
                }
            }

            _formGUI.Print();

            _isPrinting = false;
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
                    int num = Directions.directions[i + (j * 8)].North;

                    if (num == 0)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.Green);
                    if (num == 1)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.GreenYellow);
                    if (num == 2)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.DarkGreen);
                    if (num == 3)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.Gray);
                    if (num == 4)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.Black);
                    if (num == 5)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.Red);
                    if (num == 6)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.OrangeRed);
                    if (num == 7)
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.Blue);
                }
            }
            _formGUI.Print();
        }
    }
}