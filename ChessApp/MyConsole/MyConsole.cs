

namespace MyLib
{
    class MyConsole
    {
#if DEBUG
        //public const string _path = @"MyConsole\Console.txt"; // for standalone and VSC
         public const string _path = @"../../../MyConsole\Console.txt";
#else
        public const string _path = @"../../../MyConsole\Console.txt"; // for standalone
#endif

        public static void WriteLine(string text)
        {
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(_path).ToList();
            lines.Add(text);
            File.WriteAllLines(_path, lines);
        }
    }

    class FileWriter
    {
        public bool IsOpen { get; private set; } = false;
        private List<string> lines;
        private string _path;
        public FileWriter(string path)
        {
            try
            {
                lines = File.ReadAllLines(path).ToList();
                _path = path;
                IsOpen = true;
            }
            catch
            {
                lines = new List<string>();
                _path = "\0";
                IsOpen = false;
            }
        }

        public void OpenFile(string path)
        {
            try
            {
                lines = File.ReadAllLines(path).ToList();
                _path = path;
                IsOpen = true;
            }
            catch
            {
                lines = new List<string>();
                _path = "\0";
                IsOpen = false;
            }
        }

        public void WriteLine(string str)
        {
            lines.Add(str);
            File.WriteAllLines(_path, lines);
        }
    }
}