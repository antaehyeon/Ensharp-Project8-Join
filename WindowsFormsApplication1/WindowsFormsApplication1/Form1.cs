using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            webBrowser1.Navigate("http://postcode.map.daum.net/search?origin=http%3A%2F%2Fpostcode.map.daum.net&indaum=off&banner=on&mode=transmit&vt=popup&am=on&animation=off&shorthand=on&plrg=&plrgt=1.5&zn=Y&fullpath=%2Fguide");
        }

        private void viewPrice()
        {
            HtmlDocument doc = webBrowser1.Document;
            string tableId = "cm";
            HtmlElement cm = doc.GetElementById(tableId);
            if (cm == null)
            {
                //MessageBox.Show("cm을 찾을수 없음");
                return;
            }
            HtmlElementCollection trs = cm.GetElementsByTagName("query");
            HtmlElementCollection tds = trs[1].GetElementsByTagName("address");
            foreach (HtmlElement el in tds)
            {
                if (el.GetAttribute("className") == "address")
                {
                    el.Focus();
                    MessageBox.Show(el.InnerText);
                    break;
                }
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsoluteUri == webBrowser1.Url.AbsoluteUri)
                viewPrice();

            HtmlElement element = webBrowser1.Document.GetElementsByTagName("txt_addr");
//            object objElement = element.DomElement;
        }
    }
}
