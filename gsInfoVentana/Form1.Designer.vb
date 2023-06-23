<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.lvVentanas = New System.Windows.Forms.ListView()
        Me.chTítulo = New System.Windows.Forms.ColumnHeader()
        Me.chHandle = New System.Windows.Forms.ColumnHeader()
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCapturar = New System.Windows.Forms.Button()
        Me.txtHandle = New System.Windows.Forms.TextBox()
        Me.btnActualizar = New System.Windows.Forms.Button()
        Me.btnAsignarTamPos = New System.Windows.Forms.Button()
        Me.btnCopiarTamPos = New System.Windows.Forms.Button()
        Me.btnPegarTamPos = New System.Windows.Forms.Button()
        Me.chkTopMost = New System.Windows.Forms.CheckBox()
        Me.picInfo = New System.Windows.Forms.PictureBox()
        Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.labelInfo1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.labelInfo2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.labelActiva = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTitulo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboChildWnd = New System.Windows.Forms.ComboBox()
        Me.timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.optCursor = New System.Windows.Forms.RadioButton()
        Me.optHandle = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboCopias = New System.Windows.Forms.ComboBox()
        Me.txtHeight = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtWidth = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTop = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtLeft = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.picInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.statusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvVentanas
        '
        Me.lvVentanas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvVentanas.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chTítulo, Me.chHandle})
        Me.lvVentanas.FullRowSelect = True
        Me.lvVentanas.GridLines = True
        Me.lvVentanas.HideSelection = False
        Me.lvVentanas.Location = New System.Drawing.Point(14, 366)
        Me.lvVentanas.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.lvVentanas.MultiSelect = False
        Me.lvVentanas.Name = "lvVentanas"
        Me.lvVentanas.Size = New System.Drawing.Size(887, 255)
        Me.lvVentanas.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvVentanas.TabIndex = 15
        Me.toolTip1.SetToolTip(Me.lvVentanas, "Lista con la información de las ventanas actuales." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Pulsa en el botón 'Actualiz" &
        "ar' para refrescar la lista de las ventanas," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ya que inicialmente se muestran la" &
        "s que había al iniciar la aplicación.")
        Me.lvVentanas.UseCompatibleStateImageBehavior = False
        Me.lvVentanas.View = System.Windows.Forms.View.Details
        '
        'chTítulo
        '
        Me.chTítulo.Text = "Título"
        Me.chTítulo.Width = 700
        '
        'chHandle
        '
        Me.chHandle.Text = "Handle"
        Me.chHandle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.chHandle.Width = 120
        '
        'btnCapturar
        '
        Me.btnCapturar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCapturar.Location = New System.Drawing.Point(776, 64)
        Me.btnCapturar.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.btnCapturar.Name = "btnCapturar"
        Me.btnCapturar.Size = New System.Drawing.Size(125, 44)
        Me.btnCapturar.TabIndex = 4
        Me.btnCapturar.Text = "Capturar"
        Me.toolTip1.SetToolTip(Me.btnCapturar, " Capturar la información de la ventana activa o la indicada por el Handle ")
        Me.btnCapturar.UseVisualStyleBackColor = True
        '
        'txtHandle
        '
        Me.txtHandle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHandle.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtHandle.Location = New System.Drawing.Point(776, 25)
        Me.txtHandle.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtHandle.Name = "txtHandle"
        Me.txtHandle.Size = New System.Drawing.Size(125, 29)
        Me.txtHandle.TabIndex = 2
        Me.txtHandle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.toolTip1.SetToolTip(Me.txtHandle, "El Handle de la ventana que se ha capturado " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "por la posición del cursor del rató" &
        "n " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "o por seleccionar una de la lista.")
        '
        'btnActualizar
        '
        Me.btnActualizar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActualizar.Location = New System.Drawing.Point(770, 298)
        Me.btnActualizar.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(125, 44)
        Me.btnActualizar.TabIndex = 12
        Me.btnActualizar.Text = "Actualizar"
        Me.toolTip1.SetToolTip(Me.btnActualizar, "Actualizar la lista de ventanas")
        Me.btnActualizar.UseVisualStyleBackColor = True
        '
        'btnAsignarTamPos
        '
        Me.btnAsignarTamPos.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAsignarTamPos.Location = New System.Drawing.Point(605, 90)
        Me.btnAsignarTamPos.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.btnAsignarTamPos.Name = "btnAsignarTamPos"
        Me.btnAsignarTamPos.Size = New System.Drawing.Size(125, 44)
        Me.btnAsignarTamPos.TabIndex = 12
        Me.btnAsignarTamPos.Text = "Posicionar"
        Me.toolTip1.SetToolTip(Me.btnAsignarTamPos, "Asignar el tamaño y posición a la ventana con el Handle indicado.")
        Me.btnAsignarTamPos.UseVisualStyleBackColor = True
        '
        'btnCopiarTamPos
        '
        Me.btnCopiarTamPos.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCopiarTamPos.Image = CType(resources.GetObject("btnCopiarTamPos.Image"), System.Drawing.Image)
        Me.btnCopiarTamPos.Location = New System.Drawing.Point(624, 30)
        Me.btnCopiarTamPos.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.btnCopiarTamPos.Name = "btnCopiarTamPos"
        Me.btnCopiarTamPos.Size = New System.Drawing.Size(36, 36)
        Me.btnCopiarTamPos.TabIndex = 8
        Me.toolTip1.SetToolTip(Me.btnCopiarTamPos, "Copiar el tamaño y la posición")
        Me.btnCopiarTamPos.UseVisualStyleBackColor = True
        '
        'btnPegarTamPos
        '
        Me.btnPegarTamPos.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPegarTamPos.Image = CType(resources.GetObject("btnPegarTamPos.Image"), System.Drawing.Image)
        Me.btnPegarTamPos.Location = New System.Drawing.Point(678, 30)
        Me.btnPegarTamPos.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.btnPegarTamPos.Name = "btnPegarTamPos"
        Me.btnPegarTamPos.Size = New System.Drawing.Size(36, 36)
        Me.btnPegarTamPos.TabIndex = 9
        Me.toolTip1.SetToolTip(Me.btnPegarTamPos, "Pegar la posición y tamaño copiados")
        Me.btnPegarTamPos.UseVisualStyleBackColor = True
        '
        'chkTopMost
        '
        Me.chkTopMost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTopMost.AutoSize = True
        Me.chkTopMost.Checked = True
        Me.chkTopMost.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTopMost.Location = New System.Drawing.Point(776, 168)
        Me.chkTopMost.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.chkTopMost.Name = "chkTopMost"
        Me.chkTopMost.Size = New System.Drawing.Size(108, 29)
        Me.chkTopMost.TabIndex = 14
        Me.chkTopMost.Text = "TopMost"
        Me.toolTip1.SetToolTip(Me.chkTopMost, "Mantener la ventana siempre encima de las demás")
        Me.chkTopMost.UseVisualStyleBackColor = True
        '
        'picInfo
        '
        Me.picInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picInfo.Image = CType(resources.GetObject("picInfo.Image"), System.Drawing.Image)
        Me.picInfo.Location = New System.Drawing.Point(812, 124)
        Me.picInfo.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.picInfo.Name = "picInfo"
        Me.picInfo.Size = New System.Drawing.Size(24, 24)
        Me.picInfo.TabIndex = 15
        Me.picInfo.TabStop = False
        Me.toolTip1.SetToolTip(Me.picInfo, "Muestra la información sobre esta utilidad")
        '
        'statusStrip1
        '
        Me.statusStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.labelInfo1, Me.labelInfo2})
        Me.statusStrip1.Location = New System.Drawing.Point(0, 633)
        Me.statusStrip1.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.statusStrip1.Name = "statusStrip1"
        Me.statusStrip1.Padding = New System.Windows.Forms.Padding(2, 0, 23, 0)
        Me.statusStrip1.Size = New System.Drawing.Size(915, 32)
        Me.statusStrip1.TabIndex = 16
        Me.statusStrip1.Text = "StatusStrip1"
        '
        'labelInfo1
        '
        Me.labelInfo1.Name = "labelInfo1"
        Me.labelInfo1.Size = New System.Drawing.Size(740, 25)
        Me.labelInfo1.Spring = True
        Me.labelInfo1.Text = "©Guillermo Som (elGuille), 2010-2021"
        Me.labelInfo1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labelInfo2
        '
        Me.labelInfo2.AutoSize = False
        Me.labelInfo2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.labelInfo2.Name = "labelInfo2"
        Me.labelInfo2.Size = New System.Drawing.Size(150, 25)
        Me.labelInfo2.Text = "v3.0.0.0 (3.0.0.0)"
        '
        'labelActiva
        '
        Me.labelActiva.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labelActiva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.labelActiva.Location = New System.Drawing.Point(20, 67)
        Me.labelActiva.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.labelActiva.Name = "labelActiva"
        Me.labelActiva.Size = New System.Drawing.Size(737, 38)
        Me.labelActiva.TabIndex = 3
        Me.labelActiva.Text = "labelActiva"
        Me.labelActiva.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(20, 120)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(148, 31)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Título y Handle:"
        '
        'txtTitulo
        '
        Me.txtTitulo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTitulo.Location = New System.Drawing.Point(178, 117)
        Me.txtTitulo.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtTitulo.Name = "txtTitulo"
        Me.txtTitulo.Size = New System.Drawing.Size(579, 31)
        Me.txtTitulo.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(20, 166)
        Me.Label2.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(148, 31)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Subventanas:"
        '
        'cboChildWnd
        '
        Me.cboChildWnd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboChildWnd.FormattingEnabled = True
        Me.cboChildWnd.Location = New System.Drawing.Point(178, 163)
        Me.cboChildWnd.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cboChildWnd.Name = "cboChildWnd"
        Me.cboChildWnd.Size = New System.Drawing.Size(579, 33)
        Me.cboChildWnd.TabIndex = 9
        '
        'timer1
        '
        Me.timer1.Interval = 300
        '
        'optCursor
        '
        Me.optCursor.AutoSize = True
        Me.optCursor.Checked = True
        Me.optCursor.Location = New System.Drawing.Point(20, 23)
        Me.optCursor.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.optCursor.Name = "optCursor"
        Me.optCursor.Size = New System.Drawing.Size(318, 29)
        Me.optCursor.TabIndex = 0
        Me.optCursor.TabStop = True
        Me.optCursor.Text = "Según posición del cursor del ratón"
        Me.optCursor.UseVisualStyleBackColor = True
        '
        'optHandle
        '
        Me.optHandle.AutoSize = True
        Me.optHandle.Location = New System.Drawing.Point(348, 23)
        Me.optHandle.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.optHandle.Name = "optHandle"
        Me.optHandle.Size = New System.Drawing.Size(331, 29)
        Me.optHandle.TabIndex = 1
        Me.optHandle.Text = "Según valor del Handle de la ventana"
        Me.optHandle.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cboCopias)
        Me.GroupBox1.Controls.Add(Me.btnPegarTamPos)
        Me.GroupBox1.Controls.Add(Me.btnCopiarTamPos)
        Me.GroupBox1.Controls.Add(Me.btnAsignarTamPos)
        Me.GroupBox1.Controls.Add(Me.txtHeight)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtWidth)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtTop)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtLeft)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 208)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupBox1.Size = New System.Drawing.Size(740, 146)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Posición y tamaño de la ventana"
        '
        'cboCopias
        '
        Me.cboCopias.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboCopias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCopias.FormattingEnabled = True
        Me.cboCopias.Items.AddRange(New Object() {"<General>", "Ventana", "Visual Studio"})
        Me.cboCopias.Location = New System.Drawing.Point(414, 33)
        Me.cboCopias.Name = "cboCopias"
        Me.cboCopias.Size = New System.Drawing.Size(183, 33)
        Me.cboCopias.TabIndex = 13
        '
        'txtHeight
        '
        Me.txtHeight.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtHeight.Location = New System.Drawing.Point(302, 87)
        Me.txtHeight.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtHeight.Name = "txtHeight"
        Me.txtHeight.Size = New System.Drawing.Size(89, 29)
        Me.txtHeight.TabIndex = 7
        Me.txtHeight.Text = "-3399"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(207, 81)
        Me.Label6.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 37)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Height:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtWidth
        '
        Me.txtWidth.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtWidth.Location = New System.Drawing.Point(302, 37)
        Me.txtWidth.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.Size = New System.Drawing.Size(89, 29)
        Me.txtWidth.TabIndex = 3
        Me.txtWidth.Text = "-3399"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(207, 31)
        Me.Label5.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 37)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Width:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTop
        '
        Me.txtTop.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtTop.Location = New System.Drawing.Point(105, 87)
        Me.txtTop.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtTop.Name = "txtTop"
        Me.txtTop.Size = New System.Drawing.Size(89, 29)
        Me.txtTop.TabIndex = 5
        Me.txtTop.Text = "-3399"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(10, 81)
        Me.Label4.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 37)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Top:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLeft
        '
        Me.txtLeft.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtLeft.Location = New System.Drawing.Point(105, 37)
        Me.txtLeft.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtLeft.Name = "txtLeft"
        Me.txtLeft.Size = New System.Drawing.Size(89, 29)
        Me.txtLeft.TabIndex = 1
        Me.txtLeft.Text = "-3399"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(10, 31)
        Me.Label3.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 37)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Left:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(915, 665)
        Me.Controls.Add(Me.chkTopMost)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.picInfo)
        Me.Controls.Add(Me.btnActualizar)
        Me.Controls.Add(Me.optHandle)
        Me.Controls.Add(Me.optCursor)
        Me.Controls.Add(Me.txtHandle)
        Me.Controls.Add(Me.cboChildWnd)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTitulo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.labelActiva)
        Me.Controls.Add(Me.btnCapturar)
        Me.Controls.Add(Me.statusStrip1)
        Me.Controls.Add(Me.lvVentanas)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(850, 600)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "gsInfoVentana NetCore - Información de las ventanas"
        Me.TopMost = True
        CType(Me.picInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.statusStrip1.ResumeLayout(False)
        Me.statusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents lvVentanas As System.Windows.Forms.ListView
    Private WithEvents chTítulo As System.Windows.Forms.ColumnHeader
    Private WithEvents chHandle As System.Windows.Forms.ColumnHeader
    Private WithEvents labelInfo1 As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents labelInfo2 As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents statusStrip1 As System.Windows.Forms.StatusStrip
    Private WithEvents btnCapturar As System.Windows.Forms.Button
    Private WithEvents labelActiva As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents txtTitulo As System.Windows.Forms.TextBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents cboChildWnd As System.Windows.Forms.ComboBox
    Private WithEvents txtHandle As System.Windows.Forms.TextBox
    Private WithEvents timer1 As System.Windows.Forms.Timer
    Private WithEvents optCursor As System.Windows.Forms.RadioButton
    Private WithEvents optHandle As System.Windows.Forms.RadioButton
    Friend WithEvents picInfo As PictureBox
    Private WithEvents GroupBox1 As GroupBox
    Private WithEvents Label4 As Label
    Private WithEvents Label3 As Label
    Private WithEvents txtHeight As TextBox
    Private WithEvents Label6 As Label
    Private WithEvents txtWidth As TextBox
    Private WithEvents Label5 As Label
    Private WithEvents txtTop As TextBox
    Private WithEvents txtLeft As TextBox
    Private WithEvents btnAsignarTamPos As Button
    Private WithEvents btnActualizar As Button
    Private WithEvents btnPegarTamPos As Button
    Private WithEvents btnCopiarTamPos As Button
    Friend WithEvents chkTopMost As CheckBox
    Private WithEvents cboCopias As ComboBox
End Class
