using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static cl_cs_utils.GenericException;

namespace cl_cs_utils.Tests
{
    [TestClass()]
    public class GenericExceptionTests
    {
        [TestMethod()]
        public void GenericExceptionBasicExceptionTest()
        {
            var e = new GenericException( GenericException.NullError, "It went wrong" );

            Assert.IsTrue( e.Id == GenericException.NullError );
            Assert.IsTrue( e.Message == "It went wrong" );
            Assert.IsTrue( e.ToString() == "It went wrong" );
        }

        [TestMethod()]
        public void GenericExceptionWithSingleParameterTest()
        {
            var e = new GenericException( GenericException.NullError, "It went wrong" ).With( "File", "c:/home.txt" );

            Assert.IsTrue( e.Message == "It went wrong" );
            Assert.IsTrue( e.ToString() == "It went wrong... File: c:/home.txt" );
        }

        [TestMethod()]
        public void GenericExceptionWithTwoParametersTest()
        {
            var e = new GenericException( GenericException.NullError, "It went wrong" ).With( "File", "c:/home.txt" ).With( "Next", "Fred" );

            Assert.IsTrue( e.Message == "It went wrong" );
            Assert.IsTrue( e.ToString() == "It went wrong... File: c:/home.txt; Next: Fred" || e.ToString() == "It went wrong.. Next: Fred; File: c:/home.txt" );
        }

        [TestMethod()]
        public void GenericExceptionWithNumberParameterTest()
        {
            var e = new GenericException( GenericException.NullError, "It went wrong" ).With( "Count", 5 );

            Assert.IsTrue( e.Message == "It went wrong" );
            Assert.IsTrue( e.ToString() == "It went wrong... Count: 5" );
        }

        [TestMethod()]
        public void GenericExceptionPresentDataAccessTest()
        {
            var e = new GenericException( GenericException.NullError, "It went wrong" ).With( "File", "c:/home.txt" ).With( "Next", "Fred" );

            Assert.IsTrue( e.Data.Count == 2 );
            Assert.IsTrue( (string)e.Data["File"] == "c:/home.txt" );
        }

        [TestMethod()]
        public void GenericExceptionIndexParameterWithStringTest()
        {
            var e = new GenericException( GenericException.NullError, "It went wrong" ).With( "Next", "Fred" );

            Assert.IsTrue( e["Next"] == "Fred" );
        }

        [TestMethod()]
        public void GenericExceptionIndexParameterWithNumberTest()
        {
            var e = new GenericException( GenericException.NullError, "It went wrong" ).With( "Count", 5 );

            Assert.IsTrue( e["Count"] == "5" );
        }

        [TestMethod()]
        public void GenericExceptionIndexParameterWithUnspecifiedValueTest()
        {
            var e = new GenericException( GenericException.NullError, "It went wrong" ).With( "Next", "Fred" );

            Assert.IsTrue( e["Other"] == "<not-set>" );
        }

        [TestMethod()]
        public void GenericExceptionIsNamespaceTest()
        {
            var e = new GenericException( GenericException.NullError, "It went wrong" ).With( "Next", "Fred" );

            Assert.IsTrue( e.IsNamespace( GenericException.ExceptionNamespace ) );
        }

        [TestMethod()]
        public void GenericExceptionIsClassTest()
        {
            var e = new GenericException( GenericException.NullError, "It went wrong" ).With( "Next", "Fred" );

            Assert.IsTrue( e.IsClass( GenericException.ExceptionClass ) );
        }

        class NoFile : GenericException { public NoFile() : base( GenericException.NullError, "It went wrong" ) {} }

        [TestMethod()]
        public void GenericExceptionExtendedExceptionTest()
        {
            try
            {
                Exception e = new NoFile().With( "Next", "Fred" );
                throw e;
                //Assert.Fail();
            }

            catch( NoFile e )
            {
                Assert.IsTrue( e.Id == GenericException.NullError );
                Assert.IsTrue( e["Next"] == "Fred" );
            }
            catch( Exception )
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GenericExceptionCheckThatAndThrowTest()
        {
            try
            {
                CheckThat( 0 == 1 || Throw( new NoFile().With( "Next", "Fred" ) ));
                Assert.Fail();
            }

            catch( GenericException e )
            {
                Assert.IsTrue( e.Id == GenericException.NullError );
                Assert.IsTrue( e["Next"] == "Fred" );
            }
            catch( Exception )
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void EndProgramExceptionBasicExceptionTest()
        {
            var e = new EndProgramException();

            Assert.IsTrue( e.Id == EndProgramException.EndProgram );
            Assert.IsTrue( e.Message == "End Program invoked" );
            Assert.IsTrue( e.ToString() == "End Program invoked" );
        }

        [TestMethod()]
        public void EndProgramExceptionWithMessageExceptionTest()
        {
            var e = new EndProgramException( "It went wrong" );

            Assert.IsTrue( e.Id == EndProgramException.EndProgram );
            Assert.IsTrue( e.Message == "It went wrong" );
            Assert.IsTrue( e.ToString() == "It went wrong" );
        }

        [TestMethod()]
        public void EndTaskExceptionBasicExceptionTest()
        {
            var e = new EndTaskException();

            Assert.IsTrue( e.Id == EndTaskException.EndTask );
            Assert.IsTrue( e.Message == "End Task invoked" );
            Assert.IsTrue( e.ToString() == "End Task invoked" );
        }

        [TestMethod()]
        public void EndTaskExceptionWithMessageExceptionTest()
        {
            var e = new EndTaskException( "It went wrong" );

            Assert.IsTrue( e.Id == EndTaskException.EndTask );
            Assert.IsTrue( e.Message == "It went wrong" );
            Assert.IsTrue( e.ToString() == "It went wrong" );
        }
    }
}
