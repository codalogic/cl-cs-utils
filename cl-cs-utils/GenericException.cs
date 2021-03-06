﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/*
 * Class: GenericException
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
 *
 * To define an exension of this class, do similar to:
 *
 *    class NoFile : GenericException { public NoFile( string id, string message ) : base( id, message ) {} }
 *
 * Or:
 *
 *    public static readonly string NoFileError = $"NoFileError.{ExceptionClass}";
 *
 *    class NoFile : GenericException { public NoFile() : base( NoFileError, "No file found" ) {} }
 */

namespace cl_cs_utils
{
    public class EndProgramException : GenericException
    {
        new public static readonly string ExceptionNamespace = "cl_cs_utils.codalogic.com";
        new public static readonly string ExceptionClass = $"EndProgramException.{ExceptionNamespace}";

        public static readonly string EndProgram = $"EndProgram.{ExceptionClass}";

        public EndProgramException() : this( "End Program invoked" ) {}
        public EndProgramException( string message ) : base( EndProgram, message ) {}
    }

    public class EndTaskException : GenericException
    {
        new public static readonly string ExceptionNamespace = "cl_cs_utils.codalogic.com";
        new public static readonly string ExceptionClass = $"EndTaskException.{ExceptionNamespace}";

        public static readonly string EndTask = $"EndTask.{ExceptionClass}";

        public EndTaskException() : this( "End Task invoked" ) {}
        public EndTaskException( string message ) : base( EndTask, message ) {}
    }

    public class GenericException : Exception
    {
        public static readonly string ExceptionNamespace = "cl_cs_utils.codalogic.com";
        public static readonly string ExceptionClass = $"GenericException.{ExceptionNamespace}";

        public static readonly string NullError = $"null-error.{ExceptionClass}";

        public GenericException( string id, string message ) : base( message )
        {
            Id = id;
        }

        public GenericException With<T>( string key, T value )
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

        /*
         * These helper methods allow a simple check of a boolean value and throwing an exception if the value is False.
         *
         * The code should look something like this...
         *
         * In a files "using" section do:
         *
         *    using static cl_cs_utils.GenericException;
         *
         * Then to check a value do similar to:
         *
         *    CheckThat( <test term that should be True> || Throw( new GenericException( GenericException.NullError, "It went wrong" ) );
         *
         * The techniques makes use of short-circuit operations to make sure
         * the exception object is not created unless it is needed.
         */
        static public void CheckThat( bool b ) {}
        static public bool Throw( Exception e ) { throw e; }
    }
}
