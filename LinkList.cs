using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Connect4
{
    /*
     * LINKED LIST
     * 
     * Creates a linked list of nodes
     * Contains methods for adding to the front and back of the list, searching for a node, and counting the number of nodes in the list
     */
    public class LinkList : IEnumerable
    {
        private Node Head;

        public LinkList()
        {
            Head = null;
        }

        

        public int Count()
        {
            //Seperate method needed as it is recursive
            return Length(Head);
        }

        private int Length(Node a)
        {
            //Finds the length of the list
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
            //Adds to the front of the list
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
            //Returns the data at a given position
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
            //Enumerator used for foreach loops
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
