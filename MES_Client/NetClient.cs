using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MES_Client
{
    public class NetClient
    {
        private TcpClient _c = new();
        private StreamReader? _rd;
        private StreamWriter? _wr;

        public bool IsConnected => _c.Connected;
        public event Action<string>? OnLine;
        public event Action? OnDisconnected; // ★ 끊김 알림

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

        // string JSON을 직접 전송 (수동 JSON 구성용)
        public Task SendAsync(string json)
            => _wr!.WriteLineAsync(json);
        
        // object를 JSON으로 직렬화하여 전송 (기존 호환성)
        public Task SendAsync(object msg)
            => _wr!.WriteLineAsync(JsonSerializer.Serialize(msg));

        private async Task ReadLoop()
        {
            try
            {
                while (_c.Connected)
                {
                    var line = await _rd!.ReadLineAsync();
                    if (line == null) break;        // 서버가 소켓을 닫음
                    OnLine?.Invoke(line);
                }
            }
            catch
            {
                // 예외로 끝나도 끊김 처리
            }
            finally
            {
                OnDisconnected?.Invoke();          // ★ 끊김 신호
            }
        }

        public void Close()
        {
            try { _c?.Close(); } catch { }
        }
    }

    public static class ClientApp
    {
        public static readonly NetClient Net = new NetClient();
        public static string Host = "127.0.0.1";
        public static int Port = 9000;

        // UI 스레드 컨텍스트(팝업용)
        public static SynchronizationContext? UI;
    }
}
