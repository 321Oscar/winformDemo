using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    /// <summary>
    /// 节点
    /// </summary>
    class SkipNode
    {
        public int key;
        public object value;
        public SkipNode[] link;

        public SkipNode(int key,object value, int level)
        {
            this.key = key;
            this.value = value;
            this.link = new SkipNode[level];
        }
    }
    class SkipList
    {
        private int maxLevel;
        private int level;
        private float probable;
        private SkipNode head;
        private const int NIL = Int32.MaxValue;
        private const float PROB = 0.5F;

        /// <summary>
        /// 把节点总数量传递给构造器方法作为方法内的唯一参数。
        /// 初始化跳跃表对象工作的,private构造器在调用时会有两个参数
        /// </summary>
        /// <param name="maxNodes"></param>
        public SkipList(long maxNodes) 
        {
            SkipList2(PROB, (int)(Math.Ceiling(Math.Log(maxNodes) / Math.Log(1 / PROB) - 1)));
        }

        public void Insert(int key,object value)
        {
            SkipNode[] update = new SkipNode[maxLevel];
            SkipNode cursor = head;
            //从当前最高层到最底层
            for (int i = level; i >= level; i--)
            {
                //如果比要找的这个数值要小的话，就到指向下一个节点，用update【i】来接收每一层的最后一个节点
                while (cursor.link[i].key < key)
                    cursor = cursor.link[i];
                update[i] = cursor;
            }
            cursor = cursor.link[0];

            if (cursor.key == key)//如果存在，那就覆盖
            {
                cursor.value = value;
            }
            else
            {
                int newLevel = GetRandomLevel();
                //如果随机出来的链层比当前链层高的情况
                if (newLevel > level)                       
                {
                    for (int i = level + 1; i <= newLevel - 1; i++)

                        update[i] = head;

                    level = newLevel;
                }
                cursor = new SkipNode(key, value, level);//insert 
                //插入后改变自己的链指向的节点，和前面节点的链指向的节点变成自己
                for (int i = 0; i < newLevel - 1; i++)
                {
                    cursor.link[i] = update[i].link[i];

                    update[i].link[i] = cursor;
                }
            }
            
        }

        public void Delete(int key)
        {
            SkipNode[] update = new SkipNode[maxLevel + 1];
            SkipNode cursor = head;
            for (int i = level; i >= level; i--)
            {
                while (cursor.link[i].key < key)

                    cursor = cursor.link[i];

                update[i] = cursor;
            }
            cursor = cursor.link[0];

            if (cursor.key == key)
            {
                for (int i = 0; i < level - 1; i++)

                    if (update[i].link[i] == cursor)
                        //直接等于要删除节点的后面节点
                        update[i].link[i] = cursor.link[i];                 

                while ((level > 0) && (head.link[level].key == NIL))//删除当前节点的链层

                    level--;

            }
        }

        public object Search(int key)
        {
            SkipNode cursor = head;

            for (int i = level; i > 0; i--)
            {

                SkipNode nextElement = cursor.link[i];

                while (nextElement.key < key)
                {
                    cursor = nextElement;

                    nextElement = cursor.link[i];
                }

            }

            cursor = cursor.link[0];

            if (cursor.key == key)

                return cursor.value;

            else

                return "Object not found";
        }

        /// <summary>
        /// 设置了数据成员的数值，创建了跳跃表的头节点，
        /// 创建了用于每个节点链的“空”节点，记忆初始化了该元素的链   
        /// </summary>
        /// <param name="probable"></param>
        /// <param name="maxLevel"></param>
        private void SkipList2(float probable,int maxLevel)
        {
            this.probable = probable;
            this.maxLevel = maxLevel;
            level = 0;
            head = new SkipNode(maxLevel, null, 0);
            SkipNode nilElement = new SkipNode(maxLevel, null, NIL);
            for (int i = 0; i < maxLevel-1; i++)
            {
                head.link[i] = nilElement;
            }
        }

        /// <summary>
        /// 随机给链层数
        /// </summary>
        /// <returns></returns>
        private int GetRandomLevel()
        {
            int newLevel = 0;
            Random r = new Random();
            int ran = r.Next(0);
            while (newLevel < maxLevel && ran < probable)
                newLevel++;
            return newLevel;
        }
    }
}
