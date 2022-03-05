using System.Media;

// this is used to return a list of all the sprites in any given reselution
namespace MyChessGUI.Sound
{

    class MySound
    {
        private Thread SoundThread;
        private SoundPlayer soundPlayer;
        private event EventHandler? playSound;
        public MySound(string Path)
        {
            SoundThread = new Thread(PrepareForSound);
            soundPlayer = new SoundPlayer(Path);
            SoundThread.Start();
        }

        public void PlaySound()
        {
            playSound!.Invoke(this, EventArgs.Empty);
        }

        private void PrepareForSound()
        {
            playSound += _PlaySound;
        }

        private void _PlaySound(object? sender, EventArgs e)
        {
            soundPlayer.Play();
        }

        ~MySound()
        {
            SoundThread.Join();
        }
    }
}