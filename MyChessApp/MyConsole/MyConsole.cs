namespace CS_MyConsole
{
    class MyConsole
    {
        private static string _path = @"MyConsole\Console.txt";
        public static void WriteLine(string text)
        {
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(_path).ToList();
            lines.Add(text);
            File.WriteAllLines(_path, lines);
        }
        public static void WriteLine(string text, string path)
        {
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(path).ToList();
            lines.Add(text);
            File.WriteAllLines(path, lines);
        }
        public static void Write(string text)
        {
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(_path).ToList();
            lines[lines.Count() - 1] = lines[lines.Count() - 1] + text;
            File.WriteAllLines(_path, lines);
        }




        public string path = @"MyConsole\Console";
        public MyConsole(string path)
        {
            this.path = path;
        }
        public void WriteLineO(string text)
        {
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(path).ToList();
            lines.Add(text);
            File.WriteAllLines(path, lines);
        }
        public void WriteO(string text)
        {
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(path).ToList();
            lines[lines.Count() - 1] = lines[lines.Count() - 1] + text;
            File.WriteAllLines(path, lines);
        }
    }
}