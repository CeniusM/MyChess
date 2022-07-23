using MyChess;
using winForm;
using MyChess.ChessBoard;

namespace MyChessGUI
{
    public enum GameState
    {
        None = 0,
        PlayingMove,
        PlayingChosingPiece,
        Settings,
        SelectionScreen1
    }
    public class GameOfChess
    {
        private const int SquareDimensions = 100;
        private ChessGame chessGame = new ChessGame();
        private int _selecktedSquare = -1;
        private ChessPrinter _chessPrinter;
        private Form1 _form;
        private bool _isRunning = true;
        public GameOfChess(Form1 form)
        {
            _form = form;
            _chessPrinter = new ChessPrinter(_form, chessGame);

            _form.MouseClick += MouseClick;
            _form.KeyPress += KeyPress;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void Play()
        {
            _chessPrinter.PrintBoard(_selecktedSquare);
        }

        private void KeyPress(object? sender, KeyPressEventArgs e)
        {

        }

        int squareX = 0;
        int squareY = 0;
        private void MouseClick(object? sender, MouseEventArgs e)
        {
            squareX = (int)((float)8 / 800 * (float)e.X);
            squareY = (int)((float)8 / 800 * (float)e.Y);


        }

        // bool makingAMove = false;
        private void MakeMove(int x, int y)
        {

        }
    }
}