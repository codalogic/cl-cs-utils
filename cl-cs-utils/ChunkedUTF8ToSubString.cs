using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cl_cs_utils
{
    public class ChunkedUTF8ToSubString
    {
        Decoder utf8Decoder = Encoding.UTF8.GetDecoder();   // This allows partial UTF-8 sequences in a block. See https://docs.microsoft.com/en-us/dotnet/api/system.text.utf8encoding.getdecoder?view=netcore-3.1
        char[] chars = null;

        public ChunkedUTF8ToSubString()
        {}

        public string Transform( byte[] utf8, int length )
        {
            if( chars == null || chars.Length < length )
                chars = new char[length];
            int charsDecodedCount = utf8Decoder.GetChars( utf8, 0, length, chars, 0);
            return new string( chars, 0, charsDecodedCount );
        }
    }
}
