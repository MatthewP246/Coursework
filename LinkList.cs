using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Coursework_UI
{
    internal class LinkList : IEnumerable
    {
        private Node head;

        public LinkList()
        {
            head = null;


        }

        public IEnumerator GetEnumerator()
        {
            Node a = head;
            while (a != null)
            {
                yield return a.data;
                a = a.next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {

            return this.GetEnumerator();
        }

        public int Count()
        {
            return Length(head);
        }

        private int Length(Node a)
        {
            if (a == null)
            {
                return 0;
            }
            else
            {
                return 1 + Length(a.next);
            }
        }

        public void addToFront(int s)
        {
            Node n = new Node();
            n.data = s;
            n.next = head;
            

            head = n;

        }
        public void Add(int s)
        {
            //Adds to the back of the list
            Node a = head;
            if (a != null)
            {
                while (a.next != null)
                {
                    a = a.next;
                }
                Node n = new Node();
                n.data = s;
                a.next = n;
            }
            else
            {
                addToFront(s);
            }




        }


        public int peek(int position)
        {
            Node a = head;

            for (int x = 0; x < position; x++)
            {
                if (a != null)
                {
                    a = a.next;
                }
            }
            return a.data;
        }

        public void Clear()
        {
            head=null;
        }

        public void printList()
        {
            Node a = head;
            while (a != null)
            {
                Console.WriteLine(a.data);
                a = a.next;
            }

        }


    }
}
