using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class RandomHelper
    {
        public int GetNum(int min, int max)
        {
            Thread.Sleep(1000);//随机休息1秒
            return GetIntNum(min, max);
        }

        /// <summary>
        /// 获取随机数，解决重复问题
        /// </summary>
        /// <param name="min">返回的随机数字包含下限</param>
        /// <param name="max">返回的随机数字不包含上限</param>
        /// <returns></returns>
        private int GetIntNum(int min, int max)
        {

            Guid Guidnum = Guid.NewGuid();
            string guid = Guidnum.ToString();
            int seed = DateTime.Now.Millisecond;
            for (int i = 0; i < guid.Length; i++)
            {
                switch (guid[i])
                {
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                        seed = seed + 1;
                        break;
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                        seed = seed + 2;
                        break;
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                        seed = seed + seed;
                        break;
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z':
                        seed = seed + 3;
                        break;
                    default:
                        seed = seed + 4;
                        break;
                }
            }


            return new Random(seed).Next(min, max);
        }
    }
}
