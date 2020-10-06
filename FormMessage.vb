Friend Class FormMessage
    Inherits System.Windows.Forms.Form
#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Size = New Drawing.Size(456, 63)
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents LabelMessage As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.LabelMessage = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LabelMessage
        '
        Me.LabelMessage.BackColor = System.Drawing.SystemColors.HighlightText
        Me.LabelMessage.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelMessage.Font = New System.Drawing.Font("Times New Roman", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.LabelMessage.ForeColor = System.Drawing.Color.Red
        Me.LabelMessage.Location = New System.Drawing.Point(0, 0)
        Me.LabelMessage.Name = "LabelMessage"
        Me.LabelMessage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelMessage.Size = New System.Drawing.Size(456, 37)
        Me.LabelMessage.TabIndex = 0
        Me.LabelMessage.Text = "Подождите, идет печать"
        Me.LabelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FormMessage
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.HighlightText
        Me.ClientSize = New System.Drawing.Size(450, 37)
        Me.ControlBox = False
        Me.Controls.Add(Me.LabelMessage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(3, 18)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMessage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Печать"
        Me.ResumeLayout(False)

    End Sub
#End Region

End Class