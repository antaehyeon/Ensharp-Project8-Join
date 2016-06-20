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
    /// LoginControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginControl : UserControl
    {
        SharingData sd;

        int index = 0;

        public LoginControl()
        {
            InitializeComponent();

            sd = SharingData.GetInstance();
        }

        public void findName()
        {
            string currentId = sd.CurrentId;

            for (index = 0; index < sd.MemberList.Count; index++)
            {
                if(sd.MemberList[index].Id.Equals(currentId))
                {
                    break;
                }
            }
            sd.CurrentName = sd.MemberList[index].Name;
            lbl_guide.Content = sd.MemberList[index].Name + "님 환영합니다";
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            findName();
        }
    }
}
