using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Text = "Халилов Тимур Русланович, гр. 21ПМ1д, Вар. 8";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            //--------------------------------------------------------------------

            label1.Text = "Рёбра";
            label2.Text = "Вершины";
            label3.Text = "Сторона";

            label6.ForeColor = Color.Red;
            label6.Text = "Октаэдр правильная фигура. У неё 12 рёбер, 6 вершин.";

            label4.Text = " ";
            label5.Text = " ";

            radioButton1.Text = "Вычислить объём. ";
            radioButton2.Text = "Вычислить площадь поверхности. ";

            button1.Click += new EventHandler(Button1_Click);
            button1.Text = "Вычислить";
            //--------------------------------1-----------------------------------
            ToolStripMenuItem fileItem1 = new ToolStripMenuItem("Файл");

            ToolStripMenuItem saveItem = new ToolStripMenuItem("Сохранить");
            saveItem.Click += saveItem_Click;
            saveItem.ShortcutKeys = Keys.Control | Keys.S;

            ToolStripMenuItem exit = new ToolStripMenuItem("Выход");
            exit.Click += exit_Click;
            exit.ShortcutKeys = Keys.Control | Keys.W;

            //--------------------------------2-----------------------------------
            ToolStripMenuItem fileItem2 = new ToolStripMenuItem("Результаты");

            ToolStripMenuItem show = new ToolStripMenuItem("Показать");
            show.Click += Button1_Click;
            show.ShortcutKeys = Keys.Control | Keys.P;

            //--------------------------------3-----------------------------------
            ToolStripMenuItem fileItem3 = new ToolStripMenuItem("Справка");

            ToolStripMenuItem condition = new ToolStripMenuItem("Условие задачи");
            condition.Click += condition_Click;
            condition.ShortcutKeys = Keys.Control | Keys.Y;

            ToolStripMenuItem aboutItem = new ToolStripMenuItem("О программе");
            aboutItem.Click += aboutItem_Click;
            aboutItem.ShortcutKeys = Keys.Control | Keys.O;

            //--------------------------------------------------------------------


            //--------------------------------------------------------------------

            fileItem1.DropDownItems.Add(saveItem);
            fileItem1.DropDownItems.Add(exit);
            fileItem2.DropDownItems.Add(show);
            fileItem3.DropDownItems.Add(condition);
            fileItem3.DropDownItems.Add(aboutItem);

            menuStrip1.Items.Add(fileItem1);
            menuStrip1.Items.Add(fileItem2);
            menuStrip1.Items.Add(fileItem3);

            Controls.Add(button1);
        }



        void saveItem_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(label1.Text  + " " + textBox1.Text);
                sb.AppendLine(label2.Text + " " + textBox2.Text);
                sb.AppendLine(label3.Text + " " + textBox3.Text);
                sb.AppendLine(label4.Text + " " + label5.Text);
                // сохранение данных и результатов вычислений в файл
                System.IO.File.WriteAllText(fileName, sb.ToString()                                 );
            }

        }
        void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //--------------------------------2-----------------------------------

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;
                toolStripProgressBar1.Step = 25;
                toolStripProgressBar1.PerformStep();

                double edgesPerFace = double.Parse(textBox1.Text);
                if (edgesPerFace != 12) { MessageBox.Show("Октаэдр - правильная фигура. Кол-во рёбер не может быть другим значением. ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return;  }
                double edgesPerVertex = double.Parse(textBox2.Text);
                if (edgesPerVertex != 6) { MessageBox.Show("Октаэдр - правильная фигура. Кол-во вершин не может быть другим значением.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                
                double volume = 0;
                double surfaceArea = 0;

                if (radioButton1.Checked)
                {
                    volume = CalculateVolume(edgesPerFace, edgesPerVertex);
                    label4.Text = "Объем октаэдра: "; label5.Text = volume.ToString();
                    MessageBox.Show("Объем октаэдра: " + volume.ToString(), "Объем", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (radioButton2.Checked)
                {
                    surfaceArea = CalculateSurfaceArea(edgesPerFace, edgesPerVertex);
                    label4.Text = "Площадь поверхности октаэдра: "; label5.Text = surfaceArea.ToString();
                    MessageBox.Show("Площадь октаэдра: " + surfaceArea.ToString(), "Площадь", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Выберите вариант вычислений", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные значения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            toolStripProgressBar1.Step = 25;
            toolStripProgressBar1.PerformStep();
            toolStripProgressBar1.Value = toolStripProgressBar1.Maximum;
        }
        private double CalculateVolume(double edgesPerFace, double edgesPerVertex)
        {
            double a = double.Parse(textBox3.Text);
            return (Math.Pow(a, 3) + Math.Sqrt(2)) / 3;
        }

        private double CalculateSurfaceArea(double edgesPerFace, double edgesPerVertex)
        {
            double a = double.Parse(textBox3.Text);
            return Math.Pow(a, 2) * 2 * Math.Sqrt(3);
        }

        //--------------------------------3-----------------------------------
        void aboutItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Приложение для расчёта параметров октаэдра. Халилов Тимур, 21ПМ1д", "О программе", MessageBoxButtons.OK);
        }
        void condition_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Приложение для расчёта параметров октаэдра. Главная форма содержит: элементы для ввода значений числа рёбер в каждой грани и числа рёбер, сходящихся в каждой вершине; группу элементов для выбора вычислений объёма и площади поверхности октаэдра.", "Условие", MessageBoxButtons.OK);
        }
        //--------------------------------------------------------------------

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "C:\\Users\\нищий на знания\\Desktop\\labs Tecnologi Programming 2 курс\\laba9\\9\\ok.png";
            this.Controls.Add(pictureBox1);
        }

    }
}