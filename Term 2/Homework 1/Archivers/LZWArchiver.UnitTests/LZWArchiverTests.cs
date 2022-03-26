using NUnit.Framework;
using System.IO;

namespace LZWArchiver.UnitTests
{
    public class Tests
    {
        string workingDirectory = "../../../../Archiver/";

        [Test]
        public void TextFileTest()
        {
            LZWArchiver archiver = new LZWArchiver(16);
            archiver.CompressFile(workingDirectory + "TestFiles/test1.txt", workingDirectory + "TestResults/test1_16.txt.lzw");
            archiver.DecompressFile(workingDirectory + "TestResults/test1_16.txt.lzw", workingDirectory + "TestResults/test1_16_out.txt");
            FileAssert.AreEqual(workingDirectory + "TestFiles/test1.txt", workingDirectory + "TestResults/test1_16_out.txt");
            File.Delete(workingDirectory + "TestResults/test1_16.txt.lzw");
            File.Delete(workingDirectory + "TestResults/test1_16_out.txt");
        }

        [Test]
        public void ImageFileTest()
        {
            LZWArchiver archiver = new LZWArchiver(16);
            archiver.CompressFile(workingDirectory + "TestFiles/test2.bmp", workingDirectory + "TestResults/test2_16.bmp.lzw");
            archiver.DecompressFile(workingDirectory + "TestResults/test2_16.bmp.lzw", workingDirectory + "TestResults/test2_16_out.bmp", false);
            FileAssert.AreEqual(workingDirectory + "TestFiles/test2.bmp", workingDirectory + "TestResults/test2_16_out.bmp");
            File.Delete(workingDirectory + "TestResults/test2_16.bmp.lzw");
            File.Delete(workingDirectory + "TestResults/test2_16_out.bmp");
        }

        [Test]
        public void AudioFileTest()
        {
            LZWArchiver archiver = new LZWArchiver(16);
            archiver.CompressFile(workingDirectory + "TestFiles/test3.mp3", workingDirectory + "TestResults/test3_16.mp3.lzw");
            archiver.DecompressFile(workingDirectory + "TestResults/test3_16.mp3.lzw", workingDirectory + "TestResults/test3_16_out.mp3", false);
            FileAssert.AreEqual(workingDirectory + "TestFiles/test3.mp3", workingDirectory + "TestResults/test3_16_out.mp3");
            File.Delete(workingDirectory + "TestResults/test3_16.mp3.lzw");
            File.Delete(workingDirectory + "TestResults/test3_16_out.mp3");
        }

        [Test]
        public void TextFileWithOtherCompressionRatioTest()
        {
            LZWArchiver archiver = new LZWArchiver(20);
            archiver.CompressFile(workingDirectory + "TestFiles/test1.txt", workingDirectory + "TestResults/test1_20.txt.lzw");
            archiver.DecompressFile(workingDirectory + "TestResults/test1_20.txt.lzw", workingDirectory + "TestResults/test1_20_out.txt");
            FileAssert.AreEqual(workingDirectory + "TestFiles/test1.txt", workingDirectory + "TestResults/test1_20_out.txt");
            File.Delete(workingDirectory + "TestResults/test1_20.txt.lzw");
            File.Delete(workingDirectory + "TestResults/test1_20_out.txt");
        }

        [Test]
        public void ImageFileWithOtherCompressionRatioTest()
        {
            LZWArchiver archiver = new LZWArchiver(20);
            archiver.CompressFile(workingDirectory + "TestFiles/test2.bmp", workingDirectory + "TestResults/test2_20.bmp.lzw");
            archiver.DecompressFile(workingDirectory + "TestResults/test2_20.bmp.lzw", workingDirectory + "TestResults/test2_20_out.bmp", false);
            FileAssert.AreEqual(workingDirectory + "TestFiles/test2.bmp", workingDirectory + "TestResults/test2_20_out.bmp");
            File.Delete(workingDirectory + "TestResults/test2_20.bmp.lzw");
            File.Delete(workingDirectory + "TestResults/test2_20_out.bmp");
        }

        [Test]
        public void AudioFileWithOtherCompressionRatioTest()
        {
            LZWArchiver archiver = new LZWArchiver(20);
            archiver.CompressFile(workingDirectory + "TestFiles/test3.mp3", workingDirectory + "TestResults/test3_20.mp3.lzw");
            archiver.DecompressFile(workingDirectory + "TestResults/test3_20.mp3.lzw", workingDirectory + "TestResults/test3_20_out.mp3", false);
            FileAssert.AreEqual(workingDirectory + "TestFiles/test3.mp3", workingDirectory + "TestResults/test3_20_out.mp3");
            File.Delete(workingDirectory + "TestResults/test3_20.mp3.lzw");
            File.Delete(workingDirectory + "TestResults/test3_20_out.mp3");
        }

        [Test]
        public void ImageFileManyCompressionRatioValuesTest()
        {
            for (int i = 12; i < 18; i++)
            {
                LZWArchiver archiver = new LZWArchiver(i);
                archiver.CompressFile(workingDirectory + "TestFiles/test2.bmp", $"{workingDirectory}TestResults/test2_{i}.bmp.lzw");
                archiver.DecompressFile($"{workingDirectory}TestResults/test2_{i}.bmp.lzw", $"{workingDirectory}TestResults/test2_{i}_out.bmp", false);
                FileAssert.AreEqual(workingDirectory + "TestFiles/test2.bmp", $"{workingDirectory}TestResults/test2_{i}_out.bmp");
                File.Delete($"{workingDirectory}TestResults/test2_{i}.bmp.lzw");
                File.Delete($"{workingDirectory}TestResults/test2_{i}_out.bmp");
            }
        }
    }
}
