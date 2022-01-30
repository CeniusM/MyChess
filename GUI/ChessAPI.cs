using Chess.ChessBoard;
using winForm;

namespace MyChessGUI
{
    class ChessAPI // gets the instruction from the chess game and gives the FormGUI instructions on what to print
    {
        private FormGUI formGUI;
        private List<Bitmap> sprites;
        private Form1 _form;
        public ChessAPI(Form1 form)
        {
            formGUI = new FormGUI(form);
            _form = form;
        }

        public void PrintBoard()
        {

        }

        public void PrintBoard(Board board)
        {

        }

        public void PrintBoard(string FENboard)
        {

        }
    }
}