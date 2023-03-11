using MyChess;
using winForm;
using MyChess.ChessBoard;
using MyChess.ChessBoard.AIs;
using System.Diagnostics;

/*
Ideers:
Save replay
make an evaluation function to only look at the moves where you can take another piece

*/


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
        //AlphaBetaPruning
        AlphaBetaIterativeDeepening
        //OnlyMinMax1
        // MisterRandom
        ai;


        private const int SquareDimensions = 100;
        // private ChessGame chessGame = new ChessGame("rnbq1k1r/pp1Pbppp/2p5/8/2B1n3/8/PPP1N1PP/RNBQK2R b KQ - 1 8");
        //private ChessGame chessGame = new ChessGame("6k1/1p1qn1r1/1p2R3/3P2pp/1N6/2P4P/P5P1/1R1Q3K b - - 0 1");
        //private ChessGame chessGame = new ChessGame("1r4k1/R1pbb1pp/2p1pp2/2P1P3/3P1P2/5N2/5P1P/6K1 w - - 3 31");
        //private ChessGame chessGame = new ChessGame("r1b2rk1/pp2bppp/2n1pn2/1BP5/2N1pB2/2q2N2/P1P2PPP/1R1Q1RK1 w - - 0 1");
        //private ChessGame chessGame = new ChessGame("r1b1kr2/pppp3p/2n2B2/q7/2B1P3/2P2Q2/P4PPP/R3K2R w KQ - 4 20");
        //private ChessGame chessGame = new ChessGame("8/8/1p6/2kp2PP/7P/5K2/8/8 b - - 0 1");
        //private ChessGame chessGame = new ChessGame("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0");
        //private ChessGame chessGame = new ChessGame("");
        //private ChessGame chessGame = new ChessGame("");
        //private ChessGame chessGame = new ChessGame("");
        //private ChessGame chessGame = new ChessGame("");
        //private ChessGame chessGame = new ChessGame("");
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
                    case 'L': // Load in moves via the console
                        var str = Console.ReadLine();

                        break;
                    case 'P':
                        var moves = chessGame.board.moves.ToArray();
                        for (int i = 0; i < moves.Length; i++)
                            Console.WriteLine(moves[i].ToString());
                        break;
                    case ' ':
                        chessGame.UnMakeMove();
                        _chessPrinter.PrintBoard(_selecktedSquare);
                        break;
                    case 'r':
                        chessGame = new ChessGame();
                        _chessPrinter = new ChessPrinter(_form, chessGame);
                        ai = new(chessGame);
                        _chessPrinter.PrintBoard(_selecktedSquare);
                        break;
                    case 'a':
                        _chessPrinter.PrintBoard(_selecktedSquare);
                        AIMoveStart(true);
                        break;
                    case 'o':
                    case 'O':
                        statsAllreadyRenderd = false;
                        gamesToPlay = 100;
                        gamesPlayed = 0;
                        player1Win = 0;
                        player2Win = 0;
                        bool done1 = false;
                        bool done2 = false;
                        bool done3 = false;
                        bool done4 = false;
                        Task.Run(() => AIDuel(ref done1, true));
                        Task.Run(() => AIDuel(ref done2, false));
                        Task.Run(() => AIDuel(ref done3, false));
                        Task.Run(() => AIDuel(ref done4, false));
                        while (!done1 || !done2 || !done3 || !done4)
                            Thread.Sleep(100);
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
                    case 'S':
                            ai.StopClock();
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
            if (e.X > 800 || e.X < 0 || e.Y > 800 || e.Y < 0)
            {
                _selecktedSquare = -1;
                return;
            }

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
                    Console.WriteLine("AI Thinking");
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
            Console.WriteLine(board.HashKey);
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
        private void AIMoveStart(bool overrideSetting = false)
        {
            if (!Settings.Game.PlayingAI && !overrideSetting)
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
            var sw = Stopwatch.StartNew();
            var move = ai.GetMove();
            chessGame.MakeMove(move);
            //Console.WriteLine("Move took: " + sw.Elapsed.TotalMilliseconds + "ms " + " Move: " + move);
            _chessPrinter.PrintBoard(_selecktedSquare);
            _GameState = GameStates.PlayingMove;
            AIThinking = false;
        }


        bool statsAllreadyRenderd;
        int gamesToPlay;
        int gamesPlayed;
        int player1Win;
        int player2Win;
        private void AIDuel(ref bool done, bool renderboard = false)
        {
            int _50RuleThingy = 0;
            _GameState = GameStates.AIDueling;
            // ChessGame cg = new("r2qk2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/5N2/Pp1P2PP/R2Q1RK1 w kq - 0 1");
            ChessGame cg = new();
            ChessPrinter cp = new(_form, cg);
            PureAlphaBetaPruning ai1 = new(cg); // white
            AlphaBetaPruning ai2 = new(cg); // black

            //PureAlphaBetaPruning ai1SideKick = new(cg);
            //AlphaBetaPruning ai2SideKick = new(cg);

            // MisterRandom ai2 = new(cg);
            // AlphaBetaPruning ai2 = new(cg);






            // starts of with 2 random moves for each couse its all detemenistik
            int randomMoves = 2;
            var r = new Random();
            for (int i = 0; i < randomMoves; i++)
                cg.MakeMove(cg.GetPossibleMoves()[r.Next(0, cg.GetPossibleMoves().Count())]);









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
                if (renderboard)
                    cp.PrintBoard(-1);
                Move move1 = ai1.GetMove();
                _50RuleThingy++;
                if (move1.CapturedPiece != 0)
                    _50RuleThingy = 0;
                if (_50RuleThingy == 50)
                {
                    same = true;
                    break;
                }
                // Move move2 = ai1SideKick.GetMove();


                // if (move1 != move2)
                //     MyLib.DebugConsole.WriteLine("Move Failed\n" + move1.ToString() + "\n" + move2.ToString());

                cg.MakeMove(move1);
                if (cg.GetPossibleMoves().Count == 0)
                    break;
                if (AddAndDeteckt())
                    break;


                move1 = ai2.GetMove();
                _50RuleThingy++;
                if (move1.CapturedPiece != 0)
                    _50RuleThingy = 0;
                if (_50RuleThingy == 50)
                {
                    same = true;
                    break;
                }
                // move2 = ai2SideKick.GetMove();

                if (renderboard)
                    cp.PrintBoard(-1);
                cg.MakeMove(move1);
                if (cg.GetPossibleMoves().Count == 0)
                    break;
                // if (AddAndDeteckt())
                //     break;

            }
            if (renderboard)
                cp.PrintBoard(-1);
            // Thread.Sleep(5000);
            //if (same)
            //    //MyLib.DebugConsole.WriteLine("Draw due to repetition");
            //    Console.WriteLine("Draw due to repetition");
            //else if (cg.evaluator.EvaluateBoardLight(0) == 0)
            //    //MyLib.DebugConsole.WriteLine("Draw");
            //    Console.WriteLine("Draw");
            //else
            //    //MyLib.DebugConsole.WriteLine("Winner: " + ((cg.evaluator.EvaluateBoardLight(0) == int.MaxValue) ? "White" : "Black"));
            //    Console.WriteLine("Winner: " + ((cg.evaluator.EvaluateBoardLight(0) == int.MaxValue) ? "White" : "Black"));

            if (same)
                Console.WriteLine("Draw due to repetition");
            else if (cg.evaluator.EvaluateBoardLight(0) == 0)
                MyLib.DebugConsole.WriteLine("Draw");
            else if (cg.evaluator.EvaluateBoardLight(0) == int.MaxValue)
            {
                player1Win++;
                Console.WriteLine("Winner: " + "White");
            }
            else if (cg.evaluator.EvaluateBoardLight(0) == int.MinValue)
            {
                player2Win++;
                Console.WriteLine("Winner: " + "Black");
            }

            gamesPlayed++;

            gamesToPlay--;
            if (gamesToPlay < 1)
            {
                if (!statsAllreadyRenderd)
                {
                    statsAllreadyRenderd = true;
                    //MyLib.DebugConsole.WriteLine("Player 1: " + player1Win + "/" + gamesPlayed + " Won");
                    //MyLib.DebugConsole.WriteLine("Player 2: " + player2Win + "/" + gamesPlayed + " Won");
                    Console.WriteLine("Player 1: " + player1Win + "/" + gamesPlayed + " Won");
                    Console.WriteLine("Player 2: " + player2Win + "/" + gamesPlayed + " Won");
                    _GameState = GameStates.PlayingMove;
                }
                done = true;
                return;
            }
            if (gamesToPlay > 0)
                AIDuel(ref done, renderboard);
        }
    }
}