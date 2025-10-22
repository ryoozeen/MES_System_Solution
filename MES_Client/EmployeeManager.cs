using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MES.Common;

namespace MES_Client
{
    public partial class EmployeeManager : Form
    {
        private DataGridView dgvEmployees;
        private Button btnAdd, btnEdit, btnDelete, btnRetire; // ⭐ btnRefresh 삭제
        private ProfileRow? _selectedProfile;

        public EmployeeManager()
        {
            SetupUI();
            WireEvents();
        }

        private void SetupUI()
        {
            this.Text = "사원 관리";
            this.Size = new System.Drawing.Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;

            // 데이터 그리드뷰
            dgvEmployees = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                Font = new System.Drawing.Font("맑은 고딕", 12F),
                RowTemplate = { Height = 40 }
            };
            dgvEmployees.Columns.Add("employee_id", "사번");
            dgvEmployees.Columns.Add("name", "이름");
            dgvEmployees.Columns.Add("department", "부서");
            dgvEmployees.Columns.Add("position", "직급");
            dgvEmployees.Columns.Add("equipment_id", "설비ID");
            dgvEmployees.Columns.Add("status", "재직 상태");

            // 컬럼 너비 설정
            foreach (DataGridViewColumn col in dgvEmployees.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            // 버튼 패널
            var btnPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80
            };

            btnAdd = new Button { Text = "등록", Left = 30, Top = 15, Width = 120, Height = 50, Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold) };
            btnEdit = new Button { Text = "수정", Left = 170, Top = 15, Width = 120, Height = 50, Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold) };
            btnDelete = new Button { Text = "삭제", Left = 310, Top = 15, Width = 120, Height = 50, Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold) };
            btnRetire = new Button { Text = "퇴사", Left = 450, Top = 15, Width = 120, Height = 50, Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold), BackColor = System.Drawing.Color.Orange };
            // ⭐ btnRefresh 삭제 (실시간 반영되므로 불필요)

            btnPanel.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnRetire });

            this.Controls.Add(dgvEmployees);
            this.Controls.Add(btnPanel);
        }

        private void WireEvents()
        {
            btnAdd.Click += (_, __) => ShowEditDialog(null);
            btnEdit.Click += (_, __) => ShowEditDialog(_selectedProfile);
            btnDelete.Click += (_, __) => DeleteSelected();
            btnRetire.Click += (_, __) => RetireSelected();
            // ⭐ btnRefresh 이벤트 제거 (실시간 반영되므로 불필요)
            dgvEmployees.SelectionChanged += (_, __) => UpdateSelection();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadEmployees();
        }

        private void UpdateSelection()
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                var row = dgvEmployees.SelectedRows[0];
                _selectedProfile = new ProfileRow
                {
                    employee_id = row.Cells["employee_id"].Value?.ToString() ?? "",
                    name = row.Cells["name"].Value?.ToString() ?? "",
                    department = row.Cells["department"].Value?.ToString() ?? "",
                    position = row.Cells["position"].Value?.ToString() ?? "",
                    equipment_id = row.Cells["equipment_id"].Value?.ToString(),
                    status = row.Cells["status"].Value?.ToString() ?? "재직중"
                };
            }
            else
            {
                _selectedProfile = null;
            }
            
            btnEdit.Enabled = btnDelete.Enabled = _selectedProfile != null;
            // 퇴사 버튼은 재직중인 사원만 활성화
            btnRetire.Enabled = _selectedProfile != null && _selectedProfile.status == "재직중";
        }

        private async void LoadEmployees()
        {
            // 서버 연결 체크
            if (!ClientApp.Net.IsConnected)
            {
                MessageBox.Show("서버에 연결되어 있지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var tcs = new TaskCompletionSource<ProfileRow[]>();
                void Handler(string line)
                {
                    try
                    {
                        using var doc = System.Text.Json.JsonDocument.Parse(line);
                        if (doc.RootElement.GetProperty("type").GetString() != "ListProfilesAck") return;
                        var body = doc.RootElement.GetProperty("body");
                        var ok = body.GetProperty("ok").GetBoolean();
                        if (!ok) { tcs.TrySetResult(Array.Empty<ProfileRow>()); return; }

                        var profiles = System.Text.Json.JsonSerializer.Deserialize<ProfileRow[]>(body.GetProperty("profiles").GetRawText());
                        tcs.TrySetResult(profiles ?? Array.Empty<ProfileRow>());
                    }
                    catch { tcs.TrySetResult(Array.Empty<ProfileRow>()); }
                }

                ClientApp.Net.OnLine += Handler;
                try
                {
                    await ClientApp.Net.SendAsync(new { type = "ListProfilesReq", body = new { } });
                    using var cts = new System.Threading.CancellationTokenSource(3000);
                    await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());
                    var profiles = await tcs.Task;

                    dgvEmployees.Rows.Clear();
                    foreach (var p in profiles)
                    {
                        dgvEmployees.Rows.Add(p.employee_id, p.name, p.department, p.position, p.equipment_id ?? "", p.status);
                    }
                }
                finally
                {
                    ClientApp.Net.OnLine -= Handler;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"목록 로드 실패: {ex.Message}", "오류");
            }
        }

        private async void ShowEditDialog(ProfileRow? profile)
        {
            // ⭐ 신규 등록 시 자동 사번 생성
            string nextEmployeeId = "";
            if (profile == null)
            {
                nextEmployeeId = await GetNextEmployeeIdAsync();
                if (string.IsNullOrEmpty(nextEmployeeId))
                {
                    MessageBox.Show("사번 자동 생성 실패", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // ⭐ 설비 목록 조회
            var equipmentIds = await GetEquipmentListAsync();

            using var dlg = new Form
            {
                Text = profile == null ? "사원 등록" : "사원 수정",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                Width = 600,
                Height = 500
            };

            var fontSize = new System.Drawing.Font("맑은 고딕", 14F);
            var labelWidth = 150;
            var textWidth = 350;
            var startX = 50;
            var textStartX = startX + labelWidth + 20;

            // 사번 (읽기 전용)
            var txtId = new TextBox { Left = textStartX, Top = 50, Width = textWidth, Height = 40, Font = fontSize, Text = profile?.employee_id ?? nextEmployeeId, ReadOnly = true };
            
            // 이름 (텍스트 입력)
            var txtName = new TextBox { Left = textStartX, Top = 110, Width = textWidth, Height = 40, Font = fontSize, Text = profile?.name ?? "" };
            
            // ⭐ 부서 (드롭다운 - 초기값 빈 값)
            var cbDept = new ComboBox 
            { 
                Left = textStartX, 
                Top = 170, 
                Width = textWidth, 
                Height = 40, 
                Font = fontSize, 
                DropDownStyle = ComboBoxStyle.DropDownList 
            };
            cbDept.Items.Add(""); // 빈 값 추가
            cbDept.Items.AddRange(new object[] { "개발", "인사", "생산" });
            cbDept.SelectedItem = profile != null ? profile.department : ""; // 수정 시: 기존 값, 신규 시: 빈 값
            
            // ⭐ 직급 (드롭다운 - 초기값 빈 값)
            var cbPos = new ComboBox 
            { 
                Left = textStartX, 
                Top = 230, 
                Width = textWidth, 
                Height = 40, 
                Font = fontSize, 
                DropDownStyle = ComboBoxStyle.DropDownList 
            };
            cbPos.Items.Add(""); // 빈 값 추가
            cbPos.Items.AddRange(new object[] { "사원", "대리", "과장", "차장" });
            cbPos.SelectedItem = profile != null ? profile.position : ""; // 수정 시: 기존 값, 신규 시: 빈 값
            
            // ⭐ 설비ID (드롭다운 + "(없음)" 옵션)
            var cbEq = new ComboBox 
            { 
                Left = textStartX, 
                Top = 290, 
                Width = textWidth, 
                Height = 40, 
                Font = fontSize, 
                DropDownStyle = ComboBoxStyle.DropDownList 
            };
            cbEq.Items.Add("(없음)"); // 첫 번째 항목: 없음
            cbEq.Items.AddRange(equipmentIds.Cast<object>().ToArray());
            cbEq.SelectedItem = string.IsNullOrEmpty(profile?.equipment_id) ? "(없음)" : profile.equipment_id;

            var labelFont = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            dlg.Controls.Add(new Label { Left = startX, Top = 55, Width = labelWidth, Height = 30, Text = "사번:", Font = labelFont, TextAlign = System.Drawing.ContentAlignment.MiddleLeft });
            dlg.Controls.Add(txtId);
            dlg.Controls.Add(new Label { Left = startX, Top = 115, Width = labelWidth, Height = 30, Text = "이름:", Font = labelFont, TextAlign = System.Drawing.ContentAlignment.MiddleLeft });
            dlg.Controls.Add(txtName);
            dlg.Controls.Add(new Label { Left = startX, Top = 175, Width = labelWidth, Height = 30, Text = "부서:", Font = labelFont, TextAlign = System.Drawing.ContentAlignment.MiddleLeft });
            dlg.Controls.Add(cbDept);
            dlg.Controls.Add(new Label { Left = startX, Top = 235, Width = labelWidth, Height = 30, Text = "직급:", Font = labelFont, TextAlign = System.Drawing.ContentAlignment.MiddleLeft });
            dlg.Controls.Add(cbPos);
            dlg.Controls.Add(new Label { Left = startX, Top = 295, Width = labelWidth, Height = 30, Text = "설비ID:", Font = labelFont, TextAlign = System.Drawing.ContentAlignment.MiddleLeft });
            dlg.Controls.Add(cbEq);

            var btnFont = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            var okBtn = new Button { Text = profile == null ? "등록" : "수정", Left = 150, Top = 370, Width = 120, Height = 60, DialogResult = DialogResult.OK, Font = btnFont, BackColor = System.Drawing.Color.LightGreen };
            var cancelBtn = new Button { Text = "취소", Left = 300, Top = 370, Width = 120, Height = 60, DialogResult = DialogResult.Cancel, Font = btnFont, BackColor = System.Drawing.Color.LightCoral };
            dlg.Controls.Add(okBtn);
            dlg.Controls.Add(cancelBtn);
            dlg.AcceptButton = okBtn;
            dlg.CancelButton = cancelBtn;

            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            var id = txtId.Text.Trim();
            var name = txtName.Text.Trim();
            var dept = cbDept.SelectedItem?.ToString() ?? "";
            var pos = cbPos.SelectedItem?.ToString() ?? "";
            var eq = cbEq.SelectedItem?.ToString();
            if (eq == "(없음)") eq = null; // "(없음)"을 null로 변환

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(dept) || string.IsNullOrEmpty(pos))
            {
                MessageBox.Show("모든 항목을 입력하세요.");
                return;
            }

            var success = profile == null 
                ? await SendAddProfile(id, name, dept, pos, eq)
                : await SendUpdateProfile(id, name, dept, pos, eq);

            // ⭐ 성공/실패 여부와 관계없이 항상 새로고침 (DB 저장은 성공했지만 응답 파싱 실패 케이스 대응)
            LoadEmployees();
        }

        private async Task<bool> SendAddProfile(string id, string name, string dept, string pos, string eq)
        {
            var tcs = new TaskCompletionSource<(bool ok, string? msg)>();
            void Handler(string line)
            {
                try
                {
                    using var doc = System.Text.Json.JsonDocument.Parse(line);
                    if (doc.RootElement.GetProperty("type").GetString() != "AddProfileAck") return;
                    var body = doc.RootElement.GetProperty("body");
                    var ok = body.GetProperty("ok").GetBoolean();
                    var msg = body.TryGetProperty("message", out var m) ? m.GetString() : null;
                    tcs.TrySetResult((ok, msg));
                }
                catch { }
            }

            ClientApp.Net.OnLine += Handler;
            try
            {
                // ⭐ 빈 문자열을 null로 변환
                string? equipmentId = string.IsNullOrWhiteSpace(eq) ? null : eq;
                
                await ClientApp.Net.SendAsync(new
                {
                    type = "AddProfileReq",
                    body = new AddProfileReq(id, name, dept, pos, equipmentId)
                });

                using var cts = new System.Threading.CancellationTokenSource(10000); // ⭐ 타임아웃 10초
                await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());
                var res = await tcs.Task;
                
                // ⭐ 디버그 로그 추가
                System.Diagnostics.Debug.WriteLine($"[EmployeeManager] AddProfile 결과: ok={res.ok}, msg={res.msg ?? "(null)"}");
                
                if (res.ok) 
                { 
                    MessageBox.Show("등록 성공", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    return true; 
                }
                else 
                { 
                    // ⭐ 서버 오류 메시지를 팝업에 표시
                    string errorMsg = string.IsNullOrEmpty(res.msg) ? "등록 실패 (서버 응답 없음)" : res.msg;
                    MessageBox.Show($"등록 실패\n\n오류 내용:\n{errorMsg}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                    return false; 
                }
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("응답 시간 초과", "오류");
                return false;
            }
            finally
            {
                ClientApp.Net.OnLine -= Handler;
            }
        }

        private async Task<bool> SendUpdateProfile(string id, string name, string dept, string pos, string eq)
        {
            var tcs = new TaskCompletionSource<(bool ok, string? msg)>();
            void Handler(string line)
            {
                try
                {
                    using var doc = System.Text.Json.JsonDocument.Parse(line);
                    if (doc.RootElement.GetProperty("type").GetString() != "UpdateProfileAck") return;
                    var body = doc.RootElement.GetProperty("body");
                    var ok = body.GetProperty("ok").GetBoolean();
                    var msg = body.TryGetProperty("message", out var m) ? m.GetString() : null;
                    tcs.TrySetResult((ok, msg));
                }
                catch { }
            }

            ClientApp.Net.OnLine += Handler;
            try
            {
                // ⭐ 빈 문자열을 null로 변환
                string? equipmentId = string.IsNullOrWhiteSpace(eq) ? null : eq;
                
                await ClientApp.Net.SendAsync(new
                {
                    type = "UpdateProfileReq",
                    body = new UpdateProfileReq(id, name, dept, pos, equipmentId)
                });

                using var cts = new System.Threading.CancellationTokenSource(3000);
                await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());
                var res = await tcs.Task;
                if (res.ok) { MessageBox.Show("수정 성공"); return true; }
                else { MessageBox.Show(res.msg ?? "수정 실패", "오류"); return false; }
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("응답 시간 초과", "오류");
                return false;
            }
            finally
            {
                ClientApp.Net.OnLine -= Handler;
            }
        }

        private async void DeleteSelected()
        {
            if (_selectedProfile == null) return;
            if (MessageBox.Show($"'{_selectedProfile.employee_id}' 사원을 삭제하시겠습니까?", "삭제 확인", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            var tcs = new TaskCompletionSource<(bool ok, string? msg)>();
            void Handler(string line)
            {
                try
                {
                    using var doc = System.Text.Json.JsonDocument.Parse(line);
                    if (doc.RootElement.GetProperty("type").GetString() != "DeleteProfileAck") return;
                    var body = doc.RootElement.GetProperty("body");
                    var ok = body.GetProperty("ok").GetBoolean();
                    var msg = body.TryGetProperty("message", out var m) ? m.GetString() : null;
                    tcs.TrySetResult((ok, msg));
                }
                catch { }
            }

            ClientApp.Net.OnLine += Handler;
            try
            {
                await ClientApp.Net.SendAsync(new
                {
                    type = "DeleteProfileReq",
                    body = new DeleteProfileReq(_selectedProfile.employee_id)
                });

                using var cts = new System.Threading.CancellationTokenSource(3000);
                await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());
                var res = await tcs.Task;
                if (res.ok) { MessageBox.Show("삭제 성공"); LoadEmployees(); }
                else { MessageBox.Show(res.msg ?? "삭제 실패", "오류"); }
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("응답 시간 초과", "오류");
            }
            finally
            {
                ClientApp.Net.OnLine -= Handler;
            }
        }

        /// <summary>
        /// 서버로부터 다음 사원번호 조회 (자동 생성)
        /// </summary>
        private async Task<string> GetNextEmployeeIdAsync()
        {
            try
            {
                var tcs = new TaskCompletionSource<string>();
                void Handler(string line)
                {
                    try
                    {
                        using var doc = System.Text.Json.JsonDocument.Parse(line);
                        if (doc.RootElement.GetProperty("type").GetString() != "GetNextEmployeeIdAck") return;
                        var body = doc.RootElement.GetProperty("body");
                        var ok = body.GetProperty("ok").GetBoolean();
                        if (!ok) { tcs.TrySetResult(""); return; }
                        var nextId = body.GetProperty("next_employee_id").GetString() ?? "";
                        tcs.TrySetResult(nextId);
                    }
                    catch { tcs.TrySetResult(""); }
                }

                ClientApp.Net.OnLine += Handler;
                try
                {
                    await ClientApp.Net.SendAsync(new { type = "GetNextEmployeeIdReq", body = new { } });
                    using var cts = new System.Threading.CancellationTokenSource(3000);
                    await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());
                    return await tcs.Task;
                }
                finally
                {
                    ClientApp.Net.OnLine -= Handler;
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 서버로부터 설비 목록 조회
        /// </summary>
        private async Task<string[]> GetEquipmentListAsync()
        {
            try
            {
                var tcs = new TaskCompletionSource<string[]>();
                void Handler(string line)
                {
                    try
                    {
                        using var doc = System.Text.Json.JsonDocument.Parse(line);
                        if (doc.RootElement.GetProperty("type").GetString() != "GetEquipmentListAck") return;
                        var body = doc.RootElement.GetProperty("body");
                        var ok = body.GetProperty("ok").GetBoolean();
                        if (!ok) { tcs.TrySetResult(Array.Empty<string>()); return; }
                        
                        var equipmentIds = body.GetProperty("equipment_ids");
                        var list = new List<string>();
                        foreach (var item in equipmentIds.EnumerateArray())
                        {
                            var id = item.GetString();
                            if (!string.IsNullOrEmpty(id)) list.Add(id);
                        }
                        tcs.TrySetResult(list.ToArray());
                    }
                    catch { tcs.TrySetResult(Array.Empty<string>()); }
                }

                ClientApp.Net.OnLine += Handler;
                try
                {
                    await ClientApp.Net.SendAsync(new { type = "GetEquipmentListReq", body = new { } });
                    using var cts = new System.Threading.CancellationTokenSource(3000);
                    await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());
                    return await tcs.Task;
                }
                finally
                {
                    ClientApp.Net.OnLine -= Handler;
                }
            }
            catch
            {
                return Array.Empty<string>();
            }
        }

        /// <summary>
        /// 선택된 사원 퇴사 처리
        /// </summary>
        private async void RetireSelected()
        {
            if (_selectedProfile == null) return;
            if (_selectedProfile.status == "퇴사")
            {
                MessageBox.Show("이미 퇴사 처리된 사원입니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"'{_selectedProfile.name}' ({_selectedProfile.employee_id}) 사원을 퇴사 처리하시겠습니까?", 
                "퇴사 처리 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            var tcs = new TaskCompletionSource<(bool ok, string? msg)>();
            void Handler(string line)
            {
                try
                {
                    using var doc = System.Text.Json.JsonDocument.Parse(line);
                    if (doc.RootElement.GetProperty("type").GetString() != "RetireEmployeeAck") return;
                    var body = doc.RootElement.GetProperty("body");
                    var ok = body.GetProperty("ok").GetBoolean();
                    var msg = body.TryGetProperty("message", out var m) ? m.GetString() : null;
                    tcs.TrySetResult((ok, msg));
                }
                catch { }
            }

            ClientApp.Net.OnLine += Handler;
            try
            {
                await ClientApp.Net.SendAsync(new
                {
                    type = "RetireEmployeeReq",
                    body = new { employee_id = _selectedProfile.employee_id }
                });

                using var cts = new System.Threading.CancellationTokenSource(3000);
                await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());
                var res = await tcs.Task;
                if (res.ok) 
                { 
                    MessageBox.Show("퇴사 처리 완료", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    LoadEmployees(); 
                }
                else 
                { 
                    MessageBox.Show(res.msg ?? "퇴사 처리 실패", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("응답 시간 초과", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ClientApp.Net.OnLine -= Handler;
            }
        }
    }
}
