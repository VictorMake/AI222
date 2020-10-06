''' <summary>
''' Полное описание тега МКИО для вычисления его значения и визуализации
''' </summary>
Friend Class TagMKIO
    Implements IEnumerable

    ''' <summary>
    ''' Канал Дискретного Слова
    ''' </summary>
    Public Structure ChannelDiscreteWord
        Dim NameParameter As String
        ''' <summary>
        ''' Описание Параметра
        ''' </summary>
        Dim DescriptionParameter As String
        ''' <summary>
        ''' Разряд
        ''' </summary>
        Dim ByteRank As Short
        ''' <summary>
        ''' Индекс В Массиве Значений
        ''' </summary>
        Dim IndexInArrayValue As Integer
    End Structure

    ''' <summary>
    ''' Каналы состоят из аналоговых каналов таблицы {МКИО} и каналов таблицы {ДискретноеСлово}.
    ''' В процессе развертывания дискретного слова внутри цикла метода СчитатьПараметры заново образуется массив 
    ''' и копируется данному свойству.
    ''' </summary>
    ''' <returns></returns>
    Friend Property DiscreteWord() As ChannelDiscreteWord()
    ''' <summary>
    ''' Количество Параметров В Дискретном Слове
    ''' </summary>
    ''' <returns></returns>
    Public Property CountsParametersInDiscretWord() As Integer
    ''' <summary>
    ''' Индекс В Массиве Значений
    ''' </summary>
    ''' <returns></returns>
    Public Property IndexInArrayValue() As Integer
    ''' <summary>
    ''' Развернуть Дискретное Слово
    ''' </summary>
    ''' <returns></returns>
    Public Property IsDecompressDiscreteWord() As Boolean
    ''' <summary>
    ''' Примечание
    ''' </summary>
    ''' <returns></returns>
    Public Property Note() As String
    ''' <summary>
    ''' Таблица
    ''' </summary>
    ''' <returns></returns>
    Public Property TableName() As String
    ''' <summary>
    ''' Цена Старшего Разряда
    ''' </summary>
    ''' <returns></returns>
    Public Property HighOrderBit() As Double
    ''' <summary>
    ''' Цена Младшего Разряда
    ''' </summary>
    ''' <returns></returns>
    Public Property LowerOrderBit() As Double
    ''' <summary>
    ''' Масштаб
    ''' </summary>
    ''' <returns></returns>
    Public Property Scale() As Short
    ''' <summary>
    ''' ЧислоРазрядов
    ''' </summary>
    ''' <returns></returns>
    Public Property CountsByte() As Short
    Public Property UpperByte() As Short
    Public Property LoverByte() As Short
    ''' <summary>
    ''' Знак
    ''' </summary>
    ''' <returns></returns>
    Public Property Digit() As Short
    ''' <summary>
    ''' Диапазон Изменения Max
    ''' </summary>
    ''' <returns></returns>
    Public Property RangeOfChangingMax() As Single
    ''' <summary>
    ''' Диапазон Изменения Min
    ''' </summary>
    ''' <returns></returns>
    Public Property RangeOfChangingMin() As Single
    ''' <summary>
    ''' Единица Измерения
    ''' </summary>
    ''' <returns></returns>
    Public Property Unit() As String
    ''' <summary>
    ''' Тег Для Записи
    ''' </summary>
    ''' <returns></returns>
    Public Property IsTagForRecord() As Boolean
    ''' <summary>
    ''' Тег
    ''' </summary>
    ''' <returns></returns>
    Public Property Tag() As String
    ''' <summary>
    ''' Описание Параметра
    ''' </summary>
    ''' <returns></returns>
    Public Property Description() As String
    ''' <summary>
    ''' Наименование Параметра
    ''' </summary>
    ''' <returns></returns>
    Public Property NameParameter() As String
    Public Property KeyID() As Integer

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return DiscreteWord.GetEnumerator()
    End Function

    '' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    '' Однако нельзя полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    '' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    'Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    For I As Integer = 0 To КаналыДискретногоСлова.Count - 1
    '        Yield КаналыДискретногоСлова(I)
    '    Next
    'End Function

    Default Friend ReadOnly Property Item(ByVal indexKey As Integer) As ChannelDiscreteWord
        Get
            Return DiscreteWord(indexKey)
        End Get
    End Property

    Private Const const214 As Integer = 16384 ' 2 ^ 14

    ''' <summary>
    ''' Физическое Значение
    ''' </summary>
    ''' <param name="valueTag"></param>
    ''' <returns></returns>
    Public Function PhysicalValue(ByVal valueTag As Integer) As Double
        ' аналоговое значение
        'Dim intB1, intB2 As Short
        'Dim intB2 As Short

        'ConvertLongToIntegers(valueTag, intB2)
        'valueTag = intB2

        Return ConvertIntegerToShort(valueTag) / (const214 / HighOrderBit)
    End Function

    ''' <summary>
    ''' Цифровое Значение
    ''' </summary>
    ''' <param name="valueTag"></param>
    ''' <param name="indexBitWord"></param>
    ''' <returns></returns>
    Public Function DiscreteValue(ByVal valueTag As Integer, ByVal indexBitWord As Integer) As Double
        ' Значение
        'Dim Value As Integer = valueTag And MKIO.TemplateMask(КаналыДискретногоСлова(intIndex).ByteRank)
        If (valueTag And TemplateMask(DiscreteWord(indexBitWord).ByteRank)) = 0 Then
            Return 0.0
        Else
            Return 1.0
        End If
    End Function

    ''' <summary>
    ''' Представление переменной типа Integer в виде младшего байта беззнакового типа Short
    ''' </summary>
    ''' <param name="integerValue"></param>
    ''' <returns></returns>
    Private Function ConvertIntegerToShort(ByVal integerValue As Integer) As Short
        Dim shortLow As Short = CShort(integerValue And &H7FFFS) ' младшие 16 разрядов (111111111111111)

        If (integerValue And &H8000S) <> 0 Then
            shortLow = shortLow Or &H8000S ' (1000000000000000)
        End If

        Return shortLow
    End Function

    'Private Sub ConvertIntegersToLong(ByRef LongValue As Integer, ByRef IntegerHigh As Short, ByRef IntegerLow As Short)
    '    '   Представление двух беззнаковых переменных типа INTEGER
    '    '   в виде переменной типа LONG
    '    LongValue = IntegerHigh * &H10000 + CShort(IntegerLow And &H7FFFS)
    '    If IntegerLow < 0 Then LongValue = LongValue Or 32769 ' &h8000
    'End Sub

    'Private Sub ConvertLongToIntegers(ByRef LongValue As Integer, ByRef IntegerHigh As Short, ByRef IntegerLow As Short)
    '    '   Представление переменной типа LONG в виде двух
    '    '   беззнаковых переменных типа INTEGER
    '    Dim IntegetHigh As Short = LongValue \ &H10000 ' старшие 16 разрядов (10000000000000000)
    '    IntegerLow = LongValue And &H7FFFS ' младшие 16 разрядов (111111111111111)

    '    If (LongValue And &H8000S) <> 0 Then
    '        IntegerLow = IntegerLow Or &H8000S ' (1000000000000000)
    '    End If
    'End Sub
End Class