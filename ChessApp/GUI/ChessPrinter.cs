using ChessV2;
using winForm;

namespace ChessGUI
{
    // gets the instruction from the chess game and gives the FormGUI instructions on what to print
    public unsafe class ChessPrinter
    {
        public int depthOfEval = 2;
        private FormAPI _formGUI;
        private List<Bitmap> _sprites;
        private UnsafeBoard board;
        private PossibleMovesGenerator pmg;
        private Form1 _form;
        public bool _isPrinting = false;
        public ChessPrinter(Form1 form, SafeBoard board)
        {
            _formGUI = new FormAPI(form);
            this.board = board.GetUnsafeBoard();
            pmg = board.GetPossibleMovesGenerator();
            _sprites = Sprites.SpriteFetcher.GetSprites(Settings.Dimensions.PieceWidth, Settings.Dimensions.PieceHeight);
            _form = form;
            form.Paint += (s, e) => PrintBoardAgain();
        }

        public void PrintBoardAgain() => _formGUI.Print();

        public void PrintBoard(int selecktedPiece, bool useMiniMaxEval = false)
        {
            // useMiniMaxEval = false; // DOSENT WORK


            if (_isPrinting)
                return;
            _isPrinting = true;
            DrawFeild();

            DrawPossibleMoves(selecktedPiece);

            DrawPieces();

            PrintEvalBar(useMiniMaxEval);

            _formGUI.Print();
            _isPrinting = false;
        }

        private void DrawFeild(bool drawFieldBonuses = false)
        {
            // Move lastMove = new Move(-1, -1, -1, -1);
            // if (board.moves.Count != 0)
            //     lastMove = board.moves.Peek();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {




                    int pos = i + (j * 8);
                    // if (board.Square[pos] != 0 && drawFieldBonuses)
                    // {

                    //     if (PiecePosesBonus.PieceBonuses[board.Square[pos], pos] > 0)
                    //         _formGUI.DrawSquare(i * 100, j * 100, 100, 100,
                    //         Color.FromArgb(255, 0, 0, (int)(((float)PiecePosesBonus.PieceBonuses[board.Square[pos], pos] / (float)350) * 255f * 2.5f)));
                    //     else
                    //         _formGUI.DrawSquare(i * 100, j * 100, 100, 100,
                    //         Color.FromArgb(255, Math.Abs((int)(((float)PiecePosesBonus.PieceBonuses[board.Square[pos], pos] / (float)350) * 255f) * 4), 0, 0));
                    // }



                    // else
                    if ((i + j) % 2 == 0)
                    {
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Settings.Colors.LightSquare);
                    }
                    else
                    {
                        _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Settings.Colors.DarkSquare);
                    }

                    // if (pos == lastMove.StartSquare || pos == lastMove.TargetSquare)
                    // {
                    //     // maby draw the piece behind so it shows like a ghost behind
                    //     _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.FromArgb(100, 100, 100, 100));

                    // }
                }
            }
        }

        private void DrawPossibleMoves(int selecktedPiece)
        {
            if (selecktedPiece == -1)
                return;
            foreach (Move move in pmg.GetMoves())
            {
                if (move.StartSquare != selecktedPiece)
                    continue;
                int x = move.TargetSquare % 8;
                int y = move.TargetSquare >> 3;

                _formGUI.DrawSquare(x * 100, y * 100, 100, 100,
                ((x + y) % 2 == 0) ? Settings.Colors.LightPossibleMoveSquare : Settings.Colors.DarkPossibleMoveSquare);
            }
        }

        // private void DrawPieces()
        // {
        //     int x, y;
        //     void PrintWSprite(int sprite) => _formGUI.DrawBitmap(_sprites[sprite], x * 100, y * 100);
        //     void PrintBSprite(int sprite) => _formGUI.DrawBitmap(_sprites[sprite + 8], x * 100, y * 100);

        //     x = board.kingPosPtr[0] % 8; y = board.kingPosPtr[0] >> 3;
        //     PrintWSprite(Piece.King);
        //     x = board.kingPosPtr[1] % 8; y = board.kingPosPtr[1] >> 3;
        //     PrintBSprite(Piece.King);

        //     for (int i = 0; i < board.allPieceLists.Length; i++)
        //     {
        //         for (int j = 0; j < board.allPieceLists[i].numPieces; j++)
        //         {
        //             int pos = board.allPieceLists[i][j];
        //             int piece = board.square[pos];
        //             x = pos % 8;
        //             y = pos >> 3;

        //             if ((piece & Piece.White) == Piece.White)
        //                 PrintWSprite((piece & 0b111));
        //             else if ((piece & Piece.Black) == Piece.Black)
        //                 PrintBSprite((piece & 0b111));
        //             else
        //             {
        //                 MyLib.DebugConsole.WriteLine("pos: " + pos + ", type: " + (piece & 0b111) + ", colour: " +
        //                 (((piece & 0b11000) == 0) ? "Nothing" : (((piece & 0b11000) == 8) ? "White" : "Black"))); // colour
        //                 throw new NotImplementedException("Cant use Piceses with no color value");
        //             }
        //         }
        //     }
        // }

        private void DrawPieces()
        {
            int x, y;
            void PrintWSprite(int sprite) => _formGUI.DrawBitmap(_sprites[sprite], x * 100, y * 100);
            void PrintBSprite(int sprite) => _formGUI.DrawBitmap(_sprites[sprite + 8], x * 100, y * 100);

            x = board.kingPosPtr[0] % 8; y = board.kingPosPtr[0] >> 3;
            PrintWSprite(Piece.King);
            x = board.kingPosPtr[1] % 8; y = board.kingPosPtr[1] >> 3;
            PrintBSprite(Piece.King);

            for (int i = 0; i < 64; i++)
            {
                if (board.boardPtr[i] == 0)
                    continue;
                int pos = i;
                int piece = board.boardPtr[i];
                x = pos % 8;
                y = pos >> 3;

                if ((piece & Piece.White) == Piece.White)
                    PrintWSprite((piece & 0b111));
                else if ((piece & Piece.Black) == Piece.Black)
                    PrintBSprite((piece & 0b111));
                else
                {
                    MyLib.DebugConsole.WriteLine("pos: " + pos + ", type: " + (piece & 0b111) + ", colour: " +
                    (((piece & 0b11000) == 0) ? "Nothing" : (((piece & 0b11000) == 8) ? "White" : "Black"))); // colour
                    throw new NotImplementedException("Cant use Piceses with no color value");
                }
            }
        }


        public void PrintEvalBar(bool useMiniMaxEval = false)
        {
            // _formGUI.DrawSquare(Settings.Dimensions.ScreenWidth - Settings.Dimensions.EvalBarWidth, 0, Settings.Dimensions.ScreenHeight, Settings.Dimensions.EvalBarWidth, Color.White);

            // float evaluation = evaluator.EvaluateBoardLight(board.GetPossibleMoves().Count) / 300f; // idk 
            // float evalHeight = MyMath.LogisticCurve((float)evaluation, 30, 0.3f, 15); // returs a num between -15 and 15
            // int evalHeightpx = (int)((-evalHeight / 15f + 1) * 400f); // make eval height into a range between 0, 800
            // _formGUI.DrawSquare(800, 0, evalHeightpx, 100, Color.Black);
        }
    }
}