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
    /// FindPwControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FindPwControl : UserControl
    {
        SharingData sd;
        bool domainSelect = false;

        int index = 0;

        public FindPwControl()
        {
            InitializeComponent();

            sd = SharingData.GetInstance();
        }

        private void btn_fpc_back_Click(object sender, RoutedEventArgs e) { }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_Domain.Text.Equals("")) return;

            result();
            domainSelect = true;
            if (comboBox_Domain.IsEditable) return;

            ComboBox a = sender as ComboBox;
            ComboBoxItem c = a.SelectedItem as ComboBoxItem;
            string selected_text = c.Content.ToString();

            if (selected_text.Equals("직접입력"))
            {
                comboBox_Domain.IsEditable = true;
            }
        }

        private void comboBox_Domain_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            result();
        }


        private void comboBox_Domain_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            result();
        }

        public void result()
        {
            if(!findId())
            {
                lbl_result.Foreground = Brushes.Red;
                lbl_result.Content = "입력하신 ID와 일치하는 정보가 없습니다";
            }
            else if(!matchEmail())
            {
                lbl_result.Foreground = Brushes.Red;
                lbl_result.Content = "입력하신 이메일과 일치하는 정보가 없습니다";
            }
            else
            {
                lbl_result.Foreground = Brushes.Green;
                lbl_result.Content = "비밀번호는 " + sd.MemberList[index].Pw + " 입니다";
            }
        }

        public bool findId()
        {
            if(txtBox_ID.Text.Length > 0 && txtBox_email.Text.Length > 0 && domainSelect)
            {
                for (int i =0; i<sd.MemberList.Count; i++)
                {
                    if (sd.MemberList[i].Id.Equals(txtBox_ID.Text))
                    {
                        index = i;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool matchEmail()
        {
            string eMail = txtBox_email.Text + "@" + comboBox_Domain.Text;

            if(sd.MemberList[index].Email.Equals(eMail))
            {
                return true;
            }
            return false;
        }

        private void txtBox_ID_LostFocus(object sender, RoutedEventArgs e)
        {
            result();
        }

        private void txtBox_email_LostFocus(object sender, RoutedEventArgs e)
        {
            result();
        }
    }
}
