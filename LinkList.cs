using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    internal class LinkList
    {
        private Node head;

        public LinkList()
        {
            head = null;


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
            if (head != null)
            {
                head.previous = n;
            }

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
                n.previous = a;
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

        
    }
}
