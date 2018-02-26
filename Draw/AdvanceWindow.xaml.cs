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
            Lb_Number.Content = set.GetRandomNumber(int.Parse(Tb_min.Text), int.Parse(Tb_max.Text)).ToString();

        }

        private void Btn_Several_Click(object sender, RoutedEventArgs e)
        {
            //Lb_Ans.Items.Clear();
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
