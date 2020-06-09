/*
 * Class: cl_cs_utils.HTTPUri
 *
 * A simple class for building uris for use in HTTP classes.
*/

namespace cl_cs_utils
{
    public class HTTPUri
    {
        public enum Scheme { Plain, Secure };

        Scheme scheme = Scheme.Plain;
        string host = "";
        string resource = "";
        string query = "";

        public HTTPUri()
        {
        }

        public HTTPUri( string host )
        {
            this.host = host;
        }

        public HTTPUri( string host, string resource )
        {
            this.host = host;
            this.resource = resource;
        }

        public HTTPUri( string host, string resource, string query )
        {
            this.host = host;
            this.resource = resource;
            this.query = query;
        }

        public HTTPUri WithScheme( Scheme scheme )
        {
            this.scheme = scheme;
            return this;
        }

        public HTTPUri WithHost( string host )
        {
            this.host = host;
            return this;
        }

        public HTTPUri WithResource( string resource )
        {
            this.resource = resource;
            return this;
        }

        public HTTPUri WithQuery( string query )
        {
            this.query = query;
            return this;
        }

        public string GetUri()
        {
            string uri = host;

            if( ! uri.StartsWith( "http://") && ! uri.StartsWith( "https://" ) )
            {
                if( scheme == Scheme.Secure )
                    uri = "https://" + uri;
                else
                    uri = "http://" + uri;
            }
            if( resource != "" )
                uri += "/" + resource;
            if( query != "" )
                uri += "?" + query;
            return uri;
        }
    }
}
