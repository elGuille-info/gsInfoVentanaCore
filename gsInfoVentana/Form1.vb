'------------------------------------------------------------------------------
' InfoVentana
' Utilidad para capturar la información de la ventana activa
'
' Primera versión                                                   (18/Dic/10)
' Cambio el nombre a gsInfoVentana ya que InfoVentana me daba problema
' porque en Proyecto>Compilar>Plataforma estaba como x86 y no mostraba Any CPU
' Última revisión para .NET Framework 4.8                           (26/Abr/21)
' v2.0.0.9  26-abr-21   Que el Listview esté ordenado y le cambio el alto
' v2.0.0.10             Check para poner la ventana TopMost
' Revisión para usar con .NET 5.0 (net core)                        (07/Jun/21)
' v3.0.0.0  07-jun-21   Nueva versión usando .NET 5.0 (net core)
' v3.0.0.3  08-jun-21   Nuevos iconos y recordar tamaño/posición copiados
' v3.0.0.4              Recuerda 3 tamaños/posiciones: General, Ventana del explorador y Visual Studio
'
' ©Guillermo Som (elGuille), 2010, 2019-2021
'------------------------------------------------------------------------------
Option Strict On
Option Infer On

Imports Microsoft.VisualBasic

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Imports System.Windows.Forms

Imports vb = Microsoft.VisualBasic

' Importación a las clases para acceder a la información de la ventana
Imports gsMsgWnd = elGuille.gsMsg2WndLib

Imports System.Runtime.InteropServices


'******************************************************************************
' Valores del diseñador de formularios
'
' El de .NET Framework 4.8
'Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
'Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
'Me.ClientSize = New System.Drawing.Size(549, 426)
'
' De .NET Core (al crear la aplicación y asignar el tamaño)
'Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 25.0!)
'Me.ClientSize = New System.Drawing.Size(824, 655)
'
' Al cambiar el AutoScaleDimensions a 6.0!, 13.0!
'Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 25.0!)
'Me.ClientSize = New System.Drawing.Size(915, 819)
'
'******************************************************************************

Public Class Form1

    ''' <summary>
    ''' La versión de FileVersion
    ''' </summary>
    ''' <remarks>15/Sep/2020</remarks>
    Private FileVersion As String

    Private inicializando As Boolean = True

    Private mMsg2Wnd As gsMsgWnd.cMsg2Wnd
    Private mChild As gsMsgWnd.cVentana

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        labelInfo1.Text = "©Guillermo Som (elGuille), 2010-"
        If Date.Now.Year > 2021 Then
            labelInfo1.Text &= Date.Now.Year.ToString
        Else
            labelInfo1.Text &= "2021"
        End If

        Dim ensamblado As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly
        Dim fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(ensamblado.Location)
        FileVersion = fvi.FileVersion
        ' La versión de esta aplicación
        labelInfo2.Text = $" v{My.Application.Info.Version} ({FileVersion})"

        mMsg2Wnd = New gsMsgWnd.cMsg2Wnd

        labelActiva.Text = ""
        txtHandle.Text = "0"

        mostrarVentanas()

        ' Parámetros para los Tooltips                              (18/Mar/19)
        ' El tiempo durante el que se muestra, que sea el doble del predeterminado
        toolTip1.AutoPopDelay *= 2

        btnAsignarTamPos.Enabled = False
        posVentana(Me.Handle.ToInt32)

        optHandle.Checked = My.Settings.optSegunHandle

        timer1.Interval = 300
        timer1.Enabled = optCursor.Checked

        btnAsignarTamPos.Enabled = Not timer1.Enabled

        With My.Settings
            If .copiadoH > -1 Then
                txtHeight.Text = .copiadoH.ToString
            End If
            If .copiadoL > -1 Then
                txtLeft.Text = .copiadoL.ToString
            End If
            If .copiadoT > -1 Then
                txtTop.Text = .copiadoT.ToString
            End If
            If .copiadoW > -1 Then
                txtWidth.Text = .copiadoW.ToString
            End If
            PasteVent = .pasteVent ' "656;980;1920;1128"
            PasteVS = .pasteVS ' "452;27;3003;2031"
            PasteNing = .pasteNing
            chkTopMost.Checked = .topMost
        End With

        inicializando = False
    End Sub

    Private PasteVent As String = "-1"
    Private PasteVS As String = "-1"
    Private PasteNing As String = "-1"

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        With My.Settings
            .optSegunHandle = optHandle.Checked
            .copiadoH = CInt(txtHeight.Text)
            .copiadoL = CInt(txtLeft.Text)
            .copiadoT = CInt(txtTop.Text)
            .copiadoW = CInt(txtWidth.Text)
            .pasteVent = PasteVent
            .pasteVS = PasteVS
            .pasteNing = PasteNing
            .topMost = chkTopMost.Checked
            .Save()
        End With
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ajustarAnchoColumnasListView()
    End Sub

    Private Sub ajustarAnchoColumnasListView()
        Dim lwW = lvVentanas.Width
        chTítulo.Width = lwW - chHandle.Width - 48
    End Sub

    Private Sub posVentana(hWnd As Integer)
        Dim res = gsMsgWnd.cMsg2Wnd.PosicionVentana(hWnd)

        txtTop.Text = res.Top.ToString
        txtLeft.Text = res.Left.ToString
        txtWidth.Text = res.Width.ToString
        txtHeight.Text = res.Height.ToString

        cboCopias.Text = "<General>"
        If String.IsNullOrWhiteSpace(txtTitulo.Text) Then
            Return
        End If

        If txtTitulo.Text.Contains("Visual Studio") Then
            cboCopias.Text = "Visual Studio"
        Else
            If cboChildWnd.Items.Count = 0 Then Return
            For i = 0 To cboChildWnd.Items.Count - 1
                If cboChildWnd.Items(i).ToString().Contains(txtTitulo.Text) Then
                    cboCopias.Text = "Ventana"
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub mostrarVentanas()
        Dim tVent As gsMsgWnd.cVentana

        lvVentanas.Items.Clear()
        Try
            mMsg2Wnd.EnumerarVentanas(True)
            For Each tVent In mMsg2Wnd.TopWindows
                If tVent.Handle > 0 Then
                    Dim lvi = lvVentanas.Items.Add(tVent.Nombre)
                    lvi.SubItems.Add(tVent.Handle.ToString)

                End If
            Next tVent
        Catch 'ex As Exception
        End Try
    End Sub

    Private Sub btnCapturar_Click(sender As Object,
                                  e As EventArgs) Handles btnCapturar.Click
        ' Usar el handle indicado en txtHandle
        Dim h As Integer = CInt(vb.Val(txtHandle.Text))
        'txtHandle2.Text = txtHandle.Text
        txtTitulo.Text = mMsg2Wnd.GetText(h)
        labelActiva.Text = txtTitulo.Text

        cboChildWnd.Items.Clear()
        cboChildWnd.Text = ""

        mMsg2Wnd.EnumerarVentanasHijas(h, False)

        For Each tVent As gsMsgWnd.cVentana In mMsg2Wnd.ChildWindows
            ' Sólo mostrar las visibles...                          (26/Sep/02)
            If (tVent.Handle > 0) Then
                cboChildWnd.Items.Add(tVent)
            End If
        Next tVent
        If cboChildWnd.Items.Count > 0 Then
            cboChildWnd.SelectedIndex = 0
        End If

        ' mostrar la posición de la ventana al "Capturar"           (18/Mar/19)
        posVentana(h)
    End Sub

    Private Sub timer1_Tick(sender As Object,
                            e As EventArgs) Handles timer1.Tick
        timer1.Enabled = False

        Dim p As Drawing.Point = System.Windows.Forms.Cursor.Position
        Dim h As Integer = gsMsgWnd.MCallback.WindowFromPoint(p)

        If h <> 0 Then
            ' obtener el texto de la ventana según el handle
            labelActiva.Text = mMsg2Wnd.GetText(h)
            txtHandle.Text = h.ToString

            ' mostrar la posición
            posVentana(h)
        End If

        timer1.Enabled = True
    End Sub

    Private Sub optCursor_CheckedChanged(sender As Object, e As EventArgs) Handles optCursor.CheckedChanged, optHandle.CheckedChanged

        If inicializando Then Return

        timer1.Enabled = optCursor.Checked
        btnAsignarTamPos.Enabled = Not timer1.Enabled
    End Sub

    Private Sub cboChildWnd_TextChanged(sender As Object, e As EventArgs) Handles cboChildWnd.TextChanged
        If inicializando Then Return

        With cboChildWnd
            If .SelectedIndex > -1 Then
                mChild = mMsg2Wnd.ChildWindows(CType(.SelectedItem, gsMsgWnd.cVentana).Handle.ToString)
            End If
        End With

    End Sub

    Private Sub BtnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        mostrarVentanas()
    End Sub

    Private Sub LvVentanas_SelectedIndexChanged(sender As Object,
                                                e As EventArgs) Handles lvVentanas.SelectedIndexChanged
        If inicializando Then Return

        If lvVentanas.SelectedItems.Count = 0 Then Return

        Dim lvi = lvVentanas.SelectedItems(0)
        txtHandle.Text = lvi.SubItems(1).Text

        ' Mostrar la información de la ventana
        btnCapturar_Click(Nothing, Nothing)
    End Sub

    Private Sub picInfo_Click(sender As Object, e As EventArgs) Handles picInfo.Click
        ' Mostrar acerca de...                                      (15/Sep/20)
        Dim vers = $" v{My.Application.Info.Version} ({FileVersion})"
        Dim producto = "gsInfoVentana NetCore" ' My.Application.Info.Title
        Dim desc = My.Application.Info.Description


        Dim verDLL = gsMsgWnd.cMsg2Wnd.Version()
        Dim i = verDLL.LastIndexOf("  (")
        If i > -1 Then
            verDLL = $"{verDLL.Substring(0, i)}{vbCrLf}{verDLL.Substring(i + 1)}"
        End If
        i = desc.LastIndexOf("  (")
        If i > -1 Then
            desc = $"{desc.Substring(0, i)}{vbCrLf}{desc.Substring(i + 1)}"
        End If

        Dim tips = "
Al seleccionar una valor de la lista de ventanas, si es del tipo carpetas (del explorador) se selecciona 'Ventana' de la lista desplegable y al copiar se almacena para las ventanas.
Si el tipo de ventana seleccionada es el IDE de Visual Studio (o el título de la ventana contiene Visual Studio) se selecciona 'Visual Studio' del desplegable y al copiar se almacena para Visual Studio.
Si es cualquier otro tipo de los dos anteriores, se selecciona 'General' de la lista desplegable de valores copiados y se almacena para usarlo posteriormente.
Cuando seleccionas de la lista desplegable uno de los tres elementos, al pegar se asignará los valores almacenados y si pulsas en 'Posicionar', asignará esos valores a la ventana que esté seleccionada de la lista de ventanas."

        MessageBox.Show($"{producto} {vers}" & vbCrLf & vbCrLf &
                        $"{desc}" & vbCrLf &
                        tips & vbCrLf & vbCrLf &
                        "Usando la DLL externa:" & vbCrLf &
                        verDLL,
                        $"Acerca de {producto}",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub txt_KeyPress(sender As Object,
                             e As KeyPressEventArgs) Handles txtWidth.KeyPress, txtLeft.KeyPress,
                                                             txtTop.KeyPress, txtHeight.KeyPress,
                                                             txtTitulo.KeyPress,
                                                             txtHandle.KeyPress, cboChildWnd.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub btnAsignarTamPos_Click(sender As Object,
                                       e As EventArgs) Handles btnAsignarTamPos.Click
        If txtHandle.Text = "" Then Return

        Dim hWnd = CInt(txtHandle.Text)
        Dim pL = CInt(txtLeft.Text)
        Dim pT = CInt(txtTop.Text)
        Dim pW = CInt(txtWidth.Text)
        Dim pH = CInt(txtHeight.Text)
        gsMsgWnd.cMsg2Wnd.PosicionarVentana(hWnd, pL, pT, pW, pH)

    End Sub

    Private CopiaTamPos As (Left As Integer, Top As Integer, Width As Integer, Height As Integer)

    Private Sub btnCopiarTamPos_Click(sender As Object,
                                      e As EventArgs) Handles btnCopiarTamPos.Click
        With CopiaTamPos
            .Left = CInt(txtLeft.Text)
            .Top = CInt(txtTop.Text)
            .Width = CInt(txtWidth.Text)
            .Height = CInt(txtHeight.Text)
        End With

        AsignarPaste()
    End Sub

    Private Sub btnPegarTamPos_Click(sender As Object,
                                     e As EventArgs) Handles btnPegarTamPos.Click
        With CopiaTamPos
            txtTop.Text = .Top.ToString
            txtLeft.Text = .Left.ToString
            txtWidth.Text = .Width.ToString
            txtHeight.Text = .Height.ToString
        End With

        PonerPaste()
    End Sub

    ''' <summary>
    ''' Dejar la ventana TopMost al marcar en el checkBox           (26/Abr/21)
    ''' </summary>
    Private Sub chkTopMost_CheckedChanged(sender As Object, e As EventArgs) Handles chkTopMost.CheckedChanged
        Me.TopMost = chkTopMost.Checked
    End Sub

    Private Sub lvVentanas_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvVentanas.ColumnClick
        If lvVentanas.Sorting = SortOrder.Ascending Then
            lvVentanas.Sorting = SortOrder.Descending
        Else
            lvVentanas.Sorting = SortOrder.Ascending
        End If
    End Sub

    Private Sub PonerPaste()
        If cboCopias.SelectedIndex = -1 Then Return

        Dim p = cboCopias.Text
        If p = "<General>" Then
            PonerPaste(PasteNing)
            Return
        End If

        If p.StartsWith("Visual") Then
            PonerPaste(PasteVS)
        Else
            PonerPaste(PasteVent)
        End If
    End Sub
    Private Sub PonerPaste(p As String)
        If String.IsNullOrWhiteSpace(p) OrElse p.TrimStart().StartsWith("-1") Then Return

        Dim a = p.Split(";", StringSplitOptions.RemoveEmptyEntries)
        If a.Length < 4 Then Return
        txtLeft.Text = a(0)
        txtTop.Text = a(1)
        txtWidth.Text = a(2)
        txtHeight.Text = a(3)
    End Sub
    Private Sub AsignarPaste()
        If cboCopias.SelectedIndex = -1 Then Return

        Dim p = cboCopias.Text
        If p = "<General>" Then
            AsignarPaste(PasteNing)
            Return
        End If

        If p.StartsWith("Visual") Then
            AsignarPaste(PasteVS)
        Else
            AsignarPaste(PasteVent)
        End If
    End Sub
    Private Sub AsignarPaste(ByRef p As String)
        p = $"{txtLeft.Text};{txtTop.Text};{txtWidth.Text};{txtHeight.Text}"
    End Sub
End Class
