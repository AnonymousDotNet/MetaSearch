using ClawlerVarietyShow.Utility;
using Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MetaSearch.Script
{
    class Kdnet
    {
        public static void Start()
        {
            List<string> pages = new List<string>();
            //for (int i = 1; i < 2; i++)
            //{
                ParseUrl(pages, "http://search.kdnet.net/?q=%C5%ED%D6%DD%CA%AF%BB%AF&sa=%CB%D1%CB%F7&category=title&boardid=0&arrival=2013-12-29&departure=2016-12-29");
            //}
        }
        public static void ParseUrl(List<string> urls, string home)
        {
            try
            {
                string url = "";
                //string html = Http.Downloader.Download(home);
                string html = Download(home);
                HtmlDocument hn = new HtmlDocument();
                hn.LoadHtml(html);
                List<string> liststring = XpathUtil.GetAttributes(hn.DocumentNode, "//div[@class='search-result-list']/h2/a", "href");
                foreach (var item in liststring)
                {
                    //if (!item.Contains("http://tieba.baidu"))
                    //{
                    //    url = "http://tieba.baidu.com" + item;
                    //}
                    //else
                    //{
                    //    url = item;
                    //}
                    Uri uri = new Uri(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static List<VideoData> GetNeedData(string url, List<VideoData> vdList)
        {
            VideoData vd = new VideoData();
            string html = Http.Downloader.Download(url);
            HtmlDocument hn = new HtmlDocument();
            hn.LoadHtml(html);
            vd.Url = url;
            vd.Title = XpathUtil.GetText(hn.DocumentNode, "//div[@class='posts-title']");
            vd.Author = XpathUtil.GetText(hn.DocumentNode, "//div[@class='posts-posted']/span[1]/a");
            vd.Time = XpathUtil.GetText(hn.DocumentNode, "//div[@class='posts-posted']/text()");
            vd.Content = XpathUtil.GetText(hn.DocumentNode, "//div[@class='posts-cont']");
            vd.Source = "凯迪社区";
            vdList.Add(vd);
            return vdList;
        }
        private static string Download(string url)
        {
            string text = "";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream myStream = null;
            StreamReader myStreamReader = null;
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
                request.KeepAlive = true;
                request.Headers.Add("Accept-Encoding", "gzip, deflate, sdch, br");//Accept-Encoding: gzip, deflate, sdch
                request.Headers.Add("Accept-Language", "zh-Hans-CN;q=1, zh-HK;q=0.9, en-CN;q=0.8,zh-CN,zh;q=0.8,zh-CN,zh;q=0.8,en-US;q=0.5,en;q=0.3");
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                request.Referer = url;
                request.Headers["Cookie"] = "related%5Fcookies=1; PHPSESSID=u6ha50f7csvd25qaqq4k9fsbc5; kdnet%5Fuser=; __auc=f27f6a7b15865ea3c22a1c0910e; kd%5Fsessionid=d45566ff900af373696b134cb43e5379; kdnet%5Fuser%5Fintegral=100";
                request.Method = "Get";
                try
                {
                    response = request.GetResponse() as HttpWebResponse;
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                }
                myStream = response.GetResponseStream();
                myStreamReader = new StreamReader(myStream, Encoding.GetEncoding("gb2312"));
                text = myStreamReader.ReadToEnd();
                return text;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (myStreamReader != null)
                {
                    myStreamReader.Close();
                }
                if (myStream != null)
                {
                    myStream.Close();
                }
            }
            return "";
        }
    }
}
