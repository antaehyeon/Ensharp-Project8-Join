using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
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
    public partial class BeginWindow : Window
    {
        SharingData sd;
        MySQL mysql;

        BeginControl begincontrol;
        FindInfoControl fc;
        FindIdControl fic;
        FindPwControl fpc;
        LoginControl lc;
        DropControl dc;
        ClockControl cc;
        ModifyControl mc;
        ModifyPasswordControl mpc;
        ModifyNameControl mnc;
        ModifyPhoneControl mpnc;
        ModifyAddressControl mac;

        int index = 0;
        int mode = LOGIN;

        const int LOGIN = 0;
        const int AFTERLOGIN = 1;
        const int DROP = 2;
        const int MODIFY = 3;
        const int FIND = 4;
        const int FINDID = 5;
        const int FINDPW = 6;
        const int MODIFYPW = 7;
        const int MODIFYADR = 8;
        const int MODIFYNAME = 9;
        const int MODIFYPHONE = 10;

        public BeginWindow()
        {
            InitializeComponent();

            // 시계
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            begincontrol = new BeginControl();
            fc = new FindInfoControl();
            fic = new FindIdControl();
            fpc = new FindPwControl();
            lc = new LoginControl();
            dc = new DropControl();
            cc = new ClockControl();
            mc = new ModifyControl();
            mpc = new ModifyPasswordControl();
            mnc = new ModifyNameControl();
            mpnc = new ModifyPhoneControl();
            mac = new ModifyAddressControl();

            sd = SharingData.GetInstance();
            mysql = new MySQL();

            BeginGrid.Children.Add(begincontrol);
            BeginGrid.Children.Add(fc);
            BeginGrid.Children.Add(fic);
            BeginGrid.Children.Add(fpc);
            BeginGrid.Children.Add(lc);
            BeginGrid.Children.Add(dc);
            BeginGrid.Children.Add(cc);
            BeginGrid.Children.Add(mc);
            BeginGrid.Children.Add(mpc);
            BeginGrid.Children.Add(mnc);
            BeginGrid.Children.Add(mpnc);
            BeginGrid.Children.Add(mac);

            begincontrol.Visibility = Visibility.Hidden;
            fc.Visibility = Visibility.Hidden;
            fic.Visibility = Visibility.Hidden;
            fpc.Visibility = Visibility.Hidden;
            lc.Visibility = Visibility.Hidden;
            dc.Visibility = Visibility.Hidden;
            mc.Visibility = Visibility.Hidden;

            mpc.Visibility = Visibility.Hidden;
            mnc.Visibility = Visibility.Hidden;
            mpnc.Visibility = Visibility.Hidden;
            mac.Visibility = Visibility.Hidden;

            begincontrol.btn_join.Click += btnJoinClick;
            begincontrol.btn_login.Click += btn_login_Click;
            begincontrol.btn_findInfo.Click += btn_findInfo_Click;
            begincontrol.passwordBox.PreviewKeyDown += passwordBox_PreviewKeyDown;
            begincontrol.txtBox_id.PreviewKeyDown += passwordBox_PreviewKeyDown;

            /* FIND CONTROL */
            fc.btn_fc_back.Click += btn_fc_back_Click;
            fc.btn_findId.Click += btn_findId_Click;
            fc.btn_findPw.Click += btn_findPw_Click;

            /* FIND ID CONTROL */
            fic.btn_back.Click += btn_fic_back_Click;

            /* FIND PW CONTROL */
            fpc.btn_fpc_back.Click += btn_fpc_back_Click;

            /* FIND AFTER LOGIN CONTROL */
            lc.btn_logOut.Click += btn_logOut_Click;
            lc.btn_modify.Click += btn_modify_Click;
            lc.btn_drop.Click += btn_drop_Click;

            /* DROP CONTROL */
            dc.btn_yes.Click += btn_yes_Click;
            dc.btn_no.Click += btn_no_Click;

            /* MODFIY CONTROL */
            mc.btn_back.Click += btn_modify_back_Click;
            mc.btn_modifyPW.Click += btn_modifyPw_Click;
            mc.btn_modifyName.Click += btn_modifyName_Click;
            mc.btn_modifyPhone.Click += btn_modifyPhone_Click;
            mc.btn_modifyAdr.Click += btn_modifyAddress_Click;

            /* MODIFY PASSWORD CONTROL */
            mpc.btn_back.Click += btn_modifyPw_back_Click;
            mpc.btn_pwChange.Click += btn_modifyPw_ok_Click;

            /* MODIFY NAME CONTROL */
            mnc.btn_back.Click += btn_modifyName_back_Click;
            mnc.btn_modifyName_ok.Click += btn_modifyName_ok_Click;

            /* MODIFY PHONE CONTROL */
            mpnc.btn_back.Click += btn_modifyPhoneBack_Click;
            mpnc.btn_modifyPhone_ok.Click += btn_modifyPhoneOk_Click;

            /* MODIFY ADDRESS CONTROL */
            mac.btn_back.Click += btn_modifyAdrBack_Click;
            mac.btn_modifyAddress_ok.Click += btn_modifyAdrOk_Click;
        }

        // 폼이 처음 로드될 때
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mysql.DBDataToList();
            begincontrol.txtBox_id.Focus();
        }

        // 시계
        void timer_Tick(object sender, EventArgs e)
        {
            cc.lbl_clock.Content = DateTime.Now.ToLongTimeString();
        }

        // 회원가입버튼 클릭 메소드
        private void btnJoinClick(object sender, RoutedEventArgs e)
        {
            JoinWindow joinwindow = new JoinWindow(this);
            joinwindow.Show();
            this.Hide();
        }

        // 로그인 메소드
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            if(!findId())
            {
                MessageBox.Show("일치하는 아이디가 없습니다");
                begincontrol.txtBox_id.Text = "";
                begincontrol.passwordBox.Password = "";
            }
            else if(!findPw())
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다");
                begincontrol.passwordBox.Password = "";
            }
            else
            {
                this.Width = 562;
                this.Height = 347;
                sd.CurrentId = begincontrol.txtBox_id.Text;
                begincontrol.Visibility = Visibility.Hidden;
                lc.Visibility = Visibility.Visible;
                begincontrol.txtBox_id.Text = "";
                begincontrol.passwordBox.Password = "";
                mode = AFTERLOGIN;
            }
        }

        public bool findId()
        {
            for (int i= 0; i< sd.MemberList.Count; i++)
            {
                if(sd.MemberList[i].Id.Equals(begincontrol.txtBox_id.Text))
                {
                    index = i;
                    return true;
                }
            }
            return false;
        }

        public bool findPw()
        {
            if(sd.MemberList[index].Pw.Equals(begincontrol.passwordBox.Password))
            {
                return true;
            }
            return false;
        }

        // BeginControl 에서 ID/PW 찾기 클릭시
        private void btn_findInfo_Click(object sender, RoutedEventArgs e)
        {
            this.Width = 589;
            this.Height = 336;
            begincontrol.Visibility = Visibility.Hidden;
            fc.Visibility = Visibility.Visible;
            mode = FIND;
        }

        // 맨처음 화면에서 패스워드칸에서 엔터가 입력되었을 경우
        private void passwordBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key.Equals(Key.Enter))
            {
                btn_login_Click(null, null);
            }
        }

        // FindControl 에서 뒤로가기 버튼 클릭시
        private void btn_fc_back_Click(object sender, RoutedEventArgs e)
        {
            this.Width = 542;
            this.Height = 244;
            fc.Visibility = Visibility.Hidden;
            begincontrol.Visibility = Visibility.Visible;
            mode = LOGIN;
        }

        // FindControl 에서 ID 찾기 버튼 클릭시
        private void btn_findId_Click(object sender, RoutedEventArgs e)
        {
            fc.Visibility = Visibility.Hidden;
            fic.Visibility = Visibility.Visible;
            mode = FINDID;
        }

        // FIndControl 에서 PW 찾기 버튼 클릭시
        private void btn_findPw_Click(object sender, RoutedEventArgs e)
        {
            fc.Visibility = Visibility.Hidden;
            fpc.Visibility = Visibility.Visible;
            mode = FINDPW;
        }

        // FindIdControl 에서 뒤로가기 버튼 클릭시
        private void btn_fic_back_Click(object sender, RoutedEventArgs e)
        {
            fic.comboBox_PhoneNumFirst.Text = "";
            fic.txtBox_PhoneNumThird.Text = "";
            fic.txtBox_PhoneNumSec.Text = "";
            fic.lbl_help.Content = "";
            fic.lbl_result.Content = "";

            fic.Visibility = Visibility.Hidden;
            fc.Visibility = Visibility.Visible;
            mode = FIND;
        }

        // FindPwControl 에서 뒤로가기 버튼 클릭시
        private void btn_fpc_back_Click(object sender, RoutedEventArgs e)
        {
            fpc.txtBox_ID.Text = "";
            fpc.txtBox_email.Text = "";
            fpc.comboBox_Domain.Text = "";
            fpc.lbl_result.Content = "";

            fpc.Visibility = Visibility.Hidden;
            fc.Visibility = Visibility.Visible;
            mode = FIND;
        }

        // LoginControl 에서 로그아웃 버튼 클릭시
        private void btn_logOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("정상적으로 로그아웃 처리 되었습니다");
            sd.CurrentId = "";
            sd.CurrentName = "";
            sd.CurrentIndex = 0;
            this.Width = 542;
            this.Height = 244;
            lc.Visibility = Visibility.Hidden;
            begincontrol.Visibility = Visibility.Visible;
            begincontrol.txtBox_id.Focus();
            mode = LOGIN;
        }

        // LoginControl 에서 회원수정 버튼 클릭시
        private void btn_modify_Click(object sender, RoutedEventArgs e)
        {
            lc.Visibility = Visibility.Hidden;
            mc.Visibility = Visibility.Visible;
            mode = MODIFY;
        }
        
        // 수정창에서 뒤로가기 버튼 클릭시
        private void btn_modify_back_Click(object sender, RoutedEventArgs e)
        {
            mode = AFTERLOGIN;

            mc.Visibility = Visibility.Hidden;
            lc.Visibility = Visibility.Visible;
        }

        // 수정창에서 패스워드 버튼을 클릭시
        private void btn_modifyPw_Click(object sender, RoutedEventArgs e)
        {
            mode = MODIFYPW;

            mc.Visibility = Visibility.Hidden;
            mpc.Visibility = Visibility.Visible;
        }

        // 수정창에서 이름(수정) 버튼을 클릭시
        private void btn_modifyName_Click(object sender, RoutedEventArgs e)
        {
            mode = MODIFYNAME;

            mc.Visibility = Visibility.Hidden;
            mnc.Visibility = Visibility.Visible;
        }

        // 수정창에서 핸드폰번호 버튼을 클릭시
        private void btn_modifyPhone_Click(object sender, RoutedEventArgs e)
        {
            mode = MODIFYPHONE;

            mc.Visibility = Visibility.Hidden;
            mpnc.Visibility = Visibility.Visible;
        }

        // 수정창에서 주소 버튼을 클릭시
        private void btn_modifyAddress_Click(object sender, RoutedEventArgs e)
        {
            mode = MODIFYADR;

            mc.Visibility = Visibility.Hidden;
            mac.Visibility = Visibility.Visible;
        }


        // 패스워드 수정창에서 뒤로가기 버튼 클릭시
        private void btn_modifyPw_back_Click(object sender, RoutedEventArgs e)
        {
            mode = MODIFY;

            mpc.Visibility = Visibility.Hidden;
            mc.Visibility = Visibility.Visible;

            // 패스워드 입력부분 초기화
            mpc.passwordBox.Password = "";
            mpc.passwordBox_chk.Password = "";
            mpc.lbl_result.Content = "";
        }

        // 패스워드 수정하는 창에서 수정버튼을 눌렀을 경우
        // MySQL 쪽과 현재 MemberList 쪽의 데이터를 수정해준다.
        private void btn_modifyPw_ok_Click(object sender, RoutedEventArgs e)
        {
            mysql.update("PW", mpc.passwordBox.Password, "ID", sd.CurrentId);
            sd.MemberList[sd.CurrentIndex].Pw = mpc.passwordBox.Password;
            MessageBox.Show("패스워드 수정이 정상적으로 처리되었습니다");

            // 뒤로가기 효과를 주면서 초기화를 같이 진행함
            btn_modifyPw_back_Click(null, null);
        }

        // 이름수정하는 창에서 뒤로가기 버튼을 눌렀을 경우
        private void btn_modifyName_back_Click(object sender, RoutedEventArgs e)
        {
            mode = MODIFY;

            mnc.Visibility = Visibility.Hidden;
            mc.Visibility = Visibility.Visible;

            mnc.txtBox_modifyName.Text = "";
            mnc.lbl_result.Content = "";
        }

        // 이름수정하는 창에서 이름수정 버튼을 눌렀을 경우
        private void btn_modifyName_ok_Click(object sender, RoutedEventArgs e)
        {
            mysql.update("Name", mnc.txtBox_modifyName.Text, "ID", sd.CurrentId);
            sd.MemberList[sd.CurrentIndex].Name = mnc.txtBox_modifyName.Text;
            MessageBox.Show("이름 수정이 정상적으로 처리되었습니다");

            btn_modifyName_back_Click(null, null);
        }

        // 핸드폰번호 수정하는 창에서 뒤로가기 버튼을 눌렀을 경우
        private void btn_modifyPhoneBack_Click(object sender, RoutedEventArgs e)
        {
            mode = MODIFY;

            mpnc.Visibility = Visibility.Hidden;
            mc.Visibility = Visibility.Visible;

            mpnc.phoneNumFirstCheck = false;
            mpnc.comboBox_phoneNumFirst.Text = "";
            mpnc.txtBox_phoneNumSec.Text = "";
            mpnc.txtBox_phoneNumThird.Text = "";
            mpnc.lbl_result.Content = "";          
        }

        // 핸드폰번호 수정하는 창에서 OK 버튼을 클릭했을 경우
        private void btn_modifyPhoneOk_Click(object sender, RoutedEventArgs e)
        {
            string phoneNumber = mpnc.comboBox_phoneNumFirst.Text + "-" + mpnc.txtBox_phoneNumSec.Text + "-" + mpnc.txtBox_phoneNumThird.Text;

            mysql.update("PhoneNumber", phoneNumber, "ID", sd.CurrentId);
            sd.MemberList[sd.CurrentIndex].PhoneNumber = phoneNumber;
            MessageBox.Show("핸드폰번호 수정이 정상적으로 처리되었습니다");

            btn_modifyPhoneBack_Click(null, null);
        }

        private void btn_modifyAdrBack_Click(object sender, RoutedEventArgs e)
        {
            mode = MODIFY;

            mac.Visibility = Visibility.Hidden;
            mc.Visibility = Visibility.Visible;

            mac.txtBox_address.Text = "";
        }

        private void btn_modifyAdrOk_Click(object sender, RoutedEventArgs e)
        {
            string address = mac.txtBox_address.Text;

            mysql.update("Address", address, "ID", sd.CurrentId);
            sd.MemberList[sd.CurrentIndex].Address = address;
            MessageBox.Show("주소 수정이 정상적으로 처리되었습니다");

            btn_modifyAdrBack_Click(null, null);
        }

        // LoginControl 에서 회원탈퇴 버튼 클릭시
        private void btn_drop_Click(object sender, RoutedEventArgs e)
        {
            mode = DROP;

            lc.Visibility = Visibility.Hidden;
            dc.Visibility = Visibility.Visible;
        }

        // 회원탈퇴 창에서 '네' 버튼을 클릭했을 경우
        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            mode = LOGIN;

            mysql.deleteTuple();
            sd.MemberList.RemoveAt(sd.CurrentIndex);
            string str = sd.CurrentName + "님 회원탈퇴가 정상적으로 처리되었습니다";
            MessageBox.Show(str);
            sd.CurrentId = "";
            sd.CurrentName = "";
            sd.CurrentIndex = 0;
            dc.Visibility = Visibility.Hidden;
            begincontrol.Visibility = Visibility.Visible;
        }

        // 회원탈퇴 창에서 '아니오' 버튼을 클릭했을 경우
        private void btn_no_Click(object sender, RoutedEventArgs e)
        {
            mode = AFTERLOGIN;

            dc.Visibility = Visibility.Hidden;
            lc.Visibility = Visibility.Visible;
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            cc.Visibility = Visibility.Hidden;
            currentShowControl();
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            cc.Visibility = Visibility.Visible;
            begincontrol.Visibility = Visibility.Hidden;
            fc.Visibility = Visibility.Hidden;
            fic.Visibility = Visibility.Hidden;
            fpc.Visibility = Visibility.Hidden;
            lc.Visibility = Visibility.Hidden;
            dc.Visibility = Visibility.Hidden;
            mc.Visibility = Visibility.Hidden;

            mpc.Visibility = Visibility.Hidden;
            mnc.Visibility = Visibility.Hidden;
            mpnc.Visibility = Visibility.Hidden;
        }

        public void currentShowControl()
        {
            switch (mode)
            {
                case LOGIN:
                    begincontrol.Visibility = Visibility.Visible;
                    begincontrol.txtBox_id.Focus();
                    break;
                case AFTERLOGIN:
                    lc.Visibility = Visibility.Visible;
                    break;
                case DROP:
                    dc.Visibility = Visibility.Visible;
                    break;
                case MODIFY:
                    mc.Visibility = Visibility.Visible;
                    break;
                case FIND:
                    fc.Visibility = Visibility.Visible;
                    break;
                case FINDID:
                    fic.Visibility = Visibility.Visible;
                    break;
                case FINDPW:
                    fpc.Visibility = Visibility.Visible;
                    break;
                case MODIFYPW:
                    mpc.Visibility = Visibility.Visible;
                    break;
                case MODIFYNAME:
                    mnc.Visibility = Visibility.Visible;
                    break;
                case MODIFYPHONE:
                    mpnc.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
