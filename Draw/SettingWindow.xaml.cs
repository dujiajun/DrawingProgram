using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace Drawing
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        private DrawSet set;
        public SettingWindow()
        {
            InitializeComponent();
            SetFileItems();
            //System.Windows.MessageBox.Show(ConfigHelper.GetValue(StringResource.KeySpeech, bool.FalseString));
            Cb_Speech.IsChecked = bool.TrueString.Equals(ConfigHelper.GetValue(StringResource.KeySpeech, bool.TrueString));
        }

        internal void SetDrawSet(DrawSet s)
        {
            set = s;
            InitialDatas();
        }
        private void InitialDatas()
        {
            StringBuilder builder = new StringBuilder();
            foreach (string line in set.GetAllValues())
            {
                builder.Append(line);
                builder.AppendLine();
            }
            Tb_Names.Text = builder.ToString();
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
            if (string.IsNullOrWhiteSpace(Tb_Names.Text)) Tb_Names.Text = str;
            else Tb_Names.Text = Tb_Names.Text.TrimEnd('\n') + "\n" + str;
            set.AddItem(str);
            Tb_Name.Text = "";
            Tb_Name.Focus();

            FileStream fs = new FileStream(set.GetCurrentFileName(), FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            sw.Write(Tb_Names.Text);
            sw.Close();
            fs.Close();
        }

        private void Btn_Import_File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "文本文件(*.txt)|*.txt";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string str = dialog.FileName;
                str = str.Substring(str.LastIndexOf('\\') + 1);
                if (!Lb_Items.Items.Contains(str))
                {
                    Lb_Items.Items.Add(str);

                    FileStream fs = new FileStream(StringResource.FilesName, FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
                    sw.WriteLine(str + "\n");
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
