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
    public partial class BeginWindow : Window
    {
        BeginControl begincontrol;
        MySQL mysql;
        SharingData sd;

        int index = 0;

        public BeginWindow()
        {
            InitializeComponent();

            begincontrol = new BeginControl();
            mysql = new MySQL();
            sd = SharingData.GetInstance();

            BeginGrid.Children.Add(begincontrol);

            begincontrol.btn_join.Click += btnJoinClick;
            begincontrol.btn_login.Click += btn_login_Click;
        }

        // 폼이 처음 로드될 때
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mysql.DBDataToList();
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
                MessageBox.Show("로그인에 성공하였습니다");
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
    }
}
