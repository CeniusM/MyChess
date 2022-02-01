// this is used to return a list of all the sprites in any given reselution
namespace MyChessGUI.Sprites
{
    class SpriteFetcher
    {
        private static string[] SpriteNames =
        {
            "Wpawn",
            "Bpawn",
            "Wrook",
            "Brook",
            "Wbishop",
            "Bbishop",
            "Wknight",
            "Bknight",
            "Wqueen",
            "Bqueen",
            "Wking",
            "Bking"
        };
        public static List<Bitmap> GetSprites(string PathToFolder) // note* Paint.net, 100x100, 120/tomme
        {
            List<Bitmap> sprites = new List<Bitmap>();

            for (int i = 0; i < SpriteNames.Count(); i++)
            {
                string Path = PathToFolder + @"\" + SpriteNames[i] + ".png";
                sprites.Add(new Bitmap(Path));
            }

            return sprites;
        }
    }
}