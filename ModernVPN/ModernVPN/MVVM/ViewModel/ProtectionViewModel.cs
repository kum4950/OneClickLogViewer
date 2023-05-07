using ModernVPN.Core;
using ModernVPN.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModernVPN.MVVM.ViewModel
{
    class ProtectionViewModel : ObservableObject
    {
        public ObservableCollection<ServerModel> Servers {  get; set; }

        public GlobalViewModel Global { get; } = GlobalViewModel.Instance;

        private string _connectionStatus;

        public string ConnectionStatus
        {
            get { return _connectionStatus; }
            set { _connectionStatus = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand ConnectCommand { get; set; }
        public ProtectionViewModel()
        {
            Servers = new ObservableCollection<ServerModel>();

            for(int i=0; i<10; i++)
            {
                Servers.Add(new ServerModel
                {
                    Country = "USA"
                });
            }

            ConnectCommand = new RelayCommand(o => 
            {
                Task.Run(() => 
                {
                    ConnectionStatus = "Connectiong..";
                    var process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                    process.StartInfo.ArgumentList.Add(@"\c rasdial Myserver vpnbook z93cvfv /phonebook:./VPN/VPN.pbk");

                    process.Start();
                    process.WaitForExit();

                    switch (process.ExitCode)
                    {
                        case 0:
                            Console.WriteLine("Success!");
                            break;
                        case 691:
                            Console.WriteLine("Wrong credentials!");
                            break;
                        default:
                            Console.WriteLine($"Error: {process.ExitCode}");
                            break;
                    }
                });
            });
            ServerBuilder();
        }

        private void ServerBuilder()
        {
            var address = "us1.vpnbook.com";
            var FolderPath = $"{Directory.GetCurrentDirectory()}/VPN";
            var PbkPath = $"{FolderPath}/{address}.pbk";

            if(!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            if(File.Exists(PbkPath))
            {
                MessageBox.Show("Connection Already Exist");
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("[MyServer]");
            sb.AppendLine("MEDIA=rastapi");
            sb.AppendLine("Port=VPN2-0");
            sb.AppendLine("Device=WAN Miniport (IKEv2)");
            sb.AppendLine("DEVICE=vpn");
            sb.AppendLine($"PhoneNumber={address}");
            File.WriteAllText(PbkPath,sb.ToString());






        }
    }
}
