using Chess.ChessBoard;
using winForm;

namespace MyChessGUI
{
    class ChessAPI // gets the instruction from the chess game and gives the FormGUI instructions on what to print
    {
        private FormGUI _formGUI;
        private List<Bitmap> _sprites;
        private Form1 _form;
        public ChessAPI(Form1 form)
        {
            _formGUI = new FormGUI(form);
            _sprites = Sprites.SpriteFetcher.GetSprites(@"C:\GitHub\MyChess\GUI\PeiceSprites\100x100"); // larn relativ path
            _form = form;
        }

        public void PrintBoard(Board board)
        // make it so when it checks for wich peice it needs to print, 
        // make it check if there is an empty square first, then the color, and then do the pawn, rook and so on
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i + j % 2 == 0) // white squares, mnake it its own method
                    {
                        _formGUI.DrawSquare(i * _form.Width, j * _form.Height, (i + 1) * _form.Width, (j + 1) * _form.Height, Color.White);
                    }
                    else
                    {
                        _formGUI.DrawSquare(i * _form.Width, j * _form.Height, (i + 1) * _form.Width, (j + 1) * _form.Height, Color.Black);
                    }

                    if ((board.board[i + (j * 8)] & 31) != 0) // checks if there is a peice
                        PrintSquare(i, j, board.board[i]);
                }
            }

            //test
            // _formGUI.DrawSquare(100, 100, 300, 400, Color.Red);
            // _formGUI.PrintSquareOnForm(100, 100, 200, 200, Color.Blue);
            _formGUI.DrawBitmap(_sprites[5], 0, 0);

            _formGUI.Print();
        }

        public void PrintBoard(string FENboard)
        {

        }

        private void PrintSquare(int x, int y, int Piece)
        {
            if ((Piece & 8) == 8) // chescks if the peice is white
            {
                if ((Piece & 1) == 1) // pawn
                {
                    _formGUI.DrawBitmap(_sprites[0], x * _form.Height, y * _form.Width); // make this a local fucktion or method instead of writing it 200 times...
                }
                else if ((Piece & 2) == 2) // rook
                {
                    _formGUI.DrawBitmap(_sprites[1 << 1], x * _form.Height, y * _form.Width);
                }
                else if ((Piece & 3) == 3) // bishop
                {
                    _formGUI.DrawBitmap(_sprites[2 << 1], x * _form.Height, y * _form.Width);
                }
                else if ((Piece & 4) == 4) // knight
                {
                    _formGUI.DrawBitmap(_sprites[3 << 1], x * _form.Height, y * _form.Width);
                }
                else if ((Piece & 5) == 5) // queen
                {
                    _formGUI.DrawBitmap(_sprites[4 << 1], x * _form.Height, y * _form.Width);
                }
                else if ((Piece & 6) == 6) // king
                {
                    _formGUI.DrawBitmap(_sprites[5 << 1], x * _form.Height, y * _form.Width);
                }
            }
            else
            {
                if ((Piece & 1) == 1) // pawn
                {
                    _formGUI.DrawBitmap(_sprites[1], x * _form.Height, y * _form.Width);
                }
                else if ((Piece & 2) == 2) // rook
                {
                    _formGUI.DrawBitmap(_sprites[(1 << 1) + 1], x * _form.Height, y * _form.Width);
                }
                else if ((Piece & 3) == 3) // bishop
                {
                    _formGUI.DrawBitmap(_sprites[(2 << 1) + 1], x * _form.Height, y * _form.Width);
                }
                else if ((Piece & 4) == 4) // knight
                {
                    _formGUI.DrawBitmap(_sprites[(3 << 1) + 1], x * _form.Height, y * _form.Width);
                }
                else if ((Piece & 5) == 5) // queen
                {
                    _formGUI.DrawBitmap(_sprites[(4 << 1) + 1], x * _form.Height, y * _form.Width);
                }
                else if ((Piece & 6) == 6) // king
                {
                    _formGUI.DrawBitmap(_sprites[(5 << 1) + 1], x * _form.Height, y * _form.Width);
                }
            }
        }
    }
}