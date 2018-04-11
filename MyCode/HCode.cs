using System.IO;
using System.Collections.Generic;
using System;

namespace MyCode
{
    class HCode
    {
        string input_s;

        MyData data;
        public HCode(string input_s)
        {
            this.input_s = input_s;
        }
        public HCode()
        {

        }
        public void Encode()
        {
            data = new MyData();
            Element element;
            for (int i = 0; i < input_s.Length; i++)
            {
                element = new Element(1, input_s[i]);
                data.Add(element);
            }
            data.Sort();
            Tree mytree = new Tree(data);
            mytree.Grow();
            WriteToFile();
        }
        void WriteToFile()
        {
            FileStream file = new FileStream("binary.bin", FileMode.Create);
            BinaryWriter writer = new BinaryWriter(file);
            string t = FormString();
            for (int i = 0; i < t.Length - 1;)
            {
                byte b = (byte)((int.Parse(t[i].ToString()) << 7) |
                                 (int.Parse(t[i + 1].ToString()) << 6) |
                                 (int.Parse(t[i + 2].ToString()) << 5) |
                                 (int.Parse(t[i + 3].ToString()) << 4) |
                                 (int.Parse(t[i + 4].ToString()) << 3) |
                                 (int.Parse(t[i + 5].ToString()) << 2) |
                                 (int.Parse(t[i + 6].ToString()) << 1) |
                                 (int.Parse(t[i + 7].ToString()) << 0)
                                 );
                writer.Write(b);
                i += 8;

            }
            writer.Close();
            file.Close();
        }
        private string FormString()
        {
            List<string> list = new List<string>();
            FileStream file = new FileStream("table.txt", FileMode.Create);
            StreamWriter writ = new StreamWriter(file);
            string t = "";
            Queue<Element> queue = new Queue<Element>();
            for (int i = 0; i < input_s.Length; i++)
            {
                queue.Enqueue(data.GetArray()[0]);
                while (true)
                {
                    Element temp = queue.Dequeue();
                    if (temp.value == input_s[i])
                    {
                        t += temp.AddValue;
                        if (!list.Contains(temp.value + "|" + temp.AddValue)) list.Add(temp.value + "|" + temp.AddValue);
                        break;
                    }
                    if (temp.Right != null)
                    {
                        queue.Enqueue(temp.Right);
                        temp.Right.AddValue = temp.Right.Parent.AddValue + "1";

                    }
                    if (temp.Left != null)
                    {
                        queue.Enqueue(temp.Left);
                        temp.Left.AddValue = temp.Left.Parent.AddValue + "0";
                    }
                }
            }
            int q = 0;
            if (t.Length % 8 != 0)
            {
                q = 8 - t.Length % 8;
                
                for (int j = 0; j < q; j++)
                {
                    t = "0" + t;
                }
            }
            list.Add('`' + "|" + q.ToString());
            FormTable(list, writ);
            writ.Close();
            file.Close();
            return t;
        }
        private void FormTable(List<string> list, StreamWriter writ)
        {
            foreach (string item in list)
            {
                writ.WriteLine(item);
            }

        }
        public string Decode(byte[] bufer, string[] array)
        {
            string bin = "", res = "";
            string[,] table = new string[array.Length, 2];
            for (int i = 0; i < bufer.Length; i++)
            {
                bin += BinToDec(bufer[i]);
            }
            for (int i = 0; i < array.Length; i++)
            {
                string[] str = array[i].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (str.Length <= 1) continue;
                table[i, 0] = str[0];
                table[i, 1] = str[1];
            }
            res = FindConformity(bin, table);
            return res;
        }
        private string BinToDec(byte b)
        {
            string res = "";
            while (b != 0)
            {
                res = (b % 2) + res;
                b /= 2;
            }
            while (res.Length != 8)
            {
                res = "0" + res;
            }
            return res;
        }
        //Соответствие символа и кода
        private string FindConformity(string bin_code, string[,] table)
        {
                string temp = "";
                string res = "";
                bool flag = true;
                for (int i = int.Parse(table[table.Length / 2 - 1, 1]); i < bin_code.Length; i++)
                {

                    if (flag) temp = bin_code[i].ToString();
                    else temp += bin_code[i].ToString();
                    for (int j = 0; j < table.Length / 2 - 1; j++)
                    {
                        if (temp == table[j, 1])
                        {
                            res += table[j, 0];
                            flag = true;
                            break;
                        }
                        else flag = false;
                    }
                }
                return res;
            
            
        }
    }
}
