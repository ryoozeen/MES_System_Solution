using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace MES_Client
{
    /// <summary>
    /// 진행률(%)을 바 내부에 텍스트로 표시하는 커스텀 ProgressBar
    /// </summary>
    public class PercentProgressBar : ProgressBar
    {
        public Color BarColor { get; set; } = Color.ForestGreen; // 채워지는 바 색
        public Color TextColor { get; set; } = Color.Black;        // % 글자 색

        public PercentProgressBar()
        {
            // 기본 ProgressBar는 OS가 그리므로 사용자 그리기 모드로 전환
            SetStyle(ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            // 기본 범위
            if (Minimum == 0) Minimum = 0;
            if (Maximum == 100) Maximum = 100;
        }

        // Value 변경 시 자동으로 다시 그려지도록 섀도잉
        public new int Value
        {
            get => base.Value;
            set
            {
                // 범위 클램프(안전)
                int v = value;
                if (v < Minimum) v = Minimum;
                if (v > Maximum) v = Maximum;

                base.Value = v;
                Invalidate();   // 값 바뀌면 다시 그리기
                Update();       // 즉시 반영 원하면 (선택)
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.Clear(BackColor);

            // 0~1 비율 계산
            float range = Math.Max(1, Maximum - Minimum);
            float percent = (float)(Value - Minimum) / range; // 0.0 ~ 1.0

            // 채워진 바
            int fillWidth = (int)Math.Round(ClientSize.Width * percent);
            using (var barBrush = new SolidBrush(BarColor))
                g.FillRectangle(barBrush, 0, 0, fillWidth, ClientSize.Height);

            // 테두리(선택)
            using (var pen = new Pen(Color.FromArgb(200, 200, 200)))
                g.DrawRectangle(pen, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

            // 중앙 % 텍스트
            string text = $"{percent * 100:0}%";
            var flags = TextFormatFlags.HorizontalCenter
                      | TextFormatFlags.VerticalCenter
                      | TextFormatFlags.SingleLine;
            TextRenderer.DrawText(g, text, Font, ClientRectangle, TextColor, flags);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();   // 크기 변경 시 재그리기
        }
    }
}
