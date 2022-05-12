using NUnit.Framework;
using System.IO;

namespace HuffmanArchiver.UnitTests
{
    public class Tests
    {
        string workingDirectory = "../../../../Archiver/";

        [Test]
        public void TextFileTest()
        {
            HuffmanArchiver archiver = new HuffmanArchiver();
            archiver.CompressFile(workingDirectory + "TestFiles/test1.txt", workingDirectory + "TestResults/test1.txt.huffa");
            archiver.DecompressFile(workingDirectory + "TestResults/test1.txt.huffa", workingDirectory + "TestResults/test1_out.txt");
            FileAssert.AreEqual(workingDirectory + "TestFiles/test1.txt", workingDirectory + "TestResults/test1_out.txt");
            File.Delete(workingDirectory + "TestResults/test1.txt.huffa");
            File.Delete(workingDirectory + "TestResults/test1_out.txt");
        }

        [Test]
        public void ImageFileTest()
        {
            HuffmanArchiver archiver = new HuffmanArchiver();
            archiver.CompressFile(workingDirectory + "TestFiles/test2.bmp", workingDirectory + "TestResults/test2.bmp.huffa");
            archiver.DecompressFile(workingDirectory + "TestResults/test2.bmp.huffa", workingDirectory + "TestResults/test2_out.bmp", false);
            FileAssert.AreEqual(workingDirectory + "TestFiles/test2.bmp", workingDirectory + "TestResults/test2_out.bmp");
            File.Delete(workingDirectory + "TestResults/test2.bmp.huffa");
            File.Delete(workingDirectory + "TestResults/test2_out.bmp");
        }

        [Test]
        public void AudioFileTest()
        {
            HuffmanArchiver archiver = new HuffmanArchiver();
            archiver.CompressFile(workingDirectory + "TestFiles/test3.mp3", workingDirectory + "TestResults/test3.mp3.huffa");
            archiver.DecompressFile(workingDirectory + "TestResults/test3.mp3.huffa", workingDirectory + "TestResults/test3_out.mp3", false);
            FileAssert.AreEqual(workingDirectory + "TestFiles/test3.mp3", workingDirectory + "TestResults/test3_out.mp3");
            File.Delete(workingDirectory + "TestResults/test3.mp3.huffa");
            File.Delete(workingDirectory + "TestResults/test3_out.mp3");
        }
    }
}
