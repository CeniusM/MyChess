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
            _bitmap = new Bitmap(_form.Height, _form.Width);
            _graphicsObj = Graphics.FromImage(_bitmap);
            _pen = new Pen(Color.Black);
            _brush = new System.Drawing.SolidBrush(Color.White);
        }
    }
}