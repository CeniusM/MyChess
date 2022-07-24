using System.Runtime.InteropServices;
using System.Diagnostics;
using MyChess.ChessBoard;
using MyChess;
using MyChess.PossibleMoves;

using MyChessGUI;

namespace winForm;

static class Program
{
    private static GameOfChess? game;
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        Form1 myForm = new Form1();

        var gameThread = new Thread(() => StartGame(myForm));

        myForm.Shown += (s, e) => gameThread.Start();
        myForm.FormClosing += (s, e) =>
        {
            game?.Stop();
            gameThread.Join();
        };

        // myForm.Load += Form1Load;

        // var FileWriter = new Thread(() => {
        //     Task.Delay(3000).GetAwaiter().GetResult();
        //     Console.WriteLine("Hey!");
        //     FileWriterApp("Hi");
        //     });

        Application.Run(myForm);
    }

    // private static void FileWriterApp(string str)
    // {
    //     Console.WriteLine(str);
    // }

    // private static void Form1Load(object? sender, EventArgs e)
    // {
    //     AllocConsole();
    // }

    private static void StartGame(Form1 myForm)
    {
#if DEBUG
        // string s = Console.ReadLine()!;
        // if (s == "f")
        //     FullTest(myForm);
        // else if (s == "u")
        //     MyChess.UnitTester.TestRunner.Run();
        // else if (s == "s")
        //     MyChess.SpeedTester.TestRunner.Run();
        // else if (s != "p")
        // FullTest(myForm);
#else
            // MyChess.SpeedTester.TestRunner.Run();
#endif




        // FullTest(myForm);

        // UnitTest
        // MyChess.UnitTester.TestRunner.Run();

        // SpeedTest
        //MyChess.SpeedTester.TestRunner.Run();

        // game = new GameOfChess(myForm);
        // game.Play();
        MakePiecePlain m = new(myForm);
        m.Start();
    }
    // [DllImport("kernel32.dll", SetLastError = true)]
    // [return: MarshalAs(UnmanagedType.Bool)]
    // static extern bool AllocConsole();

    // need to be run now and then
    public static void FullTest(Form1 myForm)
    {
        bool hasFailed = false;
        PerftSearch("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 20, 1, ref hasFailed);
        PerftSearch("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 400, 2, ref hasFailed);
        PerftSearch("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 8902, 3, ref hasFailed);
        PerftSearch("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 197281, 4, ref hasFailed);
        // PerftSearch("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 4865609, 5, ref hasFailed);
        Console.WriteLine("1 / 7");

        PerftSearch("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 48, 1, ref hasFailed);
        PerftSearch("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 2039, 2, ref hasFailed);
        PerftSearch("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 97862, 3, ref hasFailed);
        PerftSearch("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 4085603, 4, ref hasFailed);
        // PerftSearch("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 193690690, 5, ref hasFailed);
        Console.WriteLine("2 / 7");

        PerftSearch("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 14, 1, ref hasFailed);
        PerftSearch("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 191, 2, ref hasFailed);
        PerftSearch("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 2812, 3, ref hasFailed);
        PerftSearch("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 43238, 4, ref hasFailed);
        PerftSearch("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 674624, 5, ref hasFailed);
        // PerftSearch("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 11030083, 6, ref hasFailed);
        Console.WriteLine("3 / 7");

        PerftSearch("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 6, 1, ref hasFailed);
        PerftSearch("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 264, 2, ref hasFailed);
        PerftSearch("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 9467, 3, ref hasFailed);
        PerftSearch("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 422333, 4, ref hasFailed);
        // PerftSearch("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 15833292, 5, ref hasFailed);
        Console.WriteLine("4 / 7");

        PerftSearch("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 6, 1, ref hasFailed);
        PerftSearch("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 264, 2, ref hasFailed);
        PerftSearch("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 9467, 3, ref hasFailed);
        PerftSearch("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 422333, 4, ref hasFailed);
        // PerftSearch("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 15833292, 5, ref hasFailed);
        Console.WriteLine("5 / 7");

        PerftSearch("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 44, 1, ref hasFailed);
        PerftSearch("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 1486, 2, ref hasFailed);
        PerftSearch("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 62379, 3, ref hasFailed);
        PerftSearch("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 2103487, 4, ref hasFailed);
        // PerftSearch("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 89941194, 5, ref hasFailed);
        Console.WriteLine("6 / 7");

        PerftSearch("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 46, 1, ref hasFailed);
        PerftSearch("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 2079, 2, ref hasFailed);
        PerftSearch("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 89890, 3, ref hasFailed);
        PerftSearch("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 3894594, 4, ref hasFailed);
        Console.WriteLine("7 / 7");

        if (hasFailed)
        {
            MyLib.MyConsole.WriteLine("FullTest has Failed, needed to be looked at!!!!!");
            Bitmap _bitmap = new Bitmap(900, 800);
            Graphics _graphicsObj = Graphics.FromImage(_bitmap);
            System.Drawing.SolidBrush _brush = new System.Drawing.SolidBrush(Color.Red);
            _graphicsObj.FillRectangle(_brush, 0, 0, 900, 800);
            myForm.graphicsObj.DrawImage(_bitmap, 0, 0);
            Thread.Sleep(100000);
        }
        else
            Console.WriteLine("Succes!");
    }
    private static void PerftSearch(string FEN, long ExpectedValue, int Depth, ref bool hasFailed)
    {
        ChessGame chessGame = new ChessGame(FEN);
        int searchDepth = Depth;

        long moveCount = 0;

        SearchMove(searchDepth);

        void SearchMove(int depth)
        {
            chessGame.possibleMoves.GenerateMoves();
            List<Move> movesRef = chessGame.GetPossibleMoves();
            int Count = movesRef.Count();
            Move[] moves = new Move[Count];
            movesRef.CopyTo(moves);

            for (int i = 0; i < Count; i++)
            {
                if (depth == 1)
                {
                    moveCount++;
                    continue;
                }
                chessGame.board.MakeMove(moves[i]);
                SearchMove(depth - 1);
                chessGame.board.UnMakeMove();
            }
        }
        if (moveCount != ExpectedValue)
        {
            hasFailed = true;
            Console.WriteLine("Failed!!!");
            Console.WriteLine(FEN);
            Console.WriteLine("MoveCount: " + moveCount + " Expected: " + ExpectedValue);
        }
    }
}