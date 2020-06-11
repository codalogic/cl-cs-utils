using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/*
 * Class: CodalogicException
 * 
 * A class for passing around generic exceptions.
 * 
 * Id identifies the type of exception.  It should be of the form:
 * 
 *    <error name>.<class name>.<project / namespace name>.<domain>
 *
 * e.g.:
 * 
 *    no-connection.HTTPBulkUploader.cl_cs_utils.codalogic.com
 * 
 * Non-reverse domain order is chosen so the differing letters appear at the
 * front of the string and so make look-up more efficient.
 * 
 * In a class that makes use of this class, these values would be set up as
 * public static readonly string values.
 */

namespace cl_cs_utils
{
    public class CodalogicException : Exception
    {
        public static readonly string ExceptionNamespace = "cl_cs_utils.codalogic.com";
        public static readonly string ExceptionClass = $"CodalogicException.{ExceptionNamespace}";

        public static readonly string NullError = $"null-error.{ExceptionClass}";

        public CodalogicException( string id, string message ) : base( message )
        {
            Id = id;
        }

        public CodalogicException With<T>( string key, T value )
        {
            Data.Add( key, value );
            return this;
        }

        public string Id { get; private set; }

        public bool IsNamespace( string exceptionNamespace )
        {
            return Id.EndsWith( exceptionNamespace );
        }

        public bool IsClass( string exceptionClass )
        {
            return Id.EndsWith( exceptionClass );
        }

        // public string Message; - Uses value in Exception class

        // public Dictionary< string, string > Data; - Uses value in Exception class

        public string this[ string key ] { get { return Data.Contains( key ) ? Data[key].ToString() : "<not-set>"; } }

        override public string ToString()
        {
            string output = Message;

            if( Data.Count > 0 )
            {
                var parameterString = new List<string>();
                foreach( DictionaryEntry kv in Data )
                    parameterString.Add( kv.Key.ToString() + ": " + kv.Value.ToString() );
                output += "... " + string.Join( "; ", parameterString );
            }

            return output;
        }
    }
}
