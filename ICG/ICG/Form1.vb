Imports System.Threading
Imports System.IO

Public Class Form1

    Dim FacturaStart As New ThreadStart(AddressOf BackgroundFactura)
    Dim CallFactura As New MethodInvoker(AddressOf Me.FacturaToma)
    Dim MyThread As Thread
    Dim MyThread2 As Thread
    Dim envioAutomatico As New ThreadStart(AddressOf BackgroundEnvio)
    Dim CallAutomatico As New MethodInvoker(AddressOf Me.Operar)
    Dim bandera As Integer = 0
    'Dim CallFactura As New MethodInvoker(AddressOf Me.FacturaToma)
    Private Sub ConfiguracionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfiguracionToolStripMenuItem.Click
        Dim config As New frmConfig
        config.ShowDialog()
    End Sub

    Private Sub DiariasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DiariasToolStripMenuItem.Click
        Try
            Dim conexion As New coneccion
            Dim config = conexion.cargarConfiguaracion
            If config.Count > 0 Then
                conexion.MakeConnection(config)
            Else
                MessageBox.Show("Aun no a configurado la aplicacion")
                Return

            End If
        Catch ex As Exception

        End Try
      
        Try
            MyThread = New Thread(FacturaStart)
            MyThread.IsBackground = True
            MyThread.Name = "MyThreadFactura"
            MyThread.Start()
        Catch ex As Exception
        End Try
    End Sub


    Public Sub BackgroundEnvio()
        While True
            Me.BeginInvoke(CallAutomatico)
            Thread.Sleep(1500)
        End While
    End Sub

    Public Sub BackgroundFactura()
        While True
            Me.BeginInvoke(CallFactura)
            Thread.Sleep(1500)
        End While
    End Sub


    Public Sub FacturaToma()
        Dim objFSO As Object = CreateObject("Scripting.FileSystemObject")
        Dim objSubFolder As Object = Application.StartupPath & "\xml\"
        Dim objFolder As Object = objFSO.GetFolder(objSubFolder)
        Dim colFiles As Object = objFolder.Files

        For Each objFile In colFiles

            If existeFactura() = 0 Then
                Dim entra As String = Application.StartupPath & "\xml\" & objFile.Name.ToString
                Dim sale As String = "C:\temp\in\invoice.xml"
                File.Move(entra, sale)
            ElseIf existeFactura() = 1 Then
                'Timer1.Start()
                Exit Sub
            End If
        Next
    End Sub

    Public Sub Operar()
        Dim hora As String
        Dim actual = Date.Now.ToShortTimeString
        Dim conec As New coneccion
        Dim Lista = conec.cargarConfiguaracion()
        If Lista.Count > 0 Then
            Try
                hora = Lista(4).ToString
                If hora = actual Then

                    If bandera = 0 Then
                        bandera = bandera + 1

                        'MessageBox.Show("es la hora")
                        DiariasToolStripMenuItem_Click(Nothing, Nothing)
                    End If
                Else

                    bandera = 0

                End If
            Catch ex As Exception

            End Try
        Else
            MessageBox.Show("Debe de ingresar una configuración para continuar..")
        End If
    End Sub

    Private Function existeFactura()
        If My.Computer.FileSystem.FileExists("C:\temp\in\invoice.xml") Then
            'ListBox1.Items.Add("Si Existe")
            Return 1
        Else
            'ListBox1.Items.Add("No Existe")
            Return 0
        End If
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

    End Sub

    Private Sub AutomaticoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutomaticoToolStripMenuItem.Click
        MyThread2 = New Thread(envioAutomatico)
        MyThread2.IsBackground = True
        MyThread2.Name = "MyThreadFactura2"
        MyThread2.Start()
    End Sub
End Class
