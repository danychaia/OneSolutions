Imports System.Xml
Imports System.Data.SqlClient

Public Class frmConfig

    Private Sub frmConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim conec As New coneccion
        Dim Lista = conec.cargarConfiguaracion()
        If Lista.Count > 0 Then
            Try
                txtServer.Text = Lista(0).ToString
                txtbase.Text = Lista(1).ToString
                txtuser.Text = Lista(2).ToString
                txtContra.Text = Lista(3).ToString
                txtHora.Text = Lista(4).ToString
            Catch ex As Exception

            End Try
        End If
    End Sub

   


    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        crearArchivoConfig()
    End Sub


    Private Sub crearArchivoConfig()
        Dim ecrip As New EncryptComp.Library.Encrypt()

        Dim Doc As New XmlDocument, Nodo As XmlNode
        Dim Lista As ArrayList = New ArrayList()
        Try

            If txtbase.Text = "" Or txtServer.Text = "" Or txtContra.Text = "" Or txtuser.Text = "" Then
                MessageBox.Show("Debe de Llenar todos los campos")
                Return
            End If
            Lista.Add(txtServer.Text)
            Lista.Add(txtbase.Text)
            Lista.Add(txtuser.Text)
            Lista.Add(txtContra.Text)
            Lista.Add(txtHora.Text)
            Nodo = Doc.CreateElement("CONFIGURACION")
            Doc.AppendChild(Nodo)


            For Each Elemento As String In Lista
                Nodo = Doc.CreateElement("PARAMETRO")
                Nodo.InnerText = Elemento
                Doc.DocumentElement.AppendChild(Nodo)
            Next
            Doc.Save(Application.StartupPath & "\CONFIGURACION.xml")
            Dim mytext = System.IO.File.ReadAllText(Application.StartupPath & "\CONFIGURACION.xml")
            mytext = ecrip.EncryptKey(mytext)
            System.IO.File.WriteAllText(Application.StartupPath & "\CONFIGURACION.xml", mytext)
            MessageBox.Show("Guardado Exitosamente!")
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub
End Class