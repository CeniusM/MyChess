#define vsc // if this is in vsc

namespace MyLib
{
    class DebugConsole
    {
        public static void WriteLine(string str)
        {
#if DEBUG
            Console.WriteLine(str);
#else
            MyConsole.WriteLine(str);
#endif
        }
        public static void Write(string str)
        {
#if DEBUG
            Console.Write(str);
#else
            // MyConsole.Write(str);
#endif
        }
        public static string ReadLine()
        {
#if DEBUG
            return Console.ReadLine()!;
#else
            return "Debug Console not available";
#endif
        }

        public static bool IsOn()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }

    class MyConsole
    {
#if vsc
        public const string _path = @"MyConsole\Console.txt";
#else
        public const string _path = @"../../../MyConsole\Console.txt"; // for standalone
#endif

        public static void WriteLine(string text)
        {
            try
            {
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(_path).ToList();
                lines.Add(text);
                File.WriteAllLines(_path, lines);
            }
            catch
            {
                //Console.WriteLine("Failed to write the given console -- message down below --");
                Console.WriteLine(text);
            }
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