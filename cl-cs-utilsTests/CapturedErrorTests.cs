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
    public class CapturedErrorTests
    {
        [TestMethod()]
        public void CapturedErrorOKTest()
        {
            var ce = new CapturedError();
            Assert.IsTrue( ce.IsOK );
            Assert.IsFalse( ce.IsErrored );
            Assert.IsTrue( ce.Category == "" );
            Assert.IsTrue( ce.Message == "" );
        }

        [TestMethod()]
        public void CapturedErrorSettingMessageFlagsErrorTest()
        {
            var ce = new CapturedError();
            ce.ErrorMessage( "It's too late" );
            var s = ce.Message;
            Assert.IsFalse( ce.IsOK );
            Assert.IsTrue( ce.IsErrored );
        }

        [TestMethod()]
        public void CapturedErrorSettingCodeFlagsErrorTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsFalse( ce.IsOK );
            Assert.IsTrue( ce.IsErrored );
        }

        [TestMethod()]
        public void CapturedErrorHasACategoryTest()
        {
            var ce = new CapturedError( "Testing" );
            Assert.IsTrue( ce.IsOK );
            Assert.IsFalse( ce.IsErrored );
            Assert.IsTrue( ce.Category == "Testing" );
        }

        [TestMethod()]
        public void CapturedErrorHasMessageTest()
        {
            var ce = new CapturedError();
            ce.ErrorMessage( "It's too late" );
            Assert.IsTrue( ce.HasMessage() );
        }

        [TestMethod()]
        public void CapturedErrorMessageTest()
        {
            var ce = new CapturedError();
            ce.ErrorMessage( "It's too late" );
            Assert.IsTrue( ce.Message == "It's too late" );
        }

        [TestMethod()]
        public void CapturedErrorMessageWithParamsTest()
        {
            var ce = new CapturedError();
            ce.ErrorMessage( "It's too late {0} and {1}", "p1", 2 );
            Assert.IsTrue( ce.Message == "It's too late p1 and 2" );
        }

        [TestMethod()]
        public void CapturedErrorHasCodeWithNoParametersTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsTrue( ce.HasCode() );
        }

        [TestMethod()]
        public void CapturedErrorHasCodeTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsTrue( ce.HasCode( "Level-1", "Cap" ) );
        }

        [TestMethod()]
        public void CapturedErrorHasCodeSoughtSameLengthTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsTrue( ce.HasCode( "Level-1", "Cap", 12 ) );
        }

        [TestMethod()]
        public void CapturedErrorHasCodeMismatchTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsFalse( ce.HasCode( "Level-1", "Flip" ) );
        }

        [TestMethod()]
        public void CapturedErrorHasCodeSoughtTooLongTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsFalse( ce.HasCode( "Level-1", "Cap", 12, "List" ) );
        }

        [TestMethod()]
        public void CapturedErrorIsCodeTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsTrue( ce.IsCode( "Level-1", "Cap", 12 ) );
        }

        [TestMethod()]
        public void CapturedErrorIsCodeMismatchValueTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsFalse( ce.IsCode( "Level-1", "Cap", 14 ) );
        }

        [TestMethod()]
        public void CapturedErrorIsCodeSoughtTooShortTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsFalse( ce.IsCode( "Level-1", "Cap" ) );
        }

        [TestMethod()]
        public void CapturedErrorIsCodeSoughtTooLongTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsFalse( ce.IsCode( "Level-1", "Cap", 12, "Too long" ) );
        }

        [TestMethod()]
        public void CapturedErrorIsCodeObjectTypeDifferenceTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsFalse( ce.IsCode( "Level-1", "Cap", "12" ) );
        }

        [TestMethod()]
        public void CapturedErrorCodeAtOKTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsTrue( ce.CodeAt( 2 ) is int );
            Assert.IsTrue( (int)ce.CodeAt( 2 ) == 12 );
        }

        [TestMethod()]
        public void CapturedErrorCodeAtIndexTooBigTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsTrue( ce.CodeAt( 3 ) is null );
        }

        [TestMethod()]
        public void CapturedErrorCodeAtIndexIsNegativeTest()
        {
            var ce = new CapturedError();
            ce.ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsTrue( ce.CodeAt( -1 ) is null );
        }

        [TestMethod()]
        public void CapturedErrorSetMessageAndCodeTest()
        {
            var ce = new CapturedError();
            ce.ErrorMessage( "It's too late" ).ErrorCode( "Level-1", "Cap", 12 );
            Assert.IsTrue( ce.Message == "It's too late" );
            Assert.IsTrue( ce.HasCode( "Level-1", "Cap", 12 ) );
        }
    }
}
