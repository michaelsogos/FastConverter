
<TestClass()> Public Class Test

    <TestMethod()>
    Public Sub TestInteger()
        Dim a As Integer = Integer.MinValue
        Dim b = FastConverter.GetBytes(a)
        Dim b1 As Byte = 128
        Assert.AreEqual(b.Length, 4)
        Assert.AreEqual(b(3), b1)
        Dim c = FastConverter.GetInteger(b)
        Assert.AreEqual(a, c)

        a = Integer.MaxValue
        b = FastConverter.GetBytes(a)
        b1 = 127
        Assert.AreEqual(b.Length, 4)
        Assert.AreEqual(b(3), b1)
        c = FastConverter.GetInteger(b)
        Assert.AreEqual(a, c)
    End Sub

    <TestMethod()>
    Public Sub TestUInteger()
        Dim a As UInteger = UInteger.MinValue
        Dim b = FastConverter.GetBytes(a)
        Dim b1 As Byte = 0
        Assert.AreEqual(b.Length, 4)
        Assert.AreEqual(b(3), b1)
        Dim c = FastConverter.GetUInteger(b)
        Assert.AreEqual(a, c)

        a = UInteger.MaxValue
        b = FastConverter.GetBytes(a)
        b1 = 255
        Assert.AreEqual(b.Length, 4)
        Assert.AreEqual(b(3), b1)
        c = FastConverter.GetUInteger(b)
        Assert.AreEqual(a, c)
    End Sub

    <TestMethod()>
    Public Sub TestLong()
        Dim a As Long = Long.MinValue
        Dim b = FastConverter.GetBytes(a)
        Dim b1 As Byte = 128
        Assert.AreEqual(b.Length, 8)
        Assert.AreEqual(b(7), b1)
        Dim c = FastConverter.GetLong(b)
        Assert.AreEqual(a, c)

        a = Long.MaxValue
        b = FastConverter.GetBytes(a)
        b1 = 127
        Assert.AreEqual(b.Length, 8)
        Assert.AreEqual(b(7), b1)
        c = FastConverter.GetLong(b)
        Assert.AreEqual(a, c)
    End Sub

    <TestMethod()>
    Public Sub TestULong()
        Dim a As ULong = ULong.MinValue
        Dim b = FastConverter.GetBytes(a)
        Dim b1 As Byte = 0
        Assert.AreEqual(b.Length, 8)
        Assert.AreEqual(b(7), b1)
        Dim c = FastConverter.GetULong(b)
        Assert.AreEqual(a, c)

        a = ULong.MaxValue
        b = FastConverter.GetBytes(a)
        b1 = 255
        Assert.AreEqual(b.Length, 8)
        Assert.AreEqual(b(7), b1)
        c = FastConverter.GetULong(b)
        Assert.AreEqual(a, c)
    End Sub

    <TestMethod()>
    Public Sub TestShort()
        Dim a As Short = Short.MinValue
        Dim b = FastConverter.GetBytes(a)
        Dim b1 As Byte = 128
        Assert.AreEqual(b.Length, 2)
        Assert.AreEqual(b(1), b1)
        Dim c = FastConverter.GetShort(b)
        Assert.AreEqual(a, c)

        a = Short.MaxValue
        b = FastConverter.GetBytes(a)
        b1 = 127
        Assert.AreEqual(b.Length, 2)
        Assert.AreEqual(b(1), b1)
        c = FastConverter.GetShort(b)
        Assert.AreEqual(a, c)
    End Sub

    <TestMethod()>
    Public Sub TestUShort()
        Dim a As UShort = UShort.MinValue
        Dim b = FastConverter.GetBytes(a)
        Dim b1 As Byte = 0
        Assert.AreEqual(b.Length, 2)
        Assert.AreEqual(b(1), b1)
        Dim c = FastConverter.GetUShort(b)
        Assert.AreEqual(a, c)

        a = UShort.MaxValue
        b = FastConverter.GetBytes(a)
        b1 = 255
        Assert.AreEqual(b.Length, 2)
        Assert.AreEqual(b(1), b1)
        c = FastConverter.GetUShort(b)
        Assert.AreEqual(a, c)
    End Sub

    <TestMethod()>
    Public Sub TestBoolean()
        Dim a As Boolean = True
        Dim b = FastConverter.GetBytes(a)
        Dim b1 As Byte = 1
        Assert.AreEqual(b.Length, 1)
        Assert.AreEqual(b(0), b1)
        Dim c = FastConverter.GetBoolean(b)
        Assert.AreEqual(a, c)

        a = False
        b = FastConverter.GetBytes(a)
        b1 = 0
        Assert.AreEqual(b.Length, 1)
        Assert.AreEqual(b(0), b1)
        c = FastConverter.GetBoolean(b)
        Assert.AreEqual(a, c)
    End Sub

    <TestMethod()>
    Public Sub TestSingle()
        Dim NumbersToTest() As Single = {Math.PI, Single.Epsilon, (-1 * Single.Epsilon), -5.828125F, 5.828125F, Single.MaxValue, Single.MinValue}
        For Each n In NumbersToTest
            Dim bits = FastConverter.GetBytes(n)
            Assert.AreEqual(bits.Length, 4, String.Format("The BITS (bytearray) length is {0} while expected 4 for number {1}!", bits.Length, n))
            Dim number = FastConverter.GetSingle(bits)
            Assert.AreEqual(n, number, String.Format("The testing number is {0} while the encoded number is {1}!", n, number))
        Next
    End Sub

    <TestMethod()>
    Public Sub TestDouble()
        Dim NumbersToTest() As Double = {Double.Epsilon, -5.828125, 5.828125, Double.MaxValue, Double.MinValue}
        For Each n In NumbersToTest
            Dim bits = FastConverter.GetBytes(n)
            Assert.AreEqual(bits.Length, 8, String.Format("The BITS (bytearray) length is {0} while expected 8 for number {1}!", bits.Length, n))
            Dim number = FastConverter.GetDouble(bits)
            Assert.AreEqual(n, number, String.Format("The testing number is {0} while the encoded number is {1}!", n, number))
        Next
    End Sub
End Class