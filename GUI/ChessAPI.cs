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
            _sprites = Sprites.SpriteFetcher.GetSprites(@"C:\GitHub\MyChess\GUI\PeiceSprites\100x100"); // learn relativ path
            _form = form;
        }

        public void PrintBoard(Board board, int selecktedPiece)
        // make it so when it checks for wich peice it needs to print, 
        // make it check if there is an empty square first, then the color, and then do the pawn, rook and so on
        {
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

                    if ((board.board[i + (j * 8)] & 31) != 0) // checks if there is a peice
                        PrintPeice(i, j, board.board[i + (j * 8)]);
                }
            }

            _formGUI.Print();
        }

        public void PrintBoard(Board board)
        // make it so when it checks for wich peice it needs to print, 
        // make it check if there is an empty square first, then the color, and then do the pawn, rook and so on
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0) // white squares, mnake it its own method
                    {
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.WhiteSmoke);
                    }
                    else
                    {
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.LimeGreen);
                    }

                    if ((board.board[i + (j * 8)] & 31) != 0) // checks if there is a peice
                        PrintPeice(i, j, board.board[i + (j * 8)]);
                }
            }

            _formGUI.Print();
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
    }
}