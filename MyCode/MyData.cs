using System;

namespace MyCode
{
    class MyData
    {
        Element[] array;
        public MyData()
        {
            array = new Element[0];
        }
        public Element[] GetArray()
        {
            return array;
        }
        public void SetArray(Element[] array)
        {
            this.array = array;
        }
        public void Add(Element el)
        {
            int pos;
            if (Contain(el, out pos))
                array[pos].key++;
            else
            {
                Array.Resize(ref array, array.Length + 1);
                array[array.Length - 1] = el;
            }
        }
        private bool Contain(Element el, out int pos)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].value == el.value)
                {
                    pos = i;
                    return true;
                }
            }
            pos = -1;
            return false;
        }
        public void Sort()
        {

            for (int i = 0; i < array.Length - 1; i++)
            {
                bool flag = false;
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j].key < array[j + 1].key)
                    {
                        Element temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        flag = true;
                    }
                }
                if (!flag) break;
            }

        }
    }
}
