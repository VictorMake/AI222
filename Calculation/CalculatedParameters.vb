Imports NationalInstruments.UI
Imports NationalInstruments.UI.WindowsForms

''' <summary>
''' Расчетные параметры
''' </summary>
''' <remarks></remarks>
Public Class CalculatedParameters
    Implements IEnumerable
    Public Property CalcDictionary As Dictionary(Of String, Parameter)

    Default Public Property Item(key As String) As Double
        Get
            Return CalcDictionary(key).CalculatedValue
        End Get
        Set(value As Double)
            CalcDictionary(key).CalculatedValue = value
        End Set
    End Property

    'Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    Return CalcDictionary.GetEnumerator()
    'End Function

    ' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    ' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    ' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each keysCalc As String In CalcDictionary.Keys.ToArray
            Yield CalcDictionary(keysCalc)
        Next
    End Function

    Public Sub New()
        CalcDictionary = New Dictionary(Of String, Parameter)
    End Sub

    ''' <summary>
    ''' Связать индикатор и цифровое поле в словарях с расчётным параметром по ключу
    ''' для отображения при обновлении данных от регистратора
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="inINumericPointer"></param>
    ''' <param name="inNumericEdit"></param>
    Public Sub BindingWithControls(key As String, inINumericPointer As INumericPointer, inNumericEdit As NumericEdit)
        CalcDictionary(key).ControlNumericPointer = inINumericPointer
        CalcDictionary(key).ControlNumericEdit = inNumericEdit
    End Sub

    ''' <summary>
    ''' Отменить связи с индикаторами и цифровыми полями все элементы коллекции.
    ''' </summary>
    Public Sub UnBindingAllControls()
        For Each key As String In CalcDictionary.Keys.ToArray
            CalcDictionary(key).ControlNumericPointer = Nothing
            CalcDictionary(key).ControlNumericEdit = Nothing
        Next
    End Sub
End Class

Public Class Parameter
    'Public Enum TypeEnum
    '    Pointer
    '    NumericEdit
    'End Enum
    'Public Property Type As TypeEnum
    Public Property Name As String
    Public Property ControlNumericPointer As INumericPointer
    Public Property ControlNumericEdit As NumericEdit
    Public Property CalculatedValue As Double
End Class
