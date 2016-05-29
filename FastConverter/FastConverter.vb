''' <summary>
''' BigEndian 
''' </summary>
Public Class FastConverter

#Region "To Byte Array"
    Public Shared Function GetBytes(value As Integer) As Byte()
        Dim Bytes = New Byte(3) {}
        Bytes(0) = value And Byte.MaxValue
        Bytes(1) = (value >> 8) And Byte.MaxValue
        Bytes(2) = (value >> 16) And Byte.MaxValue
        Bytes(3) = (value >> 24) And Byte.MaxValue

        Return Bytes
    End Function

    Public Shared Function GetBytes(value As UInteger) As Byte()
        Dim Bytes = New Byte(3) {}
        Bytes(0) = value And Byte.MaxValue
        Bytes(1) = (value >> 8) And Byte.MaxValue
        Bytes(2) = (value >> 16) And Byte.MaxValue
        Bytes(3) = (value >> 24) And Byte.MaxValue

        Return Bytes
    End Function

    Public Shared Function GetBytes(value As Long) As Byte()
        Dim Bytes = New Byte(7) {}
        Bytes(0) = value And Byte.MaxValue
        Bytes(1) = (value >> 8) And Byte.MaxValue
        Bytes(2) = (value >> 16) And Byte.MaxValue
        Bytes(3) = (value >> 24) And Byte.MaxValue
        Bytes(4) = (value >> 32) And Byte.MaxValue
        Bytes(5) = (value >> 40) And Byte.MaxValue
        Bytes(6) = (value >> 48) And Byte.MaxValue
        Bytes(7) = (value >> 56) And Byte.MaxValue

        Return Bytes
    End Function

    Public Shared Function GetBytes(value As ULong) As Byte()
        Dim Bytes = New Byte(7) {}
        Bytes(0) = value And Byte.MaxValue
        Bytes(1) = (value >> 8) And Byte.MaxValue
        Bytes(2) = (value >> 16) And Byte.MaxValue
        Bytes(3) = (value >> 24) And Byte.MaxValue
        Bytes(4) = (value >> 32) And Byte.MaxValue
        Bytes(5) = (value >> 40) And Byte.MaxValue
        Bytes(6) = (value >> 48) And Byte.MaxValue
        Bytes(7) = (value >> 56) And Byte.MaxValue

        Return Bytes
    End Function

    Public Shared Function GetBytes(value As Short) As Byte()
        Dim Bytes = New Byte(1) {}
        Bytes(0) = value And Byte.MaxValue
        Bytes(1) = (value >> 8) And Byte.MaxValue

        Return Bytes
    End Function

    Public Shared Function GetBytes(value As UShort) As Byte()
        Dim Bytes = New Byte(1) {}
        Bytes(0) = value And Byte.MaxValue
        Bytes(1) = (value >> 8) And Byte.MaxValue

        Return Bytes
    End Function

    Public Shared Function GetBytes(value As Boolean) As Byte()
        Dim Bytes = New Byte(0) {}
        Bytes(0) = value And &H01
        Return Bytes
    End Function

    ''' <summary>
    ''' Following IEEE754 specification, float-single precision will be converter in integer representation and then converted in byte array
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function GetBytes(value As Single) As Byte()
        Dim singleToInteger As Integer = 0

        'In case IsNaN 
        If (Single.IsNaN(value)) Then singleToInteger = (0 << 31) Or (&HFF << 23) Or &H1337

        Dim numberSign As Integer = If(value < 0, 1, 0)
        Dim numberAbsolute As Single = Math.Abs(value)
        'In case it is ZERO
        If (numberAbsolute = 0.0) Then singleToInteger = (numberSign << 31) Or (0 << 23) Or 0

        Dim exponent As Integer = Math.Min(Math.Floor(Math.Log(numberAbsolute) / Math.Log(2)), 127)

        If (exponent <= -149) Then 'subnormal - underflow
            numberSign = If(value < 0, 0, 1)
            singleToInteger = (numberSign * 2147483648) + ((exponent + 127) << 23) '2^31 = 2147483648
        Else
            Dim mantissa As Single = numberAbsolute / 2 ^ exponent
            singleToInteger = (numberSign << 31) Or ((exponent + 127) << 23) Or ((mantissa * 8388608) And 8388607) '2^23 = 8388608 = &H‭800000‬ ||| &H7FFFFF = (2^23) - 1 = 8388607
        End If

        Return GetBytes(singleToInteger)
    End Function

    ''' <summary>
    ''' Following IEEE754 specification, float-double precision will be converter in long representation and then converted in byte array
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function GetBytes(value As Double) As Byte()
        Dim doubleToLong As Long = 0

        'In case IsNaN 
        If (Double.IsNaN(value)) Then doubleToLong = (0 << 63) Or (&H7FF << 52) Or &H1337

        Dim numberSign As Long = If(value < 0, 1, 0)
        Dim numberAbsolute As Double = Math.Abs(value)
        'In case it is ZERO
        If (numberAbsolute = 0.0) Then doubleToLong = (numberSign << 63) Or (0 << 52) Or 0

        Dim exponent As Long = Math.Min(Math.Floor(Math.Log(numberAbsolute) / Math.Log(2)), 1023)

        'In case it is infinity
        If (exponent > 1023 OrElse exponent < -1022) Then
            doubleToLong = (numberSign << 63) Or (&H7FF << 52) Or 0
        Else
            Dim mantissa As Double = numberAbsolute / 2 ^ exponent
            doubleToLong = (numberSign << 63) Or ((exponent + 1023) << 52) Or ((mantissa * 4503599627370496) And 4503599627370495) '2^52 = 4503599627370496 = &H‭10000000000000‬ ||| &H‭FFFFFFFFFFFFF‬ = (2^52) - 1 = 4503599627370495
        End If

        Return GetBytes(doubleToLong)
    End Function
#End Region

#Region "From Byte Array"
    Public Shared Function GetInteger(bytes As Byte()) As Integer
        Dim Result As Integer = bytes(0)
        Result += (bytes(1) And Integer.MaxValue) << 8
        Result += (bytes(2) And Integer.MaxValue) << 16
        Result += (bytes(3) And Integer.MaxValue) << 24
        Return Result
    End Function

    Public Shared Function GetUInteger(bytes As Byte()) As UInteger
        Dim Result As UInteger = bytes(0)
        Result += (bytes(1) And UInteger.MaxValue) << 8
        Result += (bytes(2) And UInteger.MaxValue) << 16
        Result += (bytes(3) And UInteger.MaxValue) << 24
        Return Result
    End Function

    Public Shared Function GetLong(bytes As Byte()) As Long
        Dim Result As Long = bytes(0)
        Result += (bytes(1) And Long.MaxValue) << 8
        Result += (bytes(2) And Long.MaxValue) << 16
        Result += (bytes(3) And Long.MaxValue) << 24
        Result += (bytes(4) And Long.MaxValue) << 32
        Result += (bytes(5) And Long.MaxValue) << 40
        Result += (bytes(6) And Long.MaxValue) << 48
        Result += (bytes(7) And Long.MaxValue) << 56
        Return Result
    End Function

    Public Shared Function GetULong(bytes As Byte()) As ULong
        Dim Result As ULong = bytes(0)
        Result += (bytes(1) And ULong.MaxValue) << 8
        Result += (bytes(2) And ULong.MaxValue) << 16
        Result += (bytes(3) And ULong.MaxValue) << 24
        Result += (bytes(4) And ULong.MaxValue) << 32
        Result += (bytes(5) And ULong.MaxValue) << 40
        Result += (bytes(6) And ULong.MaxValue) << 48
        Result += (bytes(7) And ULong.MaxValue) << 56
        Return Result
    End Function

    Public Shared Function GetShort(bytes As Byte()) As Short
        Dim Result As Short = bytes(0)
        Result += (bytes(1) And Short.MaxValue) << 8
        Return Result
    End Function

    Public Shared Function GetUShort(bytes As Byte()) As UShort
        Dim Result As UShort = bytes(0)
        Result += (bytes(1) And UShort.MaxValue) << 8
        Return Result
    End Function

    Public Shared Function GetBoolean(bytes As Byte()) As Boolean
        Dim Result As Boolean = bytes(0)
        Return Result
    End Function

    ''' <summary>
    ''' Following IEEE754 specification, from byte array will be converted back to integer representation and then into float-single precision number
    ''' </summary>
    ''' <param name="bytes"></param>
    ''' <returns></returns>   
    Public Shared Function GetSingle(bytes As Byte()) As Single
        Dim bytesToInteger = GetInteger(bytes)

        Dim numberSign = If(bytesToInteger >> 31 < 0, -1, 1)
        Dim exponent = (bytesToInteger >> 23) - 127
        Dim significand = (bytesToInteger And Not -8388608) '(-1 * 2^23) = -8388608 = &H‭FFFFFFFFFF800000‬
        Dim mantissa As Single = 0.0F

        'normal number
        If (exponent <> -149) Then exponent = exponent And 255
        'positive subnormal - underflow
        If (exponent = 107 AndAlso significand = 0) Then exponent = -149

        ''In case number is Infinity or NaN
        'If (exponent = 128) Then Return numberSign * (If(significand, Single.NaN, Single.PositiveInfinity))
        'If (exponent = -127) Then
        '    If (significand = 0) Then Return numberSign * 0.0F
        '    exponent = -126
        '    mantissa = significand / (2 ^ 22)
        'Else
        mantissa = (significand Or 8388608) / 8388608 ' 2^23 = 8388608 = &H800000
        'End If

        Return numberSign * mantissa * 2 ^ exponent
    End Function

    ''' <summary>
    ''' Following IEEE754 specification, from byte array will be converted back to long representation and then into float-double precision number
    ''' </summary>
    ''' <param name="bytes"></param>
    ''' <returns></returns>  
    Public Shared Function GetDouble(bytes As Byte()) As Double
        Dim bytesToLong = GetLong(bytes)

        Dim numberSign = If(bytesToLong >> 63 < 0, -1, 1)
        Dim exponent = ((bytesToLong >> 52) And 2047) - 1023
        Dim significand = (bytesToLong And Not -4503599627370496) '(-1 * 2^52) = -4503599627370496 = &H‭FFF0000000000000‬
        Dim mantissa As Double = 0.0

        'In case number is Infinity or NaN
        If (exponent = 1024) Then Return numberSign * (If(significand, Single.NaN, Single.PositiveInfinity))
        If (exponent = -1023) Then
            If (significand = 0) Then Return numberSign * 0.0
            exponent = -1022
            mantissa = significand / (2 ^ 51)
        Else
            mantissa = (significand Or 4503599627370496) / 4503599627370496 '2^52 = 4503599627370496 = &H‭10000000000000
        End If

        Return numberSign * mantissa * 2 ^ exponent
    End Function
#End Region

End Class