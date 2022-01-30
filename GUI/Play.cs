using Chess;
using winForm;

namespace MyChessGUI
{
    class GameOfChess // is used to get inputs from the user and use them to make moves in the chess game
    {
        private ChessGame chessGame = new ChessGame();
        private ChessAPI chessAPI;
        private int SelecktedSquare;
        private bool _isRunning;
        private Form1 _form;
        public GameOfChess(Form1 form)
        {
            chessAPI = new ChessAPI(form);
            _form = form;

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
            
        }
    }
}