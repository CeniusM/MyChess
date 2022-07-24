using MyChess;
using winForm;
using MyChess.ChessBoard;

namespace MyChessGUI
{
    class Settings
    {
        public class Game
        {
            public static bool PlayingAI = true;
        }
        public class Dimensions
        {
            public const int FeildWidth = 100;
            public const int FeildHeight = 100;
            public const int PieceWidth = FeildWidth;
            public const int PieceHeight = FeildHeight;
            public const int EvalBarWidth = 100;
            public const int ScreenWidth = FeildWidth * 8 + EvalBarWidth;
            public const int ScreenHeight = FeildHeight * 8;
        }
        public class Colors
        {
            public readonly static Color L = Color.Wheat;
            public readonly static Color LightSquare = Color.WhiteSmoke;
            public readonly static Color DarkSquare = Color.LimeGreen;
            public readonly static Color LightPossibleMoveSquare = Color.OrangeRed;
            public readonly static Color DarkPossibleMoveSquare = Color.Red;
            public readonly static Color LightMovedOverSquare = Color.Brown; // foo
            public readonly static Color DarkMovedOverSquare = Color.Brown; // foo
            public readonly static Color LightSelecktedSquare = Color.FromArgb(255, 100, 255, 0);
            public readonly static Color DarkSelecktedSquare = Color.FromArgb(255, 100, 255, 0);
        }
    }
}