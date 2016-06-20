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

        int index = 0;
        int mode = LOGIN;

        const int LOGIN = 0;
        const int AFTERLOGIN = 1;
        const int DROP = 2;
        const int MODIFY = 3;
        const int FIND = 4;
        const int FINDID = 5;
        const int FINDPW = 6;

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

            sd = SharingData.GetInstance();
            mysql = new MySQL();

            BeginGrid.Children.Add(begincontrol);
            BeginGrid.Children.Add(fc);
            BeginGrid.Children.Add(fic);
            BeginGrid.Children.Add(fpc);
            BeginGrid.Children.Add(lc);
            BeginGrid.Children.Add(dc);
            BeginGrid.Children.Add(cc);

            begincontrol.Visibility = Visibility.Hidden;
            fc.Visibility = Visibility.Hidden;
            fic.Visibility = Visibility.Hidden;
            fpc.Visibility = Visibility.Hidden;
            lc.Visibility = Visibility.Hidden;
            dc.Visibility = Visibility.Hidden;

            begincontrol.btn_join.Click += btnJoinClick;
            begincontrol.btn_login.Click += btn_login_Click;
            begincontrol.btn_findInfo.Click += btn_findInfo_Click;
            begincontrol.passwordBox.PreviewKeyDown += passwordBox_PreviewKeyDown;

            fc.btn_fc_back.Click += btn_fc_back_Click;
            fc.btn_findId.Click += btn_findId_Click;
            fc.btn_findPw.Click += btn_findPw_Click;

            fic.btn_back.Click += btn_fic_back_Click;

            fpc.btn_fpc_back.Click += btn_fpc_back_Click;

            lc.btn_logOut.Click += btn_logOut_Click;
            lc.btn_modify.Click += btn_modify_Click;
            lc.btn_drop.Click += btn_drop_Click;

            dc.btn_yes.Click += btn_yes_Click;
            dc.btn_no.Click += btn_no_Click;
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
            fic.Visibility = Visibility.Hidden;
            fc.Visibility = Visibility.Visible;
            mode = FIND;
        }

        // FindPwControl 에서 뒤로가기 버튼 클릭시
        private void btn_fpc_back_Click(object sender, RoutedEventArgs e)
        {
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
            mode = MODIFY;
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
            mysql.deleteTuple();
            string str = sd.CurrentName + "님 회원탈퇴가 정상적으로 처리되었습니다";
            MessageBox.Show(str);
            sd.CurrentId = "";
            sd.CurrentName = "";
            dc.Visibility = Visibility.Hidden;
            begincontrol.Visibility = Visibility.Visible;
            mode = LOGIN;
        }

        // 회원탈퇴 창에서 '아니오' 버튼을 클릭했을 경우
        private void btn_no_Click(object sender, RoutedEventArgs e)
        {
            dc.Visibility = Visibility.Hidden;
            lc.Visibility = Visibility.Visible;
            mode = AFTERLOGIN;
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
        }

        public void currentShowControl()
        {
            switch (mode)
            {
                case LOGIN:
                    begincontrol.Visibility = Visibility.Visible;
                    break;
                case AFTERLOGIN:
                    lc.Visibility = Visibility.Visible;
                    break;
                case DROP:
                    dc.Visibility = Visibility.Visible;
                    break;
                case MODIFY:
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
            }
        }
    }
}
