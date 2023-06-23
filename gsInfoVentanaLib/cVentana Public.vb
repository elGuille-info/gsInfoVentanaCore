'------------------------------------------------------------------------------
' cVentana
' Clase para manejar la información de las ventanas
'
' Primera versión (VB)                                              (24/Sep/02)
' Convertida a VB.NET                                               (07/Feb/04)
' Revisión para usar con .NET 5.0 (net core)                        (07/Jun/21)
'
' ©Guillermo Som (elGuille), 2000-2004, 2021
'------------------------------------------------------------------------------
Option Strict On
'Option Explicit On
Option Compare Text

Imports Microsoft.VisualBasic
Imports System

'<System.Runtime.InteropServices.ProgId("cMsg2WndAXNET.cVentana")> _
Public Class cVentana
    'Implements IFormattable
    '
    Public Enum eTipoVentana
        eUnknown
        eForm '  1
        eButton '  2
        eCheckBox '  3
        eRadioButton '  4 un OptionBox/RadioButton
        eTextBox '  5
        eEdit '  6 Un Edit es por ejemplo la caja de textos de un comboBox
        eMemo '  7 En Borland Delphi/C++ es un TextBox Multiline
        eFrame '  8 Un frame
        ePicture '  9
        eComboBox ' 10
        eListBox ' 11
        eMDIForm ' 12
        eMDIClient ' 13
        eVbaWindow ' 14
        eStatic ' 15 Algunos controles STATIC son realmente Labels
        eTreeView ' 16
        eListView ' 17
        eCommandBar ' 18
        eHeader ' 19
        eToolBar ' 20
        eScrollBar ' 21
        eHScroll ' 22
        eVScroll ' 23
        eSlider ' 24
        eStatusBar ' 25
        eMonthCalendar ' 26
    End Enum
    '
    Private sTipo As String ' El tipo de control en formato cadena
    Private mTexto As String ' El texto de la ventana,
    ' este texto será el que se consiga al enumerar
    ' usando EnumerarVentanasHijasPos.
    Private mNombre As String ' El texto de la ventana (que no el nombre)
    Private mHandle As Integer ' El handle de la ventana
    Private mClassName As String ' El ClassName de la ventana

    Public Property ID() As String
        Get
            ' Se tiene una propiedad ID por compatibilidad,
            ' aunque hará referencia al handle de la ventana
            Return CStr(Me.Handle)
        End Get
        Set(ByVal value As String)
            ' Si se asigna un nuevo valor al ID se cambiará el valor del handle
            Try
                Me.Handle = CInt(value)
            Catch 'ex As Exception
            End Try
        End Set
    End Property

    Public Property Nombre() As String
        Get
            ' Si no se ha asignado nada y el texto tiene algo...
            ' usar lo que haya en Texto
            If mNombre = "" Then
                If mTexto <> "" Then
                    mNombre = mTexto
                End If
            End If
            Return mNombre
        End Get
        Set(ByVal value As String)
            mNombre = value
        End Set
    End Property

    ' Se usará esta propiedad cuando se enumeren las ventanas con EnumerarVentanasHijasPos
    Public Property Texto() As String
        Get
            ' Si texto no tiene un valor asignado, usar el que haya en Nombre
            If mTexto = "" Then mTexto = mNombre
            Return mTexto
        End Get
        Set(ByVal value As String)
            mTexto = value
        End Set
    End Property

    Public Property Handle() As Integer
        Get
            Return mHandle
        End Get
        Set(ByVal value As Integer)
            mHandle = value
        End Set
    End Property

    Public Property ClassName() As String
        Get
            If mClassName = "" Then
                mClassName = MCallback.ClassNameByHandle(Me.Handle)
            End If
            Return mClassName
        End Get
        Set(ByVal value As String)
            mClassName = value
        End Set
    End Property

    Public ReadOnly Property Tipo() As eTipoVentana
        Get
            Return tipoByClassName()
        End Get
    End Property

    Public ReadOnly Property TipoToString() As String
        Get
            tipoByClassName()
            Return sTipo
        End Get
    End Property

    Public ReadOnly Property Visible() As Boolean
        Get
            ' Si la ventana es visible                                      (26/Sep/02)
            ' Una ventana es visible aunque esté oculta por otra ventana.
            Return MCallback.IsWindowVisible(Me.Handle) <> 0
        End Get
    End Property

    Public Function Clone() As cVentana
        ' Devuelve una copia de este objeto
        Dim tVentana As cVentana
        tVentana = New cVentana
        '
        With tVentana
            .Nombre = Me.Nombre
            .Handle = Me.Handle
            .ClassName = Me.ClassName
            .Texto = Me.Texto
        End With
        '
        Return tVentana
    End Function

    Private Function tipoByClassName() As eTipoVentana
        Dim t As eTipoVentana
        Dim i As Integer
        Dim s As String
        '
        s = Me.ClassName
        '
        ' Quitar los nombres que asigna .NET (WindowsForms)             (25/Sep/02)
        i = InStr(s, "WindowsForms")
        If i > 0 Then
            s = Mid(s, i + Len("WindowsForms"))
        End If
        ' En .NET las ventanas principales se llaman xxx.Window.8.appN
        '
        '    If InStr(s, "button") Then
        '        t = eButton
        If InStr(s, "check") > 0 Then
            t = eTipoVentana.eCheckBox
            sTipo = "CheckBox"
        ElseIf InStr(s, "combo") > 0 Then
            t = eTipoVentana.eComboBox
            sTipo = "ComboBox"
        ElseIf InStr(s, "commandbar") > 0 Then
            t = eTipoVentana.eCommandBar
            sTipo = "CommandBar"
        ElseIf InStr(s, "edit") > 0 Then
            t = eTipoVentana.eEdit
            sTipo = "Edit"
            ' En .NET las ventanas principales (formularios) se llaman xxx.Window.8.appN
        ElseIf InStr(s, ".window.") > 0 Then
            t = eTipoVentana.eForm
            sTipo = "Form"
        ElseIf InStr(s, "form") > 0 Then
            t = eTipoVentana.eForm
            sTipo = "Form"
        ElseIf InStr(s, "frame") > 0 Then
            t = eTipoVentana.eFrame
            sTipo = "Frame"
        ElseIf InStr(s, "header") > 0 Then
            t = eTipoVentana.eHeader
            sTipo = "Header"
        ElseIf InStr(s, "hscroll") > 0 Then
            t = eTipoVentana.eHScroll
            sTipo = "HScroll"
        ElseIf InStr(s, "listbox") > 0 Then
            t = eTipoVentana.eListBox
            sTipo = "ListBox"
        ElseIf InStr(s, "listview") > 0 Then
            t = eTipoVentana.eListView
            sTipo = "ListView"
        ElseIf InStr(s, "mdiclient") > 0 Then
            t = eTipoVentana.eMDIClient
            sTipo = "MDIClient"
        ElseIf InStr(s, "mdiform") > 0 Then
            t = eTipoVentana.eMDIForm
            sTipo = "MDIForm"
        ElseIf InStr(s, "memo") > 0 Then
            t = eTipoVentana.eMemo
            sTipo = "Memo"
        ElseIf InStr(s, "monthcal") > 0 Then
            t = eTipoVentana.eMonthCalendar
            sTipo = "MonthCalendar"
        ElseIf InStr(s, "option") > 0 Then
            t = eTipoVentana.eRadioButton
            sTipo = "OptionButton"
        ElseIf InStr(s, "picture") > 0 Then
            t = eTipoVentana.ePicture
            sTipo = "Picture"
        ElseIf InStr(s, "radio") > 0 Then
            t = eTipoVentana.eRadioButton
            sTipo = "RadioButton"
        ElseIf InStr(s, "scrollbar") > 0 Then
            t = eTipoVentana.eScrollBar
            sTipo = "ScrollBar"
        ElseIf InStr(s, "slider") > 0 Then
            t = eTipoVentana.eSlider
            sTipo = "Slider"
        ElseIf InStr(s, "static") > 0 Then
            t = eTipoVentana.eStatic
            sTipo = "Static"
        ElseIf InStr(s, "statusbar") > 0 Then
            t = eTipoVentana.eStatusBar
            sTipo = "StatusBar"
        ElseIf InStr(s, "textbox") > 0 Then
            t = eTipoVentana.eTextBox
            sTipo = "TextBox"
        ElseIf InStr(s, "toolbar") > 0 Then
            t = eTipoVentana.eToolBar
            sTipo = "ToolBar"
        ElseIf InStr(s, "treeview") > 0 Then
            t = eTipoVentana.eTreeView
            sTipo = "TreeView"
        ElseIf InStr(s, "vbawindow") > 0 Then
            t = eTipoVentana.eVbaWindow
            sTipo = "VbaWindow"
        ElseIf InStr(s, "vscroll") > 0 Then
            t = eTipoVentana.eVScroll
            sTipo = "VScroll"
        ElseIf InStr(s, "bitbtn") > 0 Then ' Los botones gráficos de Delphi
            t = eTipoVentana.eButton
            sTipo = "BitBtn"
            ' Este estará al final para que se comprueben antes los radiobuttons, etc.
        ElseIf InStr(s, "button") > 0 Then
            t = eTipoVentana.eButton
            sTipo = "Button"
        Else
            t = eTipoVentana.eUnknown
            sTipo = "Desconocido"
        End If
        Return t
    End Function

    '------------------------------------------------------------------------------
    ' Métodos de la clase
    '------------------------------------------------------------------------------
    ''UPGRADE_NOTE: Equals se actualizó a Equals_Renamed. Haga clic aquí para obtener más información: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    'Public Function Equals_Renamed(ByVal compareTo As cVentana) As Boolean
    '    ' Comprobar si el elemento indicado es igual que el actual
    '    ' no se comprueba el ID
    '    Dim bIgual As Boolean
    '    '
    '    With compareTo
    '        If Me.handle = .handle Then
    '            If Me.Nombre = .Nombre Then
    '                If Me.Tipo = .Tipo Then
    '                    If Me.Texto = .Texto Then
    '                        bIgual = True
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End With
    '    Equals_Renamed = bIgual
    'End Function

    Public Function UserData() As Integer
        ' Devuelve el valor de USERDATA de la ventana
        Return MCallback.GetUserData(Me.Handle)
    End Function

    Public Sub BringToTop()
        ' Trae al frente esta ventana
        MCallback.BringToTop(Me.Handle)
    End Sub

    Public Sub EnableWindow()
        ' Habilita la ventana
        MCallback.EnableWindow(Me.Handle)
    End Sub

    Public Sub DisableWindow()
        ' Deshabilita la ventana
        MCallback.DisableWindow(Me.Handle)
    End Sub
    '
    Public Overloads Overrides Function ToString() As String
        If Me.Tipo = eTipoVentana.eUnknown Then
            Return Me.Nombre & " (" & Me.Handle.ToString & ")"
        Else
            Return Me.Nombre & " (" & Me.Handle.ToString & ") Tipo: " & Me.TipoToString
        End If
    End Function
    'Public Overloads Function ToString(ByVal format As String, ByVal formatProvider As System.IFormatProvider) As String Implements System.IFormattable.ToString
    '    If format = "NH" Then
    '        Return Me.Nombre & " (" & Me.Handle.ToString & ")"
    '    ElseIf format = "NT" Then
    '        Return Me.Nombre & " - " & Me.TipoToString
    '    ElseIf format = "N" Then
    '        Return Me.Nombre
    '    Else
    '        Return Me.Nombre & " (" & Me.Handle.ToString & ") Tipo: " & Me.TipoToString
    '    End If
    'End Function
End Class