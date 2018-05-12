using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYong.Code
{
    public class CacheFactory
    {
        /// <summary>
        /// 缓存工厂类
        /// </summary>
        /// <returns></returns>
        public static ICache Cache()
        {
            return new Cache();
        }
    }
}
