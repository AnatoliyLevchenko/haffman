using System;

namespace MyCode
{
    class Tree
    {
        MyData data;
        Element[] array;
        public Tree(MyData data)
        {
            this.data = data;
            array = data.GetArray();
        }

        public void Grow()
        {
            while (array.Length != 1)
            {
                Element min1 = array[array.Length - 1];
                Element min2 = array[array.Length - 2];
                Element parent = new Element(1, '~');
                Array.Resize(ref array, array.Length - 1);
                
                min1.Parent = parent;
                min2.Parent = parent;
                parent.Node(min2, min1);
                array[array.Length - 1] = parent;
                data.SetArray(array);
                data.Sort();
            }
        }
    }
}
