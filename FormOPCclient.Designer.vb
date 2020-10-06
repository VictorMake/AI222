<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormOPCclient
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormOPCclient))
        Me.StatusBar = New System.Windows.Forms.StatusStrip()
        Me.StatusLabelMessage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusLabelStend = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripContainerForm = New System.Windows.Forms.ToolStripContainer()
        Me.SplitContainerMain = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanelAll = New System.Windows.Forms.TableLayoutPanel()
        Me.PanelCentre = New System.Windows.Forms.Panel()
        Me.TableLayoutPanelCentre = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonInsert = New System.Windows.Forms.Button()
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.ButtonDown = New System.Windows.Forms.Button()
        Me.ButtonUp = New System.Windows.Forms.Button()
        Me.ButtonErase = New System.Windows.Forms.Button()
        Me.LabelDescription = New System.Windows.Forms.Label()
        Me.lvwSource = New System.Windows.Forms.ListView()
        Me.ImageListChannel = New System.Windows.Forms.ImageList(Me.components)
        Me.lvwReceiver = New AI222.ListViewCustomReorder.ListViewEx()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBoxPuthOPCWriterExe = New System.Windows.Forms.TextBox()
        Me.TextBoxWriter = New System.Windows.Forms.TextBox()
        Me.TextBoxTbox = New System.Windows.Forms.TextBox()
        Me.LabelWriterPuth = New System.Windows.Forms.Label()
        Me.edStatusTt = New System.Windows.Forms.TextBox()
        Me.LabelWriterTbox = New System.Windows.Forms.Label()
        Me.LabelWriterStatus = New System.Windows.Forms.Label()
        Me.edURLTt = New System.Windows.Forms.TextBox()
        Me.ButtonWriterConnect = New System.Windows.Forms.Button()
        Me.ButtonWriterDisconnect = New System.Windows.Forms.Button()
        Me.ButtonPathOPCWriter = New System.Windows.Forms.Button()
        Me.TextPathOPCWriter = New System.Windows.Forms.TextBox()
        Me.PanelControls = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanelControls = New System.Windows.Forms.FlowLayoutPanel()
        Me.ToolStripOPC = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabelStatus = New System.Windows.Forms.ToolStripLabel()
        Me.OPCStatusText = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ButtonOPCReaderConnect = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ButtonOPCReaderUpdate = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ButtonOPCReaderDisconnect = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.TextBoxНетСвязи = New System.Windows.Forms.ToolStripTextBox()
        Me.TextError = New System.Windows.Forms.ToolStripTextBox()
        Me.ImageListMenu = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolTipForm = New System.Windows.Forms.ToolTip(Me.components)
        Me.TimerOPC = New System.Windows.Forms.Timer(Me.components)
        Me.TimerCheckDataSocketOPCWriter = New System.Windows.Forms.Timer(Me.components)
        Me.DataSocketOPCReader = New NationalInstruments.Net.DataSocket(Me.components)
        Me.OpenFileDialogPuth = New System.Windows.Forms.OpenFileDialog()
        Me.DataSocketWriter = New NationalInstruments.Net.DataSocket(Me.components)
        Me.StatusBar.SuspendLayout()
        Me.ToolStripContainerForm.ContentPanel.SuspendLayout()
        Me.ToolStripContainerForm.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainerForm.SuspendLayout()
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerMain.Panel1.SuspendLayout()
        Me.SplitContainerMain.Panel2.SuspendLayout()
        Me.SplitContainerMain.SuspendLayout()
        Me.TableLayoutPanelAll.SuspendLayout()
        Me.PanelCentre.SuspendLayout()
        Me.TableLayoutPanelCentre.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.PanelControls.SuspendLayout()
        Me.ToolStripOPC.SuspendLayout()
        CType(Me.DataSocketOPCReader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSocketWriter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusBar
        '
        Me.StatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabelMessage, Me.StatusLabelStend})
        Me.StatusBar.Location = New System.Drawing.Point(0, 704)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(1008, 25)
        Me.StatusBar.TabIndex = 34
        '
        'StatusLabelMessage
        '
        Me.StatusLabelMessage.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusLabelMessage.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.StatusLabelMessage.Image = CType(resources.GetObject("StatusLabelMessage.Image"), System.Drawing.Image)
        Me.StatusLabelMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StatusLabelMessage.Name = "StatusLabelMessage"
        Me.StatusLabelMessage.Size = New System.Drawing.Size(935, 20)
        Me.StatusLabelMessage.Spring = True
        Me.StatusLabelMessage.Text = "Готов"
        Me.StatusLabelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusLabelStend
        '
        Me.StatusLabelStend.Name = "StatusLabelStend"
        Me.StatusLabelStend.Size = New System.Drawing.Size(58, 20)
        Me.StatusLabelStend.Text = "Стенд №:"
        '
        'ToolStripContainerForm
        '
        '
        'ToolStripContainerForm.ContentPanel
        '
        Me.ToolStripContainerForm.ContentPanel.Controls.Add(Me.SplitContainerMain)
        Me.ToolStripContainerForm.ContentPanel.Size = New System.Drawing.Size(1008, 679)
        Me.ToolStripContainerForm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainerForm.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainerForm.Name = "ToolStripContainerForm"
        Me.ToolStripContainerForm.Size = New System.Drawing.Size(1008, 704)
        Me.ToolStripContainerForm.TabIndex = 35
        Me.ToolStripContainerForm.Text = "ToolStripContainer1"
        '
        'ToolStripContainerForm.TopToolStripPanel
        '
        Me.ToolStripContainerForm.TopToolStripPanel.Controls.Add(Me.ToolStripOPC)
        '
        'SplitContainerMain
        '
        Me.SplitContainerMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerMain.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerMain.Name = "SplitContainerMain"
        '
        'SplitContainerMain.Panel1
        '
        Me.SplitContainerMain.Panel1.Controls.Add(Me.TableLayoutPanelAll)
        Me.SplitContainerMain.Panel1.Controls.Add(Me.TableLayoutPanel1)
        '
        'SplitContainerMain.Panel2
        '
        Me.SplitContainerMain.Panel2.Controls.Add(Me.PanelControls)
        Me.SplitContainerMain.Size = New System.Drawing.Size(1008, 679)
        Me.SplitContainerMain.SplitterDistance = 492
        Me.SplitContainerMain.TabIndex = 8
        '
        'TableLayoutPanelAll
        '
        Me.TableLayoutPanelAll.ColumnCount = 3
        Me.TableLayoutPanelAll.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelAll.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104.0!))
        Me.TableLayoutPanelAll.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelAll.Controls.Add(Me.PanelCentre, 1, 1)
        Me.TableLayoutPanelAll.Controls.Add(Me.LabelDescription, 0, 0)
        Me.TableLayoutPanelAll.Controls.Add(Me.lvwSource, 0, 1)
        Me.TableLayoutPanelAll.Controls.Add(Me.lvwReceiver, 2, 1)
        Me.TableLayoutPanelAll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelAll.Location = New System.Drawing.Point(0, 171)
        Me.TableLayoutPanelAll.Name = "TableLayoutPanelAll"
        Me.TableLayoutPanelAll.RowCount = 2
        Me.TableLayoutPanelAll.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.541667!))
        Me.TableLayoutPanelAll.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 96.45834!))
        Me.TableLayoutPanelAll.Size = New System.Drawing.Size(488, 504)
        Me.TableLayoutPanelAll.TabIndex = 93
        '
        'PanelCentre
        '
        Me.PanelCentre.Controls.Add(Me.TableLayoutPanelCentre)
        Me.PanelCentre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelCentre.Location = New System.Drawing.Point(195, 20)
        Me.PanelCentre.Name = "PanelCentre"
        Me.PanelCentre.Size = New System.Drawing.Size(98, 481)
        Me.PanelCentre.TabIndex = 22
        '
        'TableLayoutPanelCentre
        '
        Me.TableLayoutPanelCentre.ColumnCount = 1
        Me.TableLayoutPanelCentre.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelCentre.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanelCentre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelCentre.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanelCentre.Name = "TableLayoutPanelCentre"
        Me.TableLayoutPanelCentre.RowCount = 2
        Me.TableLayoutPanelCentre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 97.74775!))
        Me.TableLayoutPanelCentre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.252252!))
        Me.TableLayoutPanelCentre.Size = New System.Drawing.Size(98, 481)
        Me.TableLayoutPanelCentre.TabIndex = 6
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonInsert, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonRemove, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonDown, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonUp, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonErase, 1, 4)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 5
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(92, 464)
        Me.TableLayoutPanel2.TabIndex = 23
        '
        'ButtonInsert
        '
        Me.ButtonInsert.BackgroundImage = CType(resources.GetObject("ButtonInsert.BackgroundImage"), System.Drawing.Image)
        Me.ButtonInsert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonInsert.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ButtonInsert.ForeColor = System.Drawing.Color.Black
        Me.ButtonInsert.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonInsert.Location = New System.Drawing.Point(-1, 3)
        Me.ButtonInsert.Name = "ButtonInsert"
        Me.ButtonInsert.Size = New System.Drawing.Size(90, 60)
        Me.ButtonInsert.TabIndex = 2
        Me.ButtonInsert.Text = "Добавить"
        Me.ButtonInsert.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTipForm.SetToolTip(Me.ButtonInsert, "Добавить канал для наблюдения")
        Me.ButtonInsert.UseVisualStyleBackColor = True
        '
        'ButtonRemove
        '
        Me.ButtonRemove.BackgroundImage = CType(resources.GetObject("ButtonRemove.BackgroundImage"), System.Drawing.Image)
        Me.ButtonRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ButtonRemove.ForeColor = System.Drawing.Color.Black
        Me.ButtonRemove.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonRemove.Location = New System.Drawing.Point(-1, 95)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(90, 60)
        Me.ButtonRemove.TabIndex = 1
        Me.ButtonRemove.Text = "Удалить"
        Me.ButtonRemove.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTipForm.SetToolTip(Me.ButtonRemove, "Удалить канал из списка")
        Me.ButtonRemove.UseVisualStyleBackColor = True
        '
        'ButtonDown
        '
        Me.ButtonDown.BackgroundImage = CType(resources.GetObject("ButtonDown.BackgroundImage"), System.Drawing.Image)
        Me.ButtonDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ButtonDown.ForeColor = System.Drawing.Color.Black
        Me.ButtonDown.Location = New System.Drawing.Point(-1, 187)
        Me.ButtonDown.Name = "ButtonDown"
        Me.ButtonDown.Size = New System.Drawing.Size(90, 60)
        Me.ButtonDown.TabIndex = 14
        Me.ButtonDown.Text = "Вниз"
        Me.ButtonDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTipForm.SetToolTip(Me.ButtonDown, "Переместить канал вниз по списку")
        Me.ButtonDown.UseVisualStyleBackColor = True
        '
        'ButtonUp
        '
        Me.ButtonUp.BackgroundImage = CType(resources.GetObject("ButtonUp.BackgroundImage"), System.Drawing.Image)
        Me.ButtonUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ButtonUp.ForeColor = System.Drawing.Color.Black
        Me.ButtonUp.Location = New System.Drawing.Point(-1, 279)
        Me.ButtonUp.Name = "ButtonUp"
        Me.ButtonUp.Size = New System.Drawing.Size(90, 60)
        Me.ButtonUp.TabIndex = 15
        Me.ButtonUp.Text = "Вверх"
        Me.ButtonUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTipForm.SetToolTip(Me.ButtonUp, "Переместить канал вверх по списку")
        Me.ButtonUp.UseVisualStyleBackColor = True
        '
        'ButtonErase
        '
        Me.ButtonErase.BackgroundImage = CType(resources.GetObject("ButtonErase.BackgroundImage"), System.Drawing.Image)
        Me.ButtonErase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonErase.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ButtonErase.ForeColor = System.Drawing.Color.Black
        Me.ButtonErase.Location = New System.Drawing.Point(-1, 371)
        Me.ButtonErase.Name = "ButtonErase"
        Me.ButtonErase.Size = New System.Drawing.Size(90, 60)
        Me.ButtonErase.TabIndex = 0
        Me.ButtonErase.Text = "Очистить"
        Me.ButtonErase.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTipForm.SetToolTip(Me.ButtonErase, "Очистить список")
        Me.ButtonErase.UseVisualStyleBackColor = True
        '
        'LabelDescription
        '
        Me.LabelDescription.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanelAll.SetColumnSpan(Me.LabelDescription, 3)
        Me.LabelDescription.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelDescription.ForeColor = System.Drawing.Color.Blue
        Me.LabelDescription.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelDescription.Location = New System.Drawing.Point(3, 0)
        Me.LabelDescription.Name = "LabelDescription"
        Me.LabelDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelDescription.Size = New System.Drawing.Size(482, 17)
        Me.LabelDescription.TabIndex = 2
        Me.LabelDescription.Text = "Выбрать параметры для контроля в нужном порядке"
        Me.LabelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lvwSource
        '
        Me.lvwSource.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwSource.FullRowSelect = True
        Me.lvwSource.GridLines = True
        Me.lvwSource.HideSelection = False
        Me.lvwSource.Location = New System.Drawing.Point(3, 20)
        Me.lvwSource.Name = "lvwSource"
        Me.lvwSource.Size = New System.Drawing.Size(186, 481)
        Me.lvwSource.SmallImageList = Me.ImageListChannel
        Me.lvwSource.TabIndex = 11
        Me.ToolTipForm.SetToolTip(Me.lvwSource, "Каналы УИМС от АИ222")
        Me.lvwSource.UseCompatibleStateImageBehavior = False
        Me.lvwSource.View = System.Windows.Forms.View.Details
        '
        'ImageListChannel
        '
        Me.ImageListChannel.ImageStream = CType(resources.GetObject("ImageListChannel.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListChannel.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListChannel.Images.SetKeyName(0, "")
        Me.ImageListChannel.Images.SetKeyName(1, "")
        Me.ImageListChannel.Images.SetKeyName(2, "")
        Me.ImageListChannel.Images.SetKeyName(3, "")
        Me.ImageListChannel.Images.SetKeyName(4, "")
        Me.ImageListChannel.Images.SetKeyName(5, "")
        Me.ImageListChannel.Images.SetKeyName(6, "")
        Me.ImageListChannel.Images.SetKeyName(7, "")
        Me.ImageListChannel.Images.SetKeyName(8, "")
        Me.ImageListChannel.Images.SetKeyName(9, "")
        Me.ImageListChannel.Images.SetKeyName(10, "")
        Me.ImageListChannel.Images.SetKeyName(11, "")
        '
        'lvwReceiver
        '
        Me.lvwReceiver.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwReceiver.dropIndex = 0
        Me.lvwReceiver.FullRowSelect = True
        Me.lvwReceiver.GridLines = True
        Me.lvwReceiver.HideSelection = False
        Me.lvwReceiver.Location = New System.Drawing.Point(299, 20)
        Me.lvwReceiver.Name = "lvwReceiver"
        Me.lvwReceiver.Size = New System.Drawing.Size(186, 481)
        Me.lvwReceiver.SmallImageList = Me.ImageListChannel
        Me.lvwReceiver.TabIndex = 23
        Me.ToolTipForm.SetToolTip(Me.lvwReceiver, "Выбранные каналы для наблюдения")
        Me.lvwReceiver.UseCompatibleStateImageBehavior = False
        Me.lvwReceiver.View = System.Windows.Forms.View.Details
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxPuthOPCWriterExe, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxWriter, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxTbox, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelWriterPuth, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.edStatusTt, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelWriterTbox, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelWriterStatus, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.edURLTt, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonWriterConnect, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonWriterDisconnect, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonPathOPCWriter, 3, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.TextPathOPCWriter, 1, 6)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 8
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(488, 171)
        Me.TableLayoutPanel1.TabIndex = 92
        '
        'TextBoxPuthOPCWriterExe
        '
        Me.TextBoxPuthOPCWriterExe.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxPuthOPCWriterExe.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TableLayoutPanel1.SetColumnSpan(Me.TextBoxPuthOPCWriterExe, 4)
        Me.TextBoxPuthOPCWriterExe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxPuthOPCWriterExe.ForeColor = System.Drawing.Color.Blue
        Me.TextBoxPuthOPCWriterExe.Location = New System.Drawing.Point(3, 118)
        Me.TextBoxPuthOPCWriterExe.Name = "TextBoxPuthOPCWriterExe"
        Me.TextBoxPuthOPCWriterExe.ReadOnly = True
        Me.TextBoxPuthOPCWriterExe.Size = New System.Drawing.Size(482, 13)
        Me.TextBoxPuthOPCWriterExe.TabIndex = 93
        Me.TextBoxPuthOPCWriterExe.TabStop = False
        Me.TextBoxPuthOPCWriterExe.Text = "Настройка пути к OPCWriter.exe"
        Me.TextBoxPuthOPCWriterExe.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxWriter
        '
        Me.TextBoxWriter.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxWriter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TableLayoutPanel1.SetColumnSpan(Me.TextBoxWriter, 4)
        Me.TextBoxWriter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxWriter.ForeColor = System.Drawing.Color.Blue
        Me.TextBoxWriter.Location = New System.Drawing.Point(3, 3)
        Me.TextBoxWriter.Name = "TextBoxWriter"
        Me.TextBoxWriter.ReadOnly = True
        Me.TextBoxWriter.Size = New System.Drawing.Size(482, 13)
        Me.TextBoxWriter.TabIndex = 0
        Me.TextBoxWriter.TabStop = False
        Me.TextBoxWriter.Text = "Передача данных в УИМС222"
        Me.TextBoxWriter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxTbox
        '
        Me.TextBoxTbox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxTbox.Location = New System.Drawing.Point(392, 23)
        Me.TextBoxTbox.Name = "TextBoxTbox"
        Me.TextBoxTbox.ReadOnly = True
        Me.TextBoxTbox.Size = New System.Drawing.Size(93, 20)
        Me.TextBoxTbox.TabIndex = 91
        Me.TextBoxTbox.TabStop = False
        Me.TextBoxTbox.Text = "0.00"
        '
        'LabelWriterPuth
        '
        Me.LabelWriterPuth.BackColor = System.Drawing.SystemColors.Control
        Me.LabelWriterPuth.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelWriterPuth.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelWriterPuth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelWriterPuth.Location = New System.Drawing.Point(29, 20)
        Me.LabelWriterPuth.Name = "LabelWriterPuth"
        Me.LabelWriterPuth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelWriterPuth.Size = New System.Drawing.Size(65, 20)
        Me.LabelWriterPuth.TabIndex = 88
        Me.LabelWriterPuth.Text = "Источник:"
        Me.LabelWriterPuth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'edStatusTt
        '
        Me.edStatusTt.AcceptsReturn = True
        Me.edStatusTt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.edStatusTt.BackColor = System.Drawing.SystemColors.Window
        Me.TableLayoutPanel1.SetColumnSpan(Me.edStatusTt, 2)
        Me.edStatusTt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.edStatusTt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.edStatusTt.Location = New System.Drawing.Point(100, 43)
        Me.edStatusTt.MaxLength = 0
        Me.edStatusTt.Multiline = True
        Me.edStatusTt.Name = "edStatusTt"
        Me.edStatusTt.ReadOnly = True
        Me.edStatusTt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TableLayoutPanel1.SetRowSpan(Me.edStatusTt, 2)
        Me.edStatusTt.Size = New System.Drawing.Size(286, 33)
        Me.edStatusTt.TabIndex = 87
        Me.edStatusTt.TabStop = False
        '
        'LabelWriterTbox
        '
        Me.LabelWriterTbox.BackColor = System.Drawing.SystemColors.Control
        Me.LabelWriterTbox.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelWriterTbox.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelWriterTbox.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelWriterTbox.Location = New System.Drawing.Point(321, 20)
        Me.LabelWriterTbox.Name = "LabelWriterTbox"
        Me.LabelWriterTbox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelWriterTbox.Size = New System.Drawing.Size(65, 20)
        Me.LabelWriterTbox.TabIndex = 90
        Me.LabelWriterTbox.Text = "Тбокса:"
        Me.LabelWriterTbox.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabelWriterStatus
        '
        Me.LabelWriterStatus.BackColor = System.Drawing.SystemColors.Control
        Me.LabelWriterStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelWriterStatus.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelWriterStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelWriterStatus.Location = New System.Drawing.Point(29, 40)
        Me.LabelWriterStatus.Name = "LabelWriterStatus"
        Me.LabelWriterStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelWriterStatus.Size = New System.Drawing.Size(65, 20)
        Me.LabelWriterStatus.TabIndex = 85
        Me.LabelWriterStatus.Text = "Статус:"
        Me.LabelWriterStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'edURLTt
        '
        Me.edURLTt.AcceptsReturn = True
        Me.edURLTt.BackColor = System.Drawing.SystemColors.Window
        Me.edURLTt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.edURLTt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.edURLTt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.edURLTt.Location = New System.Drawing.Point(100, 23)
        Me.edURLTt.MaxLength = 0
        Me.edURLTt.Name = "edURLTt"
        Me.edURLTt.ReadOnly = True
        Me.edURLTt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.edURLTt.Size = New System.Drawing.Size(189, 20)
        Me.edURLTt.TabIndex = 89
        Me.edURLTt.TabStop = False
        Me.edURLTt.Text = "dstp://localhost/Tt"
        Me.ToolTipForm.SetToolTip(Me.edURLTt, "Сетевой адрес для подключения УИМС к Регистратору")
        '
        'ButtonWriterConnect
        '
        Me.ButtonWriterConnect.BackColor = System.Drawing.Color.Silver
        Me.ButtonWriterConnect.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonWriterConnect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonWriterConnect.Location = New System.Drawing.Point(100, 83)
        Me.ButtonWriterConnect.Name = "ButtonWriterConnect"
        Me.ButtonWriterConnect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonWriterConnect.Size = New System.Drawing.Size(111, 27)
        Me.ButtonWriterConnect.TabIndex = 0
        Me.ButtonWriterConnect.Text = "Установить связь"
        Me.ToolTipForm.SetToolTip(Me.ButtonWriterConnect, "Установить связь для отправки  в УИМС")
        Me.ButtonWriterConnect.UseVisualStyleBackColor = False
        '
        'ButtonWriterDisconnect
        '
        Me.ButtonWriterDisconnect.BackColor = System.Drawing.Color.Silver
        Me.TableLayoutPanel1.SetColumnSpan(Me.ButtonWriterDisconnect, 2)
        Me.ButtonWriterDisconnect.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonWriterDisconnect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonWriterDisconnect.Location = New System.Drawing.Point(295, 83)
        Me.ButtonWriterDisconnect.Name = "ButtonWriterDisconnect"
        Me.ButtonWriterDisconnect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonWriterDisconnect.Size = New System.Drawing.Size(111, 27)
        Me.ButtonWriterDisconnect.TabIndex = 86
        Me.ButtonWriterDisconnect.Text = "Прервать связь"
        Me.ToolTipForm.SetToolTip(Me.ButtonWriterDisconnect, "Прервать связь")
        Me.ButtonWriterDisconnect.UseVisualStyleBackColor = False
        '
        'ButtonPathOPCWriter
        '
        Me.ButtonPathOPCWriter.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonPathOPCWriter.BackgroundImage = CType(resources.GetObject("ButtonPathOPCWriter.BackgroundImage"), System.Drawing.Image)
        Me.ButtonPathOPCWriter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonPathOPCWriter.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonPathOPCWriter.Font = New System.Drawing.Font("Arial Black", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ButtonPathOPCWriter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonPathOPCWriter.Location = New System.Drawing.Point(392, 138)
        Me.ButtonPathOPCWriter.Name = "ButtonPathOPCWriter"
        Me.ButtonPathOPCWriter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonPathOPCWriter.Size = New System.Drawing.Size(24, 20)
        Me.ButtonPathOPCWriter.TabIndex = 4
        Me.ButtonPathOPCWriter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTipForm.SetToolTip(Me.ButtonPathOPCWriter, "Настройка пути к OPCWriter.exe")
        Me.ButtonPathOPCWriter.UseVisualStyleBackColor = False
        '
        'TextPathOPCWriter
        '
        Me.TextPathOPCWriter.AcceptsReturn = True
        Me.TextPathOPCWriter.BackColor = System.Drawing.SystemColors.Window
        Me.TableLayoutPanel1.SetColumnSpan(Me.TextPathOPCWriter, 2)
        Me.TextPathOPCWriter.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextPathOPCWriter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextPathOPCWriter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextPathOPCWriter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextPathOPCWriter.Location = New System.Drawing.Point(100, 138)
        Me.TextPathOPCWriter.MaxLength = 0
        Me.TextPathOPCWriter.Name = "TextPathOPCWriter"
        Me.TextPathOPCWriter.ReadOnly = True
        Me.TextPathOPCWriter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextPathOPCWriter.Size = New System.Drawing.Size(286, 20)
        Me.TextPathOPCWriter.TabIndex = 5
        Me.TextPathOPCWriter.TabStop = False
        Me.ToolTipForm.SetToolTip(Me.TextPathOPCWriter, "Выбранный путь к OPCWriter.exe")
        '
        'PanelControls
        '
        Me.PanelControls.BackColor = System.Drawing.SystemColors.Control
        Me.PanelControls.Controls.Add(Me.FlowLayoutPanelControls)
        Me.PanelControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControls.Location = New System.Drawing.Point(0, 0)
        Me.PanelControls.Name = "PanelControls"
        Me.PanelControls.Size = New System.Drawing.Size(508, 675)
        Me.PanelControls.TabIndex = 5
        '
        'FlowLayoutPanelControls
        '
        Me.FlowLayoutPanelControls.AutoScroll = True
        Me.FlowLayoutPanelControls.BackColor = System.Drawing.Color.DimGray
        Me.FlowLayoutPanelControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanelControls.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanelControls.Name = "FlowLayoutPanelControls"
        Me.FlowLayoutPanelControls.Size = New System.Drawing.Size(508, 675)
        Me.FlowLayoutPanelControls.TabIndex = 58
        '
        'ToolStripOPC
        '
        Me.ToolStripOPC.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStripOPC.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabelStatus, Me.OPCStatusText, Me.ToolStripSeparator3, Me.ButtonOPCReaderConnect, Me.ToolStripSeparator4, Me.ButtonOPCReaderUpdate, Me.ToolStripSeparator5, Me.ButtonOPCReaderDisconnect, Me.ToolStripSeparator6, Me.TextBoxНетСвязи, Me.TextError})
        Me.ToolStripOPC.Location = New System.Drawing.Point(3, 0)
        Me.ToolStripOPC.Name = "ToolStripOPC"
        Me.ToolStripOPC.Size = New System.Drawing.Size(821, 25)
        Me.ToolStripOPC.TabIndex = 154
        '
        'ToolStripLabelStatus
        '
        Me.ToolStripLabelStatus.Name = "ToolStripLabelStatus"
        Me.ToolStripLabelStatus.Size = New System.Drawing.Size(46, 22)
        Me.ToolStripLabelStatus.Text = "Статус:"
        '
        'OPCStatusText
        '
        Me.OPCStatusText.AutoSize = False
        Me.OPCStatusText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.OPCStatusText.Name = "OPCStatusText"
        Me.OPCStatusText.Size = New System.Drawing.Size(200, 22)
        Me.OPCStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OPCStatusText.ToolTipText = "Сообщение о связи с OPC Сервером"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ButtonOPCReaderConnect
        '
        Me.ButtonOPCReaderConnect.Image = CType(resources.GetObject("ButtonOPCReaderConnect.Image"), System.Drawing.Image)
        Me.ButtonOPCReaderConnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonOPCReaderConnect.Name = "ButtonOPCReaderConnect"
        Me.ButtonOPCReaderConnect.Size = New System.Drawing.Size(136, 22)
        Me.ButtonOPCReaderConnect.Text = "Подключить МКИО"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ButtonOPCReaderUpdate
        '
        Me.ButtonOPCReaderUpdate.Image = CType(resources.GetObject("ButtonOPCReaderUpdate.Image"), System.Drawing.Image)
        Me.ButtonOPCReaderUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonOPCReaderUpdate.Name = "ButtonOPCReaderUpdate"
        Me.ButtonOPCReaderUpdate.Size = New System.Drawing.Size(113, 22)
        Me.ButtonOPCReaderUpdate.Text = "Обновить связь"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'ButtonOPCReaderDisconnect
        '
        Me.ButtonOPCReaderDisconnect.Image = CType(resources.GetObject("ButtonOPCReaderDisconnect.Image"), System.Drawing.Image)
        Me.ButtonOPCReaderDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonOPCReaderDisconnect.Name = "ButtonOPCReaderDisconnect"
        Me.ButtonOPCReaderDisconnect.Size = New System.Drawing.Size(128, 22)
        Me.ButtonOPCReaderDisconnect.Text = "Отключить МКИО"
        Me.ButtonOPCReaderDisconnect.ToolTipText = "Отключить МКИО"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'TextBoxНетСвязи
        '
        Me.TextBoxНетСвязи.BackColor = System.Drawing.Color.Maroon
        Me.TextBoxНетСвязи.Enabled = False
        Me.TextBoxНетСвязи.Name = "TextBoxНетСвязи"
        Me.TextBoxНетСвязи.ReadOnly = True
        Me.TextBoxНетСвязи.Size = New System.Drawing.Size(160, 25)
        Me.TextBoxНетСвязи.Text = "Идет восстановление связи"
        Me.TextBoxНетСвязи.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TextBoxНетСвязи.ToolTipText = "Индикатор  восстановление связи"
        '
        'TextError
        '
        Me.TextError.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TextError.BackColor = System.Drawing.Color.Yellow
        Me.TextError.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TextError.ForeColor = System.Drawing.Color.Red
        Me.TextError.Name = "TextError"
        Me.TextError.ReadOnly = True
        Me.TextError.Size = New System.Drawing.Size(300, 26)
        Me.TextError.Text = "ошибка расчета"
        Me.TextError.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TextError.ToolTipText = "Индикатор ошибки расчёта"
        Me.TextError.Visible = False
        '
        'ImageListMenu
        '
        Me.ImageListMenu.ImageStream = CType(resources.GetObject("ImageListMenu.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListMenu.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListMenu.Images.SetKeyName(0, "")
        Me.ImageListMenu.Images.SetKeyName(1, "")
        Me.ImageListMenu.Images.SetKeyName(2, "")
        Me.ImageListMenu.Images.SetKeyName(3, "")
        Me.ImageListMenu.Images.SetKeyName(4, "")
        Me.ImageListMenu.Images.SetKeyName(5, "")
        Me.ImageListMenu.Images.SetKeyName(6, "")
        Me.ImageListMenu.Images.SetKeyName(7, "")
        Me.ImageListMenu.Images.SetKeyName(8, "")
        Me.ImageListMenu.Images.SetKeyName(9, "")
        Me.ImageListMenu.Images.SetKeyName(10, "")
        Me.ImageListMenu.Images.SetKeyName(11, "")
        Me.ImageListMenu.Images.SetKeyName(12, "")
        Me.ImageListMenu.Images.SetKeyName(13, "")
        Me.ImageListMenu.Images.SetKeyName(14, "")
        '
        'TimerOPC
        '
        Me.TimerOPC.Interval = 200
        '
        'TimerCheckDataSocketOPCWriter
        '
        Me.TimerCheckDataSocketOPCWriter.Interval = 2000
        '
        'DataSocketOPCReader
        '
        Me.DataSocketOPCReader.AccessMode = NationalInstruments.Net.AccessMode.Read
        Me.DataSocketOPCReader.AutoConnect = False
        '
        'DataSocketWriter
        '
        Me.DataSocketWriter.AccessMode = NationalInstruments.Net.AccessMode.Write
        Me.DataSocketWriter.AutoConnect = False
        '
        'FormOPCclient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.ToolStripContainerForm)
        Me.Controls.Add(Me.StatusBar)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "FormOPCclient"
        Me.Tag = ""
        Me.Text = "Контроль связи"
        Me.StatusBar.ResumeLayout(False)
        Me.StatusBar.PerformLayout()
        Me.ToolStripContainerForm.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainerForm.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainerForm.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainerForm.ResumeLayout(False)
        Me.ToolStripContainerForm.PerformLayout()
        Me.SplitContainerMain.Panel1.ResumeLayout(False)
        Me.SplitContainerMain.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerMain.ResumeLayout(False)
        Me.TableLayoutPanelAll.ResumeLayout(False)
        Me.PanelCentre.ResumeLayout(False)
        Me.TableLayoutPanelCentre.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.PanelControls.ResumeLayout(False)
        Me.ToolStripOPC.ResumeLayout(False)
        Me.ToolStripOPC.PerformLayout()
        CType(Me.DataSocketOPCReader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSocketWriter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusLabelMessage As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripContainerForm As System.Windows.Forms.ToolStripContainer
    Friend WithEvents ImageListMenu As System.Windows.Forms.ImageList
    Friend WithEvents SplitContainerMain As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelControls As System.Windows.Forms.Panel
    Friend WithEvents FlowLayoutPanelControls As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents ToolTipForm As System.Windows.Forms.ToolTip
    Public WithEvents StatusBar As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripOPC As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabelStatus As Windows.Forms.ToolStripLabel
    Friend WithEvents OPCStatusText As Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator3 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ButtonOPCReaderConnect As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ButtonOPCReaderUpdate As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ButtonOPCReaderDisconnect As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As Windows.Forms.ToolStripSeparator
    Friend WithEvents TextBoxНетСвязи As Windows.Forms.ToolStripTextBox
    Public WithEvents TimerOPC As Windows.Forms.Timer
    Public WithEvents TimerCheckDataSocketOPCWriter As Windows.Forms.Timer
    Public WithEvents DataSocketOPCReader As NationalInstruments.Net.DataSocket
    Public WithEvents TextPathOPCWriter As Windows.Forms.TextBox
    Public WithEvents ButtonPathOPCWriter As Windows.Forms.Button
    Friend WithEvents OpenFileDialogPuth As Windows.Forms.OpenFileDialog
    Friend WithEvents StatusLabelStend As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TextError As Windows.Forms.ToolStripTextBox
    Public WithEvents DataSocketWriter As NationalInstruments.Net.DataSocket
    Public WithEvents ButtonWriterDisconnect As Windows.Forms.Button
    Public WithEvents LabelWriterPuth As Windows.Forms.Label
    Public WithEvents ButtonWriterConnect As Windows.Forms.Button
    Public WithEvents edURLTt As Windows.Forms.TextBox
    Public WithEvents edStatusTt As Windows.Forms.TextBox
    Public WithEvents LabelWriterStatus As Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As Windows.Forms.TableLayoutPanel
    Friend WithEvents TextBoxWriter As Windows.Forms.TextBox
    Friend WithEvents TextBoxTbox As Windows.Forms.TextBox
    Public WithEvents LabelWriterTbox As Windows.Forms.Label
    Friend WithEvents TextBoxPuthOPCWriterExe As Windows.Forms.TextBox
    Friend WithEvents ImageListChannel As Windows.Forms.ImageList
    Friend WithEvents TableLayoutPanelAll As Windows.Forms.TableLayoutPanel
    Friend WithEvents PanelCentre As Windows.Forms.Panel
    Friend WithEvents TableLayoutPanelCentre As Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As Windows.Forms.TableLayoutPanel
    Friend WithEvents ButtonInsert As Windows.Forms.Button
    Friend WithEvents ButtonRemove As Windows.Forms.Button
    Friend WithEvents ButtonDown As Windows.Forms.Button
    Friend WithEvents ButtonUp As Windows.Forms.Button
    Friend WithEvents ButtonErase As Windows.Forms.Button
    Public WithEvents LabelDescription As Windows.Forms.Label
    Friend WithEvents lvwSource As Windows.Forms.ListView
    Friend WithEvents lvwReceiver As ListViewCustomReorder.ListViewEx
End Class
