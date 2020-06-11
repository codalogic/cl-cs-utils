using Microsoft.VisualStudio.TestTools.UnitTesting;
using cl_cs_utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cl_cs_utils.Tests
{
    [TestClass()]
    public class CodalogicExceptionTests
    {
        [TestMethod()]
        public void CodalogicExceptionBasicExceptionTest()
        {
            var e = new CodalogicException( CodalogicException.NullError, "It went wrong" );

            Assert.IsTrue( e.Id == CodalogicException.NullError );
            Assert.IsTrue( e.Message == "It went wrong" );
            Assert.IsTrue( e.ToString() == "It went wrong" );
        }

        [TestMethod()]
        public void CodalogicExceptionWithSingleParameterTest()
        {
            var e = new CodalogicException( CodalogicException.NullError, "It went wrong" ).With( "File", "c:/home.txt" );

            Assert.IsTrue( e.Message == "It went wrong" );
            Assert.IsTrue( e.ToString() == "It went wrong... File: c:/home.txt" );
        }

        [TestMethod()]
        public void CodalogicExceptionWithTwoParametersTest()
        {
            var e = new CodalogicException( CodalogicException.NullError, "It went wrong" ).With( "File", "c:/home.txt" ).With( "Next", "Fred" );

            Assert.IsTrue( e.Message == "It went wrong" );
            Assert.IsTrue( e.ToString() == "It went wrong... File: c:/home.txt; Next: Fred" || e.ToString() == "It went wrong.. Next: Fred; File: c:/home.txt" );
        }

        [TestMethod()]
        public void CodalogicExceptionWithNumberParameterTest()
        {
            var e = new CodalogicException( CodalogicException.NullError, "It went wrong" ).With( "Count", 5 );

            Assert.IsTrue( e.Message == "It went wrong" );
            Assert.IsTrue( e.ToString() == "It went wrong... Count: 5" );
        }

        [TestMethod()]
        public void CodalogicExceptionPresentDataAccessTest()
        {
            var e = new CodalogicException( CodalogicException.NullError, "It went wrong" ).With( "File", "c:/home.txt" ).With( "Next", "Fred" );

            Assert.IsTrue( e.Data.Count == 2 );
            Assert.IsTrue( (string)e.Data["File"] == "c:/home.txt" );
        }

        [TestMethod()]
        public void CodalogicExceptionIndexParameterWithStringTest()
        {
            var e = new CodalogicException( CodalogicException.NullError, "It went wrong" ).With( "Next", "Fred" );

            Assert.IsTrue( e["Next"] == "Fred" );
        }

        [TestMethod()]
        public void CodalogicExceptionIndexParameterWithNumberTest()
        {
            var e = new CodalogicException( CodalogicException.NullError, "It went wrong" ).With( "Count", 5 );

            Assert.IsTrue( e["Count"] == "5" );
        }

        [TestMethod()]
        public void CodalogicExceptionIndexParameterWithUnspecifiedValueTest()
        {
            var e = new CodalogicException( CodalogicException.NullError, "It went wrong" ).With( "Next", "Fred" );

            Assert.IsTrue( e["Other"] == "<not-set>" );
        }

        [TestMethod()]
        public void CodalogicExceptionIsNamespaceTest()
        {
            var e = new CodalogicException( CodalogicException.NullError, "It went wrong" ).With( "Next", "Fred" );

            Assert.IsTrue( e.IsNamespace( CodalogicException.ExceptionNamespace ) );
        }

        [TestMethod()]
        public void CodalogicExceptionIsClassTest()
        {
            var e = new CodalogicException( CodalogicException.NullError, "It went wrong" ).With( "Next", "Fred" );

            Assert.IsTrue( e.IsClass( CodalogicException.ExceptionClass ) );
        }
    }
}
