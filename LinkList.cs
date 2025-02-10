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


        public void addToMiddle(int s, int position)
        {
            Node n = new Node();
            n.data = s;
            if (position == 1) addToFront(s);
            else
            {
                Node temp = head;
                for (int x = 0; x < position - 1; x++)
                {
                    if (temp != null) temp = temp.next;

                }
                if (temp.next != null)
                {
                    n.next = temp.next;
                    n.previous = temp;
                    temp.next = n;
                    if (n.next != null) n.next.previous = n;
                }
                else Add(s);
            }
        }



        public int removeFromFront()
        {
            Node a = head;
            if (a.next == null) head = null;
            else
            {
                a.next.previous = null;
                head = a.next;
            }
            return a.data;
        }

        

        public int removeFromRear()
        {
            Node a = head;


            while (a.next != null)
            {
                if (a.next != null)
                {
                    a = a.next;
                }
            }
            a.previous.next = null;
            return a.data;

        }

        public int removeFromMiddle(int position)
        {
            if (position == 1) return removeFromFront();
            else
            {
                Node temp = head;
                for (int x = 0; x < position - 1; x++)
                {
                    if (temp != null) temp = temp.next;
                }
                if (temp.next != null)
                {
                    Node a = temp.next;
                    a.next = temp.next.next;
                    a.previous = temp;

                    return a.data;

                }
                else
                {
                    temp.next = null;
                    return removeFromRear();
                }
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
