using NUnit.Framework;

namespace LZWArchiver.UnitTests
{
    public class Tests
    {
        [Test]
        public void TextFileTest()
        {
            LZWArchiver archiver = new LZWArchiver(16);
            archiver.CompressFile("../../../TestFiles/test1.txt", "../../../TestResults/test1_16.txt.lzw");
            archiver.DecompressFile("../../../TestResults/test1_16.txt.lzw", "../../../TestResults/test1_16_out.txt");
            FileAssert.AreEqual("../../../TestFiles/test1.txt", "../../../TestResults/test1_16_out.txt");
        }

        [Test]
        public void ImageFileTest()
        {
            LZWArchiver archiver = new LZWArchiver(16);
            archiver.CompressFile("../../../TestFiles/test2.bmp", "../../../TestResults/test2_16.bmp.lzw");
            archiver.DecompressFile("../../../TestResults/test2_16.bmp.lzw", "../../../TestResults/test2_16_out.bmp", false);
            FileAssert.AreEqual("../../../TestFiles/test2.bmp", "../../../TestResults/test2_16_out.bmp");
        }

        [Test]
        public void VideoFileTest()
        {
            LZWArchiver archiver = new LZWArchiver(16);
            archiver.CompressFile("../../../TestFiles/test3.mp3", "../../../TestResults/test3_16.mp3.lzw");
            archiver.DecompressFile("../../../TestResults/test3_16.mp3.lzw", "../../../TestResults/test3_16_out.mp3", false);
            FileAssert.AreEqual("../../../TestFiles/test3.mp3", "../../../TestResults/test3_16_out.mp3");
        }

        [Test]
        public void TextFileWithOtherCompressionRatioTest()
        {
            LZWArchiver archiver = new LZWArchiver(20);
            archiver.CompressFile("../../../TestFiles/test1.txt", "../../../TestResults/test1_20.txt.lzw");
            archiver.DecompressFile("../../../TestResults/test1_20.txt.lzw", "../../../TestResults/test1_20_out.txt");
            FileAssert.AreEqual("../../../TestFiles/test1.txt", "../../../TestResults/test1_20_out.txt");
        }

        [Test]
        public void ImageFileWithOtherCompressionRatioTest()
        {
            LZWArchiver archiver = new LZWArchiver(20);
            archiver.CompressFile("../../../TestFiles/test2.bmp", "../../../TestResults/test2_20.bmp.lzw");
            archiver.DecompressFile("../../../TestResults/test2_20.bmp.lzw", "../../../TestResults/test2_20_out.bmp", false);
            FileAssert.AreEqual("../../../TestFiles/test2.bmp", "../../../TestResults/test2_20_out.bmp");
        }

        [Test]
        public void VideoFileWithOtherCompressionRatioTest()
        {
            LZWArchiver archiver = new LZWArchiver(20);
            archiver.CompressFile("../../../TestFiles/test3.mp3", "../../../TestResults/test3_20.mp3.lzw");
            archiver.DecompressFile("../../../TestResults/test3_20.mp3.lzw", "../../../TestResults/test3_20_out.mp3", false);
            FileAssert.AreEqual("../../../TestFiles/test3.mp3", "../../../TestResults/test3_20_out.mp3");
        }
    }
}