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
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using mshtml;
using System.Data;
using System.Xml;
using System.Web;


namespace WpfApplication2
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
            WB.Navigate("http://postcode.map.daum.net/search?origin=http%3A%2F%2Fpostcode.map.daum.net&indaum=off&banner=on&mode=transmit&vt=popup&am=on&animation=off&shorthand=on&plrg=&plrgt=1.5&zn=Y&fullpath=%2Fguide");

            WB.LoadCompleted += new LoadCompletedEventHandler(WB_LoadCompleted);

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WB.ObjectForScripting = true;

        }

        private void WB_LoadCompleted(object sender, NavigationEventArgs e)
        {

            IHTMLDocument2 Doc = this.WB.Document as IHTMLDocument2;

            HTMLDocumentEvents2_Event iEvent;
            iEvent = Doc as HTMLDocumentEvents2_Event;
            iEvent.onclick += new HTMLDocumentEvents2_onclickEventHandler(iEvent_onclick);
        }

        public void CallForm(object msg)
        {
            string sMsg = (string)msg;
            {
                // 받은 msg 값을 가지고 처리하는 로직.  
            }
        }

        bool iEvent_onclick(IHTMLEventObj pEvtObj)
        {
            //이벤트 발생 html Element 출력해봄세~
            Console.WriteLine("srcElement : " + pEvtObj.srcElement);
            return true;
        }




        //private void DoSearch()
        //{
        //    // 웹 브라우저 컨트롤이 호스팅하는 HTML Document 개체를 얻는다.
        //    mshtml.HTMLDocumentClass htmlDocumentClass = this.webBrowser.Document as mshtml.HTMLDocumentClass;

        //    // HTML Document 개체로부터 id 값이 query 인 HTML Element 개체를 얻는다.
        //    mshtml.IHTMLElement queryHtmlElement = htmlDocumentClass.getElementById("query");

        //    // quert HTML Element 개체에 검색 문자열을 기입한다.
        //    queryHtmlElement.setAttribute("value", this.searchTextBox.Text, 0);

        //    // 검색 버튼을 나타내는 HTML Element 개체를 얻는다.
        //    mshtml.HTMLInputElementClass searchbuttonHtmlElement = (queryHtmlElement.parentElement as mshtml.IHTMLDOMNode).nextSibling.nextSibling as mshtml.HTMLInputElementClass;

        //    // 검색을 실시한다.
        //    searchbuttonHtmlElement.click();
        //}

        private void WB_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
