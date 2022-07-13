using System.Runtime.InteropServices;

namespace winForm;

public partial class Form1 : Form
{
    public System.Drawing.Graphics graphicsObj;
    public Form1()
    {
        InitializeComponent();

        graphicsObj = this.CreateGraphics();

        // AllocConsole();
    }
    // [DllImport("kernel32.dll", SetLastError = true)]
    // [return: MarshalAs(UnmanagedType.Bool)]
    // static extern bool AllocConsole();

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        e.Cancel = false;
        base.OnFormClosing(e);
    }
}
