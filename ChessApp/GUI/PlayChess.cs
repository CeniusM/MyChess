using MyChess;
using winForm;
using MyChess.ChessBoard;
using MyChess.ChessBoard.AIs;

namespace MyChessGUI
{
    public enum GameStates
    {
        None = 0,
        PlayingMove,
        PlayingChosingPiece,
        AIPlaying,
        Settings,
        SelectionScreen1
    }
    public class GameOfChess
    {
        private GameStates _GameState = GameStates.None;

        // ai used
        private OnlyMinMax ai;


        private const int SquareDimensions = 100;
        private ChessGame chessGame = new ChessGame();
        private int _selecktedSquare = -1;
        // private List<int> highligtedSquare = new();
        private ChessPrinter _chessPrinter;
        private Form1 _form;
        private bool _isRunning = true;
        public GameOfChess(Form1 form)
        {
            _form = form;
            _chessPrinter = new ChessPrinter(_form, chessGame);
            ai = new(chessGame);

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
            if (_GameState != GameStates.AIPlaying)
            {
                switch (e.KeyChar)
                {
                    case ' ':
                        chessGame.UnMakeMove();
                        _chessPrinter.PrintBoard(_selecktedSquare);
                        break;
                    case 'r':
                        AIMove();
                        _chessPrinter.PrintBoard(_selecktedSquare);
                        break;
                    default:
                        break;
                }
            }
            else // CANT TOUCH THE BOARD
            {
                switch (e.KeyChar)
                {
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
                    if (!AIThinking)
                        _chessPrinter.PrintBoard(_selecktedSquare);
                    break;
                case GameStates.PlayingChosingPiece:
                    ChosePromotionPiece();
                    if (!AIThinking)
                        _chessPrinter.PrintBoard(_selecktedSquare);
                    break;
                case GameStates.AIPlaying:
                    // some kind of options while the ai is playing or thinking
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
            Board board = chessGame.board;
            makingAMove = true;
            int pressedSquare = squareX + (squareY * 8);

            // seleckt a piece
            if ((board.Square[pressedSquare] & Piece.ColorBits) == board.playerTurn)
            {
                _selecktedSquare = pressedSquare;
            }

            // move seleckted piece
            else if (_selecktedSquare != -1)
            {
                var moves = chessGame.GetPossibleMoves();
                Move? move = null;
                for (int i = 0; i < moves.Count; i++)
                {
                    if (moves[i].StartSquare == _selecktedSquare && moves[i].TargetSquare == pressedSquare)
                    {
                        move = moves[i];
                        break;
                    }
                }

                if (!move.HasValue)
                {
                    makingAMove = false;
                    return;
                }

                if (move.Value.MoveFlag > 3) // means it promote
                {
                    makingAMove = false;
                    StartSquareOfPromotionPiece = move.Value.StartSquare;
                    TargetSquareOfPromotionPiece = move.Value.TargetSquare;
                    _GameState = GameStates.PlayingChosingPiece;
                    return;
                }

                chessGame.MakeMove(move.Value);
                _GameState = GameStates.AIPlaying;
                _chessPrinter.PrintBoard(_selecktedSquare);
                AIThinking = true;
                Task.Run(AIMove);
                _selecktedSquare = -1;
            }

            makingAMove = false;
        }

        int StartSquareOfPromotionPiece = 0;
        int TargetSquareOfPromotionPiece = 0;
        private void ChosePromotionPiece()
        {
            if (makingAMove)
                return;
            makingAMove = true;

            chessGame.MakeMove(new(StartSquareOfPromotionPiece, TargetSquareOfPromotionPiece, Move.Flag.PromoteToQueen, chessGame.board.Square[TargetSquareOfPromotionPiece]));
            _GameState = GameStates.AIPlaying;
            _chessPrinter.PrintBoard(_selecktedSquare);
            AIThinking = true;
            Task.Run(AIMove);

            makingAMove = false;
        }


        private bool AIThinking = false;
        private void AIMove()
        {
            chessGame.MakeMove(ai.GetMove());
            _GameState = GameStates.PlayingMove;

            _chessPrinter.PrintBoard(_selecktedSquare);
            AIThinking = false;
        }
    }
}