using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Drawing
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DrawSet set;
        public MainWindow()
        {
            InitializeComponent();
            set = new DrawSet(ConfigHelper.GetValue(StringResource.KeyFileName, StringResource.DefaultFileName));
        }
        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
            if (set.Count() == 0)
            {
                MessageBox.Show("请导入名单！");
                return;
            }
            new Thread(o =>
            {
                string str = "请开始";
                for (int i = 0; i < 10; i++)
                {
                    str = set.DrawOnce();
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                        new Action<Button, string>(SetLabelContent), sender as Button, str);
                    Thread.Sleep(100);
                }
                //TODO 判断是否勾选朗读
                if (ConfigHelper.GetValue(StringResource.KeySpeech, bool.FalseString).Equals(bool.TrueString))
                {
                    Type type = Type.GetTypeFromProgID("SAPI.SpVoice");
                    dynamic spVoice = Activator.CreateInstance(type);
                    spVoice.Speak(str);
                }

            })
            { IsBackground = true }.Start();

        }
        private void SetLabelContent(Button btn, string content)
        {
            Label_Main.Content = content;
        }
        private void Btn_About_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
        }

        private void Btn_Setting_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow setting = new SettingWindow();
            setting.SetDrawSet(set);
            setting.ShowDialog();
        }

        private void Btn_Advance_Click(object sender, RoutedEventArgs e)
        {
            AdvanceWindow advance = new AdvanceWindow();
            advance.SetDrawSet(set);
            advance.ShowDialog();
        }
    }
}
