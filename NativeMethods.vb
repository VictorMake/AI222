Imports System.Runtime.InteropServices

Friend NotInheritable Class NativeMethods
    Private Sub New()
    End Sub

    ' для записи в Опции.INI
    Friend Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String,
                                                                                                     ByVal lpKeyName As String,
                                                                                                     ByVal lpDefault As String,
                                                                                                     ByVal lpReturnedString As String,
                                                                                                     ByVal nSize As Integer,
                                                                                                     ByVal lpFileName As String) As Integer
    Friend Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String,
                                                                                                         ByVal lpKeyName As String,
                                                                                                         ByVal lpString As String,
                                                                                                         ByVal lpFileName As String) As Integer


    <DllImport("user32.dll", EntryPoint:="CreateIconIndirect")>
    Friend Shared Function CreateIconIndirect(ByVal iconInfo As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Friend Shared Function DestroyIcon(ByVal handle As IntPtr) As Boolean
    End Function

    <DllImport("gdi32.dll")>
    Friend Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
    End Function

End Class

