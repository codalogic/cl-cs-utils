using System.Text;

/*
 * Allow blocks of bytes representing UTF-8 characters to be decoded into strings.
 *
 * An issue arises when converting blocks of UTF-8 encoded bytes into strings that
 * a break in a read block of data may be mid way through a UTF-8 multi-byte sequence.
 * This class Encoding.UTF8.GetDecoder() to store any trailing partial UTF-8 sequence
 * and then save those bytes for the next call to the Tranform() function.
 *
 * Use as:
        var utf8ToString = new ChunkedUTF8ToSubString();
        var utf8 = new byte[10];
        var str = "";
        int count;

        while( (count = stream.Read( utf8, 0, 2 )) != 0 )
        {
            str += utf8ToString.Transform( utf8, count );
        }
 *
 */

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
