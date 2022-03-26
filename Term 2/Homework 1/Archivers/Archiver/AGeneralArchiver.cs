using System.Text;

namespace Archiver
{
    public abstract class AGeneralArchiver
    {
        /// <summary>
        /// Compresses the file using the Huffman method.
        /// </summary>
        /// <param name="dataFileName">Path to the input file.</param>
        /// <param name="archiveName">Path to the output file (archive).</param>
        public void CompressFile(string dataFileName, string archiveName)
        {
            byte[] fileData = File.ReadAllBytes(dataFileName);
            byte[] archiveData = CompressBytes(fileData);
            File.WriteAllBytes(archiveName, archiveData);
        }

        /// <summary>
        /// Decompresses the archive obtained using the Huffman method.
        /// </summary>
        /// <param name="archiveName">Path to the input file (archive).</param>
        /// <param name="dataFileName">Path to the output file.</param>
        /// <param name="text"></param>
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

        protected abstract byte[] CompressBytes(byte[] fileData);

        protected abstract byte[] DecompressBytes(byte[] archiveData);
    }
}