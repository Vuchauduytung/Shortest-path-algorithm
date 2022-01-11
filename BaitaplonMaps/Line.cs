using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaitaplonMaps
{
    class Line
    {
        public Node firstNode;
        public Node seconNode;
        public double weight;
        public int gradient;
        public Line()
        {

        }
        public Line(Line line)
        {
            this.firstNode = line.firstNode;
            this.seconNode = line.seconNode;
            this.weight = line.weight;
            this.gradient = line.gradient;
        }
        public void calculateWeight()
        {
            weight = Math.Sqrt((firstNode.x - seconNode.x) * (firstNode.x - seconNode.x) + (firstNode.y - seconNode.y) * (firstNode.y - seconNode.y));
        }
    }
}
