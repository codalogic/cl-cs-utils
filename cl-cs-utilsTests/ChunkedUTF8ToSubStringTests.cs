using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cl_cs_utils.Tests
{
    [TestClass()]
    public class ChunkedUTF8ToSubStringTests
    {
        [TestMethod()]
        public void TransformEmptyStringTest()
        {
            var cbtss = new ChunkedUTF8ToSubString();
            Assert.IsTrue( cbtss.Transform( new byte[] {}, 0 ) == "" );
        }

        [TestMethod()]
        public void TransformSimpleASCIIStringTest()
        {
            var cbtss = new ChunkedUTF8ToSubString();
            Assert.IsTrue( cbtss.Transform( new byte[] { (byte)'T', (byte)'e', (byte)'s', (byte)'t' }, 4 ) == "Test" );
        }

        [TestMethod()]
        public void TransformDualSimpleASCIIStringTest()
        {
            var cbtss = new ChunkedUTF8ToSubString();
            var s1 = cbtss.Transform( new byte[] { (byte)'M', (byte)'o', (byte)'r', (byte)'e' }, 4 );
            var s2 = cbtss.Transform( new byte[] { (byte)'T', (byte)'e', (byte)'s', (byte)'t' }, 4 );
            Assert.IsTrue( s1 + s2 == "MoreTest" );
        }

        // UTF-8 C2 A3 is Pound Sterling code point (Unicode U+00A3)
        [TestMethod()]
        public void TransformNonASCIIStringTest()
        {
            var cbtss = new ChunkedUTF8ToSubString();
            var s1 = cbtss.Transform( new byte[] { 0xC2, 0xA3 }, 2 );
            Assert.IsTrue( s1 == "\u00A3" );
        }

        [TestMethod()]
        public void TransformSplitNonASCIIStringTest()
        {
            var cbtss = new ChunkedUTF8ToSubString();
            var s1 = cbtss.Transform( new byte[] { 0xC2 }, 1 );    // Pound Sterling code point
            Assert.IsTrue( s1 == "" );
            var s2 = cbtss.Transform( new byte[] { 0xA3 }, 1 );
            Assert.IsTrue( s2 == "\u00A3" );
        }

        [TestMethod()]
        public void TransformMidSplitNonASCIIStringTest()
        {
            var cbtss = new ChunkedUTF8ToSubString();
            var s1 = cbtss.Transform( new byte[] { (byte)'-', 0xC2 }, 2 );
            Assert.IsTrue( s1 == "-" );
            var s2 = cbtss.Transform( new byte[] { 0xA3, (byte)'-' }, 2 );
            Assert.IsTrue( s2 == "\u00A3-" );
            Assert.IsTrue( s1 + s2 == "-\u00A3-" );
        }
    }
}
