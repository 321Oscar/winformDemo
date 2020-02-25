using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class SeniorTest
    {
        Struct_Oct_Ele oct_Ele = new Struct_Oct_Ele(1, 2, 2);

        enum Days { Sun,Mon,Tue,Wed,Thu,Fri,Sat };

        void Swap<T>(ref T s,ref T q)
        {
            T temp;
            temp = q;
            q = s;
            s = temp;
        }

        delegate int delegate_first(string s);

        string a, b;

        int stringtoint(string param)
        {
            return int.Parse(param);
        }

        public void Main()
        {
            string aa = "2";
            
            delegate_first _First = new delegate_first(stringtoint);

            Hashtable hashtable = new Hashtable();
            

            _First(aa);
            a = "2";
            b = "22";
            Swap(ref a, ref b);
        }
    }

    struct Struct_Oct_Ele {
        private int a, b, c;//设置为私有的变量
        public Struct_Oct_Ele(int aa, int bb, int cc)
        {
            a = aa;
            b = bb;
            c = cc;
        }
        public int aOut()
        {
            return this.a;
        }
        public int bOut()
        {
            return this.b;
        }
        public int cOut()
        {
            return this.c;
        }
    }
}
