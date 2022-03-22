namespace HuffmanArchiver
{
    internal class Node
    {
        public byte Symbol { get; private set; }
        public int Frequency { get; private set; }
        public Node? Bit0 { get; private set; }
        public Node? Bit1 { get; private set; }

        public Node(byte symbol, int frequency)
        {
            Symbol = symbol;
            Frequency = frequency;
        }

        public Node(Node bit0, Node bit1, int frequency)
        {
            Bit0 = bit0;
            Bit1 = bit1;
            Frequency = frequency;
        }
    }
}