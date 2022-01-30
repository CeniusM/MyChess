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
            while (_isRunning)
                Thread.Sleep(1000);
        }

        public void Stop() => _isRunning = false;

        private void KeyPress(object? sender, KeyPressEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MouseClick(object? sender, MouseEventArgs e)
        {
            // its totaly ok its messy o.o, i mean, why would you have to click fast right?... i still need to clean it up tho
            // and its not acurate, it should always do it so like 620 = 6, and 134 = 1, and so on... but it dosent
            int squareY = (int)MathF.Floor((_squareDimensions[0] * (float)e.Y));
            int squareX = (int)MathF.Floor((_squareDimensions[1] * (float)e.X));

            if (_selecktedSquare == 64)
            {
                _selecktedSquare = squareX + squareY * 8;
            }
            else
            {
                chessGame.MakeMove(new PosebleMoves.Move(_selecktedSquare, squareX + squareY * 8));
                _selecktedSquare = 64;
            }

            chessAPI.PrintBoard(chessGame.GetBoard());

            // debugging
            // CS_MyConsole.MyConsole.WriteLine((squareX + ", " + squareY + "\n" + e.X + ", " + e.Y + "\n"));
        }
    }
}