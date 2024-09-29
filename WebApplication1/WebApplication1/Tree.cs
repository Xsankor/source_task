namespace WebApplication1
{
    public class Tree
    {
        public Node? root { get; private set; }
        private string resultStr;

        public Tree(string text)
        {
            resultStr = string.Empty;

            foreach (char chr in text)
            {
                root = InsertValue(root, chr);
            }
        }

        private Node InsertValue(Node? node, char chr)
        {
            if (node == null)
            {
                node = new Node(chr);
            }
            else if (node.value <= chr)
            {
                node.left = InsertValue(node.left, chr);
            }
            else
            {
                node.right = InsertValue(node.right, chr);
            }

            return node;
        }

        public string GetSortedResult(Node? node)
        {
            if (node != null)
            {
                GetSortedResult(node.left);
                resultStr = node.value + resultStr;
                GetSortedResult(node.right);
            }

            return resultStr;
        }
    }

    public class Node
    {
        public char value;
        public Node? left, right;
        public Node(char value)
        {
            this.value = value;
            left = null;
            right = null;
        }
    }
}
