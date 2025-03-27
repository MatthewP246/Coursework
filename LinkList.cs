using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Connect4
{
    public class LinkList : IEnumerable
    {
        private Node Head;

        public LinkList()
        {
            Head = null;
        }

        

        public int Count()
        {
            return Length(Head);
        }

        private int Length(Node a)
        {
            if (a == null)
            {
                return 0;
            }
            else
            {
                return 1 + Length(a.Next);
            }
        }

        public void AddToFront(int s)
        {
            Node n = new Node();
            n.Data = s;
            n.Next = Head;
            if(Head != null)
            {
                Head.Previous = n;
            }
            Head = n;

        }
        public void Add(int s)
        {
            //Adds to the back of the list
            Node a = Head;
            if (a != null)
            {
                while (a.Next != null)
                {
                    a = a.Next;
                }
                Node n = new Node();
                n.Data = s;
                a.Next = n;
                n.Previous = a;
            }
            else
            {
                AddToFront(s);
            }




        }


        public int Peek(int position)
        {
            Node a = Head;

            for (int x = 0; x < position; x++)
            {
                if (a != null)
                {
                    a = a.Next;
                }
            }
            if(a == null)
            {
                return -1;
            }
            return a.Data;
        }


        public IEnumerator GetEnumerator()
        {
            Node a = Head;
            while (a != null)
            {
                yield return a.Data;
                a = a.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


    }
}
