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
        }

        internal void SetDrawSet(DrawSet s)
        {
            set = s;
            InitialDatas();
        }
        private void InitialDatas()
        {
            StringBuilder builder = new StringBuilder();
            foreach (String line in set.GetAllValues())
            {
                builder.Append(line);
                builder.AppendLine();
            }
            Tb_Names.Text = builder.ToString();
            SetLabelCurrentFile(set.GetCurrentFileName());
        }
        private void SetLabelCurrentFile(String str)
        {
            Lb_CurrentFile.Content = "当前名单：" + str;
        }
        private void SetFileItems()
        {
            FileStream fs = new FileStream("files.txt", FileMode.OpenOrCreate);

            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            String line = sr.ReadLine();
            while (line != null)
            {
                //set.Add(line);
                Lb_Items.Items.Add(line);
                line = sr.ReadLine();
            }
        }

        private void Btn_Select_File_Click(object sender, RoutedEventArgs e)
        {
            String str = Lb_Items.SelectedItem.ToString();
            //SetLabelCurrentFile(str);
            set.Reset(str);
            InitialDatas();

            //TODO 保存到文件
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            String str = Tb_Name.Text.Trim(' ');
            if(str==String.Empty)
            {
                System.Windows.MessageBox.Show("请输入文本！");
                return;
            }
            Tb_Names.Text = Tb_Names.Text + str + "\n";
            set.AddItem(str);
            Tb_Name.Text = "";
            Tb_Name.Focus();

            //TODO 保存到文件
        }

        private void Btn_Import_File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "文本文件(*.txt)|*.txt";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String str = dialog.FileName;
                str = str.Substring(str.LastIndexOf('\\')+1);
                //System.Windows.MessageBox.Show(str);
                if(!Lb_Items.Items.Contains(str))
                {
                    Lb_Items.Items.Add(str);

                    //TODO 保存到文件
                }
            }
        }
    }
}
