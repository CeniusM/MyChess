using ChessV2; // it should be that this is the only change to try difrent boards
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
        PuttingPiece,
        Settings,
        SelectionScreen1
    }
    public class GameOfChess
    {
        private GameStates _GameState = GameStates.None;

        private const int SquareDimensions = 100;
        // private SafeBoard board = new SafeBoard("8/5pq1/4k1p1/1n6/1N6/4K2P/5PQ1/8 w - - 0 1");
        // private SafeBoard board = new SafeBoard("3r3b/b7/5P2/2PPP3/3K2Pq/2PPP3/8/6qk w - - 0 1"); // pines
        private SafeBoard board = new SafeBoard("1k6/p4ppp/1p3p2/8/8/5P2/PPPPP2P/5K2 w - - 0 1"); // pawns / king

        private int _selecktedSquare = -1;
        // private List<int> highligtedSquare = new();
        private ChessPrinter _chessPrinter;
        private Form1 _form;
        // private bool _isRunning = true;
        public GameOfChess(Form1 form)
        {
            _form = form;
            _chessPrinter = new ChessPrinter(_form, board);

            _form.MouseClick += MouseClick;
            _form.KeyPress += KeyPress;
        }

        public void Stop()
        {
            // _isRunning = false;
        }

        public void Play()
        {
            _GameState = GameStates.PlayingMove;
            _chessPrinter.PrintBoard(_selecktedSquare);
        }

        private int searchDepth = 3;
        private void KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (_chessPrinter._isPrinting)
                return;


            if (_GameState == GameStates.PlayingMove)
            {
                switch (e.KeyChar)
                {
                    case ' ':
                        board.UnMakeMove();
                        _chessPrinter.PrintBoard(_selecktedSquare);
                        break;
                    case 'r': // regenerateMoves
                        board.GetPossibleMovesGenerator().GenerateMoves();
                        break;
                    case 'p': // perft
                        // Console.WriteLine(PerftTester.PerftTest.Perft(board.GetUnsafeBoard(), board.GetPossibleMovesGenerator(), searchDepth) + ", Depth: " + searchDepth);
                        PerftTester.PerftTest.Start(board.GetFen(), searchDepth);
                        break;
                    case 'w': // perft
                        searchDepth++;
                        break;
                    case 's': // perft
                        searchDepth--;
                        break;
                    case 'o':
                        _GameState = GameStates.PuttingPiece;
                        break;
                    // default:
                    case 'f': // return "position fen " + fen
                        Console.WriteLine("position fen " + board.GetFen());
                        break;
                    case 'q': // return "position fen " + fen
                        Console.WriteLine("position fen " + board.GetFen() + "\n" + "go perft " + searchDepth);
                        PerftTester.PerftTest.Start(board.GetFen(), searchDepth);
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
                // case GameStates.PlayingChosingPiece:
                // PutPiece();
                // _chessPrinter.PrintBoard(_selecktedSquare);
                case GameStates.Settings:
                    break;
                case GameStates.SelectionScreen1:
                    break;
                default:
                    throw new NotImplementedException("This GameState has not been implementet some how");
            }

        }

        // private void PutPiece()
        // {
        //     int pressedSquare = squareX + (squareY * 8);
        //     _GameState = GameStates.PlayingMove;
        // }

        private bool makingAMove = false;
        private void MakeMove()
        {
            if (makingAMove)
                return;
            makingAMove = true;
            int pressedSquare = squareX + (squareY * 8);

            // MyLib.DebugConsole.WriteLine(BitBoardHelper.GetBitBoardString(0b1111111011111110111111101111111011111110111111101111111011111110));

            if (ChessV1.Piece.IsColour(board[pressedSquare], board.PlayerTurn) && pressedSquare != _selecktedSquare)
                _selecktedSquare = pressedSquare;
            else if (pressedSquare == _selecktedSquare)
                _selecktedSquare = -1;
            else if (_selecktedSquare != -1)
            {
                Move? move = null;

                // get the amount of moves that has the same start and target square
                // if 1, just used that
                // if 4, promotion
                // if 0, it wasent a valid move
                int countOfMoves = 0;
                int moveFlag = 0;
                foreach (Move m in board.GetMoves())
                {
                    if (m.StartSquare == _selecktedSquare && m.TargetSquare == pressedSquare)
                    {
                        countOfMoves++;
                        moveFlag = m.MoveFlag;
                    }
                }

                if (countOfMoves == 4)
                {
                    StartSquareOfPromotionPiece = _selecktedSquare;
                    TargetSquareOfPromotionPiece = pressedSquare;
                    _GameState = GameStates.PlayingChosingPiece;
                }

                if (countOfMoves == 1)
                    move = new(_selecktedSquare, pressedSquare, moveFlag);

                if (move.HasValue)
                    board.MakeMove(move.Value);

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

            board.MakeMove(new(StartSquareOfPromotionPiece, TargetSquareOfPromotionPiece, Move.Flag.PromoteToQueen));

            _chessPrinter.PrintBoard(_selecktedSquare);
            _selecktedSquare = -1;
            _GameState = GameStates.PlayingMove;

            makingAMove = false;
        }
    }
}