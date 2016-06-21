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

namespace Join
{
    /// <summary>
    /// ModifyPasswordControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ModifyPasswordControl : UserControl
    {
        public ModifyPasswordControl()
        {
            InitializeComponent();
        }

        private void passwordBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            passwordChk();
        }

        private void passwordBox_chk_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            passwordChk();
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            passwordChk();
        }

        private void passwordBox_chk_LostFocus(object sender, RoutedEventArgs e)
        {
            passwordChk();
        }

        public void passwordChk()
        {
            // 일단은 비밀번호가 일치할 경우
            if(passwordBox.Password.Equals(passwordBox_chk.Password))
            {
                // 그런데 공백이 있거나, 아무것도 입력이 안되있을경우
                if (passwordBox.Password.Equals("") || passwordBox.Password.Contains(' '))
                {
                    passwordformError();
                }
                // 패스워드가 6자 이하일 경우
                else if (passwordBox.Password.Length < 6)
                {
                    passwordLengthError();
                }
                // 모두 일치할 경우
                else
                {
                    passwordCorrespond();
                }
            }
            // 아예 일치하지도 않을경우
            else
            {
                passwordDiscord();
            }
        }

        public void passwordCorrespond()
        {
            lbl_result.Foreground = Brushes.Green;
            lbl_result.Content = "비밀번호가 일치합니다";
            btn_pwChange.Visibility = Visibility.Visible;
        }

        public void passwordDiscord()
        {
            lbl_result.Foreground = Brushes.Red;
            lbl_result.Content = "비밀번호가 일치하지 않습니다";
            btn_pwChange.Visibility = Visibility.Hidden;
        }

        public void passwordformError()
        {
            lbl_result.Foreground = Brushes.Red;
            lbl_result.Content = "비밀번호를 제대로 입력해주세요";
            btn_pwChange.Visibility = Visibility.Hidden;
        }

        public void passwordLengthError()
        {
            lbl_result.Foreground = Brushes.Red;
            lbl_result.Content = "비밀번호는 6자 이상이여야 합니다.";
            btn_pwChange.Visibility = Visibility.Hidden;
        }


    }
}
