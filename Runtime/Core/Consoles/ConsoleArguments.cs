using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTool
{
    public class ConsoleArguments
    {
        public readonly string[] RawArgs;

        private List<string> _mainParams;
        private Dictionary<string, string> _argumentsParams;

        public string this[int index] => this._mainParams[index];

        public string this[string index] => this._argumentsParams[index];

        public ConsoleArguments()
        {
            RawArgs = Array.Empty<string>();
            _mainParams = new List<string>();
            _argumentsParams = new Dictionary<string, string>();
        }

        public ConsoleArguments(string[] args)
            : this()
        {
            RawArgs = args;
            Parse(args);
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
                    _argumentsParams.Add(key, (string)null);
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("arguments:");
            if (RawArgs.Length == 0)
            {
                sb.Append(" [empty]");
                return sb.ToString();
            }

            foreach (var mainParam in _mainParams)
            {
                sb.Append($"\n[{mainParam}]");
            }

            foreach (var argParam in _argumentsParams)
            {
                sb.Append($"\n[{argParam.Key} = {argParam.Value}]");
            }

            return sb.ToString();
        }
    }
}