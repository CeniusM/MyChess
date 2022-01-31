using Chess;
using Chess.Moves;
using winForm;

namespace MyChessGUI
{
    class GameOfChess // is used to get inputs from the user and use them to make moves in the chess game
    {
        private ChessGame chessGame = new ChessGame();
        private ChessAPI chessAPI;
        private int[] _squareDimensions = new int[2];
        private int _selecktedSquare;
        private bool _isRunning;
        private Form1 _form;
        public GameOfChess(Form1 form)
        {
            chessAPI = new ChessAPI(form);
            _form = form;

            _squareDimensions[0] = form.Height / 8;
            _squareDimensions[1] = form.Width / 8;
            _selecktedSquare = 64;

            _form.MouseClick += MouseClick;
            _form.KeyPress += KeyPress;
        }

        public void Play()
        {
            _isRunning = true;

            chessAPI.PrintBoard(chessGame.GetBoard());

            while (_isRunning)
                Thread.Sleep(1000);
        }

        public void Stop() => _isRunning = false;

        private void KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'r') // rePrints the whole board
            {
                chessAPI.PrintBoard(chessGame.GetBoard());
            }
        }

        private void MouseClick(object? sender, MouseEventArgs e)
        {
            int squareY = (int)((float)8 / (float)_form.Height * (float)e.Y);
            int squareX = (int)((float)8 / (float)_form.Width * (float)e.X);

            if (_selecktedSquare == 64)
            {
                _selecktedSquare = squareX + squareY * 8;
            }
            else
            {
                chessGame.MakeMove(new PosebleMoves.Move(_selecktedSquare, squareX + squareY * 8));
                _selecktedSquare = 64;
            }

            // later on only print the square that is changed, make a method that takes a list of moves
            chessAPI.PrintBoard(chessGame.GetBoard());

            // debugging
            // CS_MyConsole.MyConsole.WriteLine((squareX + ", " + squareY + "\n" + e.X + ", " + e.Y + "\n"));
        }
    }
}