Imports System.Drawing
Imports System.IO
Imports System.Runtime.Serialization.Json
Imports System.Threading
Imports System.Windows.Forms
Imports AI222.ManagerMKIO
Imports BaseForm
Imports MathematicalLibrary
Imports NationalInstruments.Net
Imports NationalInstruments.UI
Imports NationalInstruments.UI.WindowsForms

Public Class FormOPCclient
    ''' <summary>
    ''' Все значения МКИО от DataSocketOPCWriter
    ''' </summary>
    Private TagValue As Integer()
    Private ControlsSize As New Dictionary(Of Control, Size)
    Private Const Separator As String = "\"
    Private Const DBase_MKIO_AI222 As String = "МКИО_АИ222.mdb"
    Private Const FileControlParameters As String = "ControlParameters.json"
    Private OPC_URL As String = "dstp://localhost/OPC"
    Private mMKIO As ManagerMKIO

    ''' <summary>
    ''' Счетчик без обновлений
    ''' </summary>
    Private counterWithoutRenewalOPC As Integer
    ''' <summary>
    ''' Счетчик для передачи Tt раз в 2 секунды
    ''' </summary>
    Private counterDataSocketWriter As Integer
    ''' <summary>
    ''' Счетчик для обновления всех индикаторов раз в 1 секунды
    ''' </summary>
    Private counterUpdateIndicator As Integer
    ''' <summary>
    ''' Связь прервана
    ''' </summary>
    Private isConnectionInterrupted As Boolean
    ''' <summary>
    ''' Ограничить число наблюдаемых параметров и соответсвенно контролов для них
    ''' </summary>
    Private Const conLimitControls As Integer = 30
    Private pathDBase_MKIO_AI222 As String
    Private pathOPCWriter As String
    Private pathChannels As String
    Private pathFileControlParameters As String
    Private Const conStatusLabelMessage As String = "StatusLabelMessage"
    Private Const conStatusLabelStend As String = "StatusLabelStend"
    ''' <summary>
    ''' Частота сбора регистратора
    ''' </summary>
    Private frequency As Integer
    'Private rand As New Random(CInt(Date.Now.Ticks And &HFFFF))
    ''' <summary>
    ''' Массив выборочных параметров для контроля при сериализации и дессириализации
    ''' </summary>
    Private arrControlParameter As ParameterForVisualization()
    Private lvSelectedIndices, lvPreviousSelectedIndices As Integer
    Private img As Bitmap
    Private hotSpot As Point

#Region "Форма"
    Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Private Sub FormOPCclient_Load(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Me.Load
        gMainFomMdiParent = CType(MdiParent, FrmMain)

        TuneTagsAI222()
        pathOPCWriter = GetIni(Path.Combine(PathResourses, "Опции.ini"), "АИ222", "OPCWriter", "Неправильный путь")
        TextPathOPCWriter.Text = pathOPCWriter
        pathDBase_MKIO_AI222 = Path.Combine(PathResourses, DBase_MKIO_AI222)
        pathFileControlParameters = Path.Combine(PathResourses, FileControlParameters)
        pathChannels = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(PathResourses)), "Channels.mdb")
        CheckFiles()

        mMKIO = New ManagerMKIO
        mMKIO.LoadParameters(pathDBase_MKIO_AI222)
        'ReDim_TagValue(mMKIO.CountTagsMKIO - 1)
        Re.Dim(TagValue, mMKIO.CountTagsMKIO - 1)

        SetOPC_URL()
        InitializeListViews()
        FlowLayoutPanelControlsResize()

        AllowDrop = True
        lvwSource.AllowDrop = True
        lvwReceiver.AllowDrop = True

        ' избавиться от мерцания
        DoubleBuffered = True
    End Sub

    Private Sub SetOPC_URL()
        If InStr(1, pathOPCWriter, "\\") <> 0 Then ' другой компьютер
            OPC_URL = Mid$(pathOPCWriter, 3, InStr(3, pathOPCWriter, Separator) - 3)
            OPC_URL = $"dstp://{OPC_URL}/OPC"
        Else ' локальный компьютер
            OPC_URL = "dstp://localhost/OPC"
        End If
    End Sub

    Public Sub StartTimerOPC()
        StatusBar.Items(conStatusLabelStend).Text = "Стенд №: " & gMainFomMdiParent.myClassCalculation.TuningParam.StandNumber.ЦифровоеЗначение.ToString
        StatusBar.Items(conStatusLabelMessage).Text = "Инициализация обмена данных с УИМС222"

        frequency = Convert.ToInt32(gMainFomMdiParent.myClassCalculation.TuningParam.Frequency.ЦифровоеЗначение)
        TimerOPC.Interval = Convert.ToInt32((1 / frequency) * 1000) ' с такой же частотой как и производится регистрация
        'TagValueHashCodeOld = 0 ' чтобы перезаписать
        TimerCheckDataSocketOPCWriter.Enabled = True
    End Sub

    Private Sub FormOPCclient_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If Not gMainFomMdiParent.IsWindowClosed Then e.Cancel = True

        'Dim Cancel As Boolean = e.Cancel
        'Dim UnloadMode As System.Windows.Forms.CloseReason = e.CloseReason
        TimerOPC.Enabled = False
        Dim channelLast As String = "Channel" & gMainFomMdiParent.myClassCalculation.TuningParam.StandNumber.ЦифровоеЗначение.ToString  ' имя последней таблицы каналов данного стенда
        mMKIO.CopySettingsFromChannelsToAI222(pathChannels, channelLast, pathDBase_MKIO_AI222)
    End Sub

    Private Sub FormOPCclient_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        DataSocketOPCReaderDisconnect()
        DataSocketWriter.Disconnect()
        mMKIO = Nothing
        gMainFomMdiParent = Nothing
    End Sub

    ''' <summary>
    ''' Проверка наличия файлов базы данных и OPCWriter.exe
    ''' </summary>
    Private Sub CheckFiles()
        If FileNotExists(pathOPCWriter) Then
            MessageBox.Show($"В каталоге <{pathOPCWriter}> нет файла OPCWriter.exe!{vbCrLf}Укажите путь к OPCWriter.exe",
                            NameOf(CheckFiles), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ButtonPathOPCWriter_Click(ButtonPathOPCWriter, New EventArgs)
        End If

        If FileNotExists(pathDBase_MKIO_AI222) Then
            MessageBox.Show($"В каталоге нет файла {DBase_MKIO_AI222}!",
                            NameOf(CheckFiles), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Environment.Exit(0) ' End
        End If
    End Sub

    ''' <summary>
    ''' Заполнить Расчетные параметры на основании каналов МКИО 'Дискретное слово' и 'Аналоговый'
    ''' </summary>
    Public Sub PopulateCalculatedParameters()
        Dim name As String

        For I As Integer = 0 To mMKIO.GetParameters.GetUpperBound(0)
            name = mMKIO.GetParameters(I).NameParameter
            gMainFomMdiParent.myClassCalculation.CalculatedParam.CalcDictionary.Add(name, New Parameter With {.Name = name})
        Next

        PopulateSourceListView()
    End Sub

    Private Const columnWidth As Integer = 100
    Private Sub InitializeListViews()
        InitializeListViews(lvwSource)
        InitializeListViews(lvwReceiver)
    End Sub
    Private Sub InitializeListViews(inListWiew As ListView)
        inListWiew.Items.Clear()
        inListWiew.Columns.Clear()
        inListWiew.Columns.Add("Параметр", columnWidth, HorizontalAlignment.Left)
        inListWiew.Columns.Add("Назначение", inListWiew.Width - inListWiew.Columns(0).Width - 8, HorizontalAlignment.Left)
    End Sub
    Private Sub ListViewResize()
        If Me.IsHandleCreated Then
            ListViewResize(lvwSource)
            ListViewResize(lvwReceiver)
        End If
    End Sub
    Private Sub ListViewResize(inListWiew As ListView)
        inListWiew.Columns(0).Width = columnWidth
        inListWiew.Columns(1).Width = inListWiew.Width - inListWiew.Columns(0).Width - 8
    End Sub
#End Region

#Region "Events"
    Private Sub FlowLayoutPanelControls_Resize(sender As Object, e As EventArgs) Handles FlowLayoutPanelControls.Resize
        FlowLayoutPanelControlsResize()
    End Sub

    Private Sub FlowLayoutPanelControlsResize()
        ListViewResize()

        If Me.IsHandleCreated AndAlso ControlsSize.Count > 0 Then
            Const modelForScalling As Double = 660.0
            Dim factor As Double = Math.Sqrt(FlowLayoutPanelControls.Width * FlowLayoutPanelControls.Height)

            If factor > modelForScalling Then
                Dim scalling As Double = factor / modelForScalling

                For Each itemControl As Control In FlowLayoutPanelControls.Controls
                    itemControl.Width = Convert.ToInt32(ControlsSize(itemControl).Width * scalling)
                    itemControl.Height = Convert.ToInt32(ControlsSize(itemControl).Height * scalling)
                Next
            Else
                For Each itemControl As Control In FlowLayoutPanelControls.Controls
                    itemControl.Size = ControlsSize(itemControl)
                Next
            End If
        End If
    End Sub

#Region "OPC"
    Private Sub ButtonPathOPCWriter_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles ButtonPathOPCWriter.Click
        With OpenFileDialogPuth
            .FileName = vbNullString
            .Title = "Поиск программы OPCWriter.exe поставщика данных тегов МКИО"
            .InitialDirectory = "c:\"
            .CheckFileExists = True
            .DefaultExt = "exe"
            .RestoreDirectory = True
            ' установить флаг атрибутов
            .Filter = "OPCWriter (*.exe)|*.exe"

            If .ShowDialog = DialogResult.OK Then
                If Len(.FileName) = 0 Then Exit Sub

                pathOPCWriter = .FileName
                TextPathOPCWriter.Text = pathOPCWriter
                WriteINI(Path.Combine(PathResourses, "Опции.ini"), "АИ222", "OPCWriter", pathOPCWriter)
                SetOPC_URL()
                DataSocketOPCReaderDisconnectAndConnect()
            End If
        End With
    End Sub

    Private Sub BButtonOPCReaderUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonOPCReaderUpdate.Click
        DataSocketOPCReader.Update()
    End Sub

    Private Sub ButtonOPCReaderConnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonOPCReaderConnect.Click
        DataSocketOPCReaderConnect()
    End Sub

    Private Sub ButtonOPCReaderDisconnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonOPCReaderDisconnect.Click
        DataSocketOPCReaderDisconnect()
    End Sub

    Public Sub DataSocketOPCReaderDisconnect()
        DataSocketOPCReader.Disconnect()
    End Sub

    Private Sub DataSocketOPCReaderConnect()
        DataSocketOPCReader.Connect(OPC_URL, AccessMode.ReadAutoUpdate)
    End Sub

    Private Sub DataSocketOPCReader_ConnectionStatusUpdated(ByVal sender As Object, ByVal e As ConnectionStatusUpdatedEventArgs) Handles DataSocketOPCReader.ConnectionStatusUpdated
        OPCStatusText.Text = e.Message

        If DataSocketOPCReader.ConnectionStatus = ConnectionStatus.ConnectionActive Then
            ButtonOPCReaderConnect.Enabled = False
            ButtonOPCReaderDisconnect.Enabled = True
            isConnectionInterrupted = False
        Else
            ButtonOPCReaderConnect.Enabled = True
            ButtonOPCReaderDisconnect.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Перход от модели проверки обновления по событию DataSocket 
    ''' к модели проверки обновления по таймеру.
    ''' </summary>
    ''' <param name="eventSender"></param>
    ''' <param name="eventArgs"></param>
    Private Sub TimerOPC_Tick(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles TimerOPC.Tick
        'Private Sub DataSocketOPCWriter_DataUpdated(ByVal sender As Object, ByVal e As NationalInstruments.Net.DataUpdatedEventArgs) Handles DataSocketOPCWriter.DataUpdated
        ReceiveDataOPC()
        WriteTemperatureToUIMS()
    End Sub

    ''' <summary>
    ''' Передать значение Тбокса как параметр Tt в УИМС по протоколу DataSocket
    ''' </summary>
    Private Sub WriteTemperatureToUIMS()
        counterDataSocketWriter += 1

        If counterDataSocketWriter > frequency * 2 Then
            counterDataSocketWriter = 0
            Dim Tbox As Double = gMainFomMdiParent.myClassCalculation.InputParam.Tбокса
            TextBoxTbox.Text = Format(Tbox, "f")

            If DataSocketWriter.IsConnected Then DataSocketWriter.Data.Value = CObj(Tbox)
        End If
    End Sub

    ''' <summary>
    ''' Получить значения тегов от OPC сервера АИ222
    ''' </summary>
    Private Sub ReceiveDataOPC()
        If DataSocketOPCReader.IsDataUpdated Then
            If IsArray(DataSocketOPCReader.Data.Value) Then
                Try
                    TagValue = CType(DataSocketOPCReader.Data.Value, Integer())
                    '' отладка
                    'Dim Value As Integer() = CType(DataSocketOPCReader.Data.Value, Integer())
                    'For I As Integer = 0 To mMKIO.CountTagsMKIO - 1
                    '    'TagValue(I) = DataSocketOPCReader.Data.Value(I) ' I * rand.NextDouble ' отладка
                    '    TagValue(I) = Value(I) ' I * rand.NextDouble ' отладка
                    'Next
                    'For I As Integer = 0 To mMKIO.CountTagsMKIO - 1
                    '    TagValue(I) = Convert.ToInt32(I * rand.NextDouble) ' отладка
                    'Next
                Catch ex As Exception
                End Try
            End If

            counterWithoutRenewalOPC = 0
            isConnectionInterrupted = False

            If TextBoxНетСвязи.Enabled Then
                TextBoxНетСвязи.ForeColor = Color.Black
                TextBoxНетСвязи.BackColor = Color.Maroon
                TextBoxНетСвязи.Enabled = False
            End If
        Else
            counterWithoutRenewalOPC += 1
            If counterWithoutRenewalOPC > frequency * 2 Then
                counterWithoutRenewalOPC = 0
                TextBoxНетСвязи.Enabled = True
                TextBoxНетСвязи.BackColor = Color.Red
                TextBoxНетСвязи.ForeColor = Color.Yellow

                isConnectionInterrupted = True
                DataSocketOPCReaderDisconnectAndConnect()
            End If
        End If
    End Sub

    Private Sub DataSocketOPCReaderDisconnectAndConnect()
        DataSocketOPCReaderDisconnect()
        Application.DoEvents()
        Thread.Sleep(100)
        DataSocketOPCReaderConnect()
        Application.DoEvents()
        Thread.Sleep(100)
    End Sub

    Private Sub TimerCheckDataSocketOPCWriter_Tick(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles TimerCheckDataSocketOPCWriter.Tick
        DataSocketOPCReaderConnect()
        Thread.Sleep(500)
        Application.DoEvents()
        '    If DataSocketOPCWriter.Status = 2 And DataSocketOPCWriter.LastError = 0 Then 'Active
        If DataSocketOPCReader.LastError = 0 Then ' Active
            TimerCheckDataSocketOPCWriter.Enabled = False
            TimerOPC.Enabled = True
        End If

        DataSocketWriter.Connect(edURLTt.Text, AccessMode.WriteAutoUpdate)
    End Sub

    Private Sub ButtonWriterConnect_Click(sender As Object, e As EventArgs) Handles ButtonWriterConnect.Click
        ' Подключите соединение данных с источником и запрос данных.
        ' Текущие данные будут получены автоматически, когда связь установится.
        If DataSocketWriter.IsConnected Then DataSocketWriter.Disconnect()
        DataSocketWriter.Connect(edURLTt.Text, AccessMode.WriteAutoUpdate)
    End Sub

    Private Sub ButtonWriterDisconnect_Click(sender As Object, e As EventArgs) Handles ButtonWriterDisconnect.Click
        ' Разъедините DataSocket из источника, с которым связан.
        DataSocketWriter.Disconnect()
    End Sub

    Private Sub DataSocketWriter_ConnectionStatusUpdated(sender As Object, e As ConnectionStatusUpdatedEventArgs) Handles DataSocketWriter.ConnectionStatusUpdated
        ' соединение обновлено
        edStatusTt.Text = e.Message
    End Sub
#End Region

#End Region

#Region "Получить данные от OPC сервера АИ222"
    ''' <summary>
    ''' Декодировать значения из тегов от OPC сервера АИ222
    ''' </summary>
    Public Sub GetDataOPC()
        Dim indexBitWord, I As Integer

        For Each itemTagMKIO As TagMKIO In mMKIO
            With itemTagMKIO
                If .IsDecompressDiscreteWord Then
                    For indexBitWord = 0 To .CountsParametersInDiscretWord - 1
                        mMKIO.GetParameters(.Item(indexBitWord).IndexInArrayValue).ValueParameterMKIO = .DiscreteValue(TagValue(I), indexBitWord)
                        ' отладка
                        'mMKIO.GetParameters(.Item(indexBitWord).IndexInArrayValue).ValueParameterMKIO = .Item(indexBitWord).IndexInArrayValue + 1
                    Next
                Else
                    mMKIO.GetParameters(.IndexInArrayValue).ValueParameterMKIO = .PhysicalValue(TagValue(I))
                    ' отладка
                    'mMKIO.GetParameters(.IndexInArrayValue).ValueParameterMKIO = .IndexInArrayValue + 1
                End If
            End With

            I += 1
        Next

        UpdateCalculatedParameters()
    End Sub

    ''' <summary>
    ''' Обновить коллекцию расчётных параметров значениями параметров МКИО
    ''' </summary>
    Private Sub UpdateCalculatedParameters()
        With gMainFomMdiParent.myClassCalculation.CalculatedParam
            'For Each keysCalc As String In .CalcDictionary.Keys.ToArray
            '    .CalcDictionary(keysCalc).CalculatedValue = mMKIO.GetParameters(mMKIO.GetParameterByName(keysCalc).NumberChannel).ValueParameterMKIO
            'Next
            ' более быстрый способ
            For I As Integer = 0 To mMKIO.GetParameters.GetUpperBound(0)
                .CalcDictionary(mMKIO.GetParameters(I).NameParameter).CalculatedValue = mMKIO.GetParameters(I).ValueParameterMKIO
            Next
        End With
    End Sub

#End Region

#Region "Оновить индикаторы"
    ''' <summary>
    ''' Индикатор выведет самое первое неправильное значение.
    ''' Остальные неправильные не будут выведены для предотвращения мерцания.
    ''' </summary>
    Public Sub UpdateVisualControls()
        If syncPoint = 1 Then Exit Sub

        Dim sync As Integer = Interlocked.CompareExchange(syncPoint, 1, 0)
        If sync = 0 Then
            counterUpdateIndicator += 1

            If counterUpdateIndicator > frequency Then
                counterUpdateIndicator = 0

                Dim isShowError As Boolean = True

                With gMainFomMdiParent.myClassCalculation
                    For Each par As Parameter In .CalculatedParam
                        CheckParameterInRange(par.Name, isShowError)
                    Next

                    ' выборочно для входных параметров
                    'ПроверкаПараметрВДиапазоне(InputParameters.conTБОКСА,
                    '                       .InputParam.Tбокса,
                    '                       isShowError,
                    '                       TankP,
                    '                       NumericEditTank)
                End With

                ' ошибок не было, значить погасить индикатор, если он зажегся ло этого
                TextError.Visible = Not isShowError
                CheckAlarmLimit()
            End If

            syncPoint = 0  ' освободить
        End If
    End Sub
    ''' <summary>
    ''' Проверка Параметр В Диапазоне
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="isShowError"></param>
    Private Sub CheckParameterInRange(ByVal name As String, ByRef isShowError As Boolean)
        With gMainFomMdiParent.myClassCalculation
            CheckParameterInRange(name,
                                .CalculatedParam(name),
                                isShowError,
                                .CalculatedParam.CalcDictionary(name).ControlNumericPointer,
                                .CalculatedParam.CalcDictionary(name).ControlNumericEdit)
        End With
    End Sub
    ''' <summary>
    ''' Проверка Параметр В Диапазоне (перегрузка)
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="inCurrentValue"></param>
    ''' <param name="isShowError"></param>
    ''' <param name="inINumericPointer"></param>
    ''' <param name="inNumericEdit"></param>
    Private Sub CheckParameterInRange(ByVal name As String,
                                    ByVal inCurrentValue As Double,
                                    ByRef isShowError As Boolean,
                                    ByRef inINumericPointer As INumericPointer,
                                    ByRef inNumericEdit As NumericEdit)
        ' NaN == NaN: False применять нельзя
        ' Double.NaN.Equals(Double.NaN) - можно NaN.Equals(NaN): True
        ' Double.IsNaN(Double.NaN))- можно IsNaN: True

        If Double.IsNaN(inCurrentValue) OrElse Double.IsNegativeInfinity(inCurrentValue) OrElse Double.IsPositiveInfinity(inCurrentValue) Then
            'Throw New ArgumentOutOfRangeException(name & " вне диапазона")
            If isShowError Then
                ShowError($"Ошибка расчета: параметр <{name}> не вычислен!")
                isShowError = False
            End If

            inCurrentValue = 0.0
        End If

        If inINumericPointer IsNot Nothing Then inINumericPointer.Value = inCurrentValue
        If inNumericEdit IsNot Nothing Then inNumericEdit.Value = inCurrentValue
    End Sub

    ''' <summary>
    ''' Проверка на настроенные аварийные допуски параметров в таблице измеренных параметров.
    ''' Вывод собщения в статусной строке. Аварийный индикатор не нужен.
    ''' </summary>
    Private Sub CheckAlarmLimit()
        StatusBar.Items(conStatusLabelMessage).Text = "В норме"

        With gMainFomMdiParent.Manager
            Dim valueT As Double

            For Each rowИзмеренныйПараметр As BaseFormDataSet.ИзмеренныеПараметрыRow In .MeasurementDataTable.Rows
                valueT = rowИзмеренныйПараметр.ЗначениеВСИ
                If valueT < rowИзмеренныйПараметр.ДопускМинимум OrElse valueT > rowИзмеренныйПараметр.ДопускМаксимум Then
                    ' вывести сообщение об обрыве
                    StatusBar.Items(conStatusLabelMessage).Text = $"Значение параметра {rowИзмеренныйПараметр.ИмяПараметра} = {Format(valueT, "##,##0.00")} вне допуска ({Format(rowИзмеренныйПараметр.ДопускМинимум, "F")} : {Format(rowИзмеренныйПараметр.ДопускМаксимум, "F")})!"
                End If
            Next

            For Each rowРасчетныйПараметр As BaseFormDataSet.РасчетныеПараметрыRow In .CalculatedDataTable.Rows
                valueT = rowРасчетныйПараметр.ВычисленноеЗначениеВСИ
                If valueT < rowРасчетныйПараметр.ДопускМинимум OrElse valueT > rowРасчетныйПараметр.ДопускМаксимум Then
                    ' вывести сообщение об обрыве
                    StatusBar.Items(conStatusLabelMessage).Text = $"Значение параметра {rowРасчетныйПараметр.ИмяПараметра} = {Format(valueT, "##,##0.00")} вне допуска ({Format(rowРасчетныйПараметр.ДопускМинимум, "F")} : {Format(rowРасчетныйПараметр.ДопускМаксимум, "F")})!"
                End If
            Next
        End With
    End Sub

    Public Sub ShowError(message As String)
        TextError.Text = message
        TextError.Visible = True
    End Sub
#End Region

#Region "Выборочные контролы"
    Enum Side
        Left
        Right
    End Enum

    Enum MeterControl
        Tank
        MeterL
        MeterR
        Gauge
        Thermometer
        Slide
    End Enum

    ''' <summary>
    ''' Сериализация массива настроек для выборочного отображения в файл.
    ''' </summary>
    ''' <param name="inArrControlParameter"></param>
    Private Sub SerializerControlParameters(inArrControlParameter As ParameterForVisualization())
        Dim jsonFormatter As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(ParameterForVisualization()))

        Using fs As FileStream = New FileStream(pathFileControlParameters, FileMode.Create)
            jsonFormatter.WriteObject(fs, inArrControlParameter)
        End Using
    End Sub

    ''' <summary>
    ''' Десериализация из файла в массив настроек для выборочного отображения.
    ''' </summary>
    Private Sub ReadControlParameters()
        Dim jsonFormatter As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(ParameterForVisualization()))

        Using fs As FileStream = New FileStream(pathFileControlParameters, FileMode.Open)
            arrControlParameter = CType(jsonFormatter.ReadObject(fs), ParameterForVisualization())
            'For Each p As ControlParameter In arrControlParameter
            '    Console.WriteLine("Имя: {0} --- Возраст: {1}", p.Name, p.Age)
            'Next
        End Using
    End Sub

    Private syncPoint As Integer = 0 ' для синхронизации
    ''' <summary>
    ''' Динамическое заполнение панели контроллами выборочных параметров
    ''' </summary>
    Public Sub PopulateControlToFlowLayoutPanel()
        If syncPoint = 1 Then Exit Sub

        Dim sync As Integer = Interlocked.CompareExchange(syncPoint, 1, 0)

        If sync = 0 Then
            ' Перед добавлением очистить коллекции
            Me.FlowLayoutPanelControls.Controls.Clear()
            ControlsSize.Clear()

            ' считать файл XML настроек параметров для визуального контроля (он перезаписывается при изменении отмеченных чеком каналов)
            If File.Exists(pathFileControlParameters) Then
                ReadControlParameters()
                gMainFomMdiParent.myClassCalculation.CalculatedParam.UnBindingAllControls()
                PopulateSourceListView()

                If arrControlParameter.Count > 0 Then
                    ' при считывании отключается событие изменения Check, после загрузки включается и производится добавление
                    ClearListViewSource()
                    Me.FlowLayoutPanelControls.SuspendLayout()

                    For Each itemPar As ParameterForVisualization In arrControlParameter
                        ' в цикле по параметрам проверяется наличие в .CalcDictionary и если есть добавление
                        If gMainFomMdiParent.myClassCalculation.CalculatedParam.CalcDictionary.ContainsKey(itemPar.Name) Then
                            ' настройка пунктов в панелях выбора параметров
                            AddParameterPanel(CreateINumericPointer(mMKIO.GetParameters(mMKIO.GetParameterByName(itemPar.Name).NumberChannel)),
                                              CreateNumericEdit)
                        End If
                    Next

                    Me.FlowLayoutPanelControls.ResumeLayout(False)
                End If
            End If

            FlowLayoutPanelControlsResize()

            syncPoint = 0  ' освободить
        End If
    End Sub

    ''' <summary>
    ''' Добавление панели с вложенным индикатором и цифровым полем в FlowLayoutPanelControls.
    ''' Связывание расчётного параметра с этими контролами.
    ''' </summary>
    ''' <param name="inTupleControlSize"></param>
    ''' <param name="inNumericEdit"></param>
    Private Sub AddParameterPanel(inTupleControlSize As Tuple(Of Control, Size), inNumericEdit As NumericEdit)
        'Private Sub AddParameterPanel(inTupleControlSize As (numericPointer As Control, panelSize As Size), inNumericEdit As NumericEdit)

        Dim newPanel As New Panel()

        'Me.FlowLayoutPanelControls.SuspendLayout()
        With newPanel
            .SuspendLayout()
            .BorderStyle = BorderStyle.Fixed3D
            '.Controls.Add(inTupleControlSize.numericPointer)
            .Controls.Add(inTupleControlSize.Item1)
            .Controls.Add(inNumericEdit)
            .Location = New Point(3, 3)
            .Name = "Panel" & FlowLayoutPanelControls.Controls.Count
            '.Size = inTupleControlSize.panelSize
            .Size = inTupleControlSize.Item2
            .ResumeLayout(False)
        End With

        Me.FlowLayoutPanelControls.Controls.Add(newPanel)
        'Me.FlowLayoutPanelControls.ResumeLayout(False)

        'gMainFomMdiParent.myClassCalculation.CalculatedParam.BindingWithControls(inTupleControlSize.numericPointer.Tag.ToString,
        '                                                                         inTupleControlSize.numericPointer,
        '                                                                         inNumericEdit)
        gMainFomMdiParent.myClassCalculation.CalculatedParam.BindingWithControls(inTupleControlSize.Item1.Tag.ToString,
                                                                                 CType(inTupleControlSize.Item1, INumericPointer),
                                                                                 inNumericEdit)

        ControlsSize(newPanel) = newPanel.Size
    End Sub

    ''' <summary>
    ''' Фабрика создающая кортеж из контрола типа INumericPointer и размера для содержащей его панели.
    ''' </summary>
    ''' <param name="inParameter"></param>
    ''' <returns></returns>
    Private Function CreateINumericPointer(ByVal inParameter As TypeBaseParameter) As Tuple(Of Control, Size)
        'Private Function CreateINumericPointer(ByVal inParameter As TypeBaseParameter) As (numericPointer As Control, panelSize As Size)
        ' "%";"K";"атм";"бар";"Вольт";"град (рад)";"град С";"Деления";"дин/см^2";"кг/кгс*час";"кг/с";"кг/час";"кгс";"кгс/м^2";"кгс/см^2";
        ' "кПа";"мм";"мм.вод.ст";"мм.рт.ст";"Мпа";"Н/см^2";"нет";"Па";
        With inParameter
            Select Case .UnitOfMeasure
                Case "кгс/м^2"
                    ' КлассДавление' "кгс/м^2", "кгс/см2", "л", "град (рад)", "кг/с"
                    'Return (CreateTank(.NameParameter, .Description, .LowerLimit, .AlarmValueMin, .AlarmValueMax, .UpperLimit), GetSize(MeterControl.Tank))
                    Return New Tuple(Of Control, Size)(CreateTank(.NameParameter, .Description, .LowerLimit, .AlarmValueMin, .AlarmValueMax, .UpperLimit), GetSize(MeterControl.Tank))
                Case "%", "кгс"
                    ' КлассОбороты'"%", "%/с"
                    ' КлассОбороты'"кгс", "км/ч"
                    Return New Tuple(Of Control, Size)(CreateGauge(.NameParameter, .Description, .LowerLimit, .AlarmValueMin, .AlarmValueMax, .UpperLimit), GetSize(MeterControl.Gauge))
                Case "Вольт"
                    ' КлассТок'"Вольт", "мА", "мкА"
                    Return New Tuple(Of Control, Size)(CreateMeter(.NameParameter, .Description, Side.Left, .LowerLimit, .AlarmValueMin, .AlarmValueMax, .UpperLimit), GetSize(MeterControl.MeterL))
                Case "кг/час"
                    ' КлассВибрация'"кг/час", "кг/ч", "нет"
                    Return New Tuple(Of Control, Size)(CreateMeter(.NameParameter, .Description, Side.Right, .LowerLimit, .AlarmValueMin, .AlarmValueMax, .UpperLimit), GetSize(MeterControl.MeterR))
                Case "град С"
                    ' КлассТемпература '"град С", "град С/с", "K"
                    Return New Tuple(Of Control, Size)(CreateThermometer(.NameParameter, .Description, .LowerLimit, .AlarmValueMin, .AlarmValueMax, .UpperLimit), GetSize(MeterControl.Thermometer))
                Case "мм", "Деления"
                    ' КлассСтолбы' "мм", "мм/с", "g", "м", "атм", "бар", "кПа", "Мпа", "Па", "мм.вод.ст", "мм.рт.ст"
                    ' КлассРасход'"Деления", "градус", "ед", "дин/см^2", "кг/кгс*час", "кгс/см^2", "Н/см^2"
                    Return New Tuple(Of Control, Size)(CreateSlide(.NameParameter, .Description, .LowerLimit, .AlarmValueMin, .AlarmValueMax, .UpperLimit), GetSize(MeterControl.Slide))
                Case Else
                    ' КлассРасход
                    Return New Tuple(Of Control, Size)(CreateGauge(.NameParameter, .Description, .LowerLimit, .AlarmValueMin, .AlarmValueMax, .UpperLimit), GetSize(MeterControl.Gauge))
            End Select
        End With
    End Function

    Private Function GetImageIndex(ByVal inUnitOfMeasure As String) As Integer
        Select Case inUnitOfMeasure
            Case "кгс/м^2"
                ' КлассДавление' "кгс/м^2", "кгс/см2", "л", "град (рад)", "кг/с"
                Return MeterControl.Tank
            Case "%", "кгс"
                ' КлассОбороты'"%", "%/с"
                ' КлассОбороты'"кгс", "км/ч"
                Return MeterControl.Gauge
            Case "Вольт"
                ' КлассТок'"Вольт", "мА", "мкА"
                Return MeterControl.MeterL
            Case "кг/час"
                ' КлассВибрация'"кг/час", "кг/ч", "нет"
                Return MeterControl.MeterR
            Case "град С"
                ' КлассТемпература '"град С", "град С/с", "K"
                Return MeterControl.Thermometer
            Case "мм", "Деления"
                ' КлассСтолбы' "мм", "мм/с", "g", "м", "атм", "бар", "кПа", "Мпа", "Па", "мм.вод.ст", "мм.рт.ст"
                ' КлассРасход'"Деления", "градус", "ед", "дин/см^2", "кг/кгс*час", "кгс/см^2", "Н/см^2"
                Return MeterControl.Slide
            Case Else
                ' КлассРасход
                Return MeterControl.Gauge
        End Select
    End Function

    ''' <summary>
    ''' Определить размер панели содержащий индикатор в зависимости от его типа.
    ''' </summary>
    ''' <param name="inMeterControl"></param>
    ''' <returns></returns>
    Private Function GetSize(inMeterControl As MeterControl) As Size
        Select Case inMeterControl
            Case MeterControl.Tank, MeterControl.Slide
                Return New Size(80, 200)
            Case MeterControl.MeterL, MeterControl.MeterR
                Return New Size(120, 200)
            Case MeterControl.Gauge
                Return New Size(200, 200)
            Case MeterControl.Thermometer
                Return New Size(90, 200)
        End Select
    End Function

#Region "Создание индикаторов"
    Private Shared Sub Swap(Of T)(ByRef lhs As T, ByRef rhs As T)
        Dim temp As T
        temp = lhs
        lhs = rhs
        rhs = temp
    End Sub

    ''' <summary>
    ''' Заливка пределов диапазона.
    ''' </summary>
    ''' <param name="inMin"></param>
    ''' <param name="inLower"></param>
    ''' <param name="inAbove"></param>
    ''' <param name="inMax"></param>
    ''' <returns></returns>
    Private Function CreateScaleRangeFill(inMin As Double, inLower As Double, inAbove As Double, inMax As Double) As ScaleRangeFill()
        If inMin = inMax Then
            inMin = 0
            inMax = 1
        End If

        Dim range As Double = Math.Abs(inMax - inMin)

        If inLower = 0 AndAlso inAbove = 0 Then
            inLower = inMin + 0.1 * range
            inAbove = inMax - 0.1 * range
        End If

        If inMin >= inLower Then inLower = inMin + 0.1 * range
        If inAbove >= inMax Then inAbove = inMax - 0.1 * range

        'If inLower > inAbove Then Swap(Of Double)(inLower, inAbove)

        Dim ScaleRangeFillLower As New ScaleRangeFill With {
            .Range = New Range(inMin, inLower),
            .Style = ScaleRangeFillStyle.CreateGradientStyle(Color.Red, Color.Yellow, 0.5R)
        }
        Dim ScaleRangeFillNormal As New ScaleRangeFill With {
            .Range = New Range(inLower, inAbove),
            .Style = ScaleRangeFillStyle.CreateSolidStyle(Color.Lime)
        }
        Dim ScaleRangeFillAbove As New ScaleRangeFill With {
            .Range = New Range(inAbove, inMax),
            .Style = ScaleRangeFillStyle.CreateGradientStyle(Color.Yellow, Color.Red, 0.5R)
        }

        Return New ScaleRangeFill() {ScaleRangeFillLower, ScaleRangeFillNormal, ScaleRangeFillAbove}
    End Function

    Private Function CreateTank(inCaption As String, inDescription As String,
                                 inMin As Double, inLower As Double, inAbove As Double, inMax As Double) As Tank
        Dim newTank As New Tank()

        CType(newTank, System.ComponentModel.ISupportInitialize).BeginInit()
        With newTank
            .Caption = inCaption
            .Tag = inCaption
            .Dock = DockStyle.Fill
            .FillStyle = FillStyle.HorizontalGradient
            .ForeColor = Color.Lime
            .Location = New Point(0, 0)
            .MajorDivisions.TickColor = Color.Blue
            .MinorDivisions.TickColor = Color.Blue
            .Name = "Tank"
            .OutOfRangeMode = NumericOutOfRangeMode.CoerceToRange
            .Range = New Range(inMin, inMax)
            .RangeFills.AddRange(CreateScaleRangeFill(inMin, inLower, inAbove, inMax))
            .Size = New Size(76, 178)
            .Value = 0.0R
        End With
        CType(newTank, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolTipForm.SetToolTip(newTank, inDescription)

        Return newTank
    End Function

    Private Function CreateMeter(inCaption As String, inDescription As String, inSide As Side,
                                 inMin As Double, inLower As Double, inAbove As Double, inMax As Double) As Meter
        Dim newMeter As New Meter()

        CType(newMeter, System.ComponentModel.ISupportInitialize).BeginInit()
        With newMeter
            .AutoDivisionSpacing = False
            .Caption = inCaption
            .Tag = inCaption
            .DialColor = Color.Gray
            .Dock = DockStyle.Fill
            .ForeColor = Color.Lime
            .Location = New Point(0, 0)
            .MajorDivisions.Interval = 0.1R
            .MajorDivisions.TickColor = Color.Blue
            .MajorDivisions.TickLength = 8.0!
            .MinorDivisions.Interval = 0.05R
            .MinorDivisions.TickColor = Color.Blue
            .MinorDivisions.TickLength = 6.0!
            .Name = "Meter"
            .OutOfRangeMode = NumericOutOfRangeMode.CoerceToRange
            .PointerColor = Color.DarkGreen
            .Range = New Range(inMin, inMax)
            .RangeFills.AddRange(CreateScaleRangeFill(inMin, inLower, inAbove, inMax))

            Select Case inSide
                Case Side.Right
                    .ScaleArc = New Arc(300.0!, 125.0!)
                Case Side.Left
                    .ScaleArc = New Arc(225.0!, -90.0!)
            End Select

            .Size = New Size(116, 178)
            .SpindleColor = Color.DimGray
            .Value = 0.0R
        End With
        CType(newMeter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolTipForm.SetToolTip(newMeter, inDescription)

        Return newMeter
    End Function

    Private Function CreateGauge(inCaption As String, inDescription As String,
                                 inMin As Double, inLower As Double, inAbove As Double, inMax As Double) As Gauge
        Dim newGauge As New Gauge()

        CType(newGauge, System.ComponentModel.ISupportInitialize).BeginInit()
        With newGauge
            .AutoDivisionSpacing = False
            .Caption = inCaption
            .Tag = inCaption
            .DialColor = Color.Black
            .Dock = DockStyle.Fill
            .ForeColor = Color.Lime
            .Location = New Point(0, 0)
            .MajorDivisions.Interval = 0.2R
            .MajorDivisions.LabelFormat = New FormatString(FormatStringMode.Numeric, "0.0")
            .MajorDivisions.TickColor = Color.Blue
            .MinorDivisions.Interval = 0.05R
            .MinorDivisions.TickColor = Color.Blue
            .Name = "Gauge"
            .OutOfRangeMode = NumericOutOfRangeMode.CoerceToRange
            .PointerColor = Color.Lime
            .Range = New Range(inMin, inMax)
            .RangeFills.AddRange(CreateScaleRangeFill(inMin, inLower, inAbove, inMax))
            .ScaleArc = New Arc(230.0!, -280.0!)
            .Size = New Size(196, 178)
            .ToolTipFormat = New FormatString(FormatStringMode.Numeric, "0.0")
            .Value = 0.0R
        End With
        CType(newGauge, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolTipForm.SetToolTip(newGauge, inDescription)

        Return newGauge
    End Function

    Private Function CreateThermometer(inCaption As String, inDescription As String,
                                 inMin As Double, inLower As Double, inAbove As Double, inMax As Double) As Thermometer
        Dim newThermometer As New Thermometer()

        CType(newThermometer, System.ComponentModel.ISupportInitialize).BeginInit()
        With newThermometer
            .Caption = inCaption
            .Tag = inCaption
            .Dock = DockStyle.Fill
            .ForeColor = Color.Lime
            .Location = New Point(0, 0)
            .MajorDivisions.TickColor = Color.Blue
            .MinorDivisions.TickColor = Color.Blue
            .Name = "Thermometer"
            .OutOfRangeMode = NumericOutOfRangeMode.CoerceToRange
            .Range = New Range(inMin, inMax)
            .RangeFills.AddRange(CreateScaleRangeFill(inMin, inLower, inAbove, inMax))
            .Size = New Size(86, 178)
            .Value = 0.0R
        End With
        CType(newThermometer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolTipForm.SetToolTip(newThermometer, inDescription)

        Return newThermometer
    End Function

    Private Function CreateSlide(inCaption As String, inDescription As String,
                                 inMin As Double, inLower As Double, inAbove As Double, inMax As Double) As Slide
        Dim newSlide As New Slide()

        CType(newSlide, System.ComponentModel.ISupportInitialize).BeginInit()
        With newSlide
            .Caption = inCaption
            .Tag = inCaption
            .Dock = DockStyle.Fill
            .FillBackColor = Color.DarkOrange
            .FillMode = NumericFillMode.ToMaximum
            .ForeColor = Color.Lime
            .InteractionMode = LinearNumericPointerInteractionModes.Indicator
            .Location = New Point(0, 0)
            .MajorDivisions.Interval = 0.5R
            .MajorDivisions.TickColor = Color.Blue
            .MinorDivisions.Interval = 0.1R
            .MinorDivisions.TickColor = Color.Blue
            .Name = "Slide"
            .OutOfRangeMode = NumericOutOfRangeMode.CoerceToRange
            .PointerColor = Color.DarkRed
            .Range = New Range(inMin, inMax)
            .RangeFills.AddRange(CreateScaleRangeFill(inMin, inLower, inAbove, inMax))
            .Size = New Size(76, 178)
            .Value = 0.0R
        End With
        CType(newSlide, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolTipForm.SetToolTip(newSlide, inDescription)

        Return newSlide
    End Function

    Private Function CreateNumericEdit() As NumericEdit
        Dim newNumericEdit As New NumericEdit()

        CType(newNumericEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        With newNumericEdit
            .BackColor = Color.DimGray
            .BorderStyle = BorderStyle.None
            .Dock = DockStyle.Bottom
            .Font = New Font("Microsoft Sans Serif", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(204, Byte))
            .ForeColor = Color.Yellow
            .FormatMode = NumericFormatMode.CreateSimpleDoubleMode(2)
            .InteractionMode = NumericEditInteractionModes.Indicator
            .Location = New Point(0, 178)
            .Name = "NumericEdit"
            .OutOfRangeMode = NumericOutOfRangeMode.CoerceToRange
            .Size = New Size(76, 18)
            .TextAlign = HorizontalAlignment.Center
            .UpDownAlign = LeftRightAlignment.Left
        End With

        CType(newNumericEdit, System.ComponentModel.ISupportInitialize).EndInit()

        Return newNumericEdit
    End Function

#End Region

#End Region

#Region "Списки"
    Private WithEvents TimerEnableButtons As Windows.Forms.Timer ' серверный таймер работает в потоке приложения
    Private Const IntervalTimerEnableButtons As Integer = 2000

    ''' <summary>
    ''' Заполнить лист источник значениями каналов МКИО 
    ''' </summary>
    Private Sub PopulateSourceListView()
        Dim nameParameter As String

        lvwReceiver.Items.Clear()
        lvwSource.BeginUpdate()
        lvwSource.Items.Clear()

        ' заполнить первый лист
        For I As Integer = 0 To mMKIO.GetParameters.GetUpperBound(0)
            nameParameter = mMKIO.GetParameters(I).NameParameter
            Dim newListViewItem As New ListViewItem(nameParameter) With {.Name = nameParameter, .ImageIndex = GetImageIndex(mMKIO.GetParameters(I).UnitOfMeasure)}
            newListViewItem.SubItems.Add(mMKIO.GetParameters(I).Description)
            'newListViewItem.ForeColor = Color.Red  ' красный как будто его нет
            lvwSource.Items.Add(newListViewItem)
        Next

        lvwSource.EndUpdate()
    End Sub

    ''' <summary>
    ''' Очистить лист источник от элементов содержащихся в листе приёмнике
    ''' </summary>
    Private Sub ClearListViewSource()
        If arrControlParameter.Count > 0 Then
            Dim nameParameter As String

            lvwSource.BeginUpdate()
            lvwReceiver.BeginUpdate()

            ' заполнить очищенный массив (параметра может не быть) по количеству, если найдены
            For I As Integer = 0 To UBound(arrControlParameter)
                For J As Integer = 0 To mMKIO.GetParameters.GetUpperBound(0)
                    If mMKIO.GetParameters(J).NameParameter = arrControlParameter(I).Name Then
                        Dim foundedListViews As ListViewItem() = lvwSource.Items.Find(arrControlParameter(I).Name, False)

                        If foundedListViews.Length > 0 Then
                            ' заполнить по содержанию второй лист по содержимому из первого уже настроенного листа
                            nameParameter = foundedListViews(0).Text
                            Dim newListViewItem As New ListViewItem(nameParameter) With {.Name = nameParameter, .ImageIndex = foundedListViews(0).ImageIndex}
                            newListViewItem.SubItems.Add(foundedListViews(0).SubItems(1).Text)
                            lvwReceiver.Items.Add(newListViewItem)

                            For Each itemSource As ListViewItem In foundedListViews
                                lvwSource.Items.Remove(itemSource) ' требует объект, а не индекс
                            Next
                        End If

                        Exit For
                    End If
                Next
            Next

            lvwSource.EndUpdate()
            lvwReceiver.EndUpdate()
        End If
    End Sub

    Private Sub ButtonInsert_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonInsert.Click
        Dim receiverListViewItem, sourceListViewItem As ListViewItem
        Dim lastIndex As Integer

        If syncPoint = 1 Then Exit Sub

        EnableButtons(False)

        ' цикл по листу1 в поисках выделенного
        For I As Integer = 0 To lvwSource.Items.Count - 1
            If lvwSource.Items(I).Selected Then
                sourceListViewItem = lvwSource.Items(I)
                receiverListViewItem = New ListViewItem(sourceListViewItem.Text) With {
                    .Name = sourceListViewItem.Text,
                    .ImageIndex = sourceListViewItem.ImageIndex}
                receiverListViewItem.SubItems.Add(sourceListViewItem.SubItems(1).Text)
                lvwReceiver.Items.Add(receiverListViewItem)
                lastIndex = I
            End If
        Next

        UpdateOnInsertOrRemoveListViewItem(lvwSource, lastIndex)
        TimerEnableButtons = New Windows.Forms.Timer With {.Interval = IntervalTimerEnableButtons}
        TimerEnableButtons.Start()
        If FlowLayoutPanelControls.Controls.Count >= conLimitControls Then MessageInsertPanel()
    End Sub

    Private Sub ButtonRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRemove.Click
        Dim deleteListViewItem As ListViewItem
        Dim lastIndex As Integer

        If syncPoint = 1 Then Exit Sub

        EnableButtons(False)
        lvwReceiver.Focus()

        ' цикл по листу2 в поисках выделенного
        For I As Integer = lvwReceiver.Items.Count - 1 To 0 Step -1
            If lvwReceiver.Items(I).Selected Then
                deleteListViewItem = lvwReceiver.Items(I)
                lvwReceiver.Items.Remove(deleteListViewItem) ' требует объект а не индекс
                lastIndex = I
            End If
        Next

        UpdateOnInsertOrRemoveListViewItem(lvwReceiver, lastIndex)
        TimerEnableButtons = New Windows.Forms.Timer With {.Interval = IntervalTimerEnableButtons}
        TimerEnableButtons.Start()
    End Sub

    ''' <summary>
    ''' Произвести обновления в панели наблюдения после
    ''' добавления или удаления записи в списке
    ''' </summary>
    ''' <param name="tempListView"></param>
    ''' <param name="last"></param>
    Private Sub UpdateOnInsertOrRemoveListViewItem(tempListView As ListView, last As Integer)
        Dim listViewИсточник As ListViewItem

        MakeUpdate()
        tempListView.Focus()

        For I As Integer = 0 To tempListView.Items.Count - 1
            tempListView.Items(I).Selected = False
        Next

        If last > tempListView.Items.Count - 1 Then last = tempListView.Items.Count - 1

        If last > 0 OrElse (last = 0 AndAlso tempListView.Items.Count > 0) Then
            listViewИсточник = tempListView.Items(last)
            listViewИсточник.EnsureVisible()
            listViewИсточник.Selected = True
        End If
    End Sub

    ''' <summary>
    ''' Произвести обновления в панели наблюдения
    ''' </summary>
    Private Sub MakeUpdate()
        'ReDim_arrControlParameter(lvwReceiver.Items.Count - 1)
        Re.Dim(arrControlParameter, lvwReceiver.Items.Count - 1)

        For I As Integer = 0 To lvwReceiver.Items.Count - 1
            arrControlParameter(I) = New ParameterForVisualization(lvwReceiver.Items(I).Text, I)
        Next

        SerializerControlParameters(arrControlParameter)
        PopulateControlToFlowLayoutPanel()
    End Sub

    Private Sub ButtonErase_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonErase.Click
        If syncPoint = 1 Then Exit Sub

        EnableButtons(False)
        ' первый лист полностью заполнить, а второй очистить
        arrControlParameter = New ParameterForVisualization() {}
        SerializerControlParameters(arrControlParameter)
        lvwReceiver.Items.Clear()
        PopulateControlToFlowLayoutPanel()

        TimerEnableButtons = New Windows.Forms.Timer With {.Interval = IntervalTimerEnableButtons}
        TimerEnableButtons.Start()
    End Sub

    ''' <summary>
    ''' Обмен содержиммым между соседними строками
    ''' </summary>
    ''' <param name="inFrom"></param>
    ''' <param name="inTo"></param>
    Private Sub SwapListViewItem(ByVal inFrom As Integer, ByVal inTo As Integer)
        Dim text As String
        Dim description As String
        Dim indexIcon As Integer

        With lvwReceiver
            Dim currentListViewItem As ListViewItem = .Items(inFrom)
            Dim targetListViewItem As ListViewItem = .Items(inTo)

            ' запомнить предыдущего
            text = targetListViewItem.Text
            description = targetListViewItem.SubItems(1).Text
            indexIcon = targetListViewItem.ImageIndex

            ' запись перемещаемого в предыдущий
            targetListViewItem.Text = currentListViewItem.Text
            targetListViewItem.Name = currentListViewItem.Text
            targetListViewItem.SubItems(1).Text = currentListViewItem.SubItems(1).Text
            targetListViewItem.ImageIndex = currentListViewItem.ImageIndex

            ' перезапись в перемещаемый сохраненные
            currentListViewItem.Text = text
            currentListViewItem.Name = text
            currentListViewItem.SubItems(1).Text = description
            currentListViewItem.ImageIndex = indexIcon

            MakeUpdate()

            ' выделить
            .Items(inFrom).Selected = False
            targetListViewItem.EnsureVisible()
            .Items(inTo).Selected = True
        End With
    End Sub

    ''' <summary>
    ''' Переместить выделенную строку в листе
    ''' </summary>
    Private Sub MoveSelectedListViewItem()
        With lvwReceiver
            .Focus()
            If .Items.Count > 0 AndAlso .SelectedIndices.Count <> 0 Then
                Dim selectedIndex As Integer = .SelectedIndices(.SelectedIndices.Count - 1)
                If lvPreviousSelectedIndices < lvSelectedIndices Then ' вверх
                    If selectedIndex <> 0 Then SwapListViewItem(selectedIndex, selectedIndex - 1)
                Else
                    If selectedIndex <> .Items.Count - 1 Then SwapListViewItem(selectedIndex, selectedIndex + 1)
                End If
            End If
        End With
    End Sub

    Private Sub ButtonDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonDown.Click
        If syncPoint = 1 Then Exit Sub

        StartMoveRow(-1)
    End Sub

    Private Sub ButtonUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonUp.Click
        If syncPoint = 1 Then Exit Sub

        StartMoveRow(1)
    End Sub

    ''' <summary>
    ''' Запустить таймер перемещения выделенной строки
    ''' </summary>
    ''' <param name="direct"></param>
    Private Sub StartMoveRow(direct As Integer)
        EnableButtons(False)
        lvPreviousSelectedIndices = lvSelectedIndices
        lvSelectedIndices += direct
        MoveSelectedListViewItem()
        TimerEnableButtons = New Windows.Forms.Timer With {.Interval = IntervalTimerEnableButtons}
        TimerEnableButtons.Start()
    End Sub

    ''' <summary>
    ''' Управление доступом к контролам
    ''' </summary>
    ''' <param name="inEnable"></param>
    Private Sub EnableButtons(inEnable As Boolean)
        lvwSource.Enabled = inEnable
        ButtonInsert.Enabled = inEnable
        ButtonRemove.Enabled = inEnable
        ButtonUp.Enabled = inEnable
        ButtonDown.Enabled = inEnable
        ButtonErase.Enabled = inEnable
        FlowLayoutPanelControls.Visible = inEnable

        If FlowLayoutPanelControls.Controls.Count >= conLimitControls Then ButtonInsert.Enabled = False
    End Sub

    Private Sub MessageInsertPanel()
        MessageBox.Show(Me, $"Число контролируемых параметров достигло ограничения в <{conLimitControls}> штук!{vbCrLf}Для добавления нового необходимо удалить любой параметр из списка.",
                "Добавление параметра для контроля.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Private Sub TimerEnableButtons_Tick(sender As Object, e As EventArgs) Handles TimerEnableButtons.Tick
        TimerEnableButtons.Stop()
        EnableButtons(True)
    End Sub

#Region "DragDrop"
    Private WithEvents TimerDragDrop As Windows.Forms.Timer ' серверный таймер работает в потоке приложения
    Private Const IntervalTimerDragDrop As Integer = 1000
    Private newListViewItemIndex As Integer

    Private Sub TimerDragDrop_Tick(sender As Object, e As EventArgs) Handles TimerDragDrop.Tick
        TimerDragDrop.Stop()
        UpdateOnInsertOrRemoveListViewItem(lvwSource, newListViewItemIndex)
        TimerEnableButtons = New Windows.Forms.Timer With {.Interval = IntervalTimerEnableButtons}
        TimerEnableButtons.Start()
    End Sub

    ''' <summary>
    ''' удостовериться что пользовательский курсор использован
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Form1_GiveFeedback(ByVal sender As Object, ByVal e As GiveFeedbackEventArgs) Handles Me.GiveFeedback, lvwSource.GiveFeedback, lvwReceiver.GiveFeedback
        If img IsNot Nothing Then
            e.UseDefaultCursors = False
            Windows.Forms.Cursor.Current = CreaterCursor.CreateCursor(img, hotSpot.X, hotSpot.Y)
        End If
    End Sub

    Private Sub lvwCode_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles lvwReceiver.DragDrop
        If e.Data.GetDataPresent(GetType(ListViewItem)) Then
            Dim newListViewItem As ListViewItem

            If lvwReceiver.dropIndex > -1 Then
                newListViewItem = lvwReceiver.Items.Insert(lvwReceiver.dropIndex, DirectCast(e.Data.GetData(GetType(ListViewItem)), ListViewItem))
            Else
                newListViewItem = lvwReceiver.Items.Add(DirectCast(e.Data.GetData(GetType(ListViewItem)), ListViewItem))
            End If

            lvwReceiver.Alignment = ListViewAlignment.Default
            lvwReceiver.Alignment = ListViewAlignment.Top
            lvwReceiver.Refresh()

            newListViewItemIndex = newListViewItem.Index

            TimerDragDrop = New Windows.Forms.Timer With {.Interval = IntervalTimerDragDrop}
            TimerDragDrop.Start()

            EnableButtons(False)
        End If
    End Sub

    Private Sub ListViews_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles lvwSource.DragOver, lvwReceiver.DragOver
        If e.Data.GetDataPresent(GetType(ListViewItem)) Then e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub lvwFunctionsList_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles lvwSource.MouseDown
        If syncPoint = 1 Then Exit Sub
        If FlowLayoutPanelControls.Controls.Count >= conLimitControls Then MessageInsertPanel() : Exit Sub

        Dim itemListView As ListViewItem = lvwSource.HitTest(e.Location).Item

        If itemListView IsNot Nothing Then
            Dim index As Integer = itemListView.Index

            lvwSource.DoDragDrop(DirectCast(lvwSource.Items(index).Clone, ListViewItem), DragDropEffects.Copy)

            Dim p As Point = e.Location
            p.Offset(0, -lvwSource.Items(index).Bounds.Top)

            Dim EffectCursor As New Windows.Forms.Cursor(New MemoryStream(My.Resources.move1))

            img = New Bitmap(lvwSource.Width, lvwSource.Items(index).Bounds.Height + EffectCursor.Size.Height)

            Dim gr As Graphics = Graphics.FromImage(img)
            gr.Clear(Color.White)

            For I As Integer = 0 To lvwSource.Items(index).SubItems.Count - 1
                gr.DrawString(lvwSource.Items(index).SubItems(I).Text, lvwSource.Font, Brushes.Black, lvwSource.Items(index).SubItems(I).Bounds.Left, 0)
            Next

            EffectCursor.Draw(gr, New Rectangle(p, EffectCursor.Size))
            img.MakeTransparent(Color.White)
            hotSpot = p
        End If
    End Sub

#End Region

#End Region

End Class

'Shell(strПутьКOPCWriter, AppWinStyle.NormalNoFocus) ' vbMinimizedNoFocus
' или
'Dim startInfo As New ProcessStartInfo(strПутьКOPCWriter)  '"G:\DiskD\Подсчеты\Zadacha_apr.exe")
'startInfo.WindowStyle = ProcessWindowStyle.Normal
''startInfo.UseShellExecute = True
'startInfo.WorkingDirectory = strПутьРесурсы '"G:\DiskD\Подсчеты"
'Process.Start(startInfo)