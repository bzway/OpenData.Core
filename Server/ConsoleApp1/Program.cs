using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Bzway.Module.Wechat.Service.WechatApiService s = new Bzway.Module.Wechat.Service.WechatApiService(null, null);

            var result = s.GetMaterialCount();


        }
    }
}
