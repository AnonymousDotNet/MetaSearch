using Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSearch.Script;

namespace MetaSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            //百度--OK
            //BaiDuTieBa.Start();

            //天涯
            //TianYa.Start();

            //猫扑
            //Mop.Start();

            //凯迪--OK
            //Kdnet.Start();

            //强国
            People.Start();

            //发展

            Console.WriteLine("POST完成！！！");
            Console.ReadKey();
        }
    }
}
