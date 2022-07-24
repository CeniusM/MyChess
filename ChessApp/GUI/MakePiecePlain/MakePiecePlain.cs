using MyChess.ChessBoard;
using winForm;
using MyChess;
using CS_Math;
using MyChess.ChessBoard.Evaluators;
using MyChess.PossibleMoves;
using MyChess.ChessBoard.AIs;

using MyChess.ChessBoard.Evaluators.Methods;


namespace MyChessGUI
{
    // gets the instruction from the chess game and gives the FormGUI instructions on what to print
    public class MakePiecePlain
    {
        private FormAPI _formGUI;
        private List<Bitmap> _sprites;
        private Form1 _form;
        // private bool _isPrinting = false;
        public MakePiecePlain(Form1 form)
        {
            _formGUI = new FormAPI(form);
            _sprites = MyChessGUI.Sprites.SpriteFetcher.GetSprites(Settings.Dimensions.PieceWidth, Settings.Dimensions.PieceHeight);
            _form = form;
            form.Paint += (s, e) => PrintScreen();
            _form.MouseClick += MouseClick;
            _form.KeyPress += KeyPress;
        }

        int currentBonus = 0;
        int[] valueBonuses =
        {
            0,
            50,
            100,
            150,
            200,
            250,
            300,
            350
        };


        // int[] currenPlain = new int[64];
        int[] currenPlain =
        {
                    0,0,0,0,0,0,0,0
                ,5,10,10,-20,-20,10,10,5
                ,5,-5,-10,0,0,-10,-5,5
                ,0,0,0,20,20,0,0,0
                ,5,5,10,25,25,10,5,5
                ,10,10,20,30,30,20,10,10
                ,50,50,50,50,50,50,50,50
                ,0,0,0,0,0,0,0,0
        };

        int myIndex = 0;
        private void KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 's')
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        int pos = j + (i * 8);
                        if (j != 7)
                            MyLib.DebugConsole.Write(currenPlain[pos] + ",");
                        else
                            MyLib.DebugConsole.WriteLine(currenPlain[pos] + "");
                    }
                    if (i != 7)
                        MyLib.DebugConsole.Write(",");
                }
                MyLib.DebugConsole.WriteLine("-------------------------------------------------");
            }
            if (e.KeyChar == 'f')
            {
                int[] Fliped = new int[64];

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        Fliped[x + ((7 - y) * 8)] = currenPlain[x + (y * 8)];
                    }
                }
                for (int i = 0; i < 64; i++)
                    currenPlain[i] = Fliped[i];
            }
            if (e.KeyChar == 'n')
            {
                for (int i = 0; i < 64; i++)
                    currenPlain[i] = PiecePosesBonus.PieceBonuses[myIndex, i];
                myIndex++;
                if (myIndex == PiecePosesBonus.PieceBonuses.GetLength(1))
                    myIndex = 0;
            }
            PrintScreen();
        }

        int squareX = 0;
        int squareY = 0;
        int pressedSquare = 0;
        private void MouseClick(object? sender, MouseEventArgs e)
        {
            squareX = (int)((float)8 / 800 * (float)e.X);
            squareY = (int)((float)8 / 800 * (float)e.Y);
            pressedSquare = squareX + (squareY * 8);

            if (pressedSquare > 63 || pressedSquare < 0)
                return;

            currenPlain[pressedSquare] += 5;
            if (currenPlain[pressedSquare] > 50)
                currenPlain[pressedSquare] = 0;

            MyLib.DebugConsole.WriteLine(currenPlain[pressedSquare] + "");
            PrintScreen();
        }

        public async Task PrintScreen()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        int place = currenPlain[i + (j * 8)];
                        if (place > 100)
                            _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.DarkRed);
                        else if (place == 0)
                            _formGUI.DrawSquare(i * 100, j * 100, 100, 100, Color.Black);
                        else if (place < 0)
                        {
                            place = Math.Abs(place);
                            _formGUI.DrawSquare(i * 100, j * 100, 100, 100,
                            // Color.FromArgb(255, 0, (351 / place), (351 / place)));
                            Color.FromArgb(255, (int)(((float)place / (float)50) * 255f), 0, 0));
                            // MyLib.DebugConsole.WriteLine(((int)((float)place / (float)100) * 255) + " " + place);
                        }

                        else
                        {
                            _formGUI.DrawSquare(i * 100, j * 100, 100, 100,
                            // Color.FromArgb(255, 0, (351 / place), (351 / place)));
                            Color.FromArgb(255, 0, 0, (int)(((float)place / (float)50) * 255f)));
                            // MyLib.DebugConsole.WriteLine(((int)((float)place / (float)100) * 255) + " " + place);
                        }

                    }
                }
                _formGUI.Print();
            });
        }

        public void Start()
        {

        }
    }
}