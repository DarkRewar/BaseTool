using System.Collections.Generic;

namespace BaseTool.Core.Consoles
{
    public class ConsoleArguments
    {
        private List<string> _mainParams;
        private Dictionary<string, string> _argumentsParams;

        public string this[int index] => this._mainParams[index];

        public string this[string index] => this._argumentsParams[index];

        public ConsoleArguments()
        {
            this._mainParams = new List<string>();
            this._argumentsParams = new Dictionary<string, string>();
        }

        public ConsoleArguments(string[] args)
            : this()
        {
            this.Parse(args);
        }

        private void Parse(string[] args)
        {
            string key = "";
            foreach (string str in args)
            {
                bool flag = str.StartsWith("+") || str.StartsWith("-");
                if (flag)
                {
                    key = str.Substring(1);
                    _argumentsParams.Add(key, (string) null);
                }
                else if (!flag && key != "")
                {
                    _argumentsParams[key] = str;
                    key = "";
                }
                else
                    _mainParams.Add(str);
            }
        }

        public bool Exists(int index) => 0 <= index && index < _mainParams.Count;

        public bool Exists(string index) => _argumentsParams.ContainsKey(index);
    }
}