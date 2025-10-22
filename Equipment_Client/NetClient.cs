// MES_Client/NetClient.cs
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Equipment_Client
{
    public class NetClient
    {
        private TcpClient _c = new();
        private StreamReader? _rd;
        private StreamWriter? _wr;

        public bool IsConnected => _c.Connected;
        public event Action<string>? OnLine;
        public event Action? OnDisconnected;

        // 공용 상태(서버 엔드포인트 + 싱글턴 NetClient)
        public static class ClientApp
        {
            public static readonly NetClient Net = new NetClient();
            public static string Host = "127.0.0.1";
            public static int Port = 9000;
        }

        public async Task<bool> ConnectAsync(string host, int port, int timeoutMs = 2000)
        {
            using var cts = new CancellationTokenSource(timeoutMs);
            try
            {
                await _c.ConnectAsync(host, port, cts.Token);
                var ns = _c.GetStream();
                _rd = new StreamReader(ns, Encoding.UTF8);
                _wr = new StreamWriter(ns, new UTF8Encoding(false)) { AutoFlush = true };
                _ = Task.Run(ReadLoop);
                return true;
            }
            catch
            {
                try { _c.Close(); } catch { }
                _c = new TcpClient();
                return false;
            }
        }

        public Task SendAsync(object msg)
            => _wr!.WriteLineAsync(JsonSerializer.Serialize(msg));

        private async Task ReadLoop()
        {
            try
            {
                while (_c.Connected)
                {
                    var line = await _rd!.ReadLineAsync();
                    if (line == null) break;
                    OnLine?.Invoke(line);
                }
            }
            catch { }
            finally
            {
                OnDisconnected?.Invoke();
            }
        }

        public void Close()
        {
            try { _c?.Close(); } catch { }
        }
    }
}
