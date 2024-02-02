using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp29
{
    public partial class Form1 : Form
    {
        Tree<int> myfirstTree;
        List<Ellipse> myfirstEllipse = new List<Ellipse>();
        Graphics g;
        int panelWidth;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int out1))
            {
                if (myfirstEllipse.Count < 1)
                {
                    myfirstTree = new Tree<int>(out1, null);
                    myfirstEllipse.Add(new Ellipse(panelWidth / 2, 10, 30, out1));
                }
                else
                {
                    if(myfirstTree.search(out1) == null)
                    {
                        myfirstTree.add(out1);
                        int r = FindIndex(myfirstTree.search(out1).Parent.Data);
                        if (myfirstTree.search(out1).Parent.Right != null && myfirstTree.search(out1).Parent.Right.Data == out1)
                            myfirstEllipse.Add(new Ellipse(myfirstEllipse[r].StartX + panelWidth / GetLevel(myfirstEllipse[r].StartY), myfirstEllipse[r].StartY + 50, 30, out1));
                        else
                            myfirstEllipse.Add(new Ellipse(myfirstEllipse[r].StartX - panelWidth / GetLevel(myfirstEllipse[r].StartY), myfirstEllipse[r].StartY + 50, 30, out1));
                    }
                    else
                    {
                        MessageBox.Show(
                                    "Элемент уже присутствует.\r\n",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
                    }
                }
            }
            else
            {
                MessageBox.Show(
                                    "Введено не число.\r\n",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
            }
            DrawEllipse();
            DrawLines();
            GetLeafandDeep();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
            panelWidth = panel1.Width;
            label3.Text = "0";
            label4.Text = "0";
        }
        private void GetLeafandDeep()
        {
            if (myfirstTree != null)
            {
                label3.Text = Convert.ToString(myfirstTree.GetDeep());
                label4.Text = Convert.ToString(myfirstTree.GetLeafs());
            }
        }
        private void DrawEllipse()
        {
            g.Clear(Color.White);
            foreach (var item in myfirstEllipse)
            {
                item.Draw(panel1);
            }
        }
        private int GetLevel(int a)
        {
            switch (a)
            {
                case 10:
                    return 4;
                case 60:
                    return 8;
                case 110:
                    return 16;
                case 160:
                    return 32;
                case 210:
                    return 64;
                case 260:
                    return 128;
                default:
                    return 128;
            }
        }
        private int FindIndex(int z)
        {
            for (int i = 0; i < myfirstEllipse.Count; i++)
            {
                if (myfirstEllipse[i].Znach == z)
                    return i;
            }
            return -1;
        }
        private void DrawLines()
        {
            for (int i = 0; i < myfirstEllipse.Count; i++)
            {
                if (myfirstTree.search(myfirstEllipse[i].Znach).Parent != null)
                {
                    int r = FindIndex(myfirstTree.search(myfirstEllipse[i].Znach).Parent.Data);
                    g.DrawLine(new Pen(Color.Black), myfirstEllipse[r].StartX + 10, myfirstEllipse[r].StartY + 10, myfirstEllipse[i].StartX + 10, myfirstEllipse[i].StartY + 10);
                }
            }
        }
        private void ChangeRoot()
        {
            for (int i = 0; i < myfirstEllipse.Count; i++)
            {
                if (myfirstTree.search(myfirstEllipse[i].Znach).Parent == null)
                {
                    myfirstEllipse[i].StartX = panelWidth / 2;
                    myfirstEllipse[i].StartY = 10;
                    myfirstEllipse[i].Rerect();
                }
            }
            for (int i = 0; i < myfirstEllipse.Count; i++)
            {
                if (myfirstTree.search(myfirstEllipse[i].Znach).Parent != null)
                {
                    int r = FindIndex(myfirstTree.search(myfirstEllipse[i].Znach).Parent.Data);
                    if (myfirstTree.search(myfirstEllipse[i].Znach).Parent.Right != null && myfirstTree.search(myfirstEllipse[i].Znach).Parent.Right.Data == myfirstEllipse[i].Znach)
                        myfirstEllipse[i].StartX = myfirstEllipse[r].StartX + panelWidth / GetLevel(myfirstEllipse[r].StartY);
                    else
                        myfirstEllipse[i].StartX = myfirstEllipse[r].StartX - panelWidth / GetLevel(myfirstEllipse[r].StartY);
                    myfirstEllipse[i].StartY = myfirstEllipse[r].StartY + 50;
                    myfirstEllipse[i].Rerect();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (myfirstTree != null && int.TryParse(textBox1.Text, out int out1))
            {
                if (myfirstTree.search(out1) != null)
                {
                    myfirstEllipse.RemoveAt(FindIndex(out1));
                    myfirstTree.Remove(out1);
                }
                else
                {
                    MessageBox.Show(
                                    "Элемент не найден.\r\n",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show(
                                    "Дерево не создано || введено не число.\r\n",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
            }
            ChangeRoot();
            DrawEllipse();
            DrawLines();
            GetLeafandDeep();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (myfirstTree != null && int.TryParse(textBox1.Text, out int out1) && myfirstTree.search(out1) != null)
            {
                if(myfirstTree.search(out1).Parent != null)
                {
                    richTextBox1.Text += "Элемент: " + out1 + " Родитель: "+ myfirstTree.search(out1).Parent.Data + "\r\n";
                }
                else
                {
                    richTextBox1.Text += "Элемент: " + out1 + " Родитель - нет.\r\n";
                }
            }
            else
            {
                MessageBox.Show(
                                    "Дерево не создано || элемент не найден.\r\n",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
