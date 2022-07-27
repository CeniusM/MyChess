using System.Runtime.InteropServices;
using System.Diagnostics;
using ChessGUI;

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

        Application.Run(myForm);
    }

    private static void StartGame(Form1 myForm)
    {
#if DEBUG

#else

#endif

        game = new(myForm);
        game.Play();
    }
}