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

## HTTPUri

A simple class for building uris for use in HTTP classes.

## GenericException

A class for passing around generic exceptions.

`Id` identifies the type of exception.  The idea is that the `Id` can be used
in a `switch` statement or similar to drill down on how to handle the error.

`Id` should be of the form:

```
<error name>.<class name>.<project / namespace name>.<domain>
```

e.g.:

```
no-connection.HTTPBulkUploader.cl_cs_utils.codalogic.com
```

Non-reverse domain order is chosen so the differing letters appear at the
front of the string and so make string comparison more efficient.

In a class that makes use of this class, these values would be set up as
`public static readonly string` values, e.g.:

```c#
public static readonly string ExceptionNamespace = "cl_cs_utils.codalogic.com";
public static readonly string ExceptionClass = $"GenericException.{ExceptionNamespace}";

public static readonly string NullError = $"null-error.{ExceptionClass}";
```

To define an exension of this class, do similar to:

```c#
public static readonly string NoFileError = $"NoFileError.{ExceptionClass}";

class NoFile : GenericException { public NoFile() : base( NoFileError, "No file found" ) {} }
```

The `CheckThat( bool b )` and `Throw( Exception e )` static helper methods
allow a simple check of a boolean value and throwing an exception if the value
is False.

The code should look something like the following.

In a files `using` section do:

```c#
using static cl_cs_utils.GenericException;
```

Then to check a value, do similar to:

```c#
CheckThat( <test term that should be True> || Throw( new NoFile().With( "Name", "/dev/null" ) );
```

The technique makes use of the || short-circuit operator to make sure
the exception object is not created unless it is needed.

## MostRecentlyUsed

Keeps track of most recently used strings for use in pull-down list boxes.

```c#
var mru = new MostRecentlyUsed( MRUFile );

Assert.AreEqual( 0, mru.Count );

mru.Update( "A" );
mru.Update( "B" );
Assert.AreEqual( 2, mru.Count );
Assert.AreEqual( "B", mru[0] );
Assert.AreEqual( "A", mru[1] );

foreach( string s in mru )
    ...;
```

## CapturedError

`CapturedError` is a class for recording an error condition for passing up the
function calling to hierachy for display or interpretation.

One usage scenario is for `CapturedError` to be a member of a class that has a
number of methods implementing the functionality. The object scoped instance is
tested at regular locations to test if an error has occurred and can be set
when an error occurs.

A `CapturedError` object can record the error condition as a message
and/or a code.

`CapturedError` can be constructed with a *category* value that can be used
to compartmentalise returned errors when processing.  For example:

```c#
readonly string FileIOErrorCategory = "FileIO";
var ce = CapturedError( FileIOErrorCategory );
```

Then in an error handler:

```c#
if( ce.Category == FileIOErrorCategory )
    ...
```

The `IsOK` property returns true if no message or code has been captured and
false otherwise.

The `IsErrored` property returns true is either a message or code has been
captured.

The message is set using the `ErrorMessage( string format )` and
`ErrorMessage( string format, params object[] args )` methods. The latter
formats the message with the supplied `args`.

The code is set using `ErrorCode( params object [] code )`. This allows
setting a hierachy of error code.

Both can be set at the same time:

```c#
return ce.ErrorMessage( "Too many files" ).ErrorCode( "File", "TooMany" );
```

`HasMessage()` returns true if a message is set and `HasCode()` (no parameters)
returns true if a code has been set.

The message can be extracted using the `Message` property.

The code can by tested by using the
`bool HasCode( params object [] soughtCode )`,
`bool IsCode( params object [] soughtCode )` and
`object CodeAt( int index )` methods.

`IsCode()` returns true if the parameters of `soughtCode` matches the
stored code list.

`HasCode()` returns true if the parameters of `soughtCode` matches the
initial sub-set of the stored code list (or the whole set of stored codes).

`CodeAt()` returns the object at the specified index of the stored code
or `null` if the index is too large.
