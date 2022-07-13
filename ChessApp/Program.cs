using System.Runtime.InteropServices;
using System.Diagnostics;

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
        // UnitTest
        MyChess.UnitTester.TestRunner.Run();

        // SpeedTest
        //MyChess.SpeedTester.TestRunner.Run();

        game = new GameOfChess(myForm);
        game.Play();
    }
    // [DllImport("kernel32.dll", SetLastError = true)]
    // [return: MarshalAs(UnmanagedType.Bool)]
    // static extern bool AllocConsole();

}