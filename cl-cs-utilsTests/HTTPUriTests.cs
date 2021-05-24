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
    public class HTTPUriTests
    {
        [TestMethod()]
        public void EmptyUriTest()
        {
            Assert.IsTrue( new HTTPUri().GetUri() == "http://" );   // A bogus case
        }

        [TestMethod()]
        public void HostOnlyUriTest()
        {
            Assert.IsTrue( new HTTPUri( "codalogic.com" ).GetUri() == "http://codalogic.com" );
        }

        [TestMethod()]
        public void HostAndPlainSchemeUriTest()
        {
            Assert.IsTrue( new HTTPUri( "http://codalogic.com" ).GetUri() == "http://codalogic.com" );
        }

        [TestMethod()]
        public void HostAndSecureSchemeUriTest()
        {
            Assert.IsTrue( new HTTPUri( "https://codalogic.com" ).GetUri() == "https://codalogic.com" );
        }

        [TestMethod()]
        public void HostAndResourceUriTest()
        {
            Assert.IsTrue( new HTTPUri( "codalogic.com", "foo" ).GetUri() == "http://codalogic.com/foo" );
        }

        [TestMethod()]
        public void HostResourceAndQueryUriTest()
        {
            Assert.IsTrue( new HTTPUri( "codalogic.com", "foo", "myquery" ).GetUri() == "http://codalogic.com/foo?myquery" );
        }

        [TestMethod()]
        public void WithHostUriTest()
        {
            Assert.IsTrue( new HTTPUri().WithHost( "codalogic.com" ).GetUri() == "http://codalogic.com" );
        }

        [TestMethod()]
        public void WithHostWithPlainSchemeUriTest()
        {
            Assert.IsTrue( new HTTPUri().WithScheme( HTTPUri.Scheme.Plain ).WithHost( "codalogic.com" ).GetUri() == "http://codalogic.com" );
        }

        [TestMethod()]
        public void WithHostWithSecureSchemeUriTest()
        {
            Assert.IsTrue( new HTTPUri().WithScheme( HTTPUri.Scheme.Secure ).WithHost( "codalogic.com" ).GetUri() == "https://codalogic.com" );
        }

        [TestMethod()]
        public void HostWithResourceUriTest()
        {
            Assert.IsTrue( new HTTPUri( "codalogic.com" ).WithResource( "foo" ).GetUri() == "http://codalogic.com/foo" );
        }

        [TestMethod()]
        public void HostWithQueryUriTest()
        {
            Assert.IsTrue( new HTTPUri( "codalogic.com" ).WithQuery( "myquery" ).GetUri() == "http://codalogic.com?myquery" );
        }

        [TestMethod()]
        public void HostWithResourceWithQueryUriTest()
        {
            Assert.IsTrue( new HTTPUri( "codalogic.com" ).WithResource( "foo" ).WithQuery( "myquery" ).GetUri() == "http://codalogic.com/foo?myquery" );
        }

        [TestMethod()]
        public void HostWithResourceWithQueryStringUriTest()
        {
            Assert.IsTrue( new HTTPUri( "codalogic.com" ).WithResource( "foo" ).WithQueryString( "myquery" ).GetUri() == "http://codalogic.com/foo?myquery" );
        }

        [TestMethod()]
        public void HostWithResourceWithQueryParameterOneParamUriTest()
        {
            Assert.IsTrue( new HTTPUri( "codalogic.com" ).WithResource( "foo" ).WithQueryParameter( "email", "test@example.com" ).GetUri() ==
                            "http://codalogic.com/foo?email=test%40example.com" );
        }

        [TestMethod()]
        public void HostWithResourceWithQueryParameterTwoParamaUriTest()
        {
            Assert.IsTrue( new HTTPUri( "codalogic.com" ).WithResource( "foo" ).
                    WithQueryParameter( "email", "test@example.com" ).WithQueryParameter( "status", "this & that" ).GetUri() ==
                            "http://codalogic.com/foo?email=test%40example.com&status=this+%26+that" );
        }

        [TestMethod()]
        public void HostWithSecureSchemeWithResourceWithQueryUriTest()
        {
            Assert.IsTrue( new HTTPUri( "codalogic.com" ).WithScheme( HTTPUri.Scheme.Secure ).WithResource( "foo" ).WithQuery( "myquery" ).GetUri() == "https://codalogic.com/foo?myquery" );
        }
    }
}
