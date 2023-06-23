'------------------------------------------------------------------------------
' cVentanas
' Clase para manejar la informaci�n de las ventanas
' Colecci�n de objetos del tipo cVentana
'
' Primera versi�n (VB)                                              (24/Sep/02)
' Convertida a VB.NET                                               (07/Feb/04)
' Revisi�n para usar con .NET 5.0 (net core)                        (07/Jun/21)
'
' �Guillermo Som (elGuille), 1997-2004, 2020-2021
'------------------------------------------------------------------------------
Option Strict On
Option Infer On
'Option Explicit On 

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections

'<System.Runtime.InteropServices.ProgId("cMsg2WndAXNET.cVentanas")> _
Public Class cVentanas
    Implements System.Collections.IEnumerable

    Private m_col As Hashtable

    Public Sub Clear()
        ' Borrar el contenido de la colecci�n
        m_col.Clear()
        'm_col = New Hashtable
    End Sub

    Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return m_col.Values.GetEnumerator
    End Function

    Public Sub Remove(ByVal index As String)
        ' M�todo Remove de una colecci�n
        '
        Try
            m_col.Remove(index)
        Catch 'ex As Exception
        End Try
    End Sub

    Default Public ReadOnly Property Item(ByVal index As String) As cVentana
        Get
            ' M�todo Item de una colecci�n.
            ' Asignarlo como m�todo predeterminado
            Dim tVentana As cVentana = Nothing
            '
            ' Iniciamos la detecci�n de errores
            Try
                tVentana = CType(m_col.Item(index), cVentana)
                If tVentana Is Nothing Then
                    ' no existe ese elemento
                    ' Creamos una nueva referencia
                    tVentana = New cVentana
                    tVentana.ID = index
                    ' lo a�adimos a la colecci�n
                    m_col.Add(index, tVentana)
                    'Throw New Exception("El objeto no existe en la colecci�n")
                End If
            Catch ex As Exception
                '' no existe ese elemento
                '' Creamos una nueva referencia
                'tVentana = New cVentana
                'tVentana.ID = index
                '' lo a�adimos a la colecci�n
                'm_col.Add(index, tVentana)
                System.Diagnostics.Debug.WriteLine(ex.Message)
            End Try
            Return tVentana
        End Get
    End Property

    Public Function Count() As Integer
        ' M�todo Count de las colecci�n
        Count = m_col.Count()
    End Function

    Public Sub Add(ByVal tVentana As cVentana)
        ' A�adir un nuevo elemento a la colecci�n
        ' A�adirlo a la colecci�n
        Try
            m_col.Add(tVentana.ID, tVentana)
        Catch 'ex As Exception
        End Try
    End Sub

    Public Sub New()
        MyBase.New()
        m_col = New Hashtable
    End Sub

    Protected Overrides Sub Finalize()
        m_col.Clear()
        MyBase.Finalize()
    End Sub

    Public Function Clone() As cVentanas
        ' Hacer una copia de este objeto                                (06/Oct/00)
        '
        ' Esta copia no se puede a�adir a una colecci�n que previamente contenga este objeto
        Dim tVentanas As cVentanas
        Dim tVentana As cVentana
        '
        tVentanas = New cVentanas
        '
        ' A�adir a la nueva colecci�n los elementos de la contenida en este objeto
        For Each tVentana In m_col
            tVentanas.Add(tVentana.Clone())
        Next tVentana
        '
        Return tVentanas
    End Function

    ''' <summary>
    ''' Comprueba si el ID indicado existe en la colecci�n.
    ''' El ID es realmente el Handle de la ventana.
    ''' </summary>
    ''' <param name="index">El ID dentro de la colecci�n (es el Handle de la ventana)</param>
    ''' <returns>Verdadero o Falso seg�n est� o no en la colecci�n</returns>
    Public Function Exists(ByVal index As String) As Boolean
        ' Comprueba si el ID indicado existe en la colecci�n            (11/Oct/00)
        ' El ID es realmente el Handle de la ventana.                   (15/Sep/20)
        ' Devuelve verdadero o falso, seg�n sea el caso
        Return m_col.Contains(index)
    End Function

    ''' <summary>
    ''' Comprueba si existe la ventana con el Handle indicado.
    ''' </summary>
    ''' <param name="hWnd">El Handle de la ventana a comprobar</param>
    ''' <returns>Verdadero o Falso seg�n est� o no en la colecci�n</returns>
    ''' <remarks>15/Sep/20</remarks>
    Public Function Exists(hWnd As Integer) As Boolean
        Return Exists(hWnd.ToString)
    End Function

    Public Function Nuevo(ByVal elHandle As Integer, Optional ByVal elNombre As String = "") As cVentana
        Dim elID As String
        Dim tVentana As cVentana
        '
        ' El ID siempre ser� el handle de la ventana                    (25/Sep/02)
        elID = CStr(elHandle)
        If Me.Exists(elID) Then
            Return Me.Item(elID)
        Else
            tVentana = New cVentana
            With tVentana
                ' El ID es de s�lo lectura, ya que el ID es el handle
                '.ID = elID
                .Handle = elHandle
                If elNombre = "" Then
                    elNombre = MCallback.GetText(elHandle)
                End If
                .Nombre = elNombre
            End With
            Me.Add(tVentana)
            Return tVentana
        End If
    End Function
End Class