using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linked_List
{
    class Node
    {
        public string data;
        public Node nextElement;
        public Node parent;
        public int index;
        public override string ToString()
        {
            return $"{this.index}: {this.data}";
        }
    }
}
