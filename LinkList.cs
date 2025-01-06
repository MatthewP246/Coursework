using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    internal class LinkList
    {
        Node head;

        public LinkList()
        {
            head = null;
        }

        public void add(string s)
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
    }
}
