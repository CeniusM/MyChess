using Chess;
using Chess.Moves;
using winForm;
using Chess.ChessBoard;

namespace MyChessGUI
{
    class GameOfChess // is used to get inputs from the user and use them to make moves in the chess game
    {
        // private ChessGame chessGame = new ChessGame(MyFEN.GetBoardFromFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"));
        private ChessGame chessGame = new ChessGame("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        private ChessAPI chessAPI;
        private int[] _squareDimensions = new int[2];
        private int _selecktedSquare;
        private bool _isRunning;
        private Random rnd = new Random();
        private Form1 _form;
        public GameOfChess(Form1 form)
        {
            chessAPI = new ChessAPI(form, chessGame);
            _form = form;

            _squareDimensions[0] = 100;//form.Height / 8;
            _squareDimensions[1] = 100;//form.Width / 8;
            _selecktedSquare = -1;

            int renderDirections = Directions.DirectionValues[0, 0]; // just makes it so its loaded

            _form.MouseClick += MouseClick;
            _form.KeyPress += KeyPress;
        }

        public void Play()
        {
            // just to run the static method in the new directions
            int foo = Chess.Moves.Directions.DirectionValues[0, 0];

            _isRunning = true;

            chessAPI.PrintBoard();

            while (_isRunning)
                Thread.Sleep(1000);
        }

        public void Stop() => _isRunning = false;

        private void KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'r') // rePrints the whole board
            {
                chessAPI.PrintBoard();
            }
            else if (e.KeyChar == 'o') // Resests the board
            {
                chessGame.StartOver();
                chessAPI.PrintBoard();
                _selecktedSquare = -1;
            }
            else if (e.KeyChar == 's') // Saves board
            {
                CS_MyConsole.MyConsole.WriteLine(MyFEN.GetFENFromBoard(chessGame._board));
            }
            else if (e.KeyChar == '1')
            {
                chessGame = new ChessGame("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
                chessAPI = new ChessAPI(_form, chessGame);
                chessAPI.PrintBoard();
            }
            else if (e.KeyChar == '2')
            {
                chessGame = new ChessGame("rnb1kbnr/ppp1pppp/8/q7/8/2N5/PPPP1PPP/R1BQKBNR b KQ - 0 1");
                chessAPI = new ChessAPI(_form, chessGame);
                chessAPI.PrintBoard();
            }
            else if (e.KeyChar == '3')
            {
                chessGame = new ChessGame("1k2r3/1p3r2/pN4p1/3p4/1R2n2P/4Q1q1/PPP3P1/2K1R3 b - - 1 1");
                chessAPI = new ChessAPI(_form, chessGame);
                chessAPI.PrintBoard();
            }

            else if (e.KeyChar == '0') // reads the last line in MyConsole and takes it in as a fen string
            {
                chessGame = new ChessGame(CS_MyConsole.MyConsole.ReadLastLine());
                chessAPI = new ChessAPI(_form, chessGame);
                chessAPI.PrintBoard();
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
                chessGame.MakeMove(new Move(_selecktedSquare, squareX + (squareY * 8)));
                _selecktedSquare = -1;
            }
            else if (squareX + (squareY * 8) == _selecktedSquare) // checks if you click the same square
            {
                _selecktedSquare = -1;
            }
            else if (chessGame.GetBoard().board[squareX + (squareY * 8)] != 0 && Board.IsPiecesSameColor(chessGame.GetBoard().board[squareX + (squareY * 8)], chessGame._board.PlayerTurn)) // first click
            {
                _selecktedSquare = squareX + (squareY * 8);
            }

            while (_isRunning && false)
            {
                if (chessGame._board.PlayerTurn == Piece.White) // play vs completly random ai
                {
                    List<Move> moves = chessGame.GetPossibleMoves(0);
                    chessGame.MakeMove(moves[rnd.Next(0, moves.Count)]);
                    chessAPI.PrintBoard(_selecktedSquare);
                    Thread.Sleep(100);
                }
                else if (chessGame._board.PlayerTurn == Piece.Black) // play vs completly random ai
                {
                    List<Move> moves = chessGame.GetPossibleMoves(0);
                    chessGame.MakeMove(moves[rnd.Next(0, moves.Count)]);
                    chessAPI.PrintBoard(_selecktedSquare);
                    Thread.Sleep(100);
                }
            }

            // later on only print the square that is changed, make a method that takes a list of moves
            chessAPI.PrintBoard(_selecktedSquare);


            // debugging
            // chessAPI.TestTheDirections();
            // CS_MyConsole.MyConsole.WriteLine((squareX + ", " + squareY + "\n" + e.X + ", " + e.Y + "\n"));
        }
    }
}