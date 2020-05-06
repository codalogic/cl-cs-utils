# cl-cs-utils

C# utility code

## ChunkedUTF8ToSubString

Allow blocks of bytes representing UTF-8 characters to be decoded into strings.

An issue arises when converting blocks of UTF-8 encoded bytes into strings that
a break in a read block of data may be mid way through a UTF-8 multi-byte sequence.
This class Encoding.UTF8.GetDecoder() to store any trailing partial UTF-8 sequence
and then save those bytes for the next call to the Tranform() function.

Use as:

```c#
    var utf8ToString = new ChunkedUTF8ToSubString();
    var utf8 = new byte[10];
    var str = "";
    int count;

    while( (count = stream.Read( utf8, 0, 2 )) != 0 )
    {
        str += utf8ToString.Transform( utf8, count );
    }
```
