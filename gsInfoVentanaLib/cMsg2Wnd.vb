'------------------------------------------------------------------------------
' cMsg2Wnd
' Clase para enviar/capturar mensajes de ventanas de Windows
' Los métodos de esta clase, son llamadas a las implementadas en MCallback
'
' Primera versión (VB)                                              (25/Sep/02)
' Convertida a VB.NET                                               (07/Feb/04)
' Revisiones (anteriores a .NET 5.0):
'   15/Sep/2020 - 18/Mar/2019 - 21/Abr/2006 - 08/Feb/2004
' v4.0.0.3  15/Sep/20   Añado el método Versión
'
' Revisión para usar con .NET 5.0 (net core)                        (07/Jun/21)
'
'
' ©Guillermo Som (elGuille), 2002-2004, 2006, 2019-2021
'------------------------------------------------------------------------------
Option Strict On
'Option Explicit On 
Option Infer On

Imports Microsoft.VisualBasic
Imports System
'Imports System.Runtime.InteropServices

'<System.Runtime.InteropServices.ProgId("cMsg2WndAXNET.cMsg2Wnd")> _
Public Class cMsg2Wnd

    ''' <summary>
    ''' Enumera las ventanas pricipales (TopWindows)
    ''' </summary>
    ''' <param name="soloLasVisibles">True para solo las visibles</param>
    Public Sub EnumerarVentanas(Optional soloLasVisibles As Boolean = True)
        ' Enumera las ventanas principales (TopWindows)
        MCallback.EnumerarVentanas(soloLasVisibles)
    End Sub

    ''' <summary>
    ''' Enumera las ventanas hijas del Handle indicado.
    ''' </summary>
    ''' <param name="handleParent">El Handle de la ventana contenedora</param>
    ''' <param name="soloLasVisibles">True para solo las visibles</param>
    Public Sub EnumerarVentanasHijas(handleParent As Integer, Optional soloLasVisibles As Boolean = True)
        ' Enumera las ventanas hijas del handle indicado
        MCallback.EnumerarVentanasHijas(handleParent, soloLasVisibles)
    End Sub

    Public Sub EnumerarVentanasHijasPos(handle As Integer, Optional traerAlFrente As Boolean = True, Optional eliminarColAnterior As Boolean = False)
        ' Enumera las ventanas hijas del handle indicado,
        ' pero sólo enumerará las ventanas que estén en el área visible de la ventana.
        ' Si se indica traerAlFrente, se traerá la ventana antes de buscar las ventanas.
        MCallback.EnumerarVentanasHijasPos(handle, traerAlFrente, eliminarColAnterior)
    End Sub

    Public Function ClassNameByTitle(title As String) As String
        ' Devuelve el ClassName de una ventana, indicando el título de la misma
        ClassNameByTitle = MCallback.ClassNameByTitle(title)
    End Function

    Public Function ClassNameByHandle(handle As Integer) As String
        ' Devuelve el ClassName de una ventana, según el handle indicado
        ClassNameByHandle = MCallback.ClassNameByHandle(handle)
    End Function

    Public Sub SendBtnClick(handle As Integer)
        ' Enviar un click a la ventana indicada
        MCallback.SendBtnClick(handle)
    End Sub

    Public Sub SendText(handle As Integer, Texto As String)
        ' Enviar el texto a la ventana indicada
        MCallback.SendText(handle, Texto)
    End Sub

    Public Sub SendTextAppend(handle As Integer, Texto As String)
        ' Añadir el texto indicado al que tenga la ventana de destino   (25/Sep/02)
        MCallback.SendTextAppend(handle, Texto)
    End Sub

    Public Function GetText(handle As Integer) As String
        ' Capturar el texto de la ventana indicada
        GetText = MCallback.GetText(handle)
    End Function

    Public Function GetTextLength(handle As Integer) As Integer
        ' Averigua la longitud del texto de la ventana indicada
        GetTextLength = MCallback.GetTextLength(handle)
    End Function

    Public Function GetLeft(handle As Integer) As Integer
        ' Obtiene la posición izquierda de la ventana indicada
        GetLeft = MCallback.GetLeft(handle)
    End Function

    Public Function GetTop(handle As Integer) As Integer
        ' Obtiene la posición arriba de la ventana indicada
        GetTop = MCallback.GetTop(handle)
    End Function

    Public Function GetBotton(handle As Integer) As Integer
        ' Obtiene la posición inferior de la ventana indicada
        GetBotton = MCallback.GetBotton(handle)
    End Function

    Public Function GetRight(handle As Integer) As Integer
        ' Obtiene la posición derecha de la ventana indicada
        GetRight = MCallback.GetRight(handle)
    End Function

    Public Function TopWindows() As cVentanas
        ' Devuelve la colección de ventana superiores obtenidas por EnumerarVentanas
        TopWindows = MCallback.TopWnd
    End Function

    Public Function ChildWindows() As cVentanas
        ' Devuelve la colección de ventana hijas obtenidas por EnumerarVentanasHijasXXX
        ChildWindows = MCallback.ChildWnd
    End Function

    Public Sub New()
        MyBase.New()
        ' Crear una nueva instancia de las clases para almacenar las ventanas
        MCallback.TopWnd = New cVentanas
        MCallback.ChildWnd = New cVentanas
    End Sub

    Protected Overrides Sub Finalize()
        MCallback.TopWnd.Clear()
        MCallback.ChildWnd.Clear()

        MyBase.Finalize()
    End Sub

    Public Sub BringToTop(handle As Integer)
        ' Trae al frente la ventana indicada
        MCallback.BringToTop(handle)
    End Sub

    Public Function GetUserData(handle As Integer) As Integer
        ' Devuelve el valor de USERDATA de la ventana indicada
        GetUserData = MCallback.GetUserData(handle)
    End Function

    Public Function FindTopWindowTitle(titulo As String) As Integer
        ' Buscará la ventana indicada en el título                      (25/Sep/02)
        ' y devolverá el handle de la misma o un cero si no se ha hayado.
        FindTopWindowTitle = MCallback.FindTopWindowTitle(titulo)
    End Function

    Public Function FindChildWindowTitle(parentHandle As Integer, titulo As String) As Integer
        ' Buscará en las ventanas hijas del handle indicado,            (25/Sep/02)
        ' la ventana indicada en el título
        ' y devolverá el handle de la misma o un cero si no se ha hayado.
        FindChildWindowTitle = MCallback.FindChildWindowTitle(parentHandle, titulo)
    End Function

    Public Sub EnableWindow(handle As Integer)
        ' Habilita la ventana indicada
        MCallback.EnableWindow(handle)
    End Sub

    Public Sub DisableWindow(handle As Integer)
        ' Deshabilita la ventana indicada
        MCallback.DisableWindow(handle)
    End Sub

    Public Sub SetForegroundWindow(handle As Integer)
        MCallback.SetForegroundWindow(handle)
    End Sub

    Public Function GetForegroundWindow() As Integer
        GetForegroundWindow = MCallback.GetForegroundWindow
    End Function

    ''' <summary>
    ''' Devuelve la versión de esta DLL
    ''' </summary>
    ''' <remarks>15/Sep/2020</remarks>
    Public Shared Function Version() As String
        Dim t = GetType(cMsg2Wnd)
        Dim ensamblado = t.Assembly
        Dim fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(ensamblado.Location)

        ' Esto no da el valor correcto
        Dim versionAttr = ensamblado.GetCustomAttributes(GetType(System.Reflection.AssemblyVersionAttribute), False)
        Dim vers = If(versionAttr.Length > 0,
                                (TryCast(versionAttr(0), System.Reflection.AssemblyVersionAttribute)).Version,
                                "5.0.0.0")

        Dim fileVerAttr = ensamblado.GetCustomAttributes(GetType(System.Reflection.AssemblyFileVersionAttribute), False)
        Dim versF = If(fileVerAttr.Length > 0,
                                (TryCast(fileVerAttr(0), System.Reflection.AssemblyFileVersionAttribute)).Version,
                                "5.0.0.0")
        versF = fvi.FileVersion
        Dim prodAttr = ensamblado.GetCustomAttributes(GetType(System.Reflection.AssemblyProductAttribute), False)
        Dim producto = If(prodAttr.Length > 0,
                                (TryCast(prodAttr(0), System.Reflection.AssemblyProductAttribute)).Product,
                                "gsInfoVentanaLib")
        ' Producto
        'producto = fvi.ProductName
        ' Título
        producto = fvi.FileDescription
        Dim descAttr = ensamblado.GetCustomAttributes(GetType(System.Reflection.AssemblyDescriptionAttribute), False)
        Dim desc = If(descAttr.Length > 0,
                                (TryCast(descAttr(0), System.Reflection.AssemblyDescriptionAttribute)).Description,
                                "(última revisión del 07/Jun/2021)")
        Return $"{producto} v{vers} ({versF}){vbCrLf}{desc}"

    End Function

    ''' <summary>
    ''' Posiciona y asigna el tamaño a la ventana indicada por el Handle.
    ''' Llama a SetWindowPos del API con HWND_TOPMOST y SWP_NOZORDER Or SWP_NOACTIVATE
    ''' </summary>
    ''' <param name="hWnd">El Handle de la ventana a posicionar</param>
    ''' <param name="left">La posición izquierda</param>
    ''' <param name="top">La posición arriba</param>
    ''' <param name="width">El ancho de la ventana</param>
    ''' <param name="height">El alto de la ventana</param>
    ''' <remarks>15/Sep/2020</remarks>
    Public Shared Function PosicionarVentana(hWnd As Integer,
                                             left As Integer, top As Integer,
                                             width As Integer, height As Integer) As Integer
        Return MCallback.SetWindowPos(hWnd, HWND_TOP, left, top, width, height, SWP_SHOWWINDOW)
    End Function

    ''' <summary>
    ''' Obtiene la posición de la ventana indicada por el Handle.
    ''' </summary>
    ''' <param name="hWnd">El Handle de la ventana a posicionar</param>
    ''' <returns>Una tupla con los valores de Left, Top, Width y Height</returns>
    ''' <remarks>15/Sep/2020</remarks>
    Public Shared Function PosicionVentana(hWnd As Integer) As (Left As Integer, Top As Integer,
                                                                Width As Integer, Height As Integer)

        Dim ra As MCallback.RECTAPI
        MCallback.GetWindowRect(hWnd, ra)

        Dim ret As (Left As Integer, Top As Integer, Width As Integer, Height As Integer)

        ret.Left = ra.Left
        ret.Top = ra.Top
        ret.Width = CInt(Math.Abs(ra.Left - ra.Right))
        ret.Height = CInt(Math.Abs(ra.Bottom - ra.Top))

        Return ret
    End Function

End Class