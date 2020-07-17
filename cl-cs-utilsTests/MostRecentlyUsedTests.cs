using Microsoft.VisualStudio.TestTools.UnitTesting;
using cl_cs_utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cl_cs_utils.Tests
{
    [TestClass()]
    public class MostRecentlyUsedTests
    {
        const string MRUFile = "MRU-Test.txt";

        [TestMethod()]
        public void MostRecentlyUsedTest()
        {
            File.Delete( MRUFile );

            var mru = new MostRecentlyUsed( MRUFile );

            Assert.AreEqual( 0, mru.Count );

            mru.Update( "A" );
            Assert.AreEqual( 1, mru.Count );
            Assert.AreEqual( "A", mru[0] );
 
            mru.Update( "B" );
            Assert.AreEqual( 2, mru.Count );
            Assert.AreEqual( "B", mru[0] );
            Assert.AreEqual( "A", mru[1] );
 
            mru.Update( "C" );
            Assert.AreEqual( 3, mru.Count );
            Assert.AreEqual( "C", mru[0] );
            Assert.AreEqual( "B", mru[1] );
            Assert.AreEqual( "A", mru[2] );
 
            mru.Update( "B" );
            Assert.AreEqual( 3, mru.Count );
            Assert.AreEqual( "B", mru[0] );
            Assert.AreEqual( "C", mru[1] );
            Assert.AreEqual( "A", mru[2] );

            var mru2 = new MostRecentlyUsed( MRUFile );
            Assert.AreEqual( 3, mru2.Count );
            Assert.AreEqual( "B", mru2[0] );
            Assert.AreEqual( "C", mru2[1] );
            Assert.AreEqual( "A", mru2[2] );

            var vals = new List<string>();
            foreach( string s in mru2 )
                vals.Add( s );
            Assert.AreEqual( 3, vals.Count );
            Assert.AreEqual( "B", vals[0] );
            Assert.AreEqual( "C", vals[1] );
            Assert.AreEqual( "A", vals[2] );
        }
    }
}
