using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaitaplonMaps
{
    public partial class Form1 : Form
    {
        public Bitmap bmp;
        public static CheckBox cb = new CheckBox();
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Image);
            cb = checkBox1;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
        string locale = "";
        int xx = 0;
            int yy = 0;
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            locale = val + " " + e.X + " " + e.Y;
            label1.Text = locale;
            xx = e.X;
            yy = e.Y;
        }
        int val = 1;
        private void button2_MouseClick(object sender, MouseEventArgs e)
        {

        }
        int i = 1;
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += locale + Environment.NewLine;
            locale = "";
            i++;
            val++;
        }
        public bool isDrawl = false;
        public int x = 0;
        public int y = 0;
        public int x1 = 0;
        public int y1 = 0;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (isDrawl)
            {
                e.Graphics.DrawLine(
                    new Pen(Color.Red, 2f),
                    new Point(nodeFirst.x, nodeFirst.y),
                    new Point(nodeFirstMin.x, nodeFirstMin.y));
                for (int i = 1; i < listDuong.Count; i++)
                {
                    Node n0 = (Node)listNode[listDuong[i - 1]];
                    Node n1 = (Node)listNode[listDuong[i]];

                    e.Graphics.DrawLine(
                    new Pen(Color.Red, 2f),
                    new Point(n0.x,n0.y),
                    new Point(n1.x, n1.y));
                }
                isDrawl = false;
                e.Graphics.DrawLine(
                    new Pen(Color.Red, 2f),
                    new Point(nodeSecond.x, nodeSecond.y),
                    new Point(nodeSecondMin.x, nodeSecondMin.y));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            AlgorithmHelper.listDuong = new List<List<string>>();
            AlgorithmHelper.tracking(listDistance, nodeFirstMin, nodeSecondMin, new List<string>());
            listDuong = AlgorithmHelper.listDuong[0];
            isDrawl = true;
            pictureBox1.Invalidate();
            for(int i=0;i< AlgorithmHelper.listDuong.Count; i++)
            {
                textBox1.Text += JsonConvert.SerializeObject(AlgorithmHelper.listDuong[i]) + Environment.NewLine;
            }    
        }
        public Hashtable listNode = new Hashtable();
        public Hashtable listDistance = new Hashtable();
        public void LoadLocation(string path)
        {
            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                string[] data = sr.ReadLine().Split(' ');
                string name = data[0];
                string X = data[1];
                string Y = data[2];
                Node node = new Node(int.Parse(X), int.Parse(Y), name);
                listNode.Add(name, node);
                List<Line> listLine = new List<Line>();
                listDistance.Add(name, listLine);
            }

            sr.Close();
        }
        public void LoadNode(string path)
        {
            StreamReader sr2 = new StreamReader(path);
            while (!sr2.EndOfStream)
            {
                string[] data = sr2.ReadLine().Split(' ');
                string nameNodeA = data[0];
                string nameNodeB = data[1];
                int gradient = int.Parse(data[2]);
                Node nodeA = (Node)listNode[nameNodeA];
                Node nodeB = (Node)listNode[nameNodeB];
                Line line = new Line();
                line.gradient = int.Parse(data[2]);
                line.firstNode = nodeA;
                line.seconNode = nodeB;
                line.calculateWeight();
                List<Line> lst = (List<Line>)listDistance[nameNodeA];
                List<Line> lst2 = (List<Line>)listDistance[nameNodeB];
                if (line.gradient == 0)
                {
                    lst.Add(line);
                    Line line2 = new Line();
                    line2.gradient = int.Parse(data[2]);
                    line2.firstNode = nodeB;
                    line2.seconNode = nodeA;
                    lst2.Add(line2);
                }
                else
                {
                    if (line.gradient == 1)
                    {
                        lst.Add(line);
                    }
                    else
                    {
                        line.firstNode = (Node)listNode[nameNodeB];
                        line.seconNode = (Node)listNode[nameNodeA];
                        lst2.Add(line);
                    }
                }
                listDistance.Remove(nameNodeA);
                listDistance.Remove(nameNodeB);
                listDistance.Add(nameNodeA, lst);
                listDistance.Add(nameNodeB, lst2);
            }
            Hashtable htb2 = new Hashtable();
            foreach (DictionaryEntry s in listDistance)
            {
                //Is Section the Key?
                List<Line> ln = AlgorithmHelper.sortMinToMax((List<Line>)s.Value);
                htb2.Add(s.Key, ln);
            }
            listDistance = htb2;
            Console.WriteLine(JsonConvert.SerializeObject(listDistance));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        List<string> listDuong;
        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                LoadLocation(openFileDialog1.FileName);
            }    
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadNode(openFileDialog1.FileName);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadLocation("locationNode.txt");
            LoadNode("Node.txt");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }
        Node nodeFirst = new Node(0,0,"A");
        Node nodeSecond = new Node(0, 0, "B");
        Node nodeFirstMin = new Node(0, 0, "A1");
        Node nodeSecondMin = new Node(0, 0, "B1");
        private void button3_Click_1(object sender, EventArgs e)
        {
            nodeFirst = new Node(xx, yy, "A");// tọa độ chuột
            nodeFirstMin = AlgorithmHelper.findMaxLine(nodeFirst, listNode);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            nodeSecond = new Node(xx, yy, "B");
            nodeSecondMin = AlgorithmHelper.findMaxLine(nodeSecond, listNode);
        }
    }
}
