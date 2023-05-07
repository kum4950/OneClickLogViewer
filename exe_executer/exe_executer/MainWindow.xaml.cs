using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace exe_executer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string id = txtbox.Text;
            DateTime? selectedDate = DatePick.SelectedDate;

            Console.WriteLine($"{id}, {selectedDate}");

            // 실행할 파일 경로와 인수 설정
            string filePath = "C:\\OneClickLogViewer\\OneClickLogViewer\\bin\\Debug\\net6.0-windows\\OneClickLogViewer.exe";
            string arguments = "161610,23/05/02,07:15:44.209";

            // 프로세스 시작 정보 객체 생성
            ProcessStartInfo startInfo = new ProcessStartInfo(filePath, arguments);

            // 프로세스 시작
            Process process = Process.Start(startInfo);
        }
    }
}
