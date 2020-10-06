Imports System.Windows.Forms
Imports System.IO
Imports MathematicalLibrary

Module GlobalVariable
    Public PathResourses As String
    Public Const PROVIDER_JET As String = "Provider=Microsoft.Jet.OLEDB.4.0;"
    Public gMainFomMdiParent As FrmMain
    '--- для ссылки на статус бар формы FormOPCclient
    Public TemplateMask As Integer()

    Public Function BuildCnnStr(ByVal provider As String, ByVal dataBase As String) As String
        'Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Data Source="D:\ПрограммыVBNET\RUD\RUD.NET\bin\Ресурсы\Channels.mdb";Jet OLEDB:Engine Type=5;Provider="Microsoft.Jet.OLEDB.4.0";Jet OLEDB:System database=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1
        Return String.Format("{0}Data Source={1};", provider, dataBase)
    End Function

    ''' <summary>
    ''' True - файла нет
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Public Function FileNotExists(ByVal path As String) As Boolean
        'FileExists = CBool(Dir(FileName) = vbNullString) 
        Return Not File.Exists(path)
    End Function

    ''' <summary>
    ''' Проверка существования файла
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Public Function CheckExistsFile(ByVal path As String) As Boolean
        If FileNotExists(path) Then
            MessageBox.Show($"В каталоге нет файла <{path}> !", "Провека существования файла", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Запись данных в INI файл - аргументы:
    ''' </summary>
    ''' <param name="sINIFile"></param>
    ''' <param name="sSection">sSection = Название раздела</param>
    ''' <param name="sKey">sKey = Название параметра</param>
    ''' <param name="sValue">sValue = Значение параметра</param>
    ''' <remarks></remarks>
    Public Sub WriteINI(ByRef sINIFile As String, ByRef sSection As String, ByRef sKey As String, ByRef sValue As String)
        Dim N As Integer
        Dim sTemp As String = sValue

        ' Заменить символы CR/LF на пробелы
        For N = 1 To Len(sValue)
            If Mid(sValue, N, 1) = vbCr Or Mid(sValue, N, 1) = vbLf Then Mid(sValue, N) = " "
        Next

        Try
            ' Пишем значения
            N = NativeMethods.WritePrivateProfileString(sSection, sKey, sTemp, sINIFile)
            ' Проверка результата записи
            If N <> 1 Then ' Неудачное завершение
                MsgBox($"Процедура WriteINI не смогла записать параметр INI Файла:{vbCrLf}{sINIFile}{vbCrLf}
-----------------------------------------------------------------{vbCrLf}[{sSection}]{vbCrLf}{sKey}={sValue}")
            End If
        Catch ex As ApplicationException
            MessageBox.Show($"Процедура {NameOf(WriteINI)} привела к ошибке:{vbCrLf}#{ex.ToString}",
                            "Ощибка чтения INI", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''' <summary>
    ''' Чтение данных из файла INI - с возможностью записи значения по умолчанию где аргументы:
    ''' </summary>
    ''' <param name="sINIFile"></param>
    ''' <param name="sSection">sSection  = Название раздела</param>
    ''' <param name="sKey">sKey  = Название параметра</param>
    ''' <param name="sDefault">sDefault = Значение по умолчанию (на случай его отсутствия)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIni(ByRef sINIFile As String, ByRef sSection As String, ByRef sKey As String, Optional ByRef sDefault As String = "") As String
        ' Значение возвращаемое функцией GetPrivateProfileString если искомое значение параметра не найдено
        Const NO_VALUE As String = ""
        Dim nLength As Integer ' Длина возвращаемой строки (функцией GetPrivateProfileString)
        Dim sTemp As String ' Возвращаемая строка

        Try
            ' Получаем значение из файла - если его нет будет возвращен 4й аргумент = strNoValue
            ' sTemp.Value = Space(256)
            sTemp = New String(Chr(0), 255)
            nLength = NativeMethods.GetPrivateProfileString(sSection, sKey, NO_VALUE, sTemp, 255, sINIFile)
            sTemp = Left(sTemp, nLength)

            ' Определяем было найдено значение или нет (если возвращено знач. константы strNoValue то = НЕТ)
            If sTemp = NO_VALUE Then ' Значение не было найдено
                If sDefault <> "" Then ' Если знач по умолчанию задано
                    WriteINI(sINIFile, sSection, sKey, sDefault) ' Записываем заданное аргументом sDefault значение по умолчанию
                    sTemp = sDefault ' и возвращаем его же
                End If
            End If

            ' Возвращаем найденное
            Return sTemp
        Catch ex As ApplicationException
            MessageBox.Show($"Функция {NameOf(GetIni)} привела к ошибке:{vbCrLf}#{ex.ToString}",
                            "Ощибка чтения INI", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Настройка Тегов АИ222
    ''' </summary>
    Public Sub TuneTagsAI222()
        'ReDim_TemplateMask(19)
        Re.Dim(TemplateMask, 19)

        TemplateMask(4) = CInt(2 ^ 15)
        TemplateMask(5) = CInt(2 ^ 14)
        TemplateMask(6) = CInt(2 ^ 13)
        TemplateMask(7) = CInt(2 ^ 12)
        TemplateMask(8) = CInt(2 ^ 11)
        TemplateMask(9) = CInt(2 ^ 10)
        TemplateMask(10) = CInt(2 ^ 9)
        TemplateMask(11) = CInt(2 ^ 8)
        TemplateMask(12) = CInt(2 ^ 7)
        TemplateMask(13) = CInt(2 ^ 6)
        TemplateMask(14) = CInt(2 ^ 5)
        TemplateMask(15) = CInt(2 ^ 4)
        TemplateMask(16) = CInt(2 ^ 3)
        TemplateMask(17) = CInt(2 ^ 2)
        TemplateMask(18) = CInt(2 ^ 1)
        TemplateMask(19) = CInt(2 ^ 0) '16 разряд в значении тега (в данных 19 разряд)

        'Dim J As Integer = 15
        'For I = 4 To 19
        '    TemplateMask(I) = 2 ^ J
        '    J = -J
        'Next
    End Sub

End Module
