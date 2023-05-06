using System;
using System.Collections.Generic;
using System.IO;
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

namespace OneClickLogViewer.Views
{
    /// <summary>
    /// DocView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DocView : UserControl
    {
        public DocView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(DocView_Loaded);
            this.Unloaded += new RoutedEventHandler(DocView_Unloaded);

        }

        void DocView_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("loaded");
            var serializer = new AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);

            serializer.LayoutSerializationCallback += (s, args) =>
            {
                args.Content = args.Content;
            };


            if (File.Exists(@".\AvalonDock.config"))
                serializer.Deserialize(@".\AvalonDock.config");
        }

        void DocView_Unloaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("unloaded");
            var serializer = new AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.Serialize(@".\AvalonDock.config");
        }
    }
}
