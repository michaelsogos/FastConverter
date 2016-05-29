# FastConverter
Tiny library to fast convert object into byte array

## Why
.NET framework already expose a similar library to reach the same result, [**BitConverter**](https://msdn.microsoft.com/en-us/library/system.bitconverter%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396), but it is **very slow**.

In situation where the **speed** of **convertion** between **Object to Byte Array (or vice versa)** is a priority, then bitconverter is not good to be used .

## When
This library is very useful when used on scenario like:

* Message Queue 
* Marshaling 
* Binary Serialization
