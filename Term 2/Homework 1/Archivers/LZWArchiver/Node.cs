namespace LZWArchiver
{
    internal class Node
    {
        public int Code { get; private set; }

        public Dictionary<int, Node> Next { get; set; } = new Dictionary<int, Node>();

        public Node(int code)
        {
            Code = code;
        }
    }
}
