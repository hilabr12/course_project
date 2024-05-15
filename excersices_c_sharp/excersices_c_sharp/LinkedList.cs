using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace excersices_c_sharp
{
    public class LinkedList
    {
        private Node Head { get; set; }
        private Node Tail { get; set; }
        private int MaxValueInList { get; set; }
        private int MinValueInList { get; set; }


        public void Append(int value)
        {
            Node nodeToAdd = new Node(value);
            if(Head == null) // if the list is empty 
            {
                
                Tail = nodeToAdd;
                Head = Tail;
            }
            else // if the list is not empty
            {
                Tail.Next = nodeToAdd; // add to last node
                Tail = Tail.Next; // move tail to last node
            }
            SetMaxAndMinValues();
        }
        public void Prepend(int value)
        {
            Node nodeToAdd = new Node(value); // create a new node with the given value
            if(Head == null) // if the list is empty
            {
                Head = nodeToAdd; // the head of the list is the new node
                Tail = Head; 
            }
            else
            {
                Head = new Node(value,Head); // putting the new node in the beggining of the list
            }
            SetMaxAndMinValues();

        }

        public int Pop() 
        {
            if (Head == null)
                return -999; // if the list is empty
            
            Node current = Head;
            if(Head.Next == null) // if the list has only one node 
            {
                int valuePopped = Head.Value;
                Head = null;
                SetMaxAndMinValues();
                return valuePopped;
            }
            while(current != null && current.Next != Tail) 
            {
                current = current.Next;
            }
            //here current is one node before tail
            Tail = current;
            int valueToPop = Tail.Next.Value;
            Tail.Next = null;
            SetMaxAndMinValues();
            return valueToPop;
        }    
        public int Unqueue()
        {

            if (Head == null) // if the list is empty
                return -999;
            else
            {
                int valuePopped = Head.Value;
                if (Head.Next == null) // if the list has only one node 
                {
                    Head = null;
                    return valuePopped;
                }

                Head = Head.Next;
                SetMaxAndMinValues();

                return valuePopped;
            }
        }
        
        public IEnumerable<int> ToList()
        {
            Node current = Head;

            while (current != null)
            {
                int currentValue = current.Value;
                current = current.Next;
                yield return currentValue;

            }
        }
        public bool IsCircular()
        {
            return Tail.Next == Head || Head == null && Tail == null;
            // empty list is also circular
        }


        public void Sort()
        {
            bool noMoreSwappingNeeded = false;
            Node current;

            if (Head == null) // if the list is empty
                return;

            while (!noMoreSwappingNeeded) // swapping until all nodes are sorted
            {
                noMoreSwappingNeeded = true;
                current = Head;

                while (current.Next != null)
                {
                    if (current.Value > current.Next.Value) // ascending order
                    {
                        int holdValueOfNode = current.Next.Value;
                        current.Next.Value = current.Value;
                        current.Value = holdValueOfNode;
                        noMoreSwappingNeeded = false;
                    }
                    current = current.Next;
                }
            }
        }

        public Node GetMaxNode()
        {
            return new Node(MaxValueInList);
        }
        public Node GetMinNode()
        {    
            return new Node(MinValueInList);
        }

        public void SetMaxAndMinValues()
        {
            MaxValueInList = FindLargestOrSmallestValue(true);
            MinValueInList = FindLargestOrSmallestValue(false);
        }


        public int FindLargestOrSmallestValue(bool findMax)
        {
            int value = Head.Value;
            Node current = Head.Next;

            if (Head == null)
                return -999;
            
            if (findMax) //find max
            {
                while (current != null && current.Next != null)
                {
                    if (value > current.Value)
                        value = current.Value;
                    current = current.Next;
                }
            }

            else //find min 
            {
                while (current != null && current.Next != null)
                {
                    if (value < current.Value)
                        value = current.Value;
                    current = current.Next;
                }
            }

            return value;
        }

    }
}
