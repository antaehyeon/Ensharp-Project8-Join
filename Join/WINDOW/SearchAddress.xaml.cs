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
    /// SearchAddress.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SearchAddress : Window
    {
        public SearchAddress()
        {
            InitializeComponent();
            webBrowser.Navigate("http://postcode.map.daum.net/search?origin=http%3A%2F%2Fpostcode.map.daum.net&indaum=off&banner=on&mode=transmit&vt=popup&am=on&animation=off&shorthand=on&plrg=&plrgt=1.5&zn=Y&fullpath=%2Fguide");
        }
    }
}
