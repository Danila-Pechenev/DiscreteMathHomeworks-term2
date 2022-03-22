using System.Text;

namespace LZWArchiver
{
    public class LZWArchiver
    {
        public int CompressionRatio { get; private set; } = 16;

        public LZWArchiver(int compressionRatio)
        {
            if (compressionRatio >= 9 && compressionRatio <= 30)
            {
                CompressionRatio = compressionRatio;
            }
            else
            {
                Console.WriteLine("CompressionRatio must be between 9 and 30 (including borders). The default value (16) was set.");
            }
        }

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
            List<byte> decompressedBytes = new List<byte>();
            Dictionary<int, List<byte>> codes = new Dictionary<int, List<byte>>();
            int alphabetPower = 256;
            for (int i = 0; i < 256; i++)
            {
                List<byte> code = new List<byte>();
                code.Add((byte)i);
                codes.Add(i, code);
            }

            int dataLength = (int)archiveData[0] + (int)archiveData[1] * 256 +
                (int)archiveData[2] * 256 * 256 + (int)archiveData[3] * 256 * 256 * 256;
 
            int bitsForCode = 9;
            byte currentByte = archiveData[4];
            int index = 5;
            byte bitNumber = 1;
            List<byte>? last = null;
            for (int i = 0; i < dataLength; i++)
            {
                int code = 0;
                int localBitNumber = 1;
                for (int j = 0; j < bitsForCode; j++)
                {
                    code += ((currentByte & bitNumber) > 0 ? 1 : 0) * localBitNumber;
                    localBitNumber *= 2;
                    if (bitNumber >= 128)
                    {
                        if (index >= archiveData.Length) break;
                        currentByte = archiveData[index];
                        index++;
                        bitNumber = 1;
                    }
                    else
                    {
                        bitNumber <<= 1;
                    }
                }

                if (codes.ContainsKey(code))
                {
                    for (int k = 0; k < codes[code].Count; k++)
                    {
                        decompressedBytes.Add(codes[code][k]);
                    }

                    if (last == null)
                    {
                        last = new List<byte>();
                        foreach (byte s in codes[code])
                        {
                            last.Add(s);
                        }
                    }
                    else
                    {
                        last.Add(codes[code][0]);
                        codes[alphabetPower] = last;
                        last = new List<byte>();
                        foreach (byte s in codes[code])
                        {
                            last.Add(s);
                        }

                        alphabetPower++;
                    }
                }
                else
                {
                    last.Add(last[0]);
                    codes[code] = new List<byte>();
                    foreach (byte s in last)
                    {
                        codes[code].Add(s);
                    }

                    alphabetPower++;

                    for (int k = 0; k < codes[code].Count; k++)
                    {
                        decompressedBytes.Add(codes[code][k]);
                    }
                }

                if (alphabetPower + 1 == (int)Math.Pow(2, bitsForCode))
                {
                    bitsForCode++;
                    if (bitsForCode > CompressionRatio)
                    {
                        bitsForCode = 9;
                        last = null;
                        codes = new Dictionary<int, List<byte>>();
                        alphabetPower = 256;
                        for (int t = 0; t < 256; t++)
                        {
                            List<byte> startCode = new List<byte>();
                            startCode.Add((byte)t);
                            codes.Add(t, startCode);
                        }
                    }
                }

            }

            return decompressedBytes.ToArray();
        }

        private byte[] CompressBytes(byte[] fileData)
        {
            List<byte> compressedBytes = new List<byte>();
            Node alphabet = new Node(0);
            int alphabetPower = 256;
            for (int i = 0; i < 256; i++)
            {
                alphabet.Next.Add(i, new Node(i));
            }

            int index = 0;
            byte sum = 0;
            byte bitNumber = 1;
            int bitsForCode = 9;
            int count = 0;
            while (index < fileData.Length)
            {
                int length = 0;
                Node current = alphabet;
                while (index + length < fileData.Length && current.Next.ContainsKey(fileData[index + length]))
                {
                    current = current.Next[fileData[index + length]];
                    length++;
                }

                int code = current.Code;
                int added = 0;
                while (code != 0 || added < bitsForCode)
                {
                    sum += (byte)((code % 2) * bitNumber);
                    added++;
                    code /= 2;
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

                index += length;
                if (index < fileData.Length)
                {
                    current.Next.Add(fileData[index], new Node(alphabetPower));
                    alphabetPower++;
                    if (alphabetPower == (int)Math.Pow(2, bitsForCode))
                    {
                        bitsForCode++;
                        if (bitsForCode > CompressionRatio)
                        {
                            bitsForCode = 9;
                            alphabet = new Node(0);
                            alphabetPower = 256;
                            for (int i = 0; i < 256; i++)
                            {
                                alphabet.Next.Add(i, new Node(i));
                            }
                        }
                    }
                }

                count++;
            }

            List<byte> header = new List<byte>();
            header.Add((byte)(count & 255));
            header.Add((byte)((count >> 8) & 255));
            header.Add((byte)((count >> 16) & 255));
            header.Add((byte)((count >> 24) & 255));

            return header.Concat(compressedBytes).ToArray();
        }
    }
}

