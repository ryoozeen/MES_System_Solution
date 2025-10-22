using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MES_Server
{
    public partial class Server : Form
    {
        private readonly TCPIP _tcp = new TCPIP();
        private CancellationTokenSource? _cts;

        public Server()
        {
            // ⭐ 콘솔 인코딩을 UTF-8로 설정 (한글 깨짐 방지)
            // WinForms 앱은 콘솔 핸들이 없을 수 있으므로 예외처리
            try
            {
                System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            }
            catch (System.IO.IOException)
            {
                // 콘솔 핸들이 없는 경우 무시 (WinForms 앱에서는 정상)
                System.Diagnostics.Debug.WriteLine("[Server] 콘솔 인코딩 설정 실패 (WinForms 앱에서는 정상)");
            }
            
            InitializeComponent();
            InitUi();
            WireEvents();
        }

        private void WireEvents()
        {
            // 버튼 클릭 이벤트 연결
            btn_Connection.Click += async (_, __) => await StartServerAsync();
            btn_Cancel.Click += async (_, __) => await StopServerAsync();

            // TCP 로그 → 리스트박스 반영
            _tcp.OnLog += AddLog;

            // 폼 닫힐 때 서버/클라이언트 정리
            this.FormClosing += Server_FormClosing;
        }

        private void InitUi()
        {
            // 0) ���̾ƿ� ��� ����(������/���ġ ����)
            this.SuspendLayout();

            // 1) ���� �θ�(�����̳�)�� ����
            if (lbl_inactive.Parent != lbl_active.Parent)
                lbl_inactive.Parent = lbl_active.Parent;
            if (pictureBox_Red.Parent != pictureBox_Green.Parent)
                pictureBox_Red.Parent = pictureBox_Green.Parent;

            // 2) Anchor/AutoSize�� �����ϰ� ����
            lbl_inactive.Anchor = lbl_active.Anchor;
            lbl_inactive.AutoSize = lbl_active.AutoSize;

            // 3) ��ġ+ũ��(Bounds) �� ���� ����ȭ
            lbl_inactive.Bounds = lbl_active.Bounds;
            pictureBox_Red.Bounds = pictureBox_Green.Bounds;

            // (����) ����/��Ʈ � ���߸� �� ���
            lbl_inactive.TextAlign = lbl_active.TextAlign;
            lbl_inactive.Font = lbl_active.Font;

            // 4) �ʱ� ����: ��Ȱ�� ǥ��(���� ON, �ʷ� OFF)
            lbl_active.Visible = false;
            pictureBox_Green.Visible = false;
            lbl_inactive.Visible = true;
            pictureBox_Red.Visible = true;

            // 5) ���̾ƿ� �簳
            this.ResumeLayout();

            // TableLayoutPanel ��� �� ���� ���� ����
            tableLayoutPanel1.SetCellPosition(lbl_inactive, tableLayoutPanel1.GetCellPosition(lbl_active));
            tableLayoutPanel1.SetCellPosition(pictureBox_Red, tableLayoutPanel1.GetCellPosition(pictureBox_Green));
        }

        private async Task StartServerAsync()
        {
            try
            {
                btn_Connection.Enabled = false;
                _cts = new CancellationTokenSource();

                AddLog("서버 시작 중...");
                // DB 헬스 체크(1회)
                var ok = await Db.HealthAsync();
                AddLog(ok ? "DB 연결 확인: OK" : "DB 연결 확인: FAIL");

                // ⭐ 모든 설비를 "미가동" 상태로 초기화
                if (ok)
                {
                    var resetOk = await Db.ResetAllEquipmentStatusAsync();
                    AddLog(resetOk ? "모든 설비 상태 초기화: 완료 (미가동)" : "설비 상태 초기화: 실패");
                }

                // 포트는 기본 9000, 필요 시 App 설정으로 분리 가능
                await _tcp.StartAsync(9000, _cts.Token);

                // 상태 변경: 활성
                ToggleActive(true);
                AddLog("서버 시작됨 (포트 9000)");
            }
            catch (Exception ex)
            {
                AddLog($"서버 시작 실패: {ex.Message}");
                btn_Connection.Enabled = true;
                ToggleActive(false);
            }
        }

        private async Task StopServerAsync()
        {
            try
            {
                AddLog("서버 중지 중...");
                _cts?.Cancel();
                await _tcp.StopAsync();
                ToggleActive(false);
                AddLog("서버 중지됨");
            }
            catch (Exception ex)
            {
                AddLog($"서버 중지 실패: {ex.Message}");
            }
            finally
            {
                btn_Connection.Enabled = true;
            }
        }

        private void ToggleActive(bool active)
        {
            // 초록/Active 보이기 or 빨강/Inactive 보이기
            this.InvokeIfRequired(() =>
            {
                lbl_active.Visible = active;
                pictureBox_Green.Visible = active;
                lbl_inactive.Visible = !active;
                pictureBox_Red.Visible = !active;
            });
        }

        private void AddLog(string msg)
        {
            this.InvokeIfRequired(() =>
            {
                // 최신 로그가 항상 보이게 스크롤
                listBox_Event.Items.Add($"[{DateTime.Now:HH:mm:ss}] {msg}");
                listBox_Event.TopIndex = listBox_Event.Items.Count - 1;
            });
        }

        // 폼 닫힐 때 정리 작업
        private async void Server_FormClosing(object? sender, FormClosingEventArgs e)
        {
            try
            {
                // 중복 클릭/닫기 방지
                btn_Connection.Enabled = false;
                btn_Cancel.Enabled = false;

                // 서버 중지 및 정리
                _cts?.Cancel();
                await _tcp.StopAsync();
                AddLog("애플리케이션 종료: 서버/클라이언트 연결 종료");
            }
            catch (Exception ex)
            {
                AddLog($"종료 중 오류: {ex.Message}");
            }
        }
    }

    internal static class ControlExtensions
    {
        public static void InvokeIfRequired(this Control c, Action action)
        {
            if (c.InvokeRequired) c.Invoke(action);
            else action();
        }
    }
}
