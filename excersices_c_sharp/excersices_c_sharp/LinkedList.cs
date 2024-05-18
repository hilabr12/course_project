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
            ///<summary>
            /// appends a new node with the given value to the end of the list
            ///</summary>
            Node nodeToAdd = new Node(value);
            if (Head == null) // if the list is empty 
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
            ///<summary>
            /// prepends a new node with the given value to the start of the list.
            ///</summary>
            Node nodeToAdd = new Node(value); // create a new node with the given value
            if (Head == null) // if the list is empty
            {
                Head = nodeToAdd; // the head of the list is the new node
                Tail = Head;
            }
            else
            {
                Head = new Node(value, Head); // putting the new node in the beginning of the list
            }
            SetMaxAndMinValues();
        }

        public int Pop()
        {
            ///<summary>
            /// removes and returns the value of the last node in the list.
            /// Returns -999 if the list is empty.
            ///</summary>
            /// <returns>The value of the popped node or -999 if the list is empty.</returns>
            if (Head == null)
                return -999; // if the list is empty

            Node current = Head;
            if (Head.Next == null) // if the list has only one node 
            {
                int valuePopped = Head.Value;
                Head = null;
                SetMaxAndMinValues();
                return valuePopped;
            }
            while (current != null && current.Next != Tail)
            {
                current = current.Next;
            }
            // here current is one node before tail
            Tail = current;
            int valueToPop = Tail.Next.Value;
            Tail.Next = null;
            SetMaxAndMinValues();
            return valueToPop;
        }

        public int Unqueue()
        {
            ///<summary>
            /// Removes and returns the value of the first node in the list.
            /// returns -999 if the list is empty.
            ///</summary>
            /// <returns>The value of the dequeued node or -999 if the list is empty.</returns>
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
            ///<summary>
            /// converts the linked list to a list of integers.
            ///</summary>
            /// <returns>An IEnumerable containing the values of the nodes in the linked list.</returns>
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
            ///<summary>
            /// checks if the linked list is circular.
            ///</summary>
            /// <returns>True if the list is circular, otherwise false.</returns>
            return Tail.Next == Head || Head == null && Tail == null;
            // empty list is also circular
        }

        public void Sort()
        {
            ///<summary>
            /// Sorts the linked list in ascending order.
            ///</summary>
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
            ///<summary>
            /// Gets the node containing the maximum value
            ///</summary>
            /// <returns>The node with the maximum value.</returns>
            return new Node(MaxValueInList);
        }

        public Node GetMinNode()
        {
            ///<summary>
            /// Gets the node containing the minimum value
            ///</summary>
            /// <returns>The node with the minimum value.</returns>
            return new Node(MinValueInList);
        }

        public void SetMaxAndMinValues()
        {
            ///<summary>
            /// Sets the maximum and minimum values
            ///</summary>
            MaxValueInList = FindLargestOrSmallestValue(true);
            MinValueInList = FindLargestOrSmallestValue(false);
        }

        public int FindLargestOrSmallestValue(bool findMax)
        {
            ///<summary>
            /// Finds the largest or smallest value
            ///</summary>
            /// <param name="findMax">A boolean flag indicating whether to find the maximum (true) or minimum (false) value.</param>
            /// <returns>The largest or smallest value</returns>
            int value = Head.Value;
            Node current = Head.Next;

            if (Head == null)
                return -999;

            if (findMax) // find max
            {
                while (current != null)
                {
                    if (value < current.Value)
                        value = current.Value;
                    current = current.Next;
                }
            }
            else // find min 
            {
                while (current != null)
                {
                    if (value > current.Value)
                        value = current.Value;
                    current = current.Next;
                }
            }

            return value;
        }

    }
}
