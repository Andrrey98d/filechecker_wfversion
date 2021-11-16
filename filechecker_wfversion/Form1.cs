using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace filechecker_wfversion
{
    public partial class Form1 : MaterialForm
    {
        const string path = @"D:\\sign.txt";
        public static FileInfo fn = new FileInfo(path);
        public static FileInfo fn_2 = new FileInfo(path);
        const string path_nonsign = @"D:\\nonconst.txt";
        static string[] literals = new string[16];
        public object patt_1 = "Сигнатура";
        static Tuple<string, string, string>[] sign_array = new Tuple<string, string, string>[literals.Length];
        static Tuple<string, string>[] array_nonsign = new Tuple<string, string>[5];
        public string temp = null;
        public string paste = null;
        public int line_count = 0;
        public int noncons_count = 0;

        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            ColorScheme colorScheme = new ColorScheme(Primary.Blue800, Primary.Blue900, Primary.Blue500, Accent.LightBlue200, TextShade.WHITE);
            materialSkinManager.ColorScheme = colorScheme;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            line_count += CountAllLines(fn);
            noncons_count += CountAllLines(fn_2);
            string[] temp_array = File.ReadAllLines(fn.FullName);
            string[] temp_nons = File.ReadAllLines(fn_2.FullName);

            for (int j = 0; j < sign_array.Length; j++)
            {
                string[] t_a_ = Array.Empty<string>();
                t_a_ = temp_array[j].Split('-'); //3
                sign_array[j] = new Tuple<string, string, string>(t_a_[0], t_a_[1], t_a_[2]);
                listBox1.Items.Add(sign_array[j]);
                literals[j] = t_a_[0].ToUpper(); // 0x800..., то есть первый столбец
                t_a_ = null;
            }

            array_nonsign[0] = new Tuple<string, string>("DrWeb антивирус", "2193_TCP");
            array_nonsign[1] = new Tuple<string, string>("Viber", "4244_TCP");
            array_nonsign[2] = new Tuple<string, string>("SCAP", "2010");
            array_nonsign[3] = new Tuple<string, string>("VERITAS_Backup_exec", "3527");
            array_nonsign[4] = new Tuple<string, string>("CoC", "9339");
            Array.Resize(ref array_nonsign, array_nonsign.Length + (noncons_count - array_nonsign.Length));
        }

        private void VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.AutoScroll = true;
        }
        public static int CountAllLines(FileInfo file) => File.ReadAllLines(file.FullName).Length;

        private void ListBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            KeyPreview = true;
            paste = listBox1.SelectedItem.ToString();
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
            {
                materialTextBox1.Text = paste;
                paste = null;
            }
        }
        private void ListBox2_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPreview = true;
            paste = listBox2.SelectedItem.ToString();
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
            {
                materialTextBox1.Text = paste;
                paste = null;
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e) => listBox1.HorizontalScrollbar = true;

        private async void materialButton1_Click(object sender, EventArgs e)
        {
            List<string> temp = new List<string>();
            int CNST;
            List<string> tnp_ = new List<string>();
            while (listBox1.SelectedItems.Count > 0)
            {
                temp.Add(String.Join(",", listBox1.SelectedItems[0]));
                listBox1.Items.Remove(listBox1.SelectedItems[0]);
                string line;
                StreamReader sr = new StreamReader(path);
                StreamWriter sw = new StreamWriter(path);
                var new_path = @"D:\\sign_.txt";
                StreamWriter SC = new StreamWriter(new_path);
                line = await sr.ReadLineAsync();
                using (sr)
                {
                    var OBJ_ = listBox1.SelectedItems[0];
                    while (line != null)
                    {
                        tnp_.Add(line);
                    }
                    if (tnp_.Contains(OBJ_)) { tnp_.RemoveAt(tnp_.IndexOf(OBJ_ as string)); }

                    for (CNST = 0; CNST < tnp_.Count; CNST++)
                    {
                        await SC.WriteLineAsync(tnp_[CNST]);
                        SC.Close();
                    }
                    File.Delete(path);
                    File.Move(new_path, path);
                    
                }
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();

            string first_line = null;
            string first_line_hexed = null;
            string nm_ = null;
            string pattern1 = "Использование";
            string pattern2 = "Порт/протокол";
            Thread.Sleep(200);
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (listBox2.Items.Count > 2)
                {
                    listBox2.Items.Clear();
                }
                nm_ += ofd.FileName;
                Encoding enc = Encoding.Default;
                first_line += File.ReadLines(nm_).First();
                byte[] bytes = enc.GetBytes(first_line);
                first_line_hexed += BitConverter.ToString(bytes); // !!!
                var first_line_hexed_lines = first_line_hexed.Where(t => t != '-').ToArray();
                first_line_hexed = String.Join("", first_line_hexed_lines);
                //для первого случая
                bool result = first_line.IndexOf("Protocol", StringComparison.OrdinalIgnoreCase) >= 0;
                bool result_1 = first_line.IndexOf("agent", StringComparison.OrdinalIgnoreCase) >= 0;
                bool result_2 = first_line.IndexOf("install", StringComparison.OrdinalIgnoreCase) >= 0;
                bool result_3 = first_line.IndexOf("msnapshv", StringComparison.OrdinalIgnoreCase) >= 0;
                //для второго случая
                bool result_4 = first_line.IndexOf("Wi-Fi", StringComparison.OrdinalIgnoreCase) >= 0;
                bool result_5 = first_line.IndexOf("iPhone", StringComparison.OrdinalIgnoreCase) >= 0;
                bool result_6 = first_line.IndexOf("Desktop", StringComparison.OrdinalIgnoreCase) >= 0;
                //для третьего случая
                bool result_7 = first_line.IndexOf("scap v1.0", StringComparison.OrdinalIgnoreCase) >= 0;
                bool result_8 = first_line.IndexOf("scap v2.0", StringComparison.OrdinalIgnoreCase) >= 0;
                bool result_9 = first_line.IndexOf("scap v2.1", StringComparison.OrdinalIgnoreCase) >= 0;
                //для четвертого случая
                bool result_10 = first_line.IndexOf("N-KS", StringComparison.OrdinalIgnoreCase) >= 0;
                bool result_11 = first_line.IndexOf("ZI - G", StringComparison.OrdinalIgnoreCase) >= 0;
                bool result_12 = first_line.IndexOf("P 1 ", StringComparison.OrdinalIgnoreCase) >= 0;
                //для шестого случая
                bool result_13 = first_line.IndexOf("Swarm protocol", StringComparison.OrdinalIgnoreCase) >= 0;

                if (result || result_1 || result_2 || result_3)
                {
                    Console.WriteLine($"\n||{pattern1}: {array_nonsign[0].Item1} ||\n||{pattern2}: {array_nonsign[0].Item2} ||");
                }

                else if (result_4 || result_5 || result_6)
                {
                    Console.WriteLine($"\n||{pattern1}: {array_nonsign[1].Item1} ||\n||{pattern2}: {array_nonsign[1].Item2} ||");
                }
                else if (result_7 || result_8 || result_9)
                {
                    Console.WriteLine($"\n||{pattern1}: {array_nonsign[2].Item1} ||\n||{pattern2}: {array_nonsign[2].Item2} ||");
                }
                //else if..10,11,12
                else if (result_13)
                {
                    Console.WriteLine($"\n||{pattern1}: {array_nonsign[4].Item1} ||\n||{pattern2}: {array_nonsign[4].Item2} ||");
                }

                for (int i = 0; i < sign_array.Length; i++)
                {
                    int l = 0;
                    if (first_line_hexed.IndexOf(literals[i], StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        listBox2.Items.Add($"\n{patt_1}: {literals[i]}");
                        listBox2.Items.Add($"{pattern1}: {sign_array[i].Item2}");
                        listBox2.Items.Add($"{pattern2}: {sign_array[i].Item3}");
                    }
                }
            }
            else if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
        }

        private void materialTextBox1_TextChanged(object sender, EventArgs e) { }

        private void materialTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            temp = materialTextBox1.Text;
            listBox1.Items.Add(temp);

            temp += materialTextBox1.Text;
            KeyPreview = true;
            if (e.KeyChar == (char)Keys.Enter)
            {
                temp = materialTextBox1.Text;
                //listBox1.Items.Add(temp);
                string[] t_a_ = Array.Empty<string>();
                var temp_a_ = temp.Replace(',', '-'); //было t_a_, и text.Split("-");       
                FileInfo fl = new FileInfo(path);
                using (StreamWriter sw = fl.AppendText())
                {
                    sw.WriteLine($"{temp_a_}"); //sign_array[sign_array.Length - 1]
                    sw.Close();
                    MessageBox.Show($"Файл {fl.FullName} перезаписан! \nДобавлена строка: {temp_a_}");
                }
                temp = null;
            }
        }

        private void materialButton3_Click(object sender, EventArgs e) => this.materialTextBox1.Text = null;

        private void materialButton4_Click(object sender, EventArgs e)
        {
        }

        private void materialListView1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPreview = true;
            paste = listBox2.SelectedItems.ToString();
            var paste_formatted = paste.Where(t => t != ')' && t != '(' && t != ',').ToArray();
            Console.ReadLine();
            paste = String.Join("", paste_formatted);
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
            {
                materialTextBox1.Text = paste;
                paste = null;
            }
        }

        private void listBox2_SelectedIndexChanged_1(object sender, EventArgs e) { }
    }
}

