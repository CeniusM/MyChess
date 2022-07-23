using MyChess.ChessBoard;
using winForm;
using MyChess;
using CS_Math;
using MyChess.ChessBoard.Evaluators;
using MyChess.PossibleMoves;

namespace MyChessGUI
{
    // gets the instruction from the chess game and gives the FormGUI instructions on what to print
    public class ChessPrinter
    {
        private FormAPI _formGUI;
        private List<Bitmap> _sprites;
        private ChessGame chessGame;
        private Form1 _form;
        // private bool _isPrinting = false;
        public ChessPrinter(Form1 form, ChessGame chessGame)
        {
            _formGUI = new FormAPI(form);
            this.chessGame = chessGame;
            _sprites = MyChessGUI.Sprites.SpriteFetcher.GetSprites(Settings.Dimensions.PieceWidth, Settings.Dimensions.PieceHeight);
            _form = form;
            form.Paint += (s, e) => PrintBoardAgain();
        }

        public void PrintBoardAgain() => _formGUI.Print();

        public void PrintBoard(int selecktedPiece)
        {
            DrawFeild();

            DrawPossibleMoves(selecktedPiece);

            DrawPieces();

            PrintEvalBar();

            _formGUI.Print();
        }

        private void DrawFeild()
        {
            Move lastMove = chessGame.lastMove;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Settings.Colors.LightSquare);
                    }
                    else
                    {
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Settings.Colors.DarkSquare);
                    }
                }
            }
        }

        private void DrawPossibleMoves(int selecktedPiece)
        {
            if (selecktedPiece == -1)
                return;
            foreach (Move move in chessGame.GetPossibleMoves())
            {
                if (move.StartSquare != selecktedPiece)
                    continue;
                int x = move.TargetSquare % 8;
                int y = move.TargetSquare >> 3;

                _formGUI.DrawSquare(x * 100, y * 100, 100, 100,
                ((x + y) % 2 == 0) ? Settings.Colors.LightPossibleMoveSquare : Settings.Colors.DarkPossibleMoveSquare);
            }
        }

        private void DrawPieces()
        {
            int x, y;
            void PrintWSprite(int sprite) => _formGUI.DrawBitmap(_sprites[(sprite - 1) << 1], x * 100, y * 100);
            void PrintBSprite(int sprite) => _formGUI.DrawBitmap(_sprites[((sprite - 1) << 1) + 1], x * 100, y * 100);

            for (int i = 0; i < chessGame.board.piecePoses.Count; i++)
            {
                int pos = chessGame.board.piecePoses[i];
                int piece = chessGame.board.Square[pos];
                x = pos % 8;
                y = pos >> 3;

                if ((piece & Piece.White) == Piece.White)
                    PrintWSprite((piece & 0b111));
                else if ((piece & Piece.Black) == Piece.Black)
                    PrintBSprite((piece & 0b111));
                else
                    throw new NotImplementedException("Cant use Piceses with no color value");
            }
        }


        public void PrintEvalBar()
        {
            _formGUI.DrawSquare(Settings.Dimensions.ScreenWidth - Settings.Dimensions.EvalBarWidth, 0, Settings.Dimensions.ScreenHeight, Settings.Dimensions.EvalBarWidth, Color.White);

            float evaluation = chessGame.evaluator.EvaluateBoardLight(chessGame.GetPossibleMoves().Count) / 300f; // idk 

            float evalHeight = MyMath.LogisticCurve((float)evaluation, 30, 0.3f, 15); // returs a num between -15 and 15

            int evalHeightpx = (int)((-evalHeight / 15f + 1) * 400f); // make eval height into a range between 0, 800

            _formGUI.DrawSquare(800, 0, evalHeightpx, 100, Color.Black);
        }
    }
}