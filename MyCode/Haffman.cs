using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MyCode
{
    public partial class Haffman : Form
    {
        public Haffman()
        {
            InitializeComponent();
        }
        string s = "";
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "") throw new Exception("Входная строка должа именть как минимум 1 символ!");
                string[] t = textBox1.Lines;
                foreach (var item in t)
                {
                    s += item;
                }
                HCode mycode = new HCode(s);
                mycode.Encode();
                MessageBox.Show("Строка закодированна успешно!");           
                FileInfo file = new FileInfo("binary.bin");
                label6.Text = "Имя файла - " + file.Name;
                label7.Text = $"Размер закодированного файла - {file.Length} байт";
                label8.Text = $"Размер входной строки - {textBox1.Text.Length} байт";
                MessageBox.Show($"Коэффициент сжатия - {float.Parse(file.Length.ToString())/ float.Parse(textBox1.Text.Length.ToString())*100d }%");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                HCode mycode = new HCode();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileStream file = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                    int leng = (int)file.Length;
                    byte[] bufer = new byte[leng];
                    file.Read(bufer, 0, leng);
                    file.Close();
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        file = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                        StreamReader reader = new StreamReader(file);
                        string[] array = new string[100000];
                        string temp;
                        int i = 0;
                        while ((temp = reader.ReadLine()) != null)
                        {
                            array[i] = temp;
                            i++;
                        }
                        reader.Close();
                        string[] new_array = new string[i];
                        for (int j = 0; j < i; j++)
                        {
                            new_array[j] = array[j];
                        }
                        file = new FileStream("result.txt", FileMode.Create);
                        StreamWriter writer = new StreamWriter(file);
                        string result = mycode.Decode(bufer, new_array);
                        writer.Write(result);
                        writer.Close();
                        file.Close();
                        MessageBox.Show("Строка декодированна успешно!");
                        textBox2.Text = result;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Выбран неправильный файл.\nСледуйте инструкциям во избежание ошибки!");
            }
            
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = "";
                FileStream file = new FileStream(openFileDialog1.FileName, FileMode.Open);
                StreamReader reader = new StreamReader(file,Encoding.Default);
                string temp;

                while ((temp = reader.ReadLine()) != null)
                {
                    textBox1.Text += temp + "\n";
                }
                reader.Close();
                file.Close();
            }
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
