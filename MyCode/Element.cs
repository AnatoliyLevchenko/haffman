using System;

namespace MyCode
{
    class Element 
    {
        public int key { get; set; }
        public void Node(Element right, Element left)
        {
            Right = right;
            Left = left;
            key = right.key + left.key;
        }
        public string AddValue { get; set; }
        public Element Parent { get; set; }
        public Element Right { get; set; }
        public Element Left { get; set; }
        public char value { get; private set; }
        public Element(int key, char value)
        {
            this.key = key;
            this.value = value;
        }
        public Element()
        {

        }

        public override string ToString()
        {
            return String.Format("Key - {0}, Value - '{1}'", key, value);
        }

       
    }
}
