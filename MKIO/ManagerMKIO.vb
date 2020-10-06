Imports System.Threading
Imports System.Data.OleDb
Imports System.Windows.Forms
Imports AI222.TagMKIO
Imports MathematicalLibrary

''' <summary>
''' Менеджер управления коллекцией тегов МКИО
''' </summary>
Friend Class ManagerMKIO
    Implements IEnumerable

    Friend Structure TypeBaseParameter
        'Dim NumberParameter As Short            ' НомерПараметра
        ''' <summary>
        ''' Наименование Параметра
        ''' </summary>
        Dim NameParameter As String
        ''' <summary>
        ''' НомерКанала DAQ
        ''' </summary>
        Dim NumberChannel As Short
        ''' <summary>
        ''' НомерУстройства или корзины
        ''' </summary>
        Dim NumberDevice As Short
        'Dim NumberModuleChassis As Short        ' Номер модуля в корзине
        'Dim NumberChannelModule As Short        ' Номер канала модуля
        'Dim TypeConnection As String            ' ТипПодключения
        'Dim LowerMeasure As Single              ' НижнийПредел
        'Dim UpperMeasure As Single              ' ВерхнийПредел
        'Dim SignalType As String                ' ТипСигнала
        'Dim NumberFormula As Short              ' НомерФормулы
        'Dim LevelOfApproximation As Short       ' СтепеньАппроксимации
        '<VBFixedArray(5)> Dim Coefficient As Double()
        'Dim CompensationXC As Boolean
        'Dim Offset As Double                    ' Смещение
        ''' <summary>
        ''' Единица Измерения
        ''' </summary>
        Dim UnitOfMeasure As String
        ''' <summary>
        ''' Допуск Минимум
        ''' </summary>
        Dim LowerLimit As Single
        ''' <summary>
        ''' Допуск Максимум
        ''' </summary>
        Dim UpperLimit As Single
        ''' <summary>
        ''' Разнос Умин
        ''' </summary>
        Dim RangeYmin As Short
        ''' <summary>
        ''' Разнос Умакс
        ''' </summary>
        Dim RangeYmax As Short
        ''' <summary>
        ''' Аварийное Значение Мин
        ''' </summary>
        Dim AlarmValueMin As Single
        ''' <summary>
        ''' Аварийное Значение Макс
        ''' </summary>
        Dim AlarmValueMax As Single
        ''' <summary>
        ''' Блокировка
        ''' </summary>
        Dim Blocking As Boolean
        ''' <summary>
        ''' Видимость
        ''' </summary>
        Dim IsVisible As Boolean
        ''' <summary>
        ''' Видимость Регистратор
        ''' </summary>
        Dim IsVisibleRegistration As Boolean
        'Dim Mistake As Single                   ' Погрешность
        ''' <summary>
        ''' Примечания
        ''' </summary>
        Dim Description As String
        ''' <summary>
        ''' Значение расшифрованного тега
        ''' </summary>
        Dim ValueParameterMKIO As Double
        'Public Sub Initialize()
        '    ReDim_Coefficient(5)
        'End Sub
    End Structure

    Private mCountTagsMKIO As Integer
    ''' <summary>
    ''' Количество МКИО 'Дискретное слово' и 'Аналоговый'
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CountTagsMKIO As Integer
        Get
            Return mCountTagsMKIO
        End Get
    End Property

    Private mCountParametersMKIO As Integer
    ''' <summary>
    ''' Количество Параметров МКИО
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CountParametersMKIO() As Integer
        Get
            Return mCountParametersMKIO
        End Get
    End Property

    'Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    Return mMkioTagDictionary.GetEnumerator
    'End Function

    ''Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    '' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    '' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each keysCalc As String In mMkioTagDictionary.Keys.ToArray
            Yield mMkioTagDictionary(keysCalc)
        Next
    End Function

    Default Friend ReadOnly Property Item(key As String) As TagMKIO
        Get
            Return mMkioTagDictionary(key)
        End Get
    End Property

    ''' <summary>
    ''' Возвращает базовй TypeBaseParameter по имени
    ''' </summary>
    ''' <param name="strKey"></param>
    ''' <returns></returns>
    Friend ReadOnly Property GetParameterByName(ByVal strKey As String) As TypeBaseParameter
        Get
            'Dim indexKey As Integer
            'For I As Integer = 0 To UBound(aParametersChannelMKIO)
            '    If strKey = aParametersChannelMKIO(I).NameParameter Then
            '        indexKey = I
            '        Exit For
            '    End If
            'Next
            'Return aParametersChannelMKIO(indexKey)

            Return aParametersChannelMKIO.Single(Function(baseParameter) baseParameter.NameParameter = strKey)
        End Get
    End Property

    ''' <summary>
    ''' Массив каналов МКИО 'Дискретное слово' и 'Аналоговый'
    ''' </summary>
    ''' <returns></returns>
    Friend ReadOnly Property GetParameters() As TypeBaseParameter()
        Get
            Return aParametersChannelMKIO
        End Get
    End Property

    Private Structure CloneOfPropertyChannel
        ''' <summary>
        ''' Наименование Параметра
        ''' </summary>
        Dim NameParameter As String
        ''' <summary>
        ''' Разнос Умин
        ''' </summary>
        Dim RangeYmin As Short
        ''' <summary>
        ''' Разнос Умакс
        ''' </summary>
        Dim RangeYmax As Short
        ''' <summary>
        ''' Аварийное Значение Мин
        ''' </summary>
        Dim AlarmValueMin As Single
        ''' <summary>
        ''' Аварийное Значение Макс
        ''' </summary>
        Dim AlarmValueMax As Single
        ''' <summary>
        ''' Блокировка
        ''' </summary>
        Dim Blocking As Boolean
        ''' <summary>
        ''' Видимость
        ''' </summary>
        Dim IsVisible As Boolean
        ''' <summary>
        ''' Видимость Регистратор
        ''' </summary>
        Dim IsVisibleRegistration As Boolean
    End Structure

    Private mMkioTagDictionary As Dictionary(Of String, TagMKIO)
    Private aParametersChannelMKIO As TypeBaseParameter()

    Public Sub New()
        MyBase.New()

        mMkioTagDictionary = New Dictionary(Of String, TagMKIO)
    End Sub

    ''' <summary>
    ''' Считать Параметры
    ''' </summary>
    ''' <param name="inPathDBase_MKIO_AI222"></param>
    Public Sub LoadParameters(ByVal inPathDBase_MKIO_AI222 As String)
        Dim K, I, indexTag As Integer
        Dim nameParamFromTag As String
        Dim strSQL As String
        Dim cn As OleDbConnection = Nothing
        Dim cmd As OleDbCommand
        Dim rdr As OleDbDataReader
        Dim odaDataAdapter As OleDbDataAdapter
        Dim dtDataTable As New DataTable
        Dim itemDataRow As DataRow
        Dim aRows() As DataRow

        ' 2 Считать теги которые без "Дискретное слово" и "Состояние" и добавить в mvarКоллекцияТегов
        ' 3 Считать теги которые равны "Дискретное слово" сделать разбор в массив arrКаналыДискретногоСлова данного тега, добавить тег в mvarКоллекцияТегов
        ' 4 В цикле пор тегам и по массиву arrКаналыДискретногоСлова заполняем массив aParametersChannelMKIO и включаем видимость с ранее считанного
        mMkioTagDictionary.Clear()

        Try
            cn = New OleDbConnection(BuildCnnStr(PROVIDER_JET, inPathDBase_MKIO_AI222))
            cn.Open()

            strSQL = "SELECT МкиоАтрибуты.*, ДискрАтрибуты.* " &
                        "FROM МкиоАтрибуты LEFT JOIN ДискрАтрибуты ON МкиоАтрибуты.KeyId=ДискрАтрибуты.KeyIdМКИО " &
                        "WHERE (((МкиоАтрибуты.Примечание)='Дискретное слово' Or (МкиоАтрибуты.Примечание)='Аналоговый'));"
            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            dtDataTable = New DataTable
            odaDataAdapter.Fill(dtDataTable)
            mCountParametersMKIO = dtDataTable.Rows.Count ' 429 записей добавятся в Channels

            'If mCountParametersMKIO > 0 Then ReDim_aParametersChannelMKIO(mCountParametersMKIO - 1)
            If mCountParametersMKIO > 0 Then Re.Dim(aParametersChannelMKIO, mCountParametersMKIO - 1)

            '    strSQL = "SELECT МКИО.*, ДискретноеСлово.* " & _
            '"FROM МКИО LEFT JOIN ДискретноеСлово ON МКИО.KeyId = ДискретноеСлово.KeyIdМКИО;"
            '    strSQL = "SELECT МкиоАтрибуты.*, ДискрАтрибуты.* FROM МкиоАтрибуты LEFT JOIN ДискрАтрибуты ON МкиоАтрибуты.KeyId=ДискрАтрибуты.KeyIdМКИО;"

            strSQL = "SELECT Count(*) AS Выражение1 " &
                    "FROM МКИО INNER JOIN АтрибутыВидимости ON МКИО.НаименованиеПараметра = АтрибутыВидимости.НаименованиеПараметра " &
                    "WHERE (((МКИО.Примечание)='Дискретное слово' Or (МКИО.Примечание)='Аналоговый'));"
            cmd = cn.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQL
            mCountTagsMKIO = CInt(cmd.ExecuteScalar) ' должно быть 134

            strSQL = "SELECT МКИО.*, АтрибутыВидимости.* " &
                    "FROM МКИО INNER JOIN АтрибутыВидимости ON МКИО.НаименованиеПараметра = АтрибутыВидимости.НаименованиеПараметра " &
                    "WHERE (((МКИО.Примечание)='Дискретное слово' Or (МКИО.Примечание)='Аналоговый')) " &
                    "ORDER BY МКИО.KeyId;"
            I = 0
            cmd.CommandText = strSQL
            rdr = cmd.ExecuteReader

            ' заполнить mMkioTagDictionary и aParametersChannelMKIO записями МКИО
            If mCountParametersMKIO > 0 AndAlso mCountTagsMKIO > 0 Then
                Do While (rdr.Read)
                    nameParamFromTag = CStr(rdr("Тег"))
                    'sTemp = StrConv(LoadResData(sResName, sResType), vbUnicode)
                    mMkioTagDictionary.Add(nameParamFromTag, New TagMKIO)
                    indexTag += 1 'увеличить номер тега

                    With mMkioTagDictionary.Item(nameParamFromTag)
                        .KeyID = CInt(rdr("KeyId"))
                        .Tag = CStr(rdr("Тег"))
                        .IsTagForRecord = CBool(rdr("ТегДляЗаписи"))
                        .NameParameter = CStr(rdr("МКИО.НаименованиеПараметра"))
                        .Description = CStr(rdr("ОписаниеПараметра"))

                        If CStr(rdr("Примечание")) = "Аналоговый" Then
                            .Unit = ПеревестиЕдИзмерения(rdr("ЕдиницаИзмерения").ToString)
                            .RangeOfChangingMin = CSng(rdr("ДиапазонИзмененияMin"))
                            .RangeOfChangingMax = CSng(rdr("ДиапазонИзмененияMax"))
                            .Digit = CShort(rdr("Знак"))
                            .LoverByte = CShort(rdr("МладшийРазряд"))
                            .UpperByte = CShort(rdr("СтаршийРазряд"))
                            .CountsByte = CShort(rdr("ЧислоРазрядов"))
                            .Scale = CShort(rdr("Масштаб"))
                            .LowerOrderBit = CDbl(rdr("ЦенаМладшегоРазряда"))
                            .HighOrderBit = CDbl(rdr("ЦенаСтаршегоРазряда"))
                            .IsDecompressDiscreteWord = False
                            .IndexInArrayValue = I

                            aParametersChannelMKIO(I).NumberChannel = CShort(I) ' для того чтобы знать индекс для этого канала в масииве типа
                            aParametersChannelMKIO(I).NumberDevice = CShort(indexTag)
                            aParametersChannelMKIO(I).NameParameter = CStr(rdr("МКИО.НаименованиеПараметра"))
                            aParametersChannelMKIO(I).UnitOfMeasure = .Unit
                            aParametersChannelMKIO(I).LowerLimit = CSng(rdr("ДиапазонИзмененияMin"))
                            aParametersChannelMKIO(I).UpperLimit = CSng(rdr("ДиапазонИзмененияMax"))
                            aParametersChannelMKIO(I).RangeYmin = CShort(rdr("РазносУмин"))
                            aParametersChannelMKIO(I).RangeYmax = CShort(rdr("РазносУмакс"))
                            aParametersChannelMKIO(I).AlarmValueMin = CSng(rdr("АварийноеЗначениеМин"))
                            aParametersChannelMKIO(I).AlarmValueMax = CSng(rdr("АварийноеЗначениеМакс"))
                            aParametersChannelMKIO(I).Blocking = CBool(rdr("Блокировка"))
                            aParametersChannelMKIO(I).IsVisible = CBool(rdr("Видимость"))
                            aParametersChannelMKIO(I).IsVisibleRegistration = CBool(rdr("ВидимостьРегистратор"))
                            aParametersChannelMKIO(I).Description = CStr(rdr("ОписаниеПараметра"))
                            I += 1
                        Else ' Дискретное слово
                            ' применить к rstRecordsetAll фильтр по полю МкиоАтрибуты.KeyId= rstRecordset("KeyId")
                            aRows = dtDataTable.Select("МкиоАтрибуты.KeyId = " & CStr(rdr("KeyId")))
                            If aRows.Length > 0 Then
                                K = 0
                                Dim arrChannelDiscreteWord(aRows.Length - 1) As ChannelDiscreteWord

                                For Each itemDataRow In aRows
                                    arrChannelDiscreteWord(K).NameParameter = CStr(itemDataRow("ДискретноеСлово.НаименованиеПараметра"))
                                    arrChannelDiscreteWord(K).DescriptionParameter = CStr(itemDataRow("ДискрАтрибуты.ОписаниеПараметра"))
                                    arrChannelDiscreteWord(K).ByteRank = CShort(itemDataRow("Разряд"))
                                    arrChannelDiscreteWord(K).IndexInArrayValue = I

                                    aParametersChannelMKIO(I).NumberChannel = CShort(I) ' для того чтобы знать индекс для этого канала в масииве типа
                                    aParametersChannelMKIO(I).NumberDevice = CShort(indexTag)
                                    aParametersChannelMKIO(I).NameParameter = CStr(itemDataRow("ДискретноеСлово.НаименованиеПараметра"))
                                    aParametersChannelMKIO(I).UnitOfMeasure = "Деления"
                                    aParametersChannelMKIO(I).LowerLimit = 0
                                    aParametersChannelMKIO(I).UpperLimit = 1
                                    aParametersChannelMKIO(I).RangeYmin = CShort(itemDataRow("ДискрАтрибуты.РазносУмин"))
                                    aParametersChannelMKIO(I).RangeYmax = CShort(itemDataRow("ДискрАтрибуты.РазносУмакс"))
                                    aParametersChannelMKIO(I).AlarmValueMin = CSng(itemDataRow("ДискрАтрибуты.АварийноеЗначениеМин"))
                                    aParametersChannelMKIO(I).AlarmValueMax = CSng(itemDataRow("ДискрАтрибуты.АварийноеЗначениеМакс"))
                                    aParametersChannelMKIO(I).Blocking = CBool(itemDataRow("ДискрАтрибуты.Блокировка"))
                                    aParametersChannelMKIO(I).IsVisible = CBool(itemDataRow("ДискрАтрибуты.Видимость"))
                                    aParametersChannelMKIO(I).IsVisibleRegistration = CBool(itemDataRow("ДискрАтрибуты.ВидимостьРегистратор"))
                                    aParametersChannelMKIO(I).Description = CStr(itemDataRow("ДискрАтрибуты.ОписаниеПараметра"))
                                    I += 1
                                    K += 1
                                Next
                                '.КаналыДискретногоСлова = VB6.CopyArray(arrКаналыДискретногоСлова) 'передать массив
                                'Array.Copy(arrКаналыДискретногоСлова, .КаналыДискретногоСлова, arrКаналыДискретногоСлова.Length)'так нельзя
                                .DiscreteWord = CType(arrChannelDiscreteWord.Clone, ChannelDiscreteWord()) ' передать массив
                                .CountsParametersInDiscretWord = aRows.Length
                            End If

                            If Not IsDBNull(rdr("Таблица")) Then
                                .TableName = CStr(rdr("Таблица"))
                            Else
                                .TableName = " "
                            End If

                            .IsDecompressDiscreteWord = True
                        End If

                        .Note = CStr(rdr("Примечание"))
                    End With
                Loop
            End If

            rdr.Close()
            cn.Close()
        Catch ex As Exception
            Dim caption As String = $"Процедура <{NameOf(LoadParameters)}> - " & ex.Source
            Dim text As String = ex.ToString
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
        ' 6 организуем массив с для ValuesParametersMKIO() as double
        ' 7 при открытии формы в цикле по числу тегов  загружаем массив OPCDataSocket настраиваем по тегам
        ' 8 при пуске сбора запускаем таймер в событии которого по индексу считываем значение а по тегу ищем
        ' в mvarКоллекцияТегов и
        ' если  РазвернутьДискретноеСлово=False по индексу IndexInArrayValue записать вычисленное значени в ValuesParametersMKIO
        ' если свойство тега РазвернутьДискретноеСлово=True, то через Item(I).IndexInArrayValue
        ' производится по маске считывание бита и значение 0 или 1 записать в ValuesParametersMKIO
        UpdateDBaseAI222()
    End Sub

    ''' <summary>
    ''' Заполнить заново таблицу РасчетныеПараметры базы AI222 
    ''' по всем каналам МКИО из сформированного aParametersChannelMKIO
    ''' </summary>
    Private Sub UpdateDBaseAI222()
        ' --- Таблица РасчётныеПараметры
        'ИмяПараметра	                    Короткий текст
        'ОписаниеПараметра	                Короткий текст
        'ВычисленноеЗначениеВСИ	            Числовой
        'ВычисленноеПереведенноеЗначение	Числовой
        'РазмерностьСИ	                    Короткий текст
        'РазмерностьВыходная	            Короткий текст
        'НакопленноеЗначение	            Числовой
        'ИндексКаналаИзмерения	            Числовой
        'ДопускМинимум	                    Числовой
        'ДопускМаксимум	                    Числовой
        'РазносУмин	                        Числовой
        'РазносУмакс	                    Числовой
        'АварийноеЗначениеМин	            Числовой
        'АварийноеЗначениеМакс	            Числовой
        'Видимость	                        Логический
        'ВидимостьРегистратор               Логический

        Dim newRow As BaseForm.BaseFormDataSet.РасчетныеПараметрыRow

        ' вначале очистить BaseForm.Manager.РасчетныеПараметры
        'это gMainFomMdiParent.Manager.РасчетныеПараметры.Rows.Clear() не работает. Надо конкретно по записям:
        For Each itemRow As BaseForm.BaseFormDataSet.РасчетныеПараметрыRow In gMainFomMdiParent.Manager.CalculatedDataTable.Rows
            itemRow.Delete()
        Next

        ' из aParametersChannelMKIO занести в типизированную таблицу BaseForm.Manager.РасчетныеПараметры
        For I As Integer = 0 To aParametersChannelMKIO.GetUpperBound(0) 'mCountParametersMKIO - 1
            newRow = gMainFomMdiParent.Manager.CalculatedDataTable.NewРасчетныеПараметрыRow
            With newRow
                .ИмяПараметра = aParametersChannelMKIO(I).NameParameter
                .ОписаниеПараметра = Left(aParametersChannelMKIO(I).Description, 49)
                .ВычисленноеЗначениеВСИ = 0.0 ' aParametersChannelMKIO(I)
                .ВычисленноеПереведенноеЗначение = 0.0 ' aParametersChannelMKIO(I)
                .РазмерностьСИ = aParametersChannelMKIO(I).UnitOfMeasure
                .РазмерностьВыходная = aParametersChannelMKIO(I).UnitOfMeasure
                .НакопленноеЗначение = 0.0 ' aParametersChannelMKIO(I)
                .ИндексКаналаИзмерения = aParametersChannelMKIO(I).NumberChannel
                .ДопускМинимум = aParametersChannelMKIO(I).LowerLimit
                .ДопускМаксимум = aParametersChannelMKIO(I).UpperLimit
                .РазносУмин = aParametersChannelMKIO(I).RangeYmin
                .РазносУмакс = aParametersChannelMKIO(I).RangeYmax
                .АварийноеЗначениеМин = aParametersChannelMKIO(I).AlarmValueMin
                .АварийноеЗначениеМакс = aParametersChannelMKIO(I).AlarmValueMax
                .Видимость = aParametersChannelMKIO(I).IsVisible
                .ВидимостьРегистратор = aParametersChannelMKIO(I).IsVisibleRegistration
            End With

            gMainFomMdiParent.Manager.CalculatedDataTable.AddРасчетныеПараметрыRow(newRow)
        Next

        'сохранить изменения
        gMainFomMdiParent.Manager.SaveTable()
    End Sub

    Const IndexAI222 As Single = 10000 ' признак расчётного параметра

    ''' <summary>
    ''' считать из базы Channels параметры МКИО и если есть копировать в массив признаки
    ''' </summary>
    Public Sub CopySettingsFromChannelsToAI222(inPathChannels As String, inChannelLast As String, inPathDBase_MKIO_AI222 As String)
        Dim I, countChannels As Integer
        Dim nameParameter As String
        Dim cn As OleDbConnection = Nothing
        Dim cmd As OleDbCommand
        Dim rdr As OleDbDataReader
        Dim odaDataAdapter As OleDbDataAdapter
        Dim dtDataTable As New DataTable
        Dim cb As OleDbCommandBuilder
        Dim strSQL As String = $"SELECT COUNT(*) FROM {inChannelLast} WHERE Погрешность={IndexAI222.ToString}"

        Try
            cn = New OleDbConnection(BuildCnnStr(PROVIDER_JET, inPathChannels))
            cn.Open()
            cmd = cn.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQL
            countChannels = CInt(cmd.ExecuteScalar)

            If countChannels > 0 Then
                Dim arrПараметрыChannel(countChannels - 1) As CloneOfPropertyChannel

                I = 0
                strSQL = "SELECT НаименованиеПараметра, Погрешность, РазносУмин, РазносУмакс, АварийноеЗначениеМин, АварийноеЗначениеМакс, Блокировка, Видимость, ВидимостьРегистратор " &
                        "FROM " & inChannelLast &
                        " WHERE Погрешность=" & IndexAI222.ToString
                ' 100 признак каналов АИ222
                cmd.CommandText = strSQL
                rdr = cmd.ExecuteReader

                Do While (rdr.Read)
                    arrПараметрыChannel(I).NameParameter = CStr(rdr("НаименованиеПараметра"))
                    arrПараметрыChannel(I).RangeYmin = CShort(rdr("РазносУмин"))
                    arrПараметрыChannel(I).RangeYmax = CShort(rdr("РазносУмакс"))
                    arrПараметрыChannel(I).AlarmValueMin = CSng(rdr("АварийноеЗначениеМин"))
                    arrПараметрыChannel(I).AlarmValueMax = CSng(rdr("АварийноеЗначениеМакс"))
                    arrПараметрыChannel(I).Blocking = CBool(rdr("Блокировка"))
                    arrПараметрыChannel(I).IsVisible = CBool(rdr("Видимость"))
                    arrПараметрыChannel(I).IsVisibleRegistration = CBool(rdr("ВидимостьРегистратор"))
                    I += 1
                Loop

                rdr.Close()
                cn.Close()

                ' считать из базы АИ222 АтрибутыВидимости и поиск по именам и обновить поля РазносУмин РазносУмакс АварийноеЗначениеМин АварийноеЗначениеМакс Блокировка Видимость ВидимостьРегистратор
                ' обновить базу
                cn = New OleDbConnection(BuildCnnStr(PROVIDER_JET, inPathDBase_MKIO_AI222))
                cn.Open()
                strSQL = "SELECT * FROM АтрибутыВидимости;"
                odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
                odaDataAdapter.Fill(dtDataTable)

                If dtDataTable.Rows.Count > 0 Then
                    ' должны быть только по 1 на данном запуске
                    For Each itemDataRow As DataRow In dtDataTable.Rows
                        nameParameter = CStr(itemDataRow("НаименованиеПараметра"))
                        For I = 0 To countChannels - 1
                            If arrПараметрыChannel(I).NameParameter = nameParameter Then
                                itemDataRow("НаименованиеПараметра") = arrПараметрыChannel(I).NameParameter
                                itemDataRow("РазносУмин") = arrПараметрыChannel(I).RangeYmin
                                itemDataRow("РазносУмакс") = arrПараметрыChannel(I).RangeYmax
                                itemDataRow("АварийноеЗначениеМин") = arrПараметрыChannel(I).AlarmValueMin
                                itemDataRow("АварийноеЗначениеМакс") = arrПараметрыChannel(I).AlarmValueMax
                                itemDataRow("Блокировка") = arrПараметрыChannel(I).Blocking
                                itemDataRow("Видимость") = arrПараметрыChannel(I).IsVisible
                                itemDataRow("ВидимостьРегистратор") = arrПараметрыChannel(I).IsVisibleRegistration
                                Exit For
                            End If
                        Next I
                    Next

                    cb = New OleDbCommandBuilder(odaDataAdapter)
                    odaDataAdapter.Update(dtDataTable)
                    dtDataTable.AcceptChanges()
                End If
            End If ' countChannels > 0

            cn.Close()

            Thread.Sleep(500)
            Application.DoEvents()
        Catch ex As Exception
            Dim caption As String = $"Процедура <{NameOf(CopySettingsFromChannelsToAI222)}> - " & ex.Source
            Dim text As String = ex.ToString
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Добавить параметры в базу ChannelNNN.
    ''' </summary>
    Public Sub InsertFromAI222ToChannels(inPathChannels As String, inChannelLast As String)
        Dim J As Integer
        Dim cn As OleDbConnection = Nothing
        Dim strSQL As String
        Dim odaDataAdapter As OleDbDataAdapter
        Dim dtDataTable As New DataTable
        Dim newDataRow As DataRow
        Dim cb As OleDbCommandBuilder

        Try
            ' 5 делаем запрос на выборку(вставку) и добавляем параметры в базу ChannelNNN
            cn = New OleDbConnection(BuildCnnStr(PROVIDER_JET, inPathChannels))
            cn.Open()
            ' 1000 признак каналов АИ222
            strSQL = $"SELECT * FROM {inChannelLast} ORDER BY НомерПараметра" ' & " WHERE Погрешность=" & indexАИ222.tostring
            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            dtDataTable = New DataTable
            odaDataAdapter.Fill(dtDataTable)
            ' узнать последий номер канала в таблице стенда
            J = CInt(dtDataTable.Rows(dtDataTable.Rows.Count - 1).Item("НомерПараметра")) + 1

            Dim listNameRows As New List(Of String)
            For Each row As DataRow In dtDataTable.Rows
                listNameRows.Add(CStr(row("НаименованиеПараметра")))
            Next

            For I As Integer = 0 To mCountParametersMKIO - 1
                newDataRow = dtDataTable.NewRow
                newDataRow.BeginEdit()
                newDataRow("НомерПараметра") = J

                'If listNameRows.Where(Function(item) item = aParametersChannelMKIO(I).NameParameter).Count <> 0 Then
                If listNameRows.Contains(aParametersChannelMKIO(I).NameParameter) Then
                    MessageBox.Show($"Невозможно дабавить канал с именем <{aParametersChannelMKIO(I).NameParameter}>" & vbCrLf &
                                "из базы каналов АИ222, т.к. в текущей базе каналов уже содержится с таким именем." & vbCrLf &
                                $"Канал будет добавлен под именем <{J.ToString}>." & vbCrLf &
                                "Приведите имена в соответствие и повторно запустите программу.",
                                "Конфликт имён каналов", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    newDataRow("НаименованиеПараметра") = J.ToString
                Else
                    newDataRow("НаименованиеПараметра") = aParametersChannelMKIO(I).NameParameter
                End If

                newDataRow("НомерКанала") = aParametersChannelMKIO(I).NumberChannel
                newDataRow("НомерУстройства") = aParametersChannelMKIO(I).NumberDevice
                newDataRow("НомерМодуляКорзины") = 0
                newDataRow("НомерКаналаМодуля") = 0
                newDataRow("ТипПодключения") = "DIFF"
                newDataRow("НижнийПредел") = 0
                newDataRow("ВерхнийПредел") = 0
                newDataRow("ТипСигнала") = "DC"
                newDataRow("НомерФормулы") = 1
                newDataRow("СтепеньАппроксимации") = 1
                newDataRow("A0") = 0
                newDataRow("A1") = 0
                newDataRow("A2") = 0
                newDataRow("A3") = 0
                newDataRow("A4") = 0
                newDataRow("A5") = 0
                newDataRow("Смещение") = 0

                newDataRow("КомпенсацияХС") = False
                newDataRow("ЕдиницаИзмерения") = aParametersChannelMKIO(I).UnitOfMeasure
                newDataRow("ДопускМинимум") = aParametersChannelMKIO(I).LowerLimit
                newDataRow("ДопускМаксимум") = aParametersChannelMKIO(I).UpperLimit
                newDataRow("РазносУмин") = aParametersChannelMKIO(I).RangeYmin
                newDataRow("РазносУмакс") = aParametersChannelMKIO(I).RangeYmax
                newDataRow("АварийноеЗначениеМин") = aParametersChannelMKIO(I).AlarmValueMin
                newDataRow("АварийноеЗначениеМакс") = aParametersChannelMKIO(I).AlarmValueMax
                newDataRow("Блокировка") = aParametersChannelMKIO(I).Blocking
                newDataRow("Дата") = Date.Today.ToShortDateString

                newDataRow("Видимость") = aParametersChannelMKIO(I).IsVisible
                newDataRow("ВидимостьРегистратор") = aParametersChannelMKIO(I).IsVisibleRegistration
                newDataRow("Погрешность") = IndexAI222
                newDataRow("Примечания") = Left(aParametersChannelMKIO(I).Description, 99) '100)
                newDataRow.EndEdit()
                dtDataTable.Rows.Add(newDataRow)
                J += 1
            Next

            cb = New OleDbCommandBuilder(odaDataAdapter)
            odaDataAdapter.Update(dtDataTable)
            cn.Close()

            'odaDataAdapter.InsertCommand = cb.GetInsertCommand
            '' Update Database with OleDbDataAdapter
            'odaDataAdapter.Update(dtDataTable)
            'dtDataTable.AcceptChanges()
            'odaDataAdapter.InsertCommand.Connection.Close()
            'cn.Close()

            Thread.Sleep(500)
            Application.DoEvents()

        Catch ex As Exception
            Dim caption As String = $"Процедура <{NameOf(InsertFromAI222ToChannels)}> - " & ex.Source
            Dim text As String = ex.Message
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Private Function ПеревестиЕдИзмерения(ByVal inUnitOfMeasure As String) As String
        ' outUnitOfMeasure:
        ' "%";"K";"атм";"бар";"Вольт";"град (рад)";"град С";"Деления";"дин/см^2";"кг/кгс*час";"кг/с";"кг/час";"кгс";"кгс/м^2";"кгс/см^2";
        ' "кПа";"мм";"мм.вод.ст";"мм.рт.ст";"Мпа";"Н/см^2";"нет";"Па";

        Dim outUnitOfMeasure As String = "Деления"

        Select Case inUnitOfMeasure
            Case "кгс/м^2", "кгс/см2", "л", "град (рад)", "кг/с"
                ' КлассДавление
                outUnitOfMeasure = "кгс/м^2"
                Exit Select
            Case "%", "%/с"
                ' КлассОбороты
                outUnitOfMeasure = "%"
                Exit Select
            Case "кгс", "км/ч"
                ' КлассОбороты
                outUnitOfMeasure = "кгс"
                Exit Select
            Case "Вольт", "мА", "мкА"
                ' КлассТок
                outUnitOfMeasure = "Вольт"
                Exit Select
            Case "град С", "град С/с", "K"
                ' КлассТемпература
                outUnitOfMeasure = "град С"
                Exit Select
            Case "мм", "мм/с", "g", "м", "атм", "бар", "кПа", "Мпа", "Па", "мм.вод.ст", "мм.рт.ст"
                ' КлассСтолбы
                outUnitOfMeasure = "мм"
                Exit Select
            Case "кг/час", "кг/ч", "нет"
                ' КлассВибрация
                outUnitOfMeasure = "кг/час"
                Exit Select
            Case "Деления", "градус", "ед", "дин/см^2", "кг/кгс*час", "кгс/см^2", "Н/см^2"
                ' КлассРасход
                outUnitOfMeasure = "Деления"
                Exit Select
            Case Else
                ' КлассРасход
                outUnitOfMeasure = "Деления"
                Exit Select
        End Select

        Return outUnitOfMeasure
    End Function

End Class