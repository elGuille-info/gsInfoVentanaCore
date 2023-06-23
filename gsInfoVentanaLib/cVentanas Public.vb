'------------------------------------------------------------------------------
' cVentanas
' Clase para manejar la información de las ventanas
' Colección de objetos del tipo cVentana
'
' Primera versión (VB)                                              (24/Sep/02)
' Convertida a VB.NET                                               (07/Feb/04)
' Revisión para usar con .NET 5.0 (net core)                        (07/Jun/21)
'
' ©Guillermo Som (elGuille), 1997-2004, 2020-2021
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
        ' Borrar el contenido de la colección
        m_col.Clear()
        'm_col = New Hashtable
    End Sub

    Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return m_col.Values.GetEnumerator
    End Function

    Public Sub Remove(ByVal index As String)
        ' Método Remove de una colección
        '
        Try
            m_col.Remove(index)
        Catch 'ex As Exception
        End Try
    End Sub

    Default Public ReadOnly Property Item(ByVal index As String) As cVentana
        Get
            ' Método Item de una colección.
            ' Asignarlo como método predeterminado
            Dim tVentana As cVentana = Nothing
            '
            ' Iniciamos la detección de errores
            Try
                tVentana = CType(m_col.Item(index), cVentana)
                If tVentana Is Nothing Then
                    ' no existe ese elemento
                    ' Creamos una nueva referencia
                    tVentana = New cVentana
                    tVentana.ID = index
                    ' lo añadimos a la colección
                    m_col.Add(index, tVentana)
                    'Throw New Exception("El objeto no existe en la colección")
                End If
            Catch ex As Exception
                '' no existe ese elemento
                '' Creamos una nueva referencia
                'tVentana = New cVentana
                'tVentana.ID = index
                '' lo añadimos a la colección
                'm_col.Add(index, tVentana)
                System.Diagnostics.Debug.WriteLine(ex.Message)
            End Try
            Return tVentana
        End Get
    End Property

    Public Function Count() As Integer
        ' Método Count de las colección
        Count = m_col.Count()
    End Function

    Public Sub Add(ByVal tVentana As cVentana)
        ' Añadir un nuevo elemento a la colección
        ' Añadirlo a la colección
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
        ' Esta copia no se puede añadir a una colección que previamente contenga este objeto
        Dim tVentanas As cVentanas
        Dim tVentana As cVentana
        '
        tVentanas = New cVentanas
        '
        ' Añadir a la nueva colección los elementos de la contenida en este objeto
        For Each tVentana In m_col
            tVentanas.Add(tVentana.Clone())
        Next tVentana
        '
        Return tVentanas
    End Function

    ''' <summary>
    ''' Comprueba si el ID indicado existe en la colección.
    ''' El ID es realmente el Handle de la ventana.
    ''' </summary>
    ''' <param name="index">El ID dentro de la colección (es el Handle de la ventana)</param>
    ''' <returns>Verdadero o Falso según esté o no en la colección</returns>
    Public Function Exists(ByVal index As String) As Boolean
        ' Comprueba si el ID indicado existe en la colección            (11/Oct/00)
        ' El ID es realmente el Handle de la ventana.                   (15/Sep/20)
        ' Devuelve verdadero o falso, según sea el caso
        Return m_col.Contains(index)
    End Function

    ''' <summary>
    ''' Comprueba si existe la ventana con el Handle indicado.
    ''' </summary>
    ''' <param name="hWnd">El Handle de la ventana a comprobar</param>
    ''' <returns>Verdadero o Falso según esté o no en la colección</returns>
    ''' <remarks>15/Sep/20</remarks>
    Public Function Exists(hWnd As Integer) As Boolean
        Return Exists(hWnd.ToString)
    End Function

    Public Function Nuevo(ByVal elHandle As Integer, Optional ByVal elNombre As String = "") As cVentana
        Dim elID As String
        Dim tVentana As cVentana
        '
        ' El ID siempre será el handle de la ventana                    (25/Sep/02)
        elID = CStr(elHandle)
        If Me.Exists(elID) Then
            Return Me.Item(elID)
        Else
            tVentana = New cVentana
            With tVentana
                ' El ID es de sólo lectura, ya que el ID es el handle
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