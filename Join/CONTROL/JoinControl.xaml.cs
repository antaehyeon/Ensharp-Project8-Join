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
    /// JoinControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class JoinControl : UserControl
    {
        SharingData sd;
        MySQL mysql;

        bool idChk = false;
        bool pwChk = false;
        bool nameChk = false;
        bool addressChk = true;
        bool emailIdChk = false;
        bool emailDomainChk = false;
        bool phoneChk = false;
        bool sexChk = false;
        private bool dateChk = false;
        private bool complete = false;

        string sex = "";

        public bool DateChk
        {
            get { return dateChk; }
            set { dateChk = value; }
        }

        public bool Complete
        {
            get { return complete; }
            set { complete = value; }
        }

        public JoinControl()
        {
            InitializeComponent();

            sd = SharingData.GetInstance();
            mysql = new MySQL();
        }

        // e.Handle
        // true : 해당 키 무시
        // false : 해당 키 입력
        private void txtBox_id_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // 아이디 박스에 어떤 키라도 입력되었을 경우
            // 체크값 false
            idChk = false;

            if(e.Key.Equals(Key.Tab))
            {
                e.Handled = true;
                passwordBox.Focus();
            }
        }

        // 해당 텍스트박스에 Focus가 해제되었을 때 Event 발생
        private void txtBox_id_LostFocus(object sender, RoutedEventArgs e)
        {
            // 아이디가 공백일 경우
            if (string.IsNullOrWhiteSpace(txtBox_id.Text))
            {
                lbl_help.Foreground = Brushes.Red;
                lbl_help.Content = "아이디는 공백을 사용할 수 없습니다";
                lbl_id.Foreground = Brushes.Red;
                return;
            }

            // 아이디가 중복되었는지 검사
            for (int i = 0; i < sd.MemberList.Count; i++)
            {
                if (txtBox_id.Text.Equals(sd.MemberList[i].Id))
                {
                    txtBox_id.Text = "";
                    lbl_help.Foreground = Brushes.Red;
                    lbl_help.Content = "아이디가 중복됩니다";
                    return;
                }
            }

            // 아이디 형식이 맞을경우
            if (Regex.IsMatch(txtBox_id.Text, "^[a-z]+[a-z0-9]{6,14}$"))
            {
                lbl_help.Foreground = Brushes.Green;
                lbl_help.Content = "현재 아이디를 사용하셔도 좋습니다";
                lbl_id.Foreground = Brushes.Green;
                idChk = true;
            }
            else
            {
                lbl_help.Foreground = Brushes.Red;
                lbl_help.Content = "아이디는 6자이상, 영어와 숫자만 가능합니다";
                lbl_id.Foreground = Brushes.Red;
            }
        }

        // 패스워드 텍스트박스 Key Event
        private void passwordBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            pwChk = false;

            if (e.Key.Equals(Key.Space))
            {
                e.Handled = true;
            }
            else if (e.Key.Equals(Key.Tab))
            {
                e.Handled = true;
                passwordBox_chk.Focus();
            }
        }

        // 패스워드 테스트박스 KeyUp Event
        private void passwordBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            // 패스워드가 일치한다면 
            if (passwordBox.Password.Equals(passwordBox_chk.Password))
            {
                lbl_pw.Foreground = Brushes.Green;
                lbl_pwChk.Foreground = Brushes.Green;
            }
            else
            {
                lbl_pwChk.Foreground = Brushes.Red;
            }
        }

        // 패스워드 박스 Lost Focus
        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox_chk.Password.Length > 0)
            {
                if (passwordBox.Password.Equals(passwordBox_chk.Password))
                {
                    lbl_pw.Foreground = Brushes.Green;
                    lbl_pwChk.Foreground = Brushes.Green;
                    lbl_help.Foreground = Brushes.Green;
                    lbl_help.Content = "비밀번호가 일치합니다";
                    pwChk = true;
                }
                else
                {
                    lbl_pwChk.Foreground = Brushes.Red;
                    passwordBox_chk.Password = "";
                    lbl_help.Foreground = Brushes.Red;
                    lbl_help.Content = "비밀번호가 일치하지 않습니다";
                    passwordBox_chk.Focus();
                }
            }
            else
            {
                lbl_pw.Foreground = Brushes.Red;
                lbl_help.Foreground = Brushes.Red;
                lbl_help.Content = "비밀번호가 일치하지 않습니다";
            }
        }

        // 패스워드확인 텍스트박스 Key Event
        private void passwordBox_chk_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            pwChk = false;

            if (e.Key.Equals(Key.Space))
            {
                e.Handled = true;
            }
            else if (e.Key.Equals(Key.Tab))
            {
                e.Handled = true;
                txtBox_name.Focus();
            }
        }

        // 패스워드확인 텍스트박스 KeyUp Event
        private void passwordBox_chk_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            // 패스워드가 일치한다면 
            if (passwordBox.Password.Equals(passwordBox_chk.Password))
            {
                lbl_pw.Foreground = Brushes.Green;
                lbl_pwChk.Foreground = Brushes.Green;
                lbl_help.Foreground = Brushes.Green;
                lbl_help.Content = "비밀번호가 일치합니다";
                pwChk = true;
            }
            else
            {
                lbl_pwChk.Foreground = Brushes.Red;
                lbl_help.Foreground = Brushes.Red;
                lbl_help.Content = "비밀번호가 일치하지 않습니다";
            }
        }

        // 패스워드 확인 텍스트박스 Lost Focus
        private void passwordBox_chk_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password.Equals(passwordBox_chk.Password))
            {
                // 공백일경우
                if (passwordBox.Password.Equals("") || passwordBox.Password.Contains(' '))
                {
                    lbl_pw.Foreground = Brushes.Red;
                    lbl_pwChk.Foreground = Brushes.Red;
                    lbl_help.Foreground = Brushes.Red;
                    lbl_help.Content = "비밀번호는 공백을 사용할 수 없습니다";
                    return;
                }
                lbl_pw.Foreground = Brushes.Green;
                lbl_pwChk.Foreground = Brushes.Green;
                lbl_help.Foreground = Brushes.Green;
                lbl_help.Content = "비밀번호가 일치합니다";
                pwChk = true;
            }
            else
            {
                lbl_pwChk.Foreground = Brushes.Red;
                lbl_help.Foreground = Brushes.Red;
                lbl_help.Content = "비밀번호가 일치하지 않습니다";
            }
        }

        // 이름 텍스트박스 Key Event
        private void txtBox_name_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.ImeProcessed) || e.Key.Equals(Key.Tab) || e.Key.Equals(Key.Back))
            {
                e.Handled = false;
            }
            else if (e.Key.Equals(Key.Tab))
            {
                e.Handled = true;
                btn_findAddress.Focus();
            }
            else
            {
                e.Handled = true;
            }
        }

        // 주소부분
        private void txtBox_addressDetails_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Tab))
            {
                e.Handled = true;
                txtBox_mail.Focus();
            }
        }

        // 이름 텍스트박스 Lost Focus
        private void txtBox_name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(txtBox_name.Text, "^[가-힣]+$"))
            {
                lbl_help.Foreground = Brushes.Green;
                lbl_help.Content = "사용할 수 있는 이름입니다";
                lbl_name.Foreground = Brushes.Green;
                nameChk = true;
            }
            else
            {
                txtBox_name.Text = "";
                lbl_help.Foreground = Brushes.Red;
                lbl_help.Content = "이름을 정확히 입력해주세요";
                lbl_name.Foreground = Brushes.Red;
            }
        }


        // 이메일 ID 부분 Key Event
        private void txtBox_mail_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            emailIdChk = false;
        }

        // 이메일 ID 부분 Lost Focus
        private void txtBox_mail_LostFocus(object sender, RoutedEventArgs e)
        {
            // 이메일 중복체크
            if (emailDuplicationCheck()) return;

            if (Regex.IsMatch(txtBox_mail.Text, "^[a-z0-9]{6,14}$"))
            {
                lbl_help.Foreground = Brushes.Green;
                lbl_help.Content = "사용할 수 있는 이메일 ID 입니다";
                emailIdChk = true;

                if (!emailDomainChk)
                {
                    lbl_email.Foreground = Brushes.Red;
                    lbl_help.Foreground = Brushes.Red;
                    lbl_help.Content = "도메인을 선택해주세요";
                }
                else
                {
                    emailDomainChk = true;
                }
                return;
            }
            else
            {
                txtBox_mail.Text = "";
                lbl_help.Foreground = Brushes.Red;
                lbl_help.Content = "이메일은 6자이상, 영어와 숫자만 가능합니다";
                lbl_email.Foreground = Brushes.Red;
            }
        }

        // 이메일 도메인 Select Event
        private void comboBox_eamilDomain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_eamilDomain.Text.Equals("")) return;

            emailDomainChk = true;
            if (comboBox_eamilDomain.IsEditable) return;

            var selected_text = ((System.Windows.Controls.ContentControl)comboBox_eamilDomain.SelectedValue).Content;

            if (selected_text.Equals("직접입력"))
            {
                comboBox_eamilDomain.IsEditable = true;
            }
        }

        // 이메일 도메인 lost focus
        private void comboBox_eamilDomain_LostFocus(object sender, RoutedEventArgs e)
        {
            if (emailDuplicationCheck()) return;

            if (comboBox_eamilDomain.Text.Contains(".com") ||
                comboBox_eamilDomain.Text.Contains(".kr") ||
                comboBox_eamilDomain.Text.Contains(".net"))
            {
                if (emailIdChk)
                {
                    lbl_email.Foreground = Brushes.Green;
                    lbl_help.Foreground = Brushes.Green;
                    lbl_help.Content = "사용할 수 있는 이메일 ID 입니다";
                    emailDomainChk = true;
                }
                else
                {
                    lbl_email.Foreground = Brushes.Red;
                    lbl_help.Foreground = Brushes.Red;
                    lbl_help.Content = "이메일 아이디를 확인하세요";
                }
            }

            else
            {
                comboBox_eamilDomain.Text = "";
                lbl_email.Foreground = Brushes.Red;
                lbl_help.Foreground = Brushes.Red;
                lbl_help.Content = "이메일 도메인을 확인하세요";
            }
        }

        //핸드폰번호 첫째자리 ComboBox Select Event
        private void comboBox_phoneNumFirst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (phoneNumDuplicationCheck()) return;

            phoneChk = false;
            txtBox_phoneNumSecond.Focus();
        }

        // 핸드폰번호 둘째자리 Key Event
        private void txtBox_phoneNumSecond_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            phoneChk = false;
            if (e.Key.Equals(Key.Back))
            {
                e.Handled = false;
                return;
            }
            else if (e.Key.Equals(Key.Tab) || txtBox_phoneNumSecond.Text.Length.Equals(4))
            {
                e.Handled = true;
                txtBox_phoneNumThird.Focus();
            }
        }

        // 핸드폰 둘째자리 Lost Focus
        private void txtBox_phoneNumSecond_LostFocus(object sender, RoutedEventArgs e)
        {
            if (phoneNumDuplicationCheck()) return;

            if (!Regex.IsMatch(txtBox_phoneNumSecond.Text, @"^[0-9]{3,4}$"))
            {
                txtBox_phoneNumSecond.Text = "";
                lbl_phoneNumber.Foreground = Brushes.Red;
                lbl_help.Foreground = Brushes.Red;
                lbl_help.Content = "핸드폰번호를 제대로 입력해주세요";
                return;
            }
        }

        // 핸드폰 세번째자리 LostFocus
        private void txtBox_phoneNumThird_LostFocus(object sender, RoutedEventArgs e)
        {
            if (phoneNumDuplicationCheck()) return;

            if (comboBox_phoneNumFirst.Text.Length > 0 &&
                Regex.IsMatch(txtBox_phoneNumSecond.Text, @"^[0-9]{3,4}$") &&
                Regex.IsMatch(txtBox_phoneNumThird.Text, @"^[0-9]{4,4}$"))
            {
                lbl_phoneNumber.Foreground = Brushes.Green;
                lbl_help.Foreground = Brushes.Green;
                lbl_help.Content = "정상적인 핸드폰 번호입니다";
                phoneChk = true;
            }
            else
            {
                comboBox_phoneNumFirst.Text = "";
                txtBox_phoneNumSecond.Text = "";
                txtBox_phoneNumThird.Text = "";
                lbl_phoneNumber.Foreground = Brushes.Red;
                lbl_help.Foreground = Brushes.Red;
                lbl_help.Content = "핸드폰번호를 제대로 입력해주세요";
                phoneChk = true;
                return;
            }
        }

        // 남자가 체크되었을 경우
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            chkBox_woman.IsEnabled = false;
            sexChk = true;
            lbl_sex.Foreground = Brushes.Green;
            txtBox_year.Focus();
            sex = "남자";
        }

        // 남자가 체크 해제되었을 경우
        private void chkBox_man_Unchecked(object sender, RoutedEventArgs e)
        {
            chkBox_woman.IsEnabled = true;
            lbl_sex.Foreground = Brushes.Red;
            sexChk = false;
            sex = "";
        }

        // 여자가 체크 되었을 경우
        private void chkBox_woman_Checked(object sender, RoutedEventArgs e)
        {
            chkBox_man.IsEnabled = false;
            lbl_sex.Foreground = Brushes.Green;
            txtBox_year.Focus();
            sexChk = true;
            sex = "여자";
        }

        // 여자가 체크 해제되었을 경우
        private void chkBox_woman_Unchecked(object sender, RoutedEventArgs e)
        {
            lbl_sex.Foreground = Brushes.Red;
            chkBox_man.IsEnabled = true;
            sexChk = false;
            sex = "";
        }

        // 부모-자식간의 데이터 교환작업
        private void btn_calendar_Click(object sender, RoutedEventArgs e)
        {
            CalendarWindow calendar = new CalendarWindow(this);
            calendar.Show();
            btn_calendar.IsEnabled = false;
        }

        private void btn_joinMember_Click(object sender, RoutedEventArgs e)
        {
            if (idChk && pwChk && nameChk && addressChk && emailIdChk && emailDomainChk && phoneChk && sexChk && DateChk)
            {
                string eMail = txtBox_mail.Text + "@" + comboBox_eamilDomain.Text;
                string birthDate = txtBox_year.Text + "." + txtBox_month.Text + "." + txtBox_day.Text;
                string phoneNumber = comboBox_phoneNumFirst.SelectionBoxItem.ToString() + "-" + txtBox_phoneNumSecond.Text + "-" + txtBox_phoneNumThird.Text;

                MemberVO newMember = new MemberVO(
                    txtBox_id.Text,
                    passwordBox.Password,
                    txtBox_name.Text,
                    phoneNumber,
                    txtBox_postalCode.Text,
                    txtBox_addressDetails.Text,
                    eMail,
                    sex,
                    birthDate
                    );

                mysql.insertMemberData(newMember);
                MessageBox.Show("회원가입이 정상적으로 진행되었습니다");
                Complete = true;
            }
            else
            {
                lbl_help.Content = "정보를 제대로 입력하세요";
                lbl_help.Foreground = Brushes.Red;
            }
        }

        public bool emailDuplicationCheck()
        {
            string email = txtBox_mail.Text + "@" + comboBox_eamilDomain.Text;
            for (int i = 0; i < sd.MemberList.Count; i++)
            {
                if (email.Equals(sd.MemberList[i].Email))
                {
                    txtBox_mail.Text = "";
                    comboBox_eamilDomain.Text = "";
                    emailIdChk = false;
                    emailDomainChk = false;
                    lbl_help.Foreground = Brushes.Red;
                    lbl_help.Content = "이메일이 중복됩니다";
                    return true;
                }
            }
            return false;
        }

        public bool phoneNumDuplicationCheck()
        {
            string phoneNum = comboBox_phoneNumFirst.Text + "-" + txtBox_phoneNumSecond.Text + "-" + txtBox_phoneNumThird.Text;

            for(int i = 0; i <sd.MemberList.Count; i++)
            {
                if(phoneNum.Equals(sd.MemberList[i].PhoneNumber))
                {
                    phoneChk = false;
                    MessageBox.Show("핸드폰번호가 중복됩니다");
                    comboBox_phoneNumFirst.Text = "";
                    txtBox_phoneNumSecond.Text = "";
                    txtBox_phoneNumThird.Text = "";
                    return true;
                }
            }
            return false;
        }
    }
}
