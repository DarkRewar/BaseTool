using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows;

namespace BaseTool
{
    public static class SaveBuffer
    {
        private static readonly ConcurrentQueue<SaveRequest> _saveQueue = new();
        private static UTF8Encoding _encoding = new();

        private static CancellationTokenSource _token;

        public static void SaveToFile(string filepath, string data)
        {
            _saveQueue.Enqueue(new SaveRequest(filepath, data));
        }

        private static async Task SaveFileAsync()
        {
            while (!_token.IsCancellationRequested)
            {
                if (_saveQueue.Count != 0)
                {
                    while (_saveQueue.TryDequeue(out SaveRequest saveRequest))
                    {
                        byte[] data = _encoding.GetBytes(saveRequest.Data);
                        File.WriteAllBytes(saveRequest.FilePath, data);
                    }
                }
                await Task.Yield();
            }
            _token.Dispose();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void OnBeforeSceneLoad()
        {
            _saveQueue.Clear();
            _token = new CancellationTokenSource();
            _ = SaveFileAsync();
            Application.quitting += OnApplicationQuitting;
        }

        private static void OnApplicationQuitting()
        {
            _token.Cancel();
            Application.quitting -= OnApplicationQuitting;
        }

        private struct SaveRequest
        {
            public string FilePath;
            public string Data;

            public SaveRequest(string filepath, string data)
            {
                FilePath = filepath;
                Data = data;
            }
        }
    }
}
