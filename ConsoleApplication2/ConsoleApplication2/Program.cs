using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;
using System.Net;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        /* String apiUrl = 
	"http://www.juso.go.kr/addrlink/addrLinkApi.do?currentPage="+currentPage+"&countPerPage="
	+countPerPage+"&keyword="+URLEncoder.encode(keyword,"UTF-8")+"&confmKey="+confmKey;*/
        static void Main(string[] args)
        {
            Encoding e;

            String apiUrl = "http://www.juso.go.kr/addrlink/addrLinkApi.do?currentPage=" + 1 + "&countPerPage=" + 10 + "&keyword=" + HttpUtility.UrlEncode("양재동") + "&confmKey=" + "TESTJUSOGOKR";

            XmlDocument doc = new XmlDocument();
            doc.Load("http://www.juso.go.kr/addrlink/addrLinkApi.do?currentPage=" + 1 + "&countPerPage=" + 10 + "&keyword=" + HttpUtility.UrlEncode("양재동") + "&confmKey=" + "TESTJUSOGOKR");

            XmlNodeList imageUrlList = doc.GetElementsByTagName("totalCount");

        }
    }
}
