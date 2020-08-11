using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cl_cs_utils
{
    public class CapturedError
    {
        string message = null;
        object [] code = null;

        public CapturedError()
        {
            Category = "";  
        }

        public CapturedError( string category )
        {
            Category = category;  
        }

        public CapturedError OK()   // For returning when a function has not errored
        {
            return this;
        }

        public CapturedError ErrorMessage( string format )
        {
            message = format;
            return this;
        }

        public CapturedError ErrorMessage( string format, params object[] args )
        {
            message = String.Format( format, args );
            return this;
        }

        public CapturedError ErrorCode( params object [] code )
        {
            this.code = code;
            return this;
        }

        public bool IsOK { get { return message is null && code is null; } }
        public bool IsErrored { get { return ! IsOK; } }

        public string Category { get; private set; }

        public string Message { get { return message ?? ""; } private set { message = value; } }

        public bool HasCode( params object [] soughtCode )
        {
            if( code is null || code.Length < soughtCode.Length )
                return false;
            return CheckCodeElements( soughtCode );
        }

        public bool IsCode( params object [] soughtCode )
        {
            if( code is null || code.Length != soughtCode.Length )
                return false;
            return CheckCodeElements( soughtCode );
        }

        bool CheckCodeElements( params object [] soughtCode )
        {
            // Condition to be met by calling method
            System.Diagnostics.Debug.Assert( code is object && code.Length >= soughtCode.Length );

            for( int i = 0; i < soughtCode.Length; ++i )
                if( ! Object.ReferenceEquals( code[i].GetType(), soughtCode[i].GetType()) ||
                        code[i].ToString() != soughtCode[i].ToString() )
                    return false;
            return true;
        }

        public object CodeAt( int index )
        {
            if( code is null || index < 0 || index >= code.Length )
                return null;
            return code[index];
        }
    }
}
