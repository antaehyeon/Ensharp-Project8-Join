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
    /// ModifyNameControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ModifyNameControl : UserControl
    {
        public ModifyNameControl()
        {
            InitializeComponent();
        }

        private void txtBox_modifyName_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (Regex.IsMatch(txtBox_modifyName.Text, "^[가-힣]+$"))
            {
                lbl_result.Foreground = Brushes.Green;
                lbl_result.Content = "사용할 수 있는 이름입니다";
                btn_modifyName_ok.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_result.Foreground = Brushes.Red;
                lbl_result.Content = "이름을 정확히 입력해주세요";
                btn_modifyName_ok.Visibility = Visibility.Hidden;
            }
        }
    }
}
