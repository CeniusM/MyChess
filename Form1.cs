namespace winForm;

public partial class Form1 : Form
{
    public System.Drawing.Graphics graphicsObj;
    public Form1()
    {
        InitializeComponent();

        graphicsObj = this.CreateGraphics();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        e.Cancel = false;
        base.OnFormClosing(e);
    }
}
