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
        AIDueling,
        Settings,
        SelectionScreen1
    }
    public class GameOfChess
    {
        private GameStates _GameState = GameStates.None;

        // ai used
        private
        OnlyMinMax
        // MisterRandom
        ai;


        private const int SquareDimensions = 100;
        // private ChessGame chessGame = new ChessGame("rnbq1k1r/pp1Pbppp/2p5/8/2B1n3/8/PPP1N1PP/RNBQK2R b KQ - 1 8");
        // private ChessGame chessGame = new ChessGame("8/8/8/8/7k/8/5r2/7K w - - 3 2");
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

        private async void KeyPress(object? sender, KeyPressEventArgs e)
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
                        chessGame = new ChessGame();
                        _chessPrinter.PrintBoard(_selecktedSquare);
                        break;
                    case 'a':
                        _chessPrinter.PrintBoard(_selecktedSquare);
                        AIMoveStart();
                        break;
                    case 'o':
                        await Task.Run(AIDuel);
                        _chessPrinter.PrintBoard(_selecktedSquare);
                        break;
                    case 'g':
                        chessGame.possibleMoves.GenerateMoves();
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
                    MyLib.DebugConsole.WriteLine("AI Thinking");
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
                    _selecktedSquare = -1;
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
                AIMoveStart();
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
            AIMoveStart();
            _selecktedSquare = -1;

            makingAMove = false;
        }


        private bool AIThinking = false;
        private void AIMoveStart()
        {
            if (!Settings.Game.PlayingAI)
            {
                _GameState = GameStates.PlayingMove;
                return;
            }
            if (AIThinking)
                return;
            AIThinking = true;
            _GameState = GameStates.AIPlaying;

            _chessPrinter.PrintBoard(_selecktedSquare);
            Task.Run(AIThinkingOfMove);
            // Task.WaitAll(Task.Run(() => chessGame.MakeMove(ai.GetMove())));
        }

        private void AIThinkingOfMove()
        {
            chessGame.MakeMove(ai.GetMove());
            _chessPrinter.PrintBoard(_selecktedSquare);
            _GameState = GameStates.PlayingMove;
            AIThinking = false;
        }


        int gamesToPlay = 100;
        int gamesPlayed = 0;
        int player1Win = 0;
        int player2Win = 0;
        private void AIDuel()
        {
            _GameState = GameStates.AIDueling;
            // ChessGame cg = new("r2qk2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/5N2/Pp1P2PP/R2Q1RK1 w kq - 0 1");
            ChessGame cg = new();
            ChessPrinter cp = new(_form, cg);
            OnlyMinMax ai1 = new(cg);
            MisterRandom ai2 = new(cg);
            // AlphaBetaPruning ai2 = new(cg);



            //repetetion detecktiond
            int gamesPtr = 0;
            int[,] games = new int[6, 64]; // first: the amount of saved games, second: game
            bool same = false;
            bool AddAndDeteckt()
            {
                gamesPtr++;
                if (gamesPtr == 6)
                    gamesPtr = 0;
                for (int i = 0; i < 64; i++)
                    games[gamesPtr, i] = cg.board.Square[i];


                same = false;
                int sameSquares1 = 0;
                int sameSquares2 = 0;
                for (int i = 0; i < 64; i++)
                {
                    if (games[0, i] == games[2, i] && games[2, i] == games[4, i]) sameSquares1++;
                    if (games[1, i] == games[3, i] && games[3, i] == games[5, i]) sameSquares2++;
                    // if (games[1, i] == games[3, i] && games[3, i] == games[5, i]) same = false;
                }

                if (sameSquares1 == 64 && sameSquares2 == 64)
                    same = true;
                return same;
            }

            while (true)
            {
                cp.PrintBoard(-1);
                cg.MakeMove(ai1.GetMove());
                if (cg.GetPossibleMoves().Count == 0)
                    break;
                if (AddAndDeteckt())
                    break;

                cp.PrintBoard(-1);
                cg.MakeMove(ai2.GetMove());
                if (cg.GetPossibleMoves().Count == 0)
                    break;
                // if (AddAndDeteckt())
                //     break;

            }
            cp.PrintBoard(-1);
            // Thread.Sleep(5000);
            if (same)
                MyLib.DebugConsole.WriteLine("Draw due to repetition");
            else if (cg.evaluator.EvaluateBoardLight(0) == 0)
                MyLib.DebugConsole.WriteLine("Draw");
            else
                MyLib.DebugConsole.WriteLine("Winner: " + ((cg.evaluator.EvaluateBoardLight(0) == int.MaxValue) ? "White" : "Black"));

            if (cg.evaluator.EvaluateBoardLight(0) == int.MaxValue)
                player1Win++;
            else if (cg.evaluator.EvaluateBoardLight(0) == int.MinValue)
                player2Win++;

            gamesPlayed++;

            gamesToPlay--;
            if (gamesToPlay == 0)
            {
                MyLib.DebugConsole.WriteLine("Player 1: " + player1Win + "/" + gamesToPlay + " Won");
                _GameState = GameStates.PlayingMove;
                return;
            }
            AIDuel();
        }
    }
}