using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Drawing
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("开始");
            new Thread(o =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                        new Action<Button, String>(SetLabelContent), sender as Button, i.ToString());
                    Thread.Sleep(100);
                }
            })
            { IsBackground = true }.Start();
            
        }
        private void SetLabelContent(Button btn,String content)
        {
            Label_Main.Content = content;
        }
        private void Btn_About_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow().Show();
        }

        private void Btn_Setting_Click(object sender, RoutedEventArgs e)
        {
            new SettingWindow().Show();
        }
    }
}
