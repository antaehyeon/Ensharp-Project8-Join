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
using System.Windows.Shapes;

namespace Join
{
    /// <summary>
    /// JoinWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class JoinWindow : Window
    {
        BeginWindow beginWindow;
        JoinControl joinControl = new JoinControl();
        SharingData sd;

        public JoinWindow(BeginWindow beginWindow)
        {
            InitializeComponent();
            JoinGrid.Children.Add(joinControl);

            this.beginWindow = beginWindow;
            sd = SharingData.GetInstance();

            joinControl.btn_joinMember.Click += btnJoinMemberClick;
            this.Closed += windowClosed;

            joinControl.btn_findAddress.Click += btn_findAddress_Click;
        }

        // 가입하기 버튼 
        private void btnJoinMemberClick(object sender, System.EventArgs e)
        {
            if(joinControl.Complete)
            {
                beginWindow.Show();
                Close();
            }
        }

        // 회원가입창이 닫힐경우
        // 초기화면 창(BeginWindow)을 띄워줌
        public void windowClosed(object sender, System.EventArgs e)
        {
            beginWindow.Show();
        }

        private void btn_findAddress_Click(object sender, System.EventArgs e)
        {
            SearchAddress sa = new SearchAddress();
            sa.Show();
        }
    }
}
