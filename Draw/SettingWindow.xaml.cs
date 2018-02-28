using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;

namespace Drawing
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        private DrawSet set;
        private List<string> listNames;
        public SettingWindow()
        {
            InitializeComponent();
            //listNames = new string[0];
            listNames = new List<string>();
            SetFileItems();
            //System.Windows.MessageBox.Show(ConfigHelper.GetValue(StringResource.KeySpeech, bool.FalseString));
            Cb_Speech.IsChecked = bool.TrueString.Equals(ConfigHelper.GetValue(StringResource.KeySpeech, bool.FalseString));
        }
        internal void SetDrawSet(DrawSet s)
        {
            set = s;
            InitialDatas();
        }
        private void InitialDatas()
        {
            //listNames = set.GetAllValues();
            //set.GetAllValues().CopyTo(listName,0);
            listNames.Clear();
            string[] tmp = set.GetAllValues();
            foreach(string t in tmp)
            {
                listNames.Add(t);
            }
            Lb_Names.ItemsSource = listNames;
            Lb_Names.Items.Refresh();
            SetLabelCurrentFile(set.GetCurrentFileName());
        }
        private void SetLabelCurrentFile(string str)
        {
            Lb_CurrentFile.Content = "当前名单：" + str;
        }
        private void SetFileItems()
        {
            FileStream fs = new FileStream(StringResource.FilesName, FileMode.OpenOrCreate);

            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            string line = sr.ReadLine();
            while (line != null)
            {
                //if (string.IsNullOrEmpty(line)) continue;
                Lb_Items.Items.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
        }

        private void Btn_Select_File_Click(object sender, RoutedEventArgs e)
        {
            if (Lb_Items.SelectedItem == null) return;
            string str = Lb_Items.SelectedItem.ToString();
            set.Reset(str);
            InitialDatas();

            //TODO 保存到文件
            ConfigHelper.SetValue(StringResource.KeyFileName, str);
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            string str = Tb_Name.Text.Trim(' ');
            if (string.IsNullOrWhiteSpace(str))
            {
                System.Windows.MessageBox.Show("请输入文本！");
                return;
            }
            //Lb_Names.ItemsSource = null;
            listNames.Add(str);
            Lb_Names.Items.Refresh();

            set.AddItem(str);
            Tb_Name.Text = "";
            Tb_Name.Focus();

            FileStream fs = new FileStream(set.GetCurrentFileName(), FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            sw.Write(str + "\r\n");
            sw.Close();
            fs.Close();
        }

        private void Btn_Import_File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "文本文件(*.txt)|*.txt"
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string str = dialog.FileName;
                str = str.Substring(str.LastIndexOf('\\') + 1);
                if (!Lb_Items.Items.Contains(str))
                {
                    Lb_Items.Items.Add(str);

                    FileStream fs = new FileStream(StringResource.FilesName, FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
                    sw.WriteLine(str);
                    sw.Close();
                    fs.Close();
                }
            }
        }

        private void Cb_Speech_Checked(object sender, RoutedEventArgs e)
        {
            ConfigHelper.SetValue(StringResource.KeySpeech, bool.TrueString);
        }

        private void Cb_Speech_Unchecked(object sender, RoutedEventArgs e)
        {
            ConfigHelper.SetValue(StringResource.KeySpeech, bool.FalseString);
        }

        private void Cb_Speech_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
