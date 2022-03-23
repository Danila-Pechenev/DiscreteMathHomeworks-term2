using NUnit.Framework;

namespace HuffmanArchiver.UnitTests
{
    public class Tests
    {
        [Test]
        public void TextFileTest()
        {
            HuffmanArchiver archiver = new HuffmanArchiver();
            archiver.CompressFile("../../../TestFiles/test1.txt", "../../../TestResults/test1.txt.huffa");
            archiver.DecompressFile("../../../TestResults/test1.txt.huffa", "../../../TestResults/test1_out.txt");
            FileAssert.AreEqual("../../../TestFiles/test1.txt", "../../../TestResults/test1_out.txt");
        }

        [Test]
        public void ImageFileTest()
        {
            HuffmanArchiver archiver = new HuffmanArchiver();
            archiver.CompressFile("../../../TestFiles/test2.bmp", "../../../TestResults/test2.bmp.huffa");
            archiver.DecompressFile("../../../TestResults/test2.bmp.huffa", "../../../TestResults/test2_out.bmp", false);
            FileAssert.AreEqual("../../../TestFiles/test2.bmp", "../../../TestResults/test2_out.bmp");
        }

        [Test]
        public void VideoFileTest()
        {
            HuffmanArchiver archiver = new HuffmanArchiver();
            archiver.CompressFile("../../../TestFiles/test3.mp3", "../../../TestResults/test3.mp3.huffa");
            archiver.DecompressFile("../../../TestResults/test3.mp3.huffa", "../../../TestResults/test3_out.mp3", false);
            FileAssert.AreEqual("../../../TestFiles/test3.mp3", "../../../TestResults/test3_out.mp3");
        }
    }
}
