namespace BaseTool.Core.Consoles
{
    public delegate void MethodDelegate(ConsoleArguments args);

    internal class ConsoleCommand
    {
        public string name;
        public MethodDelegate method;
        public string description;
        public int tag;

        public ConsoleCommand(
            string name,
            MethodDelegate method,
            string description,
            int tag)
        {
            this.name = name;
            this.method = method;
            this.description = description;
            this.tag = tag;
        }
    }
}