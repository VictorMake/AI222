''' <summary>
''' Входные аргументы
''' </summary>
''' <remarks></remarks>
Public Class InputParameters
    'Public Const conБАРОМЕТР As String = "Барометр"
    Public Const conTБОКСА As String = "Tбокса" ' температура в боксе

    'Private mБарометр As Double
    'Public Property Барометр() As Double
    '    Get
    '        Return mБарометр
    '    End Get
    '    Set(ByVal value As Double)
    '        mБарометр = value
    '        InputParameterDictionary(conБАРОМЕТР) = value
    '    End Set
    'End Property

    Private mTбокса As Double
    Public Property Tбокса() As Double
        Get
            Return mTбокса
        End Get
        Set(ByVal value As Double)
            mTбокса = value
            InputParameterDictionary(conTБОКСА) = value
        End Set
    End Property

    Public Property InputParameterDictionary As Dictionary(Of String, Double)

    Public Sub New()
        InputParameterDictionary = New Dictionary(Of String, Double) From {
        {conTБОКСА, Tбокса}}
        '{conБАРОМЕТР, Барометр},
    End Sub

End Class
