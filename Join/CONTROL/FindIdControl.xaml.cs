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
    /// FindIdControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FindIdControl : UserControl
    {
        bool selectFirstPN = false;
        SharingData sd;

        public FindIdControl()
        {
            InitializeComponent();
            sd = SharingData.GetInstance();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e) { }

        private void comboBox_PhoneNumFirst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectFirstPN = true;
            txtBox_PhoneNumSec.Focus();
        }

        private void txtBox_PhoneNumSec_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(txtBox_PhoneNumSec.Text, @"^[0-9]{3,4}$"))
            {
                lbl_help.Foreground = Brushes.Red;
                lbl_help.Content = "핸드폰번호 두번째 자리를 정확히 입력해주세요";
                txtBox_PhoneNumSec.Text = "";
                return;
            }
        }

        private void txtBox_PhoneNumThird_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(txtBox_PhoneNumSec.Text, @"^[0-9]{3,4}$"))
            {
                lbl_help.Foreground = Brushes.Red;
                lbl_help.Content = "핸드폰번호 세번째 자리를 정확히 입력해주세요";
                txtBox_PhoneNumThird.Text = "";
                return;
            }
        }

        private void txtBox_PhoneNumThird_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (selectFirstPN && txtBox_PhoneNumSec.Text.Length > 2 && txtBox_PhoneNumThird.Text.Length.Equals(4))
            {
                string phoneNum = comboBox_PhoneNumFirst.SelectionBoxItem.ToString() + "-" + txtBox_PhoneNumSec.Text + "-" + txtBox_PhoneNumThird.Text;

                for (int i = 0; i < sd.MemberList.Count; i++)
                {
                    if (phoneNum.Equals(sd.MemberList[i].PhoneNumber))
                    {
                        lbl_help.Content = "";
                        lbl_result.Foreground = Brushes.Green;
                        lbl_result.Content = "입력하신 핸드폰 번호와 맞는 아이디는 " + sd.MemberList[i].Id + " 입니다";
                        return;
                    }
                }

                lbl_result.Foreground = Brushes.Red;
                lbl_result.Content = "입력하신 정보와 맞는 아이디가 존재하지 않습니다";
            }
        }



    }
}
