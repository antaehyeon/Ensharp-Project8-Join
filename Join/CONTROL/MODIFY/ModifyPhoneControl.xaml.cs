using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;

namespace Join
{
    /// <summary>
    /// ModifyPhoneControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ModifyPhoneControl : UserControl
    {
        public bool phoneNumFirstCheck = false;

        public ModifyPhoneControl()
        {
            InitializeComponent();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            phoneNumFirstCheck = true;
            phoneNumberCheck();
        }

        private void txtBox_phoneNumSec_KeyUp(object sender, KeyEventArgs e)
        {
            phoneNumberCheck();
        }

        private void txtBox_phoneNumThird_KeyUp(object sender, KeyEventArgs e)
        {
            phoneNumberCheck();
        }

        public void phoneNumberCheck()
        {
            if (phoneNumFirstCheck && Regex.IsMatch(txtBox_phoneNumSec.Text, @"^[0-9]{3,4}$") && Regex.IsMatch(txtBox_phoneNumThird.Text, @"^[0-9]{4,4}$"))
            {
                lbl_result.Foreground = Brushes.Green;
                lbl_result.Content = "변경 가능한 핸드폰번호 입니다";
                btn_modifyPhone_ok.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_result.Foreground = Brushes.Red;
                lbl_result.Content = "핸드폰번호가 제대로 입력되지 않았습니다";
                btn_modifyPhone_ok.Visibility = Visibility.Hidden;
            }
        }
    }
}
