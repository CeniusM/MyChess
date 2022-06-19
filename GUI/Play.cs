using MyChess;
using winForm;
using MyChess.ChessBoard;
using System.Media;
using MyChessGUI.Sound;

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

        // Proof of consept, anoying sound tho, and make it not a complete path
        private MySound sounds = new MySound(@"C:\GitHub\MyChess\GUI\MySound\Sounds\Click.wav");
        private Random rnd = new Random();
        private Form1 _form;
        public GameOfChess(Form1 form)
        {
            chessGame = new ChessGame("7k/4k3/8/8/8/2K3K1/8/K7 w - - 0 1");

            chessAPI = new ChessAPI(form, chessGame);
            _form = form;

            _squareDimensions[0] = 100;//form.Height / 8;
            _squareDimensions[1] = 100;//form.Width / 8;
            _selecktedSquare = -1;

            //int renderDirections = Directions.DirectionValues[0, 0]; // just makes it so its loaded

            _form.MouseClick += MouseClick;
            _form.KeyPress += KeyPress;
        }

        public void Play()
        {
            // just to run the static method in the Directions class
            //int foo = Chess.Moves.Directions.DirectionValues[0, 0];

            _isRunning = true;

            chessAPI.PrintBoard();

            // while (_isRunning)
            //     Thread.Sleep(1000);
        }

        public void Stop() => _isRunning = false;

        private void KeyPress(object? sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 'r') // rePrints the whole board
            //{
            //    chessAPI.PrintBoard();
            //}
            //else if (e.KeyChar == 'o') // Resests the board
            //{
            //    chessGame.StartOver();
            //    chessAPI.PrintBoard();
            //    _selecktedSquare = -1;
            //}
            //else if (e.KeyChar == 's') // Saves board
            //{
            //    CS_MyConsole.MyConsole.WriteLine(MyFEN.GetFENFromBoard(chessGame._board));
            //}
            //else if (e.KeyChar == '1')
            //{
            //    chessGame = new ChessGame("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            //    chessAPI = new ChessAPI(_form, chessGame);
            //    chessAPI.PrintBoard();
            //}
            //else if (e.KeyChar == '2')
            //{
            //    chessGame = new ChessGame("rnb1kbnr/ppp1pppp/8/q7/8/2N5/PPPP1PPP/R1BQKBNR b KQ - 0 1");
            //    chessAPI = new ChessAPI(_form, chessGame);
            //    chessAPI.PrintBoard();
            //}
            //else if (e.KeyChar == '3')
            //{
            //    chessGame = new ChessGame("1k2r3/1p3r2/pN4p1/3p4/1R2n2P/4Q1q1/PPP3P1/2K1R3 b - - 1 1");
            //    chessAPI = new ChessAPI(_form, chessGame);
            //    chessAPI.PrintBoard();
            //}
//
            //else if (e.KeyChar == '0') // reads the last line in MyConsole and takes it in as a fen string
            //{
            //    chessGame = new ChessGame(CS_MyConsole.MyConsole.ReadLastLine());
            //    chessAPI = new ChessAPI(_form, chessGame);
            //    chessAPI.PrintBoard();
            //}
//
            //else if (e.KeyChar == 'l') // reads the last line in MyConsole and takes it in as a fen string
            //{
            //    if (chessGame.gameMoves.Count != 0)
            //    {
            //        chessGame.UnmakeMove();
            //        chessAPI.PrintBoard();
            //    }
//
            //}
        }


        int squareX = 0;
        int squareY = 0;
        private async void MouseClick(object? sender, MouseEventArgs e)
        {
            squareX = (int)((float)8 / 800 * (float)e.X);
            squareY = (int)((float)8 / 800 * (float)e.Y);

            await Task.Run(() => MakeMove(squareX, squareY));

            // await Task.Run(() => AIPlay());
        }

        bool makingAMove = false;
        private async void MakeMove(int x, int y)
        {
            if (makingAMove)
                return;
            makingAMove = true;

            int pressedSquare = x + (y * 8);
            if (_selecktedSquare == -1)
            {
                if ((chessGame.board[pressedSquare] & Piece.ColorBits) == chessGame.board.playerTurn)
                {
                    _selecktedSquare = pressedSquare;
                }
            }
            else if (pressedSquare == _selecktedSquare) // if pressed the same square
            {
                _selecktedSquare = -1;
            }
            else
            {
                List<Move> moves = chessGame.GetPossibleMoves();
                int movesCount = 0;
                for (var i = 0; i < moves.Count(); i++)
                {
                    if (moves[i].StartSquare == _selecktedSquare && moves[i].TargetSquare == pressedSquare)
                        movesCount++;
                }

                if (movesCount == 0)
                {
                    if ((chessGame.board[pressedSquare] & Piece.ColorBits) == chessGame.board.playerTurn)
                    {
                        _selecktedSquare = pressedSquare;
                    }
                    else
                    {
                        _selecktedSquare = -1;
                    }
                }
                if (movesCount == 1)
                {
                    chessGame.MakeMove(moves[0]);

                    _selecktedSquare = -1;
                }
                else if (movesCount == 4)
                {
                    int promotionPiece = await Task.Run(() => GetChosenPromotionPiece());

                    if (promotionPiece == 0)
                        chessGame.MakeMove(new Move(_selecktedSquare, pressedSquare, Move.Flag.PromoteToQueen));
                    else if (promotionPiece == 1)
                        chessGame.MakeMove(new Move(_selecktedSquare, pressedSquare, Move.Flag.PromoteToRook));
                    else if (promotionPiece == 2)
                        chessGame.MakeMove(new Move(_selecktedSquare, pressedSquare, Move.Flag.PromoteToBishop));
                    else if (promotionPiece == 3)
                        chessGame.MakeMove(new Move(_selecktedSquare, pressedSquare, Move.Flag.PromoteToKnight));

                    _selecktedSquare = -1;
                }
            }

            chessAPI.PrintBoard(_selecktedSquare);

            makingAMove = false;
        }

        private int GetChosenPromotionPiece()
        {


            return 0;
        }

        private bool AIRunning = false;
        private void AIPlay()
        {
            if (AIRunning)
                return;

            AIRunning = true;
            while (_isRunning)
            {
                const int timePerMove = 100;
                if (chessGame.board.playerTurn == Piece.White) // play vs completly random ai
                {
                    List<Move> moves = chessGame.GetPossibleMoves();
                    chessGame.MakeMove(moves[rnd.Next(0, moves.Count)]);
                    chessAPI.PrintBoard(_selecktedSquare);
                    Thread.Sleep(timePerMove);
                }
                else if (chessGame.board.playerTurn == Piece.Black) // play vs completly random ai
                {
                    List<Move> moves = chessGame.GetPossibleMoves();
                    chessGame.MakeMove(moves[rnd.Next(0, moves.Count)]);
                    chessAPI.PrintBoard(_selecktedSquare);
                    Thread.Sleep(timePerMove);
                }
                sounds.PlaySound();
            }

            AIRunning = false;
        }

        private int GetIndexOfMove(int startSquare, int targetSquare)
        {
            List<Move> moves = chessGame.GetPossibleMoves();

            for (int i = 0; i < moves.Count; i++)
            {
                if (moves[i].StartSquare == startSquare && targetSquare == moves[i].TargetSquare)
                    return i;
            }

            throw new Exception("dosent excist");
        }
    }
}