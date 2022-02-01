using Chess;
using Chess.Moves;
using winForm;
using Chess.ChessBoard;

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
            _selecktedSquare = -1;

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
            else if (e.KeyChar == 'o') // Resests the board
            {
                chessGame.StartOver();
                chessAPI.PrintBoard(chessGame.GetBoard());
                _selecktedSquare = -1;
            }
        }

        private void MouseClick(object? sender, MouseEventArgs e)
        {
            int squareX = (int)((float)8 / 800 * (float)e.X);
            int squareY = (int)((float)8 / 800 * (float)e.Y);

            if ((squareX + (squareY * 8)) > 63) return;

            if (_selecktedSquare != -1 && (chessGame.GetBoard().board[squareX + (squareY * 8)] == 0 || (chessGame.GetBoard().board[squareX + (squareY * 8)] & Piece.White + Piece.Black) != (chessGame.GetBoard().board[_selecktedSquare] & Piece.White + Piece.Black))) // second click
            {
                // checks of the first piece is moving to either another colored piece or or nothing
                chessGame.MakeMove(new PosebleMoves.Move(_selecktedSquare, squareX + (squareY * 8)));
                _selecktedSquare = -1;
            }
            else if (squareX + (squareY * 8) == _selecktedSquare) // checks if you click the same square
            {
                _selecktedSquare = -1;
            }
            else if (chessGame.GetBoard().board[squareX + (squareY * 8)] != 0) // first click
            {
                _selecktedSquare = squareX + (squareY * 8);
            }

            // later on only print the square that is changed, make a method that takes a list of moves
            chessAPI.PrintBoard(chessGame.GetBoard(), _selecktedSquare);

            // debugging
            CS_MyConsole.MyConsole.WriteLine((squareX + ", " + squareY + "\n" + e.X + ", " + e.Y + "\n"));
        }
    }
}