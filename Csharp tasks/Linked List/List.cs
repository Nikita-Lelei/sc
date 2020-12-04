using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linked_List
{
    class List
    {
        public Node root;
        public int index;
   
        public void WhenNoChild(Node node, Node newNode)
        {
            if (node.index < newNode.index)
            {
                node.nextElement = newNode;
                newNode.parent = node;
            }
            else
            { 
                newNode.parent = node.parent;
                newNode.nextElement = node;
                node.parent.nextElement = newNode;
                node.parent = newNode;
            }
        }

        public void InsertNode(Node node, int index, Node newNode)
        {
            newNode.index = index;
            if (node.index == index)
            {
                if(node.parent == null)
                {
                    root = newNode;
                    newNode.nextElement = node;
                    node.parent = newNode;

                }
                else
                {
                    newNode.parent = node.parent;
                    newNode.nextElement = node;
                    node.parent.nextElement = newNode;
                    node.parent = newNode;
                }
               

                ChangeIndex(node, index + 1);
            }
            else
            {
                if (node.nextElement != null)
                {
                    InsertNode(node.nextElement, index, newNode);
                }
                else
                {
                    WhenNoChild(node, newNode);
                }

            }
        }

        public Node FindByIndex_(Node node,int index)
        {
            if(node.index == index)
            {
                return node;
            }
            else
            {
                if(node.nextElement != null)
                {
                    return FindByIndex_(node.nextElement, index);
                }
                else
                {
                    Console.WriteLine("nothing was find");
                    return null;
                }
              
            }
       
        }
      
        public void ChangeIndex(Node node, int index)
        {
            node.index = index;
            if(node.nextElement != null && node.nextElement.index <= index)
            {
                ChangeIndex(node.nextElement, index + 1);
            }
        }
    
        public void ShowNode(Node node)
        {
            Console.WriteLine(node);
            if(node.nextElement != null)
            {
                ShowNode(node.nextElement);
            }
        }

        public void FindByIndex(int index)
        {
            Node findedNode = FindByIndex_(root, index);
            if (findedNode != null)
            {
                Console.WriteLine($"node with index {index}: {findedNode}");
            }

        }

        public void DeleteByIndex(int index)
        {
            Node toDelete = FindByIndex_(root, index);
            if (toDelete != null)
            {
                if(toDelete.parent != null)
                {
                    toDelete.parent.nextElement = toDelete.nextElement;
                    toDelete.nextElement.parent = toDelete.parent;
                } 
                else
                {
                    toDelete.nextElement.parent = null;
                    root = toDelete.nextElement;
                }

            }
        }

        public void AddByIndex(int index, string data)
        {
            if(index < 0)
            {
                Console.WriteLine("Incorrect walue");

            }
            else
            {
                Node newNode = new Node();
                newNode.data = data;
                InsertNode(root, index, newNode);
            }
            
        }

        public void AddNode(string data)
        {
            Node newNode = new Node();
            newNode.data = data;
            if (root == null)
            {
                newNode.index = index;
                root = newNode;
            }
            else
            {
                InsertNode(root, index, newNode);

            }
            index++;

        }

        public void ShowList()
        {
            ShowNode(root);
        }

    }

}
