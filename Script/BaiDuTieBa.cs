using ClawlerVarietyShow.Utility;
using Http;
using Html;
using System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MetaSearch
{
    class BaiDuTieBa
    {
        public static void Start()
        {
            List<string> pages = new List<string>();
            for (int i = 1; i < 42; i++)
            {
                ParseUrl(pages, "http://tieba.baidu.com/f/search/res?isnew=1&kw=&qw=%C5%ED%D6%DD%CA%AF%BB%AF&rn=10&un=&only_thread=1&sm=1&sd=&ed=&pn=" + i);
            }
            //foreach (string url in pages)
            //{
            //    VideoData data = new VideoData();
            //    Console.WriteLine("开始检测：" + url);
            //    string source = Downloader.Download(url);
            //    HtmlDocument htmlPlate = new HtmlDocument();
            //    List<string> urls = XpathUtil.GetAttributes(htmlPlate.DocumentNode, "//ul[@class='ul02_l']/a", "href");
            //}
        }

        //public static List<UrlEntity> MatchTextCollection(string text, string parrern, int index)
        //{
        //    List<UrlEntity> list = new List<UrlEntity>();
        //    string match1 = parrern.Split('|')[0];
        //    string match2 = parrern.Split('|')[1];
        //    MatchCollection mcc = Regex.Matches(text, match2, RegexOptions.IgnoreCase);
        //    foreach (Match mc in mcc)
        //    {
        //        list.Add(new UrlEntity(new Uri(match1 + mc.Groups[index].Value), match1 + mc.Groups[index].Value));
        //    }
        //    return list;
        //}

        public static void ParseUrl(List<string> urls, string home)
        {
            try
            {
                string url = "";
                string html = Http.Downloader.Download(home);
                HtmlDocument hn = new HtmlDocument();
                hn.LoadHtml(html);
                List<string> liststring = XpathUtil.GetAttributes(hn.DocumentNode, "//span[@class='p_title']/a", "href");
                foreach (var item in liststring)
                {
                    if (!item.Contains("http://tieba.baidu"))
                    {
                        url = "http://tieba.baidu.com" + item;
                    }
                    else
                    {
                        url = item;
                    }
                    Uri uri = new Uri(item);
                    //拿到链接
                }
                //List<UrlEntity> ueList = MatchTextCollection(html, "http://www.le.com/ptv/vplay/|\"vid\":\"(.*?)\",", 1);
                //foreach (UrlEntity ue in ueList)
                //{
                //    Uri uri = ue.uri;
                //    //符合规则
                //    urls.Add(uri.AbsoluteUri);
                //}
                //StringUtil.FilterRepeat(urls);
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
            vd.Content = XpathUtil.GetText(hn.DocumentNode, "//div[@class='d_post_content j_d_post_content ']");
            vd.Title = XpathUtil.GetText(hn.DocumentNode, "//div[@class='core_title_wrap_bright clearfix']/h3");
            vd.Author = XpathUtil.GetText(hn.DocumentNode, "//li[@class='d_name']/a");
            vd.Time = XpathUtil.GetText(hn.DocumentNode, "//div[@class='post-tail-wrap']/span[4]");
            vd.Source = "百度贴吧";
            vdList.Add(vd);
            return vdList;
        }
    }
}
