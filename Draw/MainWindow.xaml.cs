﻿using System;
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
            set = new DrawSet("1.txt");
            //MessageBox.Show(String.Format("{0}",set.set[0]));
        }
        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("开始");
            if(set.Count()==0)
            {
                MessageBox.Show("请导入名单！");
                return;
            }
            new Thread(o =>
            {
                String str = "请开始";
                for (int i = 0; i < 10; i++)
                {
                    str = set.DrawOnce();
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                        new Action<Button, String>(SetLabelContent), sender as Button, str);
                    Thread.Sleep(100);
                }
                //TODO 判断是否勾选朗读
                Type type = Type.GetTypeFromProgID("SAPI.SpVoice");
                dynamic spVoice = Activator.CreateInstance(type);
                spVoice.Speak(str);
            })
            { IsBackground = true }.Start();
            
        }
        private void SetLabelContent(Button btn,String content)
        {
            Label_Main.Content = content;
        }
        private void Btn_About_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.Show();
        }

        private void Btn_Setting_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow setting = new SettingWindow();
            setting.SetDrawSet(set);
            setting.Show();
        }
    }
}