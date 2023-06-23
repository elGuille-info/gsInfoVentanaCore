'------------------------------------------------------------------------------
' Módulo para definir funciones callback
'
' Primera versión (para VB)                                         (24/Sep/02)
' Convertida a VB.NET                                               (07/Feb/04)
' Última revisión para .NET 4.8                                     (15/Sep/20)
' Revisión para usar con .NET 5.0 (net core)                        (07/Jun/21)
'
'
' ©Guillermo Som (elGuille), 2002-2004, 2019-2021
'------------------------------------------------------------------------------
Option Strict On
Option Explicit On
Option Infer On

Imports vb = Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms

Public Module MCallback

    Public TopWnd As cVentanas
    Public ChildWnd As cVentanas

    Private visible As Boolean

    Public Structure RECTAPI
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    ' Nuevas funciones añadidas el 17/Oct/2020

    Private Structure POINTAPI
        Public x As Integer
        Public y As Integer
    End Structure

    Private Structure WINDOWPLACEMENT
        Public Length As Integer
        Public flags As Integer
        Public showCmd As Integer
        Public ptMinPosition As POINTAPI
        Public ptMaxPosition As POINTAPI
        Public rcNormalPosition As RECTAPI
    End Structure
    Private Declare Function GetWindowPlacement Lib "user32" (ByVal hWnd As IntPtr, ByRef lpwndpl As WINDOWPLACEMENT) As Integer

    ''' <summary>
    ''' Obtiene el estado de la ventana indicada por el Handle.
    ''' </summary>
    ''' <param name="hWnd"></param>
    ''' <returns></returns>
    Public Function GetWindowState(hWnd As IntPtr) As FormWindowState
        Dim intRet As Integer
        Dim wpTemp As WINDOWPLACEMENT

        wpTemp.Length = System.Runtime.InteropServices.Marshal.SizeOf(wpTemp)
        intRet = GetWindowPlacement(hWnd, wpTemp)

        Dim ws As FormWindowState
        If wpTemp.showCmd = 1 Then
            'normal
            ws = FormWindowState.Normal
        ElseIf wpTemp.showCmd = 2 Then
            'minimized
            ws = FormWindowState.Minimized
        ElseIf wpTemp.showCmd = 3 Then
            'maximized
            ws = FormWindowState.Maximized
        End If
        Return ws
    End Function

    Private Declare Function SetWindowPlacement Lib "user32" (ByVal hWnd As IntPtr, ByRef lpwndpl As WINDOWPLACEMENT) As Integer

    ''' <summary>
    ''' Asigna el estado a la ventana indicada por el Handle.
    ''' </summary>
    ''' <param name="hWnd"></param>
    ''' <param name="ws"></param>
    Public Sub SetWindowState(hWnd As IntPtr, ws As FormWindowState)
        Dim wpTemp As WINDOWPLACEMENT
        wpTemp.Length = System.Runtime.InteropServices.Marshal.SizeOf(wpTemp)
        If ws = FormWindowState.Maximized Then
            wpTemp.showCmd = 3
        ElseIf ws = FormWindowState.Minimized Then
            wpTemp.showCmd = 2
        ElseIf ws = FormWindowState.Normal Then
            wpTemp.showCmd = 1
        End If
        SetWindowPlacement(hWnd, wpTemp)
    End Sub


    ' SetWindowPos() hwndInsertAfter values
    Public Const HWND_TOP As Integer = 0
    Public Const HWND_BOTTON As Integer = 1
    Public Const HWND_TOPMOST As Integer = -1
    Public Const HWND_NOTOPMOST As Integer = -2
    ' SetWindowPos wFlags
    Public Const SWP_NOSIZE As Integer = &H1
    Public Const SWP_NOMOVE As Integer = &H2
    Public Const SWP_NOZORDER As Integer = &H4
    Public Const SWP_NOACTIVATE As Integer = &H10
    Public Const SWP_SHOWWINDOW As Integer = &H40

    'Private Const SWP_NOREDRAW = &H8
    'Private Const SWP_FRAMECHANGED = &H20        '  The frame changed: send WM_NCCALCSIZE
    'Private Const SWP_HIDEWINDOW = &H80
    'Private Const SWP_NOCOPYBITS = &H100
    'Private Const SWP_NOOWNERZORDER = &H200      '  Don't do owner Z ordering
    '' Para mantenerlo siempre visible
    Public Const SWP_FLAGS As Integer = SWP_NOMOVE Or SWP_NOSIZE Or SWP_SHOWWINDOW Or SWP_NOACTIVATE
    ''
    'Private Const SWP_DRAWFRAME = SWP_FRAMECHANGED
    'Private Const SWP_NOREPOSITION = SWP_NOOWNERZORDER

    Public Declare Function SetWindowPos Lib "user32" (ByVal hWnd As Integer,
                                                       ByVal hWndInsertAfter As Integer,
                                                       ByVal x As Integer, ByVal y As Integer,
                                                       ByVal cx As Integer, ByVal cy As Integer,
                                                       ByVal wFlags As Integer) As Integer

    ' Para posicionar la ventana...
    ' Dim ra As RECTAPI
    ' (asignar a ra la posición)
    'SetWindowPos(hWnd, 0, ra.Left, ra.Top, (ra.Right - ra.Left), (ra.Bottom - ra.Top), SWP_NOZORDER Or SWP_NOACTIVATE)


    Private Declare Function GetForegroundWindowAPI Lib "user32" Alias "GetForegroundWindow" () As Integer
    Private Declare Function SetForegroundWindowAPI Lib "user32" Alias "SetForegroundWindow" (ByVal hWnd As Integer) As Integer
    '
    Private Declare Function EnableWindowAPI Lib "user32" Alias "EnableWindow" (ByVal hWnd As Integer, ByVal fEnable As Integer) As Integer
    '
    ' FindWindowEx también busca en las ventanas hijas                  (25/Sep/02)
    ' Esta constante se puede usar sólo con Windows 2000/XP para indicar que sólo
    ' se buscará en las ventanas que reciban mensajes.
    Public Const HWND_MESSAGE As Integer = (-3)
    Private Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
    '
    Private Const GWL_USERDATA As Integer = (-21)
    Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hWnd As Integer, ByVal nIndex As Integer) As Integer
    '
    Private Declare Function BringWindowToTop Lib "user32" (ByVal hWnd As Integer) As Integer

    Public Declare Function GetWindowRect Lib "user32" (ByVal hWnd As Integer, ByRef lpRect As RECTAPI) As Integer

    Private Declare Function WindowFromPointXY Lib "user32" Alias "WindowFromPoint" (ByVal xPoint As Integer, ByVal yPoint As Integer) As Integer

    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Public Function WindowFromPoint(ByVal point As System.Drawing.Point) As Integer
    End Function


    Private Const BM_CLICK As Integer = &HF5S
    '
    ' Constantes para los mensajes a las ventanas
    Private Const WM_SETFOCUS As Integer = &H7S
    'WM_SETTEXT
    '   wParam = 0;                     // not used; must be zero
    '   lParam = (LPARAM)(LPCTSTR)lpsz; // address of window-text string
    Private Const WM_SETTEXT As Integer = &HCS
    'WM_GETTEXT
    '   wParam = (WPARAM) cchTextMax;   // number of characters to copy
    '   lParam = (LPARAM) lpszText;     // address of buffer for text
    Private Const WM_GETTEXT As Integer = &HDS
    Private Const WM_GETTEXTLENGTH As Integer = &HES
    Private Const WM_CLOSE As Integer = &H10S
    '
    'Private Declare Function SendMessage Lib "user32"  Alias "SendMessageA"(ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByRef lParam As Any) As Integer
    'Public Declare Function SendMessageStr Lib "user32" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As String) As Integer
    'Public Declare Function SendMessageLng Lib "user32" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As String) As Integer
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    '
    Private Declare Function SetWindowText Lib "user32" Alias "SetWindowTextA" (ByVal hWnd As Integer, ByVal lpString As String) As Integer
    '
    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Private Declare Function GetClassName Lib "user32" Alias "GetClassNameA" (ByVal hWnd As Integer, ByVal lpClassName As String, ByVal nMaxCount As Integer) As Integer
    ' Dejarla como pública, ya que se usa en la clase cVentana          (26/Sep/02)
    Public Declare Function IsWindowVisible Lib "user32" (ByVal hWnd As Integer) As Integer

    '------------------------------------------------------------------------------
    ' El código original para enumerar las ventanas usando AddressOf,
    ' es el que envió Nacho Cassou como colaboración a mis páginas.
    '
    ' 'Private Declare Function EnumWindows Lib "user32" _
    ' ''    (ByVal lpfn As Long, lParam As Any) As Boolean
    ' ' Si se quiere usar lParam como parámetro de retorno
    'Private Declare Function EnumWindowsRef Lib "user32" Alias "EnumWindows" _
    ''    (ByVal lpfn As Long, ByRef lParam As Long) As Boolean
    'Private Declare Function EnumWindowsVal Lib "user32" Alias "EnumWindows" _
    ''    (ByVal lpfn As Long, ByVal lParam As Long) As Boolean
    '
    'Private Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hWnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
    Private Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hWnd As Integer, ByVal lpString As System.Text.StringBuilder, ByVal cch As Integer) As Integer
    '------------------------------------------------------------------------------
    Private Delegate Function EnumWindowsDelegate(ByVal hWnd As Integer, ByVal parametro As Integer) As Boolean
    'Private Declare Function EnumWindows Lib "user32" (ByVal lpfn As Integer, ByVal lParam As Integer) As Boolean
    Private Declare Function EnumWindows Lib "user32" (ByVal lpfn As EnumWindowsDelegate, ByVal lParam As Integer) As Boolean
    'Private Declare Function EnumChildWindows Lib "user32" (ByVal hWndParent As Integer, ByVal lpEnumFunc As Integer, ByVal lParam As Integer) As Integer
    Private Declare Function EnumChildWindows Lib "user32" (ByVal hWndParent As Integer, ByVal lpEnumFunc As EnumWindowsDelegate, ByVal lParam As Integer) As Integer

    Public Function EnumWindowsProc(ByVal hWnd As Integer, ByVal parametro As Integer) As Boolean
        ' Esta función se usará con EnumWindows
        Dim titulo As New System.Text.StringBuilder(New String(" "c, 256))   'VB6.FixedLengthString(256)
        Dim ret As Integer
        Dim nombreVentana As String
        Dim bProcesar As Boolean
        '
        On Error Resume Next
        '
        System.Windows.Forms.Application.DoEvents()
        '
        ret = GetWindowText(hWnd, titulo, titulo.Length)
        nombreVentana = vb.Left(titulo.ToString, ret)
        If vb.Len(nombreVentana) > 0 Then
            ' si se muestran todas las ventanas o sólo las visibles
            If visible = False Then
                bProcesar = True
            Else
                ' Si la ventana es visible
                If IsWindowVisible(hWnd) <> 0 Then
                    bProcesar = True
                End If
            End If
            If bProcesar Then
                TopWnd.Nuevo(hWnd, nombreVentana)
            End If
        End If
        '
        'Err.Number = 0
        '
        EnumWindowsProc = True
    End Function

    Public Function EnumChildWindowsProc(ByVal hWnd As Integer, ByVal parametro As Integer) As Boolean
        ' Esta función se usará con EnumChildWindows
        Dim titulo As New System.Text.StringBuilder(New String(" "c, 256))
        Dim ret As Integer
        Dim nombreVentana As String
        Dim bProcesar As Boolean
        '
        On Error Resume Next
        '
        System.Windows.Forms.Application.DoEvents()
        '
        ret = GetWindowText(hWnd, titulo, titulo.Length)
        nombreVentana = vb.Left(titulo.ToString, ret)
        If vb.Len(nombreVentana) > 0 Then
            ' si se muestran todas las ventanas o sólo las visibles
            If visible = False Then
                bProcesar = True
            Else
                ' Si la ventana es visible
                If IsWindowVisible(hWnd) <> 0 Then
                    bProcesar = True
                End If
            End If
            If bProcesar Then
                ChildWnd.Nuevo(hWnd, nombreVentana)
            End If
        End If
        '
        'Err.Number = 0
        '
        Return True
    End Function

    Public Sub EnumerarVentanas(Optional ByVal soloLasVisibles As Boolean = True)
        ' Enumera las ventanas principales (TopWindows)
        TopWnd = New cVentanas
        visible = soloLasVisibles
        Call EnumWindows(AddressOf EnumWindowsProc, 0)
    End Sub

    Public Sub EnumerarVentanasHijas(ByVal handleParent As Integer, Optional ByVal soloLasVisibles As Boolean = True)
        ' Enumera las ventanas hijas del handle indicado
        '
        ' NOTA: El texto que se obtiene con este método es el texto
        '       que tenía el control cuando se mostró la ventana (o el que tenía al crearla)
        '
        ChildWnd = New cVentanas
        visible = soloLasVisibles
        Call EnumChildWindows(handleParent, AddressOf EnumChildWindowsProc, 0)
    End Sub

    Public Sub EnumerarVentanasHijasPos(ByVal handle As Integer, Optional ByVal traerAlFrente As Boolean = True, Optional ByVal eliminarColAnterior As Boolean = False)
        ' Enumera las ventanas hijas del handle indicado,
        ' pero sólo enumerará las ventanas que estén en el área visible de la ventana.
        ' Si se indica traerAlFrente, se traerá la ventana antes de buscar las ventanas.
        ' Si eliminarColAnterior es True, se creará una nueva colección.
        '
        ' Enumera los controles (y ventanas) de la ventana indicada.
        ' Esta forma de hacerlo es un poco "manual", ya que se trata de recorrer
        ' toda la ventana y buscar los controles (o ventanas) que estén en ella,
        ' de esta forma se podrán obtener también los listbox, etc.
        '
        ' NOTA: El texto que se obtiene con este método es el que actualmente está
        '       en el control, a diferencia de EnumerarVentanasHijas que es el texto
        '       que tenía el control cuando se mostró la ventana (o el que tenía al crearla)
        '
        Dim x, y As Integer
        Dim r As RECTAPI
        Dim h As Integer
        '
        If handle = 0 Then Exit Sub
        '
        ' Puede devolver 1 aunque esté oculta tras otras ventanas
        'If IsWindowVisible(handle) = 0 Then Exit Sub
        ' Si no se trae al frente, puede que la información no sea correcta
        If traerAlFrente Then
            BringWindowToTop(handle)
            System.Windows.Forms.Application.DoEvents()
        End If
        '
        If eliminarColAnterior Then
            ChildWnd = New cVentanas
        Else
            ' Comprobamos que la colección ya esté creada.
            If ChildWnd Is Nothing Then
                ChildWnd = New cVentanas
            End If
        End If
        '
        'getRect(handle, r)
        r = getRect(handle)
        '
        ' Recorrer la ventana buscando las distintas ventanas
        For y = r.Top + 8 To r.Bottom - 8 Step 16
            For x = r.Left + 8 To r.Right - 8 Step 16
                h = WindowFromPointXY(x, y)
                If h > 0 Then
                    ' En caso de que ya exista,                             (25/Sep/02)
                    ' sólo se cambiará el contenido de la propiedad Texto
                    ChildWnd.Item(CStr(h)).Texto = MCallback.GetText(h)
                End If
            Next
        Next
    End Sub

    Public Function ClassNameByTitle(ByVal title As String) As String
        ' Devuelve el ClassName de una ventana, indicando el título de la misma
        Dim hWnd As Integer
        Dim sClassName As String = String.Empty
        Dim nMaxCount As Integer
        '
        hWnd = FindWindow(sClassName, title)
        '
        nMaxCount = 256
        sClassName = New String(" "c, nMaxCount) ' Space(nMaxCount)
        nMaxCount = GetClassName(hWnd, sClassName, nMaxCount)
        Return vb.Left(sClassName, nMaxCount)
    End Function

    Public Function ClassNameByHandle(ByVal handle As Integer) As String
        ' Devuelve el ClassName de una ventana, según el handle indicado
        Dim sClassName As String
        Dim nMaxCount As Integer
        '
        If handle = 0 Then Return ""
        '
        nMaxCount = 256
        sClassName = New String(" "c, nMaxCount) ' Space(nMaxCount)
        nMaxCount = GetClassName(handle, sClassName, nMaxCount)
        Return vb.Left(sClassName, nMaxCount)
    End Function

    Public Sub SendBtnClick(ByVal handle As Integer)
        ' Enviar un click a la ventana indicada
        If handle = 0 Then Exit Sub
        '
        Call SendMessage(handle, BM_CLICK, 0, 0)
    End Sub

    Public Sub SendText(ByVal handle As Integer, ByVal Texto As String)
        ' Enviar el texto a la ventana indicada
        If handle = 0 Then Exit Sub
        '
        Texto = Texto & vb.ChrW(0)
        Call SendMessage(handle, WM_SETTEXT, 0, Texto)
    End Sub

    Public Sub SendTextAppend(ByVal handle As Integer, ByVal Texto As String)
        ' Añadir el texto indicado al que tenga la ventana de destino   (25/Sep/02)
        If handle = 0 Then Exit Sub
        '
        Dim s As String
        s = GetText(handle)
        Texto = s & Texto & vb.ChrW(0)
        Call SendMessage(handle, WM_SETTEXT, 0, Texto)
    End Sub

    Public Function GetText(ByVal handle As Integer) As String
        ' Capturar el texto de la ventana indicada
        Dim i As Integer
        Dim s As String
        '
        If handle = 0 Then Return ""
        '
        i = GetTextLength(handle) + 1
        If i > 60000 Then i = 60000
        s = New String(" "c, i) ' Space(i)
        Call SendMessage(handle, WM_GETTEXT, i, s)
        '
        Return strZToStr(s)
    End Function

    Public Function GetTextLength(ByVal handle As Integer) As Integer
        ' Averigua la longitud del texto de la ventana indicada
        If handle = 0 Then Exit Function
        '
        Return SendMessage(handle, WM_GETTEXTLENGTH, 0, 0)
    End Function

    Public Function GetLeft(ByVal handle As Integer) As Integer
        ' Obtiene la posición izquierda de la ventana indicada
        Dim r As RECTAPI
        '
        If handle = 0 Then Exit Function
        '
        GetWindowRect(handle, r)
        Return r.Left
    End Function

    Public Function GetTop(ByVal handle As Integer) As Integer
        ' Obtiene la posición arriba de la ventana indicada
        Dim r As RECTAPI
        '
        If handle = 0 Then Exit Function
        '
        GetWindowRect(handle, r)
        Return r.Top
    End Function

    Public Function GetBotton(ByVal handle As Integer) As Integer
        ' Obtiene la posición inferior de la ventana indicada
        Dim r As RECTAPI
        '
        If handle = 0 Then Exit Function
        '
        GetWindowRect(handle, r)
        Return r.Bottom
    End Function

    Public Function GetRight(ByVal handle As Integer) As Integer
        ' Obtiene la posición derecha de la ventana indicada
        Dim r As RECTAPI
        '
        If handle = 0 Then Exit Function
        '
        GetWindowRect(handle, r)
        Return r.Right
    End Function

    Private Function getRect(ByVal handle As Integer) As RECTAPI
        ' Obtiene la posición de la ventana indicada, la cual estará en la estructura r
        If handle = 0 Then Exit Function
        '
        Dim r As RECTAPI
        GetWindowRect(handle, r)
        Return r
    End Function

    Private Function strZToStr(ByVal s As String) As String
        ' Quita los nulos del final de la cadena
        Dim i As Integer
        '
        i = vb.InStrRev(s, vb.ChrW(0))
        If i > 0 Then
            s = vb.Left(s, i - 1)
        End If
        Return s
        '
        'StrZToStr = Left$(s, Len(s) - 1)
    End Function

    Public Function GetUserData(ByVal handle As Integer) As Integer
        ' Devuelve el valor de USERDATA de la ventana indicada
        Return GetWindowLong(handle, GWL_USERDATA)
    End Function

    Public Sub BringToTop(ByVal handle As Integer)
        ' Trae al frente la ventana indicada
        BringWindowToTop(handle)
    End Sub

    Public Function FindTopWindowTitle(ByVal titulo As String) As Integer
        ' Buscará la ventana indicada en el título                      (25/Sep/02)
        ' y devolverá el handle de la misma o un cero si no se ha hayado.
        Dim lpClassName As String = ""
        Return FindWindow(lpClassName, titulo)
        'Return FindWindow("", titulo)
    End Function

    Public Function FindChildWindowTitle(ByVal parentHandle As Integer, ByVal titulo As String) As Integer
        ' Buscará en las ventanas hijas del handle indicado,            (25/Sep/02)
        ' la ventana indicada en el título
        ' y devolverá el handle de la misma o un cero si no se ha hayado.
        Return FindWindowEx(parentHandle, 0, "", titulo)
    End Function

    Public Sub EnableWindow(ByVal handle As Integer)
        ' Habilita la ventana indicada
        EnableWindowAPI(handle, 1)
    End Sub

    Public Sub DisableWindow(ByVal handle As Integer)
        ' Deshabilita la ventana indicada
        EnableWindowAPI(handle, 0)
    End Sub

    Public Sub SetForegroundWindow(ByVal handle As Integer)
        SetForegroundWindowAPI(handle)
    End Sub

    Public Function GetForegroundWindow() As Integer
        Return GetForegroundWindowAPI
    End Function

    ''' <summary>
    ''' Comprueba si el Handle indicado es una ventana principal.
    ''' </summary>
    ''' <param name="hWnd">El Handle de la ventana a comprobar</param>
    ''' <returns>
    ''' Devuelve verdadero si está en la colección TopWnd 
    ''' o falso si no está o no está inicializada la colección
    ''' </returns>
    ''' <remarks>15/Sep/20</remarks>
    Public Function EsTopWindow(hWnd As Integer) As Boolean
        Return If(TopWnd IsNot Nothing AndAlso TopWnd.Exists(hWnd), True, False)
    End Function
End Module