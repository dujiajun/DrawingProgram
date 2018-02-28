using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Drawing
{
    /// <summary>
    /// AdvanceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AdvanceWindow : Window
    {
        private DrawSet set;
        public AdvanceWindow()
        {
            InitializeComponent();
        }
        internal void SetDrawSet(DrawSet s)
        {
            set = s;
        }
        private void Btn_Number_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(Tb_max.Text)||string.IsNullOrWhiteSpace(Tb_min.Text))
            {
                MessageBox.Show("请输入范围！");
                return;
            }
            Lb_Number.Content = set.GetRandomNumber(int.Parse(Tb_min.Text), int.Parse(Tb_max.Text)).ToString();
        }

        private void Btn_Several_Click(object sender, RoutedEventArgs e)
        {
            //Lb_Ans.Items.Clear();
            if (string.IsNullOrWhiteSpace(Tb_num.Text))
            {
                MessageBox.Show("请抽取个数！");
                return;
            }
            string[] ans = set.DrawTimes(int.Parse(Tb_num.Text));
            Lb_Ans.ItemsSource = ans;
        }

        private void Btn_Upset_Click(object sender, RoutedEventArgs e)
        {
            string[] ans = set.Upset();
            Lb_Ans.ItemsSource = ans;
        }
    }
}
