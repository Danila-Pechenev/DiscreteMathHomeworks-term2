using System.Text;

namespace HuffmanArchiver
{
    public class HuffmanArchiver
    {
        public void CompressFile(string dataFileName, string archiveName)
        {
            byte[] fileData = File.ReadAllBytes(dataFileName);
            byte[] archiveData = CompressBytes(fileData);
            File.WriteAllBytes(archiveName, archiveData);
        }

        public void DecompressFile(string archiveName, string dataFileName, bool text = true)
        {
            byte[] archiveData = File.ReadAllBytes(archiveName);
            byte[] fileData = DecompressBytes(archiveData);
            if (text)
            {
                Encoding encoder = Encoding.Default;
                string result = encoder.GetString(fileData);
                File.WriteAllText(dataFileName, result);
            }
            else
            {
                File.WriteAllBytes(dataFileName, fileData);
            }
        }

        private byte[] DecompressBytes(byte[] archiveData)
        {
            ParseHeader(archiveData, out int dataLength, out int startIndex, out byte[] frequenciesDict);
            Node root = CreateHuffmanTree(frequenciesDict);
            byte[] decompressedBytes = Decode(archiveData, startIndex, dataLength, root);
            return decompressedBytes;
        }

        private byte[] Decode(byte[] archiveData, int startIndex, int dataLength, Node root)
        {
            int added = 0;
            Node current = root;
            List<byte> decompressedBytes = new List<byte>();
            for (int i = startIndex; i < archiveData.Length; i++)
            {
                for (int bit = 1; bit <= 128; bit <<= 1)
                {
                    bool zero = (archiveData[i] & bit) == 0;
                    if (zero)
                    {
                        current = current.Bit0;
                    }
                    else
                    {
                        current = current.Bit1;
                    }

                    if (current.Bit0 != null) continue;

                    if (added++ < dataLength) decompressedBytes.Add(current.Symbol);

                    current = root;
                }

            }

            return decompressedBytes.ToArray();
        }

        private void ParseHeader(byte[] archiveData, out int dataLength, out int startIndex, out byte[] frequenciesDict)
        {
            dataLength = (int)archiveData[0] + (int)archiveData[1] * 256 + 
                (int)archiveData[2] * 256 * 256 + (int)archiveData[3] * 256 * 256 * 256;
            frequenciesDict = new byte[256];
            for (int symbol = 0; symbol < 256; symbol++)
            {
                frequenciesDict[symbol] = archiveData[symbol + 4];
            }

            startIndex = 4 + 256;
        }

        private byte[] CompressBytes(byte[] fileData)
        {
            byte[] frequenciesDict = CalculateFrequencies(fileData);
            byte[] header = CreateHeader(fileData.Length, frequenciesDict);
            Node root = CreateHuffmanTree(frequenciesDict);
            string[] codes = CreateHuffmanCode(root);
            byte[] compressedBytes = Encode(fileData, codes);
            return header.Concat(compressedBytes).ToArray();
        }

        private byte[] CreateHeader(int dataLength, byte[] frequenciesDict)
        {
            var header = new List<byte>();
            header.Add((byte)(dataLength & 255));
            header.Add((byte)((dataLength >> 8) & 255));
            header.Add((byte)((dataLength >> 16) & 255));
            header.Add((byte)((dataLength >> 24) & 255));
            for (int symbol = 0; symbol < 256; symbol++)
            {
                header.Add(frequenciesDict[symbol]);
            }

            return header.ToArray();
        }

        private byte[] Encode(byte[] fileData, string[] codes)
        {
            var compressedBytes = new List<byte>();
            byte sum = 0;
            byte bitNumber = 1;
            foreach (byte symbol in fileData)
            {
                foreach (char bit in codes[symbol])
                {
                    if (bit == '1') sum |= bitNumber;

                    if (bitNumber < 128)
                    {
                        bitNumber <<= 1;
                    }
                    else
                    {
                        compressedBytes.Add(sum);
                        sum = 0;
                        bitNumber = 1;
                    }

                }

            }

            if (bitNumber > 1) compressedBytes.Add(sum);

            return compressedBytes.ToArray();
        }

        private string[] CreateHuffmanCode(Node root)
        {
            string[] codes = new string[256];
            treeRecursion(root);
            return codes;
            void treeRecursion(Node node, string code = "")
            {
                if (node.Bit0 == null)
                {
                    codes[node.Symbol] = code;
                }
                else
                {
                    treeRecursion(node.Bit0, code + "0");
                    treeRecursion(node.Bit1, code + "1");
                }
            }
        }

        private Node CreateHuffmanTree(byte[] frequenciesDict)
        {
            PriorityQueue<Node, int> priorityQueue = new PriorityQueue<Node, int>();
            for (int symbol = 0; symbol < 256; symbol++)
            {
                if (frequenciesDict[symbol] > 0)
                {
                    priorityQueue.Enqueue(new Node((byte)symbol, frequenciesDict[symbol]), frequenciesDict[symbol]);
                }
            }

            while (priorityQueue.Count != 1)
            {
                Node bit0 = priorityQueue.Dequeue();
                Node bit1 = priorityQueue.Dequeue();
                int newFrequency = bit0.Frequency + bit1.Frequency;
                Node united = new Node(bit0, bit1, newFrequency);
                priorityQueue.Enqueue(united, newFrequency);
            }

            return priorityQueue.Dequeue();
        }

        private byte[] CalculateFrequencies(byte[] fileData)
        {
            int[] frequenciesDict = new int[256];
            foreach (byte symbol in fileData) frequenciesDict[symbol]++;

            int maxFrequency = (int)Math.Ceiling((double)frequenciesDict.Max() / 254);
            byte[] normalizedFrequenciesDict = new byte[256];
            for (int symbol = 0; symbol < 256; symbol++)
            {
                normalizedFrequenciesDict[symbol] = (byte)Math.Round((double)frequenciesDict[symbol] / maxFrequency);
                if (frequenciesDict[symbol] > 0) normalizedFrequenciesDict[symbol]++;
            }

            return normalizedFrequenciesDict;
        }
    }
}
