using ChessV1; // it should be that this is the only change to try difrent boards
using winForm;
using MyLib;

/*
This is only used for debuging and engaging with the new boards

*/


namespace ChessGUI
{
    public enum GameStates
    {
        None = 0,
        PlayingMove,
        PlayingChosingPiece,
        Settings,
        SelectionScreen1
    }
    public class GameOfChess
    {
        private GameStates _GameState = GameStates.None;

        private const int SquareDimensions = 100;
        private SafeBoard board = new SafeBoard("8/5pq1/4k1p1/1n6/1N6/4K2P/5PQ1/8 w - - 0 1");
        private int _selecktedSquare = -1;
        // private List<int> highligtedSquare = new();
        private ChessPrinter _chessPrinter;
        private Form1 _form;
        private bool _isRunning = true;
        public GameOfChess(Form1 form)
        {
            _form = form;
            _chessPrinter = new ChessPrinter(_form, board);

            _form.MouseClick += MouseClick;
            _form.KeyPress += KeyPress;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void Play()
        {
            _GameState = GameStates.PlayingMove;
            _chessPrinter.PrintBoard(_selecktedSquare);
        }

        private void KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (_chessPrinter._isPrinting)
                return;


            if (_GameState == GameStates.None)
            {
                switch (e.KeyChar)
                {
                    case ' ':
                        board.UnMakeMove();
                        break;
                    default:
                        break;
                }
            }
        }

        int squareX = 0;
        int squareY = 0;
        private void MouseClick(object? sender, MouseEventArgs e)
        {
            squareX = (int)((float)8 / 800 * (float)e.X);
            squareY = (int)((float)8 / 800 * (float)e.Y);

            switch (_GameState)
            {
                case GameStates.None:
                    break;
                case GameStates.PlayingMove:
                    MakeMove();
                    _chessPrinter.PrintBoard(_selecktedSquare);
                    break;
                case GameStates.PlayingChosingPiece:
                    ChosePromotionPiece();
                    _chessPrinter.PrintBoard(_selecktedSquare);
                    break;
                case GameStates.Settings:
                    break;
                case GameStates.SelectionScreen1:
                    break;
                default:
                    throw new NotImplementedException("This GameState has not been implementet some how");
            }

        }

        private bool makingAMove = false;
        private void MakeMove()
        {
            if (makingAMove)
                return;
            makingAMove = true;
            int pressedSquare = squareX + (squareY * 8);

            if (Piece.IsColour(board[pressedSquare], board.PlayerTurn) && pressedSquare != _selecktedSquare)
                _selecktedSquare = pressedSquare;
            else if (pressedSquare == _selecktedSquare)
                _selecktedSquare = -1;
            else if (_selecktedSquare != -1)
            {
                board.MakeMove(new(_selecktedSquare, pressedSquare, 0));
                _selecktedSquare = -1;
            }


            makingAMove = false;
        }

        // int StartSquareOfPromotionPiece = 0;
        // int TargetSquareOfPromotionPiece = 0;
        private void ChosePromotionPiece()
        {
            if (makingAMove)
                return;
            makingAMove = true;

            // board.MakeMove(new(StartSquareOfPromotionPiece, TargetSquareOfPromotionPiece, Move.Flag.PromoteToQueen, board.board.Square[TargetSquareOfPromotionPiece]));

            _chessPrinter.PrintBoard(_selecktedSquare);
            _selecktedSquare = -1;

            makingAMove = false;
        }
    }
}