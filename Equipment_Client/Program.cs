namespace Equipment_Client
{
    internal static class Program
    {
        // 로그인한 사원 정보 (설비 담당자)
        public static string? LoggedInEmployeeId { get; set; }
        public static string? AssignedEquipmentId { get; set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // 로그인 화면 먼저 표시
            var loginForm = new EquipmentLogin();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // 로그인 성공 시 사원 정보 저장
                LoggedInEmployeeId = loginForm.LoggedInEmployeeId;
                AssignedEquipmentId = loginForm.AssignedEquipmentId;

                // 메인 폼 실행
                Application.Run(new Equipment_Client());
            }
            // 로그인 실패 또는 취소 시 프로그램 종료
        }
    }
}