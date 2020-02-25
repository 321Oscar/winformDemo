using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    //节点 
    class Node<T>
    {
        /// <summary>
        /// 数据域
        /// </summary>
        T data;

        /// <summary>
        /// 指针域
        /// </summary>
        Node<T> next;
        
        public Node(T value)
        {
            data = value;
            next = null;
        }

        public Node(T value,Node<T> next)
        {
            data = value;
            this.next = next;
        }

        public Node(Node<T> next)
        {
            this.next = next;
        }
        /// <summary>
        /// 数据域
        /// </summary>
        public T Data
        {
            get => data; set { data = value; }
        }
        /// <summary>
        /// 指针域
        /// </summary>
        public Node<T> Next
        {
            get => next; set { next = value; }
        }

    }

    //链表
    class LinkList<T>
    {
        private Node<T> head;//Head Node
        
        public LinkList()
        {
            head = null;
        }

        //isempty
        public bool IsEmpty()
        {
            return head == null;
        }

        public int Count()
        {
            if (head == null) return 0;
            Node<T> temp = head;
            int count = 1;
            while(temp.Next != null)
            {
                count++;
                temp = temp.Next;
            }
            return count;
        }

        #region add

        //add head
        public void AddHead(T Data)
        {
            Node<T> node = new Node<T>(Data);
            node.Next = head;
            head = node;
        }
        
        /// <summary>
        /// add data by index(From 0)
        /// </summary>
        /// <param name="Data">data</param>
        /// <param name="index">index:start from 0</param>
        public void AddMid(T Data,int index)
        {
            if (index > Count() || index < 0)
                return;
            if (index == 0)
            {
                AddHead(Data);
            }
            else
            {
                //遍历到 index 的Node
                Node<T> temp = head;
                for (int i = 0; i < index-1; i++)//index = 1 -- No.1
                {
                    temp = temp.Next;
                }
                //指定的节点
                Node<T> preTemp = temp;//No.1
                //指定节点的下一个节点
                Node<T> curTemp = temp.Next;//No.2
                //新的节点
                Node<T> newTemp = new Node<T>(Data);
                //指定的节点的下一个节点为新的节点
                preTemp.Next = newTemp;//Set new No.2
                //新的节点的下一个节点为指定节点的下一个节点
                newTemp.Next = curTemp;//Set new No.3
            }
        }

        //add end
        public void AddEnd(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (head == null)
                head = newNode;
            else
            {
                Node<T> temp = head;
                while (temp.Next != null)
                {
                    temp = temp.Next;
                }
                temp.Next = newNode;
            }
        }

        #endregion

        # region delete

        //delete head
        public void DeleteHead()
        {
            head = head.Next;
        }

        /// <summary>
        /// delete data by Index(From 0)
        /// </summary>
        /// <param name="index">index:From 0</param>
        public void DeleteMid(int index)
        {
            if (index < 0 || index > Count())
                return;
            if(index == 0)
            {
                DeleteHead();
            }
            Node<T> temp = head;
            for (int i = 0; i < index-1; i++)//index = 1--No.1
            {
                temp = temp.Next;
            }
            Node<T> preNode = temp;//No.1
            Node<T> indexNode = preNode.Next;//No.2
            Node<T> nextNode = indexNode.Next;//No.3
            preNode.Next = nextNode;//delete No.2
         }

        //delete end
        public void DeleteEnd()
        {
            if(head.Next == null)
            {
                head = null;
                return;
            }
            Node<T> temp = head;
            //当 next 为null时 节点为end
            while (temp.Next.Next != null)
            {
                temp = temp.Next;
            }
            temp.Next = null;
        }

        #endregion

        public T this[int index]
        {
            get
            {
                Node<T> temp = head;
                if(index == 0)
                {
                    return temp.Data;
                }
                for (int i = 0; i < index; i++)
                {
                    temp = temp.Next;
                }
                return temp.Data;
            }
        }

        public T GetEle(int index)
        {
            return this[index];
        }

        public int Locate(T value)
        {
            Node<T> temp = head;
            if (temp == null)
            {
                return -1;
            }
            else
            {
                int index = 0;
                while (true)
                {
                    if (temp.Data.Equals(value))
                    {
                        return index;
                    }
                    else
                    {
                        if (temp.Next != null)
                        {
                            temp = temp.Next;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                return -1;
            }
        }
    }
}
