using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaitaplonMaps
{
    class Node
    {
        public int x;
        public int y;
        public string name;
        public Node(int x, int y, string name)
        {
            this.x = x;
            this.y = y;
            this.name = name;
        }
        public Node(Node n)
        {
            x = n.x;
            y = n.y;
            name = n.name;
        }
    }
}
