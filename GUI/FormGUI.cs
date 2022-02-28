using winForm;

namespace MyChessGUI
{
    class FormGUI // just for taking simple form instruction that will be printed to the form
    {
        private Form1 _form;
        private Bitmap _bitmap;
        private Graphics _graphicsObj;
        private Pen _pen;
        private System.Drawing.SolidBrush _brush;
        public FormGUI(Form1 form)
        {
            _form = form;
            _bitmap = new Bitmap(900, 800);
            _graphicsObj = Graphics.FromImage(_bitmap);
            _pen = new Pen(Color.Black);
            _brush = new System.Drawing.SolidBrush(Color.White);
        }

        public void Print()
        {
            _form.graphicsObj.DrawImage(_bitmap, 0, 0);
        }

        // public void DrawSquare(int x1, int y1, int x2, int y2, Color color) // test :D
        // {
        //     _brush.Color = color;
        //     _graphicsObj.FillRectangle(_brush, x2 - x1, y2 - y1, (x2 - x1) * 2 << 1, (y2 - y1) << 1);
        // }

        public void DrawSquare(int x, int y, int height, int width, Color color)
        {
            _brush.Color = color;
            _graphicsObj.FillRectangle(_brush, x, y, width, height);
        }

        public void DrawBitmap(Bitmap bitmap, int x, int y)
        {
            _graphicsObj.DrawImage(bitmap, x, y);
        }

        public void PrintBitmapOnForm(Bitmap bitmap, int x, int y)
        {
            _form.graphicsObj.DrawImage(bitmap, x, y);
        }

        public void PrintSquareOnForm(int x1, int y1, int x2, int y2, Color color)
        {
            _brush.Color = color;
            _form.graphicsObj.FillRectangle(_brush, x2 - x1, y2 - y1, (x2 - x1) * 2 << 1, (y2 - y1) << 1);
        }
    }
}