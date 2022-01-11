using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaitaplonMaps
{
    class AlgorithmHelper
    {
        public static List<Line> sortMinToMax(List<Line> listLine)
        {
            for(int i = 1;i < listLine.Count; i++)
            {
                for(int j = listLine.Count - 2; j>=i-1;j--)
                {
                    if(listLine[j].weight>listLine[j+1].weight)
                    {
                        Line line = new Line(listLine[j]);
                        listLine[j] = listLine[j + 1];
                        listLine[j + 1] = line;
                    }    
                }    
            }
            return listLine;
        }
        public static List<Line> sortByWeight(List<Line> list, Node node)
        {
            for(int i=0; i < list.Count;i++)
            {
                Line line = new Line();
                line.firstNode = list[i].seconNode;
                line.seconNode = node;
                line.calculateWeight();
                list[i].weight = line.weight;
            }
            list = sortMinToMax(list);
            for(int i=0; i<list.Count;i++)
            {
                list[i].calculateWeight();
            }
            return list;
        }
        public static List<List<string>> listDuong = new List<List<string>>();
        public static void tracking(Hashtable list, Node nodeA, Node nodeB, List<string> listDaDuyet)
        {
            if(listDuong.Count!=0&& !Form1.cb.Checked)
            {
                return;
            }    
            listDaDuyet.Add(nodeA.name);
            if (nodeB.name==nodeA.name)
            {
                Console.WriteLine(JsonConvert.SerializeObject(listDaDuyet));
                listDuong.Add(listDaDuyet);
                return;
            }    
            List<Line> listNodeNow = (List<Line>)list[nodeA.name];
            listNodeNow = sortByWeight(listNodeNow, nodeB);
            List<string> listMax = new List<string>();
            foreach(Line line in listNodeNow)
            {

                //Console.WriteLine(JsonConvert.SerializeObject(line));
                if (!listDaDuyet.Contains(line.seconNode.name))
                {
                    tracking(list, line.seconNode, nodeB, new List<string>(listDaDuyet));
                }
            }
        }
        public static Node findMaxLine(Node nodeNow, Hashtable list)
        {
            double min = 100000;
            Node nodeMin = new Node(0, 0, "C");
            foreach (DictionaryEntry node in list)
            {
                Line line = new Line();
                line.firstNode = nodeNow;
                line.seconNode = (Node)node.Value;
                line.gradient = 0;
                line.calculateWeight();
                if (line.weight < min)
                {
                    min = line.weight;
                    nodeMin = new Node((Node)node.Value);
                }
            }
            return nodeMin;
        }
    }
}
