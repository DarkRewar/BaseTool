using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace BaseTool
{
    public class Console
    {
        private const int HistoryCount = 50;

        private static string LastMsg = "";
        private static double TimeLastMsg;
        private static int ConsoleShowLastLine = 0;

        private static List<string> PendingCommands = new List<string>();
        public static int PendingCommandsWaitForFrames = 0;
        public static bool PendingCommandsWaitForLoad = false;

        private static Dictionary<string, ConsoleCommand> _commands = new Dictionary<string, ConsoleCommand>();
        private static string[] History = new string[HistoryCount];
        private static int HistoryNextIndex = 0;
        private static int HistoryIndex = 0;

        private static ConsoleManager _consoleManager;

        public Console()
        {
            var go = new GameObject("Console (manager)");
            Object.DontDestroyOnLoad(go);

#if UNITY_2021_1_OR_NEWER
            go.AddComponent<UIDocument>();
#endif
            _consoleManager = go.AddComponent<ConsoleManager>();

            AddCommand("help", "How to use the command", HelpCommand);
            AddCommand("list", "Display the list of commands", ListCommand);
        }

        private static void OutputString(string message)
        {
            if (_consoleManager == null)
                return;
            _consoleManager.WriteLine(message);
        }

        public static void Write(string msg)
        {
            if (ConsoleShowLastLine > 0)
            {
                LastMsg = msg;
                TimeLastMsg = Time.time;
            }
            OutputString(msg);
        }

        private static string ParseQuoted(string input, ref int pos)
        {
            ++pos;
            int startIndex = pos;
            while (pos < input.Length)
            {
                if (input[pos] == '"' && input[pos - 1] != '\\')
                {
                    ++pos;
                    return input.Substring(startIndex, pos - startIndex - 1);
                }
                ++pos;
            }
            return input.Substring(startIndex);
        }

        private static string Parse(string input, ref int pos)
        {
            int startIndex = pos;
            while (pos < input.Length)
            {
                if (" \t".IndexOf(input[pos]) > -1)
                    return input.Substring(startIndex, pos - startIndex);
                ++pos;
            }
            return input.Substring(startIndex);
        }

        private static List<string> Tokenize(string input)
        {
            int pos = 0;
            List<string> stringList = new List<string>();
            int num = 0;
            while (pos < input.Length && num++ < 10000)
            {
                SkipWhite(input, ref pos);
                if (pos != input.Length)
                {
                    if (input[pos] == '"' && (pos == 0 || input[pos - 1] != '\\'))
                        stringList.Add(ParseQuoted(input, ref pos));
                    else
                        stringList.Add(Parse(input, ref pos));
                }
                else
                    break;
            }
            return stringList;
        }

        public static string HistoryUp(string current)
        {
            if (HistoryIndex == 0 || HistoryNextIndex - HistoryIndex >= 49)
                return "";
            if (HistoryIndex == HistoryNextIndex)
                History[HistoryIndex % 50] = current;
            --HistoryIndex;
            return History[HistoryIndex % 50];
        }

        public static string HistoryDown()
        {
            if (HistoryIndex == HistoryNextIndex)
                return "";
            ++HistoryIndex;
            return History[HistoryIndex % 50];
        }

        public static ConsoleArguments ParseArguments(string[] argument) => new ConsoleArguments(argument);

        private static void SkipWhite(string input, ref int pos)
        {
            while (pos < input.Length && " \t".IndexOf(input[pos]) > -1)
                ++pos;
        }

        #region UPDATES

        public static void ConsoleUpdate()
        {
            double num = (double)Time.time - TimeLastMsg;
            //IconikFramework.Console.Console.s_ConsoleUI.ConsoleUpdate();
            while (PendingCommands.Count > 0)
            {
                if (PendingCommandsWaitForFrames > 0)
                {
                    --PendingCommandsWaitForFrames;
                    break;
                }
                if (PendingCommandsWaitForLoad)
                    PendingCommandsWaitForLoad = false;
                string pendingCommand = PendingCommands[0];
                PendingCommands.RemoveAt(0);
                ExecuteCommand(pendingCommand);
            }
        }

        #endregion

        #region COMMAND EXECUTION

        public static void ExecuteCommand(string command)
        {
            List<string> stringList = Tokenize(command);
            if (stringList.Count < 1)
                return;
            OutputString(">" + command);
            string lower = stringList[0].ToLower();
            if (_commands.TryGetValue(lower, out var consoleCommand))
            {
                string[] array = stringList.GetRange(1, stringList.Count - 1).ToArray();
                consoleCommand.method(ParseArguments(array));
            }
            else
                OutputString("Unknown command: " + stringList[0]);
        }

        public static void EnqueueCommandNoHistory(string command) => PendingCommands.Add(command);

        public static void EnqueueCommand(string command)
        {
            History[HistoryNextIndex % 50] = command;
            ++HistoryNextIndex;
            HistoryIndex = HistoryNextIndex;
            EnqueueCommandNoHistory(command);
        }

        public static string TabComplete(string prefix)
        {
            List<string> stringList = new List<string>();
            foreach (KeyValuePair<string, ConsoleCommand> command in _commands)
            {
                string key = command.Key;
                if (key.StartsWith(prefix, true, (CultureInfo)null))
                    stringList.Add(key);
            }
            if (stringList.Count == 0)
                return prefix;
            int a = stringList[0].Length;
            for (int index = 0; index < stringList.Count - 1; ++index)
                a = Mathf.Min(a, CommonPrefix(stringList[index], stringList[index + 1]));
            prefix += stringList[0].Substring(prefix.Length, a - prefix.Length);
            if (stringList.Count > 1)
            {
                for (int index = 0; index < stringList.Count; ++index)
                    Write(" " + stringList[index]);
            }
            else
                prefix += " ";
            return prefix;
        }

        private static int CommonPrefix(string a, string b)
        {
            int num = Mathf.Min(a.Length, b.Length);
            for (int length = 1; length <= num; ++length)
            {
                if (!a.StartsWith(b.Substring(0, length), true, (CultureInfo)null))
                    return length - 1;
            }
            return num;
        }

        #endregion

        #region COMMANDS

        public static void AddCommand(
            string name,
            string description,
            MethodDelegate method,
            int tag = 0)
        {
            name = name.ToLower();
            if (_commands.ContainsKey(name))
                Debug.LogError("Cannot add command " + name + " twice");
            else
                _commands.Add(name, new ConsoleCommand(name, method, description, tag));
        }

        public static void RemoveCommand(string name) => _commands.Remove(name.ToLower());

        private void HelpCommand(ConsoleArguments args)
        {
            OutputString("Hello this is some help!");
        }

        private void ListCommand(ConsoleArguments args)
        {
            StringBuilder sb = new StringBuilder("List of available commands:");
            foreach (var commandPair in _commands)
            {
                sb.Append($"\n- {commandPair.Key}: {commandPair.Value.description}");
            }
            OutputString(sb.ToString());
        }

        #endregion

        #region AUTOLOAD

        private static Console _instance;

        public static Console Instance => _instance ?? Init();

        [RuntimeInitializeOnLoadMethod]
        public static Console Init()
        {
            _instance = new Console();
            return _instance;
        }

        #endregion
    }
}
