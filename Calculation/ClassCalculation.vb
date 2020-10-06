Imports BaseForm

' для COM видимости
'<System.Runtime.InteropServices.ProgId("ClassDiagram_NET.ClassDiagram")> Public Class ClassCalculation
'    Implements BaseForm.IClassCalculation
Public Class ClassCalculation
    Implements IClassCalculation

    Public Property Manager() As ProjectManager Implements IClassCalculation.Manager
        Get
            Return mProjectManager
        End Get
        Set(ByVal value As ProjectManager)
            mProjectManager = value
        End Set
    End Property

    ''' <summary>
    ''' Входные аргументы
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InputParam() As InputParameters

    ''' <summary>
    ''' Настроечные параметры
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TuningParam() As TuningParameters

    ''' <summary>
    '''  Расчетные параметры
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CalculatedParam() As CalculatedParameters

    'Delegate Sub DataErrorventHandler(ByVal sender As Object, ByVal e As DataErrorEventArgs)
    'Public Event DataError(ByVal sender As Object, ByVal e As BaseForm.IClassCalculation.DataErrorEventArgs) Implements BaseForm.IClassCalculation.DataError
    'Public Event DataError(ByVal sender As BaseForm.IClassCalculation, ByVal e As BaseForm.DataErrorEventArgs) Implements BaseForm.DataError
    ''' <summary>
    ''' событие для выдачи ошибки в вызывающую программу
    ''' </summary>
    Public Event DataError As EventHandler(Of DataErrorEventArgs)

    Private mProjectManager As ProjectManager

    Public Sub New(ByVal manager As ProjectManager)
        MyBase.New()

        Me.Manager = manager

        InputParam = New InputParameters
        TuningParam = New TuningParameters
        CalculatedParam = New CalculatedParameters
    End Sub

    ''' <summary>
    ''' Последовательное прохождение по этапам приведениия и вычисления.
    ''' Здесь индивидуальные настройки для класса.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Calculate() Implements IClassCalculation.Calculate
        ' Для Приведенных и Пересчитанных параметров входные единицы измерения
        ' только в единицах СИ, выходные единицы измерения - любого типа
        'gMainFomMdiParent.varТемпературныеПоля.TextError.Visible = False

        Try
            ' здесь пока не надо получать от контролов
            If mProjectManager.NeedToRewrite Then ПолучитьЗначенияНастроечныхПараметров()
            ' Переводим в Си только измеренные пареметры
            mProjectManager.СonversionToSiUnitMeasurementParameters()
            'получение абсолютных давлений
            'mProjectManager.УчетБазовыхВеличин()
            ' весь подсчет производится исключительно в единицах СИ
            ' извлекаем значения измеренных параметров
            ИзвлечьЗначенияИзмеренныхПараметров()
            ВычислитьРасчетныеПараметры()
            mProjectManager.СonversionToTuningUnitCalculationParameters()

            'Dim result As String = Await AsynchronouslyAsync()
            'Await CalcAsynchronouslyAsync()

            gMainFomMdiParent.VarFormOPCclient.UpdateVisualControls()
            ' там же заполняется массив y()
            'If gРисоватьГрафикСечений Then gMainFomMdiParent.varТемпературныеПоля.РисоватьПолеПоСечению()
        Catch ex As Exception
            ' ошибка проглатывается
            'Description = "Процедура: Подсчет"
            ''перенаправление встроенной ошибки
            'Dim fireDataErrorEventArgs As New IClassCalculation.DataErrorEventArgs(ex.Message, Description)
            ''  Теперь вызов события с помощью вызова делегата. Проходя в
            ''   object которое инициирует  событие (Me) такое же как FireEventArgs. 
            ''  Вызов обязан соответствовать сигнатуре FireEventHandler.
            'RaiseEvent DataError(Me, fireDataErrorEventArgs)
        End Try
    End Sub

    '''' <summary>
    '''' Подсчёты не связанные с графическим интерфейсом.
    '''' Графический интерфейс не блокируется.
    '''' </summary>
    '''' <returns></returns>
    'Public Async Function CalcAsynchronouslyAsync() As Task 'Task(Of String) '
    '    'Await Task.Delay(10000)
    '    'Return "Finished"
    '    Dim t As Task = Task.Factory.StartNew(Sub()
    '                                              ' здесь пока не надо получать от контролов
    '                                              If mProjectManager.NeedToRewrite Then ПолучитьЗначенияНастроечныхПараметров()
    '                                              ' Переводим в Си только измеренные пареметры
    '                                              mProjectManager.ПереводВЕдиницыСИИзмеренныеПараметры()
    '                                              'получение абсолютных давлений
    '                                              mProjectManager.УчетБазовыхВеличин()
    '                                              ' весь подсчет производится исключительно в единицах СИ
    '                                              ' извлекаем значения измеренных параметров
    '                                              ИзвлечьЗначенияИзмеренныхПараметров()
    '                                              ВычислитьРасчетныеПараметры()
    '                                              mProjectManager.ПереводВНастоечныеЕдиницыРасчетныхПараметров()

    '                                              If gНакопитьДляПоля Then НакопитьЗначенияИзмеренныхИРасчетныхПараметров()
    '                                          End Sub)
    '    t.Wait()

    '    Await t
    'End Function

    Dim description As String = $"Процедура: <{NameOf(ПолучитьЗначенияНастроечныхПараметров)}>"
    ''' <summary>
    ''' Получить значения параметров, используемых как настраиваемые глобальные переменные.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ПолучитьЗначенияНастроечныхПараметров()
        If Manager.TuningDataTable Is Nothing Then Exit Sub

        Dim success As Boolean = False

        ' Вначале проверяется наличие расчетных параметров в базе
        For Each имяНастроечногоПараметра As String In TuningParam.TuningDictionary.Keys.ToArray 'arrНастроечныеПараметры
            success = False

            For Each rowНастроечныйПараметр As BaseFormDataSet.НастроечныеПараметрыRow In Manager.TuningDataTable.Rows
                If rowНастроечныйПараметр.ИмяПараметра = имяНастроечногоПараметра Then
                    success = True
                    Exit For
                End If
            Next

            If success = False Then
                ' перенаправление встроенной ошибки
                RaiseEvent DataError(Me, New DataErrorEventArgs($"Настроечный параметр {имяНастроечногоПараметра} в базе параметров не найден!", description)) 'не ловит в конструкторе
                'MessageBox.Show(Message, Description, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next

        ' проверяется наличие в расчетном модуле переменных, соответствующих расчетным настроечным
        ' и присвоение им значений
        success = True
        Try
            For Each rowНастроечныйПараметр As BaseFormDataSet.НастроечныеПараметрыRow In Manager.TuningDataTable.Rows
                If TuningParam.TuningDictionary.Keys.Contains(rowНастроечныйПараметр.ИмяПараметра) Then

                    Select Case rowНастроечныйПараметр.ИмяПараметра
                        'Case "GвМПитоПриводить"
                        '    'GвМПитоПриводить = rowНастроечныйПараметр.ЦифровоеЗначение
                        '    'n1ГПриводить = CInt(rowНастроечныйПараметр.ЛогическоеЗначение)
                        '    GвМПитоПриводить = rowНастроечныйПараметр.ЛогическоеЗначение
                        '    Exit Select
                    End Select
                Else
                    success = False
                    'перенаправление встроенной ошибки
                    RaiseEvent DataError(Me, New DataErrorEventArgs($"Настроечный параметр {rowНастроечныйПараметр.ИмяПараметра} не имеет соответствующей переменной в модуле расчета!", description)) ' не ловит в конструкторе
                    'MessageBox.Show(Message, Description, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Next

            'With gMainFomMdiParent.Manager.НастроечныеПараметры
            '    'D20трубОсн = .FindByИмяПараметра(TuningParameters.conD20трубОсн).ЦифровоеЗначение
            'End With

            If success = False Then Exit Sub

            ' занести значения настроечных параметров
            With Manager.TuningDataTable
                For Each keysTuning As String In TuningParam.TuningDictionary.Keys.ToArray
                    If .FindByИмяПараметра(keysTuning).ЛогикаИлиЧисло Then
                        TuningParam.TuningDictionary(keysTuning).ЛогикаИлиЧисло = True
                        TuningParam.TuningDictionary(keysTuning).ЛогическоеЗначение = .FindByИмяПараметра(keysTuning).ЛогическоеЗначение
                    Else
                        TuningParam.TuningDictionary(keysTuning).ЛогикаИлиЧисло = False
                        TuningParam.TuningDictionary(keysTuning).ЦифровоеЗначение = .FindByИмяПараметра(keysTuning).ЦифровоеЗначение
                    End If
                Next
            End With

        Catch ex As Exception
            ' перенаправление встроенной ошибки
            RaiseEvent DataError(Me, New DataErrorEventArgs(ex.Message, description)) 'не ловит в конструкторе
            'MessageBox.Show(fireDataErrorEventArgs, Description, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' Поиск всех параметров по пользовательскому запросу в DataSet.ИзмеренныеПараметры
    ''' (с одним входным параметром являющимся именем связи для реального измеряемого канала Сервера).
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ИзвлечьЗначенияИзмеренныхПараметров()
        'Dim rowИзмеренныйПараметр As BaseFormDataSet.ИзмеренныеПараметрыRow
        Try
            With Manager.MeasurementDataTable
                ' вместо последовательного извлечения применяется обход по коллекции
                ' ARG1 = .FindByИмяПараметра(conARG1).ЗначениеВСИ
                ' ...
                ' ARG10 = .FindByИмяПараметра(conARG10).ЗначениеВСИ

                'For Each keysArg As String In inputArg.InputArgDictionary.Keys.ToArray
                '    inputArg.InputArgDictionary(keysArg) = .FindByИмяПараметра(keysArg).ЗначениеВСИ
                'Next

                ' '' иттератор по коллекции как KeyValuePair objects.
                ''For Each kvp As KeyValuePair(Of String, Double) In inputArg.InputArgDictionary
                ''    'Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value)
                ''    inputArg.InputArgDictionary(kvp.Key) = .FindByИмяПараметра(kvp.Key).ЗначениеВСИ
                ''Next

                ''For Each value As Double In inputArg.InputArgDictionary.Values
                ''    Console.WriteLine("Value = {0}", value)
                ''Next

                InputParam.Tбокса = .FindByИмяПараметра(InputParameters.conTБОКСА).ЗначениеВСИ ' температура в боксе
                'InputParam.Барометр = .FindByИмяПараметра(InputParameters.conБАРОМЕТР).ЗначениеВСИ ' БРС1-М
            End With
        Catch ex As Exception
            gMainFomMdiParent.VarFormOPCclient.ShowError("Ошибка извлечения измеренных параметров")
            'перенаправление встроенной ошибки
            RaiseEvent DataError(Me, New DataErrorEventArgs(ex.Message, $"Процедура: <{NameOf(ИзвлечьЗначенияИзмеренныхПараметров)}>"))
        End Try
    End Sub

    ''' <summary>
    ''' Поиск всех параметров по пользовательскому запросу в DataSet.РасчетныеПараметры
    ''' (с одним входным параметром являющимся именем расчётной величины).
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ВычислитьРасчетныеПараметры()
        Try
            gMainFomMdiParent.VarFormOPCclient.GetDataOPC()

            ' занести вычисленные значения
            With Manager.CalculatedDataTable
                '' вместо последовательного извлечения применяется обход по коллекции
                '' .FindByИмяПараметра(conCalc1).ВычисленноеЗначениеВСИ = Calc1
                ' ********************************** и т.д. ********************************
                '' .FindByИмяПараметра(conCalc10).ВычисленноеЗначениеВСИ = Calc10

                '.FindByИмяПараметра(CalculatedParameter.G_СУМ_РАСХОД_ТОПЛИВА_КС_КП).ВычисленноеЗначениеВСИ = CalculatedParam.Gсум_расход_топливаКС_КП_кг_час
                '.FindByИмяПараметра(CalculatedParameter.G_ОТБОРА_ОТНОСИТЕЛЬНЫЙ).ВычисленноеЗначениеВСИ = CalculatedParam.Gотбора_относительный
                ' ********************************** и т.д. ********************************

                For Each keysCalc As String In CalculatedParam.CalcDictionary.Keys.ToArray
                    .FindByИмяПараметра(keysCalc).ВычисленноеЗначениеВСИ = CalculatedParam(keysCalc)
                Next

                ' расчетные вспомогательные ...
                '.FindByИмяПараметра(XXXXX).ВычисленноеЗначениеВСИ = Ystart
            End With
        Catch ex As Exception
            gMainFomMdiParent.VarFormOPCclient.ShowError("Ошибка вычисления расчётных параметров")
            'перенаправление встроенной ошибки
            RaiseEvent DataError(Me, New DataErrorEventArgs(ex.Message, $"Процедура: <{NameOf(ВычислитьРасчетныеПараметры)}>"))
        End Try
    End Sub

End Class
