using System.Resources;
using System.Reflection;

// this is used to return a list of all the sprites in any given reselution
namespace ChessGUI.Sprites
{

    public class SpriteFetcher
    {
        private static string[] SpriteNames =
        {
            "None",
            "Wking",
            "WPawn",
            "Wknight",
            "None",
            "Wbishop",
            "Wrook",
            "Wqueen",
            "None",
            "Bking",
            "BPawn",
            "Bknight",
            "None",
            "Bbishop",
            "Brook",
            "Bqueen"
        };

        public static List<Bitmap> GetSprites(int width, int height)
        {
            List<Bitmap> sprites = new List<Bitmap>();

            for (int i = 0; i < SpriteNames.Count(); i++)
            {
                if (SpriteNames[i] == "None")
                {
                    sprites.Add(ResizeBitmap(new Bitmap(100, 100), width, height));
                    continue;
                }
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MyChess.GUI.PeiceSprites._100x100.{SpriteNames[i]}.png");
                sprites.Add(ResizeBitmap(new Bitmap(stream!), width, height));
            }

            return sprites;
        }

        public static Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }
    }
}