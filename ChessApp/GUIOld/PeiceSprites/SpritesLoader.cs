// using System.Resources;
// using System.Reflection;

// // this is used to return a list of all the sprites in any given reselution
// namespace MyChessGUI.Sprites
// {

//     public class SpriteFetcher
//     {
//         private static string[] SpriteNames =
//         {
//             "WPawn",
//             "BPawn",
//             "Wrook",
//             "Brook",
//             "Wknight",
//             "Bknight",
//             "Wbishop",
//             "Bbishop",
//             "Wqueen",
//             "Bqueen",
//             "Wking",
//             "Bking"
//         };
        
//         public static List<Bitmap> GetSprites(int width, int height)
//         {
//             List<Bitmap> sprites = new List<Bitmap>();

//             for (int i = 0; i < SpriteNames.Count(); i++)
//             {
//                 var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MyChess.GUI.PeiceSprites._100x100.{SpriteNames[i]}.png");
//                 sprites.Add(ResizeBitmap(new Bitmap(stream!), 100, 100));
//             }

//             return sprites;
//         }

//         public static Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
//         {
//             Bitmap result = new Bitmap(width, height);
//             using (Graphics g = Graphics.FromImage(result))
//             {
//                 g.DrawImage(bmp, 0, 0, width, height);
//             }
        
//             return result;
//         }
//     }
// }